using LinkZoneSdk;
using LinkZoneSdk.Models.System;
using System;
using System.Reactive;
using System.Reactive.Linq;
using LinkZoneManager.Infrastructure;
using LinkZoneManager.Infrastructure.Extensions;
using LinkZoneManager.Services.Interfaces;

namespace LinkZoneManager.Services;

internal sealed class BasicInfoReaderService : DeviceSettingBase, IBasicInfoReaderService
{
    public BasicInfoReaderService(ISdk sdk)
    {
        var listening = Observable.FromEvent<bool>(
                eh => AutoUpdaterObserver += eh,
                eh => AutoUpdaterObserver -= eh)
            .StartWith(true);

        var timer = Observable.Timer(TimeSpan.MinValue, TimeSpan.FromSeconds(5)).ToUnit();

        var manualUpdate = Observable.FromEvent<Unit>(
            eh => ManualUpdateObserver += eh,
            eh => ManualUpdateObserver -= eh);

        var status = listening.Select(isListening => isListening ? timer : Observable.Empty<Unit>())
            .Switch()
            .Merge(manualUpdate)
            .Select(_ => Observable.FromAsync(cancellation => sdk.System().GetStatus(cancellation)))
            .Switch()
            .Select(value => value.ValueOrDefault)
            .Where(value => value != default)
            .Publish()
            .RefCount();

        BatteryStatus = status.Select(value => value.ChargeState).DistinctUntilChanged();

        BatteryCapacity = status.Select(value => value.BatCap).DistinctUntilChanged();

        ConnectedUsers = status.Select(value => value.TotalConnNum).DistinctUntilChanged();
    }
    
    public IObservable<ChargeState> BatteryStatus { get; }
    public IObservable<int> BatteryCapacity { get; }
    public IObservable<int> ConnectedUsers { get; }
}