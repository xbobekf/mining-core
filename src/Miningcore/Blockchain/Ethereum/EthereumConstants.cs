using System.Numerics;
using System.Text.RegularExpressions;

namespace Miningcore.Blockchain.Ethereum;

public class EthereumConstants
{
    public const ulong EpochLength = 30000;
    public static BigInteger BigMaxValue = BigInteger.Pow(2, 256);
    public static double Pow2x32 = Math.Pow(2, 32);
    public static BigInteger BigPow2x32 = new(Pow2x32);
    public const int AddressLength = 20;
    public const decimal Wei = 1000000000000000000;
    public static BigInteger WeiBig = new(1000000000000000000);
    public const string EthereumStratumVersion = "EthereumStratum/1.0.0";
    public const decimal StaticTransactionFeeReserve = 0.0025m; // in ETH
    public const string BlockTypeUncle = "uncle";
    public const string BlockTypeBlock = "block";

#if !DEBUG
    public const int MinPayoutPeerCount = 1;
#else
    public const int MinPayoutPeerCount = 1;
#endif

    public static readonly Regex ValidAddressPattern = new("^0x[0-9a-fA-F]{40}$", RegexOptions.Compiled);
    public static readonly Regex ZeroHashPattern = new("^0?x?0+$", RegexOptions.Compiled);

    public const ulong ByzantiumHardForkHeight = 4370000;
    public const ulong ConstantinopleHardForkHeight = 7280000;
    public const decimal HomesteadBlockReward = 5.0m;
    public const decimal ByzantiumBlockReward = 3.0m;
    public const decimal ConstantinopleReward = 2.0m;

    public const int MinConfimations = 16;

    public const string RpcRequestWorkerPropertyName = "worker";
}
//Rethereum
public class RethereumConstants
{
    public const ulong EpochLength = 32000;
    public const ulong LondonHeight = 13524557;
    public const decimal LondonBlockReward = 3.0m;
    public const ulong ArrowGlacierHeight = 27200177;
    public const decimal ArrowGlacierBlockReward = 2.0m;
    public const ulong GrayGlacierHeight = 40725107;
    public const decimal GrayGlacierBlockReward = 1.0m;
    public const decimal BaseRewardInitial = 4.0m;
}

// OCTA block reward distribution
// https://docs.octa.space/cryptocurrency/monetary-policy
public class OctaSpaceConstants
{
    public const ulong TriangulumHardForkHeight = 10000000;
    public const decimal TriangulumBlockReward = 1.0m;
    public const ulong VegaHardForkHeight = 8000000;
    public const decimal VegaBlockReward = 1.1m;
    public const ulong BlackeyeHardForkHeight = 6000000;
    public const decimal BlackeyeBlockReward = 1.2m;
    public const ulong DneprHardForkHeight = 4000000;
    public const decimal DneprBlockReward = 1.85m;
    public const ulong MahasimHardForkHeight = 3000000;
    public const decimal MahasimBlockReward = 2.3m;
    public const ulong PolarisHardForkHeight = 2500000;
    public const decimal PolarisBlockReward = 2.8m;
    public const ulong SpringwaterHardForkHeight = 2000000;
    public const decimal SpringwaterBlockReward = 3.0m;
    public const ulong ZagamiHardForkHeight = 1500000;
    public const decimal ZagamiBlockReward = 3.5m;
    public const ulong OldenburgHardForkHeight = 1000000;
    public const decimal OldenburgBlockReward = 4.0m;
    public const ulong ArcturusHardForkHeight = 650000;
    public const decimal ArcturusBlockReward = 5.0m;
    public const decimal BaseRewardInitial = 6.5m;
}

// ETC block reward distribution - ECIP 1017
// https://ecips.ethereumclassic.org/ECIPs/ecip-1017
public class EthereumClassicConstants
{
    public const ulong HardForkBlockMainnet = 11700000;
    public const ulong HardForkBlockMordor = 2520000;
    public const ulong EpochLength = 60000;
    public const ulong EraLength = 5000001;
    public const double DisinflationRateQuotient = 4.0;
    public const double DisinflationRateDivisor = 5.0;
    public const decimal BaseRewardInitial = 5.0m;
}

// Callisto Monetary Policy
// https://github.com/EthereumCommonwealth/Roadmap/issues/56
public class CallistoConstants
{
    public const decimal BaseRewardInitial = 77.76m;
    public const decimal TreasuryPercent = 50m;
}

public class EthOneConstants
{
    public const decimal BaseRewardInitial = 2.0m;
}

public class PinkConstants
{
    public const decimal BaseRewardInitial = 1.0m;
}

// Hypra
// https://github.com/Rethereum-blockchain/open-rethereum-pool/blob/master/payouts/unlocker.go
public class HypraConstants
{
    public const ulong EpochLength = 32000;
    public const ulong LondonHeight = 15787969;
    public const decimal LondonBlockReward = 3.0m;
    public const ulong ArrowGlacierHeight = 27200177;
    public const decimal ArrowGlacierBlockReward = 2.0m;
    public const ulong GrayGlacierHeight = 40725107;
    public const decimal GrayGlacierBlockReward = 1.0m;
    public const decimal BaseRewardInitial = 4.0m;
}

// UBIQ block reward distribution - 
// https://github.com/ubiq/UIPs/issues/16 - https://ubiqsmart.com/en/monetary-policy
public class UbiqConstants
{
    public const ulong YearOneHeight = 358363;
    public const decimal YearOneBlockReward = 7.0m;
    public const ulong YearTwoHeight = 716727;
    public const decimal YearTwoBlockReward = 6.0m;
    public const ulong YearThreeHeight = 1075090;
    public const decimal YearThreeBlockReward = 5.0m;
    public const ulong YearFourHeight = 1433454;
    public const decimal YearFourBlockReward = 4.0m;
    public const ulong OrionHardForkHeight = 1791793;
    public const decimal OrionBlockReward = 1.5m;
    public const decimal BaseRewardInitial = 8.0m;
}

public class JibChainConstants
{
    public const decimal BaseRewardInitial = 2.0m;
}

public class AltcoinConstants
{
    public const decimal BaseRewardInitial = 2.0m;
}

public class MaxxChainConstants
{
    public const ulong MaxxHardForkHeight = 103307;
    public const decimal MaxxBlockReward = 5.0m;
    public const decimal BaseRewardInitial = 2.0m;
}

public class PomConstants
{
    public const decimal BaseRewardInitial = 7.0m;
}

public class CanxiumConstants
{
    public const decimal BaseRewardInitial = 0.1875m;
}

public class BitnetConstants
{
    public const decimal BaseRewardInitial = 1.0m;
}

public class RedeV2Constants
{
    public const decimal BaseRewardInitial = 0.5m;
}

public class EtherChainConstants
{
    public const decimal BaseRewardInitial = 6.0m;
}

public class EtherGemConstants
{
    public const decimal BaseRewardInitial = 3.0m;
}

public class PowBlocksConstants
{
    public const decimal BaseRewardInitial = 9.0m;
}

public class ZetherConstants
{
    public const decimal BaseRewardInitial = 10.0m;
    public const decimal HalvingReward1 = 9.0m;
    public const decimal HalvingReward2 = 8.0m;
    public const decimal HalvingReward3 = 7.0m;
    public const decimal HalvingReward4 = 6.0m;
    public const decimal HalvingReward5 = 5.0m;
    public const decimal StakePercent = 5m;
    
    public const ulong HalvingHeight1 = 100000;
    public const ulong HalvingHeight2 = 200000;
    public const ulong HalvingHeight3 = 300000;
    public const ulong HalvingHeight4 = 400000;
    public const ulong HalvingHeight5 = 500000;
}

public class SlayerXConstants
{
    public const decimal BaseRewardInitial = 60.0m;
    public const decimal HalvingReward1 = 30.0m;
    public const decimal HalvingReward2 = 15.0m;
    public const decimal HalvingReward3 = 7.5m;
    public const decimal HalvingReward4 = 3.75m;
    public const decimal HalvingReward5 = 1.875m;
    public const decimal StakePercent = 5m;

    public const ulong HalvingHeight1 = 840000;
    public const ulong HalvingHeight2 = 1680000;
    public const ulong HalvingHeight3 = 2520000;
    public const ulong HalvingHeight4 = 3360000;
    public const ulong HalvingHeight5 = 4200000;
}

public enum EthereumNetworkType
{
    Main = 1,
    Ropsten = 3,
    Ubiq = 8,
    Classic = 1,
    Mordor = 7,
    Callisto = 820,
    MainPow = 10001,
    EtherOne = 4949,
    Pink = 10100,
    JibChain = 8899,
    Altcoin = 2330,
    MaxxChain = 10201,
    Pom = 801921,
    Canxium = 3003,
    Rethereum = 622277,
    Bitnet = 210,
    OctaSpace = 800001,
    RedeV2 = 1972,
    EtherChain = 11777,
    EtherGem = 1987,
    PowBlocks = 12300,
    SlayerX = 1,
    Hypra = 622277,
    Zether = 715131,

    Unknown = -1,
}

public enum GethChainType
{
    Main,
    Ropsten,
    Ubiq,
    Classic,
    Mordor,
    Callisto,
    MainPow = 10001,
    EtherOne = 4949,
    Pink = 10100,
    JibChain = 8899,
    Altcoin = 2330,
    MaxxChain = 10201,
    Pom = 801921,
    Canxium = 3003,
    Rethereum = 622277,
    Bitnet,
    OctaSpace,
    RedeV2 = 1972,
    EtherChain = 11777,
    EtherGem = 1987,
    PowBlocks = 12300,
    SlayerX = 9119,
    Hypra,
    Zether = 715131,
    
    Unknown = -1,
}

public static class EthCommands
{
    public const string GetWork = "eth_getWork";
    public const string SubmitWork = "eth_submitWork";
    public const string Sign = "eth_sign";
    public const string GetNetVersion = "net_version";
    public const string GetClientVersion = "web3_clientVersion";
    public const string GetCoinbase = "eth_coinbase";
    public const string GetAccounts = "eth_accounts";
    public const string GetPeerCount = "net_peerCount";
    public const string GetSyncState = "eth_syncing";
    public const string GetBlockNumber = "eth_blockNumber";
    public const string GetBlockByNumber = "eth_getBlockByNumber";
    public const string GetBlockByHash = "eth_getBlockByHash";
    public const string GetUncleByBlockNumberAndIndex = "eth_getUncleByBlockNumberAndIndex";
    public const string GetTxReceipt = "eth_getTransactionReceipt";
    public const string SendTx = "eth_sendTransaction";
    public const string UnlockAccount = "personal_unlockAccount";
    public const string Subscribe = "eth_subscribe";
    public const string MaxPriorityFeePerGas = "eth_maxPriorityFeePerGas";
}
