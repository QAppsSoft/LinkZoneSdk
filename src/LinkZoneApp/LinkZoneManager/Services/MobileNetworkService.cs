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

public class MobileNetworkService : DeviceSettingBase, IMobileNetworkService
{
    private readonly ISdk _sdk;

    public MobileNetworkService(ISdk sdk)
    {
        _sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        
        var listening = Observable.FromEvent<bool>(
                eh => AutoUpdaterObserver += eh,
                eh => AutoUpdaterObserver -= eh)
            .StartWith(true);

        var timer = Observable.Timer(TimeSpan.MinValue, TimeSpan.FromSeconds(5)).ToUnit();

        var manualUpdate = Observable.FromEvent<Unit>(
            eh => ManualUpdateObserver += eh,
            eh => ManualUpdateObserver -= eh);

        var updater = listening.Select(isListening => isListening ? timer : Observable.Empty<Unit>())
            .Switch()
            .Merge(manualUpdate)
            .Publish()
            .RefCount();

        var status = updater.Select(_ => Observable.FromAsync(cancellation => sdk.System().GetStatus(cancellation)))
            .Switch()
            .Select(value => value.ValueOrDefault)
            .Where(value => value != default)
            .Publish()
            .RefCount();

        MobilNetworkStatus = status.Select(value => value.ConnectionStatus switch
        {
            0 => false,
            2 => true,
            _ => false
        }).DistinctUntilChanged();

        MobilNetworkName = status.Select(value => value.NetworkName).DistinctUntilChanged();

        MobilNetworkType = status.Select(value => value.NetworkType).DistinctUntilChanged();

        SignalLevel = status.Select(value => value.SignalStrength).DistinctUntilChanged();

        var networkMode = updater.Select(_ => Observable.FromAsync(cancellation => sdk.Network().GetSettings(cancellation)))
            .Switch()
            .Select(value => value.ValueOrDefault)
            .Where(value => value != default)
            .Publish()
            .RefCount();

        NetworkMode = networkMode.Select(value => value.NetworkMode).DistinctUntilChanged();
    }

    public async Task SwitchNetworkMode(NetworkMode networkMode, CancellationToken cancellation)
    {
        AutoUpdaterObserver(false);

        var status = await _sdk.System().GetStatus(cancellation);

        var connected = status.Value.ConnectionStatus switch
        {
            0 => false,
            2 => true,
            _ => false
        };

        if (connected)
        {
            await _sdk.Network().SetSettings(networkMode, NetworkSelection.Auto, cancellation).ConfigureAwait(false);
            await Task.Delay(TimeSpan.FromSeconds(5), cancellation); // Needed to allow the setting to be fully applied
            await _sdk.Connection().Connect(cancellation).ConfigureAwait(false);
        }
        else
        {
            await _sdk.Network().SetSettings(networkMode, NetworkSelection.Auto, cancellation).ConfigureAwait(false);
        }
        
        AutoUpdaterObserver(true);
    }

    public async Task SwitchState(bool connect, CancellationToken cancellation)
    {
        AutoUpdaterObserver(false);

        if (connect)
        {
            await _sdk.Connection().Connect(cancellation).ConfigureAwait(false);
        }
        else
        {
            await _sdk.Connection().Disconnect(cancellation).ConfigureAwait(false);
        }

        AutoUpdaterObserver(true);
    }

    public IObservable<bool> MobilNetworkStatus { get; }
    public IObservable<string> MobilNetworkName { get; }
    public IObservable<string> MobilNetworkType { get; }
    public IObservable<int> SignalLevel { get; }
    public IObservable<NetworkMode> NetworkMode { get; }
}