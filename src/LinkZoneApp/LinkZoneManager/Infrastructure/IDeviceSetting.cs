using System;

namespace LinkZoneManager.Infrastructure
{
    public interface IDeviceSetting
    {
        IObservable<bool> CanChangeSettingsObservable { get; }
        void AutoUpdate(bool enabled);
        void Update();
    }
}