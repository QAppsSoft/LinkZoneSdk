using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using LinkZoneManager.Infrastructure;
using LinkZoneManager.Infrastructure.Extensions;
using LinkZoneManager.Services.Interfaces;
using LinkZoneSdk;
using LinkZoneSdk.Enums;

namespace LinkZoneManager.Services;

internal sealed class MobileNetworkService : DeviceSettingBase, IMobileNetworkService
{
    private readonly ISdk _sdk;

    public MobileNetworkService(ISdk sdk, ISchedulerProvider schedulerProvider) : base(schedulerProvider)
    {
        _sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        
        var timer = Observable.Timer(TimeSpan.MinValue, TimeSpan.FromSeconds(5), schedulerProvider.TaskPool).ToUnit();

        var updater = IsListeningObservable.Select(isListening => isListening ? timer : Observable.Empty<Unit>())
            .Switch()
            .Merge(ManualUpdateObservable)
            .Publish()
            .RefCount();

        var status = updater.Select(_ => Observable.FromAsync(cancellation => sdk.System().GetStatus(cancellation), schedulerProvider.TaskPool))
            .Switch()
            .Select(value => value.ValueOrDefault)
            .Where(value => value != default)
            .Publish()
            .RefCount();

        MobilNetworkStatus = status.Select(value => IsConnected(value.ConnectionStatus));

        MobilNetworkName = status.Select(value => value.NetworkName);

        MobilNetworkType = status.Select(value => value.NetworkType);

        SignalLevel = status.Select(value => value.SignalStrength);

        var networkMode = updater.Select(_ => Observable.FromAsync(cancellation => sdk.Network().GetSettings(cancellation), schedulerProvider.TaskPool))
            .Switch()
            .Select(value => value.ValueOrDefault)
            .Where(value => value != default)
            .Publish()
            .RefCount();

        NetworkMode = networkMode.Select(value => value.NetworkMode);
    }
    public Task SwitchNetworkMode(NetworkMode networkMode,  CancellationToken cancellation)
    {
        return SwitchNetworkMode(networkMode, TimeSpan.MaxValue, cancellation);
    }

    public async Task SwitchNetworkMode(NetworkMode networkMode, TimeSpan timeout, CancellationToken cancellation)
    {
        using var internalTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellation);

        internalTokenSource.CancelAfter(timeout);

        AutoUpdate(false);
        
        var status = await _sdk.System().GetStatus(internalTokenSource.Token).ConfigureAwait(false);

        var connected = IsConnected(status.Value.ConnectionStatus);

        if (connected)
        {
            await _sdk.Network().SetSettings(networkMode, NetworkSelection.Auto, internalTokenSource.Token).ConfigureAwait(false);
            await Task.Delay(TimeSpan.FromSeconds(5), internalTokenSource.Token).ConfigureAwait(false); // Needed to allow the setting to be fully applied
            await _sdk.Connection().Connect(internalTokenSource.Token).ConfigureAwait(false);
        }
        else
        {
            await _sdk.Network().SetSettings(networkMode, NetworkSelection.Auto, internalTokenSource.Token).ConfigureAwait(false);
        }

        AutoUpdate(true);
    }
    public Task SwitchState(bool connect, CancellationToken cancellation)
    {
        return SwitchState(connect, TimeSpan.MaxValue, cancellation);
    }

    public async Task SwitchState(bool connect, TimeSpan timeout, CancellationToken cancellation)
    {
        using var internalTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellation);
        
        internalTokenSource.CancelAfter(timeout);
        
        AutoUpdate(false);

        if (connect)
        {
            await _sdk.Connection().Connect(internalTokenSource.Token).ConfigureAwait(false);
        }
        else
        {
            await _sdk.Connection().Disconnect(internalTokenSource.Token).ConfigureAwait(false);
        }
        
        AutoUpdate(true);
    }

    public IObservable<bool> MobilNetworkStatus { get; }
    public IObservable<string> MobilNetworkName { get; }
    public IObservable<string> MobilNetworkType { get; }
    public IObservable<int> SignalLevel { get; }
    public IObservable<NetworkMode> NetworkMode { get; }

    private static bool IsConnected(int connectionStatus)
    {
        return connectionStatus switch
        {
            0 => false,
            2 => true,
            _ => false
        };
    }
}