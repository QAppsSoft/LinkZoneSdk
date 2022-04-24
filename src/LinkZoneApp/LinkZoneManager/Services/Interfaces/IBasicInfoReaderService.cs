using System;
using LinkZoneManager.Infrastructure;
using LinkZoneSdk.Enums;

namespace LinkZoneManager.Services.Interfaces;

public interface IBasicInfoReaderService : IDeviceSetting, IService
{
    IObservable<ChargeState> BatteryStatus { get; }
    IObservable<int> BatteryCapacity { get; }
    IObservable<int> ConnectedUsers { get; }
}