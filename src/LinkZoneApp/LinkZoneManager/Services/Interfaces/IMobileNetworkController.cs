using System;
using System.Threading;
using System.Threading.Tasks;
using LinkZoneManager.Infrastructure;
using LinkZoneSdk.Enums;

namespace LinkZoneManager.Services.Interfaces;

public interface IMobileNetworkController : IDeviceSetting
{
    Task SwitchState(bool connect, CancellationToken cancellation);
    Task SwitchNetworkMode(NetworkMode networkMode, CancellationToken cancellation);

    IObservable<bool> MobilNetworkStatus { get; }
    IObservable<string> MobilNetworkName { get; }
    IObservable<string> MobilNetworkType { get; }
    IObservable<int> SignalLevel { get; }
    IObservable<NetworkMode> NetworkMode { get; }
}