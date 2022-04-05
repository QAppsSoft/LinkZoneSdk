namespace LinkZoneManager.Infrastructure
{
    public interface IDeviceSetting
    {
        void AutoUpdate(bool enabled);
        void Update();
    }
}