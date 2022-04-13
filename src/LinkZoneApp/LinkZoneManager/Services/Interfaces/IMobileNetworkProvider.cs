using System;
using LinkZoneSdk.Enums;

namespace LinkZoneManager.Services.Interfaces;

public interface IMobileNetworkProvider
{
    IObservable<bool> MobilNetworkStatus { get; }
    IObservable<string> MobilNetworkName { get; }
    IObservable<string> MobilNetworkType { get; }
    IObservable<int> SignalLevel { get; }
    IObservable<NetworkMode> NetworkMode { get; }
}