using System;
using System.Reactive;
using System.Reactive.Linq;

namespace LinkZoneManager.Infrastructure;

internal abstract class DeviceSettingBase : IDeviceSetting
{
    private Action<bool> _autoUpdaterObserver = _ => { };
    private Action<Unit> _manualUpdateObserver = _ => { };

    protected readonly IObservable<bool> IsListeningObservable;
    protected readonly IObservable<Unit> ManualUpdateObservable;

    protected DeviceSettingBase()
    {
        IsListeningObservable = Observable.FromEvent<bool>(
                eh => _autoUpdaterObserver += eh,
                eh => _autoUpdaterObserver -= eh)
            .StartWith(true);
        
        ManualUpdateObservable = Observable.FromEvent<Unit>(
            eh => _manualUpdateObserver += eh,
            eh => _manualUpdateObserver -= eh);
    }

    public void AutoUpdate(bool enabled)
    {
        _autoUpdaterObserver(enabled);
    }

    public void Update()
    {
        _manualUpdateObserver(Unit.Default);
    }
}