using LinkZoneSdk;
using System;
using System.Reactive;
using System.Reactive.Linq;
using LinkZoneManager.Infrastructure;
using LinkZoneManager.Infrastructure.Extensions;
using LinkZoneManager.Services.Interfaces;
using LinkZoneSdk.Enums;

namespace LinkZoneManager.Services;

internal sealed class BasicInfoReaderService : DeviceSettingBase, IBasicInfoReaderService
{
    public BasicInfoReaderService(ISdk sdk, ISchedulerProvider schedulerProvider) : base(schedulerProvider)
    {
        var timer = Observable.Timer(TimeSpan.MinValue, TimeSpan.FromSeconds(5), schedulerProvider.TaskPool).ToUnit();

        var status = IsListeningObservable.Select(isListening => isListening ? timer : Observable.Empty<Unit>())
            .Switch()
            .Merge(ManualUpdateObservable)
            .Select(_ => Observable.FromAsync(cancellation => sdk.System().GetStatus(cancellation), schedulerProvider.TaskPool))
            .Switch()
            .Select(value => value.ValueOrDefault)
            .Where(value => value != default)
            .Publish()
            .RefCount();

        BatteryStatus = status.Select(value => value.ChargeState);

        BatteryCapacity = status.Select(value => value.BatCap);

        ConnectedUsers = status.Select(value => value.TotalConnNum);
    }
    
    public IObservable<ChargeState> BatteryStatus { get; }
    public IObservable<int> BatteryCapacity { get; }
    public IObservable<int> ConnectedUsers { get; }
}