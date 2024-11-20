using System.Globalization;
using System.Text;
using Miningcore.Blockchain.Bitcoin;
using Miningcore.Blockchain.Bitcoin.DaemonResponses;
using Miningcore.Crypto;
using Miningcore.Crypto.Hashing.Progpow;
using Miningcore.Extensions;
using Miningcore.Stratum;
using Miningcore.Time;
using Miningcore.Util;
using NBitcoin;
using NBitcoin.DataEncoders;
using Newtonsoft.Json.Linq;
using NLog;
using Transaction = NBitcoin.Transaction;

namespace Miningcore.Blockchain.Progpow.Custom.Kiiro;

public class KiiroJob : ProgpowJob
{
    public override (Share Share, string BlockHex) ProcessShareInternal(ILogger logger,
        StratumConnection worker, ulong nonce, string inputHeaderHash, string mixHash)
    {
        var context = worker.ContextAs<ProgpowWorkerContext>();
        var extraNonce1 = context.ExtraNonce1;

        // build coinbase
        var coinbase = SerializeCoinbase(extraNonce1);
        Span<byte> coinbaseHash = stackalloc byte[32];
        coinbaseHasher.Digest(coinbase, coinbaseHash);

        // hash block-header
        var headerBytes = SerializeHeader(coinbaseHash);
        Span<byte> headerHash = stackalloc byte[32];
        headerHasher.Digest(headerBytes, headerHash);
        headerHash.Reverse();

        var headerHashHex = headerHash.ToHexString();

        if(headerHashHex != inputHeaderHash)
            throw new StratumException(StratumError.MinusOne, $"bad header-hash");

        if(!progpowHasher.Compute(logger, (int) BlockTemplate.Height, headerHash.ToArray(), nonce, out var mixHashOut, out var resultBytes))
            throw new StratumException(StratumError.MinusOne, "bad hash");

        if(mixHash != mixHashOut.ToHexString())
            throw new StratumException(StratumError.MinusOne, $"bad mix-hash");

        resultBytes.ReverseInPlace();
        mixHashOut.ReverseInPlace();

        var resultValue = new uint256(resultBytes);
        var resultValueBig = resultBytes.AsSpan().ToBigInteger();
        // calc share-diff
        var shareDiff = (double) new BigRational(FiroConstants.Diff1, resultValueBig) * shareMultiplier;
        var stratumDifficulty = context.Difficulty;
        var ratio = shareDiff / stratumDifficulty;

        // check if the share meets the much harder block difficulty (block candidate)
        var isBlockCandidate = resultValue <= blockTargetValue;

        // test if share meets at least workers current difficulty
        if(!isBlockCandidate && ratio < 0.99)
        {
            // check if share matched the previous difficulty from before a vardiff retarget
            if(context.VarDiff?.LastUpdate != null && context.PreviousDifficulty.HasValue)
            {
                ratio = shareDiff / context.PreviousDifficulty.Value;

                if(ratio < 0.99)
                    throw new StratumException(StratumError.LowDifficultyShare, $"low difficulty share ({shareDiff})");

                // use previous difficulty
                stratumDifficulty = context.PreviousDifficulty.Value;
            }

            else
                throw new StratumException(StratumError.LowDifficultyShare, $"low difficulty share ({shareDiff})");
        }

        var result = new Share
        {
            BlockHeight = BlockTemplate.Height,
            NetworkDifficulty = Difficulty,
            Difficulty = stratumDifficulty / shareMultiplier,
        };

        if(!isBlockCandidate)
        {
            return (result, null);
        }

        result.IsBlockCandidate = true;
        
        var nonceBytes = (Span<byte>) nonce.ToString("X").HexToReverseByteArray();
        var mixHashBytes = (Span<byte>) mixHash.HexToReverseByteArray();
        // concat headerBytes, nonceBytes and mixHashBytes
        Span<byte> headerBytesNonceMixHasBytes = stackalloc byte[headerBytes.Length + nonceBytes.Length + mixHashBytes.Length];
        headerBytes.CopyTo(headerBytesNonceMixHasBytes);
        var offset = headerBytes.Length;
        nonceBytes.CopyTo(headerBytesNonceMixHasBytes[offset..]);
        offset += nonceBytes.Length;
        mixHashBytes.CopyTo(headerBytesNonceMixHasBytes[offset..]);
        
        Span<byte> blockHash = stackalloc byte[32];
        blockHasher.Digest(headerBytesNonceMixHasBytes, blockHash);
        result.BlockHash = blockHash.ToHexString();

        var blockBytes = SerializeBlock(headerBytes, coinbase, nonce, mixHashOut);
        var blockHex = blockBytes.ToHexString();

        return (result, blockHex);
    }
    
    #region Masternodes

    protected override Money CreateMasternodeOutputs(Transaction tx, Money reward)
    {
        if(masterNodeParameters.Masternode != null)
        {
            Masternode[] masternodes;

            // Dash v13 Multi-Master-Nodes
            if(masterNodeParameters.Masternode.Type == JTokenType.Array)
                masternodes = masterNodeParameters.Masternode.ToObject<Masternode[]>();
            else
                masternodes = new[] { masterNodeParameters.Masternode.ToObject<Masternode>() };

            if(masternodes != null)
            {
                foreach(var masterNode in masternodes)
                {
                    if(!string.IsNullOrEmpty(masterNode.Payee))
                    {
                        var payeeDestination = BitcoinUtils.AddressToDestination(masterNode.Payee, network);
                        var payeeReward = masterNode.Amount;

                        tx.Outputs.Add(payeeReward, payeeDestination);
                    /*  A block reward of 30 KIIRO/block is divided as follows:
                    
                            Miners (20%, 6 KIIRO)
                            Masternodes (60%, 18 KIIRO)
                            Development Fund (10%, 3 KIIRO)
                            Community Fund (10%, 3 KIIRO)
                    */
                        //reward -= payeeReward; // KIIRO does not deduct payeeReward from coinbasevalue (reward) since it's the amount which goes to miners
                    }
                }
            }
        }

        return reward;
    }

    #endregion // Masternodes
    
    #region Founder

    protected override Money CreateFounderOutputs(Transaction tx, Money reward)
    {
        if (coin.HasFounderFee)
        {
            Founder[] founders;
            
            if(network.Name.ToLower() == "testnet")
            {
                founders = new[] { new Founder{ Payee = "TCkC4uoErEyCB4MK3d6ouyJELoXnuyqe9L", Amount = 300000000 }, new Founder{ Payee = "TWDxLLKsFp6qcV1LL4U2uNmW4HwMcapmMU", Amount = 450000000 } };
            }
            else
            {
                founders = new[] { new Founder{ Payee = "KDW8CeScVpWFzekvZm4f37qs5GxByEGSKE", Amount = 300000000 }, new Founder{ Payee = "KWTco92wURX5Jwu3mMdWrs36j574meAvew", Amount = 300000000 } };
            }
            
            foreach(var Founder in founders)
            {
                if(!string.IsNullOrEmpty(Founder.Payee))
                {
                    var payeeAddress = BitcoinUtils.AddressToDestination(Founder.Payee, network);
                    var payeeReward = Founder.Amount;

                    tx.Outputs.Add(payeeReward, payeeAddress);
                    
                    /*  A block reward of 30 KIIRO/block is divided as follows:
                    
                            Miners (20%, 6 KIIRO)
                            Masternodes (60%, 18 KIIRO)
                            Development Fund (10%, 3 KIIRO)
                            Community Fund (10%, 3 KIIRO)
                    */
                    //reward -= payeeReward; // KIIRO does not deduct payeeReward from coinbasevalue (reward) since it's the amount which goes to miners
                }
            }
        }

        return reward;
    }

    #endregion // Founder
}