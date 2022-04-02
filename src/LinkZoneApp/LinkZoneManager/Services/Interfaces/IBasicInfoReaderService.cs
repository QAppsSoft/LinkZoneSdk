using System;
using LinkZoneManager.Infrastructure;
using LinkZoneSdk.Models.System;

namespace LinkZoneManager.Services.Interfaces;

public interface IBasicInfoReaderService : IDeviceSetting
{
    IObservable<bool> MobilNetworkStatus { get; }
    IObservable<string> MobilNetworkName { get; }
    IObservable<string> MobilNetworkType { get; }
    IObservable<int> SignalLevel { get; }
    IObservable<ChargeState> BatteryStatus { get; }
    IObservable<int> BatteryCapacity { get; }
    IObservable<int> ConnectedUsers { get; }
}