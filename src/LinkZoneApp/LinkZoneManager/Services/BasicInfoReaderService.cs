using LinkZoneSdk;
using LinkZoneSdk.Models.System;
using System;
using System.Reactive.Linq;
using LinkZoneManager.Services.Interfaces;

namespace LinkZoneManager.Services;

internal class BasicInfoReaderService : IBasicInfoReaderService
{
    private Action<bool> _listeningObserver = _ => { };

    public BasicInfoReaderService(ISdk sdk)
    {
        var listening = Observable.FromEvent<bool>(
                eh => _listeningObserver += eh,
                eh => _listeningObserver -= eh)
            .StartWith(true);

        var timer = Observable.Timer(TimeSpan.MinValue, TimeSpan.FromSeconds(5));

        var status = listening.Select(isListening => isListening ? timer : Observable.Empty<long>())
            .Switch()
            .Select(_ => Observable.FromAsync(cancellation => sdk.System().GetStatus(cancellation)))
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

        BatteryStatus = status.Select(value => value.ChargeState).DistinctUntilChanged();

        BatteryCapacity = status.Select(value => value.BatCap).DistinctUntilChanged();

        ConnectedUsers = status.Select(value => value.TotalConnNum).DistinctUntilChanged();
    }

    public IObservable<bool> MobilNetworkStatus { get; }
    public IObservable<string> MobilNetworkName { get; }
    public IObservable<string> MobilNetworkType { get; }
    public IObservable<int> SignalLevel { get; }

    public IObservable<ChargeState> BatteryStatus { get; }
    public IObservable<int> BatteryCapacity { get; }

    public IObservable<int> ConnectedUsers { get; }
    public void StopListening()
    {
        _listeningObserver(false);
    }

    public void StartListening()
    {
        _listeningObserver(true);
    }
}