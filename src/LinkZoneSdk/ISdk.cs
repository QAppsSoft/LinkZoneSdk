namespace LinkZoneSdk
{
    public interface ISdk : IFluentInterface
    {
        IUser User();
        IConnection Connection();
    }
}