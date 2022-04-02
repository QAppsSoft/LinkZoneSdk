using System;
using LinkZoneManager.Infrastructure;
using LinkZoneSdk.Models.System;

namespace LinkZoneManager.Services.Interfaces;

public interface IBasicInfoReaderService : IDeviceSetting
{
    IObservable<ChargeState> BatteryStatus { get; }
    IObservable<int> BatteryCapacity { get; }
    IObservable<int> ConnectedUsers { get; }
}