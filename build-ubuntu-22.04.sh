#!/bin/bash

# install install-dependencies
sudo apt-get update; \
  sudo apt-get -y install wget

# install dev-dependencies
sudo apt-get update; \
sudo apt-get -y install dotnet6 git cmake ninja-build build-essential libssl-dev pkg-config libboost-all-dev libsodium-dev libzmq5-dev libgmp-dev

(cd src/Miningcore && \
BUILDIR=${1:-../../build} && \
echo "Building into $BUILDIR" && \
dotnet publish -c Release --framework net6.0 -o $BUILDIR)
