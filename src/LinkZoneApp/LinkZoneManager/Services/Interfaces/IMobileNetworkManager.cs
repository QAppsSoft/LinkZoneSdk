using System;
using System.Threading;
using System.Threading.Tasks;
using LinkZoneSdk.Enums;

namespace LinkZoneManager.Services.Interfaces;

public interface IMobileNetworkManager
{
    Task SwitchState(bool connect, CancellationToken cancellation);
    Task SwitchState(bool connect, TimeSpan timeout, CancellationToken cancellation);

    Task SwitchNetworkMode(NetworkMode networkMode, CancellationToken cancellation);
    Task SwitchNetworkMode(NetworkMode networkMode, TimeSpan timeout, CancellationToken cancellation);
}