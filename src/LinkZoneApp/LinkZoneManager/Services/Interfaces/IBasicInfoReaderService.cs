using System;
using LinkZoneSdk.Models.System;

namespace LinkZoneManager.Services.Interfaces;

public interface IBasicInfoReaderService
{
    IObservable<bool> MobilNetworkStatus { get; }
    IObservable<string> MobilNetworkName { get; }
    IObservable<string> MobilNetworkType { get; }
    IObservable<int> SignalLevel { get; }
    IObservable<ChargeState> BatteryStatus { get; }
    IObservable<int> BatteryLevel { get; }
    IObservable<int> ConnectedUsers { get; }

    void StopListening();
    void StartListening();
}