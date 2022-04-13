using System;
using System.Threading;
using System.Threading.Tasks;
using LinkZoneSdk.Enums;

namespace LinkZoneManager.Services.Interfaces;

public interface IMobileNetworkManager
{
    Task SwitchStateAsync(bool connect, CancellationToken cancellation);
    Task SwitchStateAsync(bool connect, TimeSpan timeout, CancellationToken cancellation);
    
    Task SwitchNetworkModeAsync(NetworkMode networkMode, bool isConnected, CancellationToken cancellation);
    Task SwitchNetworkModeAsync(NetworkMode networkMode, bool isConnected, TimeSpan timeout, CancellationToken cancellation);
}