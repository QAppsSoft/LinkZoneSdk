using System;
using System.Reactive;

namespace LinkZoneManager.Infrastructure;

public abstract class DeviceSettingBase : IDeviceSetting
{
    protected Action<bool> AutoUpdaterObserver = _ => { };
    protected Action<Unit> ManualUpdateObserver = _ => { };

    public void AutoUpdate(bool enabled)
    {
        AutoUpdaterObserver(enabled);
    }

    public void Update()
    {
        ManualUpdateObserver(Unit.Default);
    }
}