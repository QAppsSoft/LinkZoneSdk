namespace LinkZoneManager.Infrastructure
{
    public interface IDeviceSetting
    {
        public void AutoUpdate(bool enabled);
        public void Update();
    }
}
