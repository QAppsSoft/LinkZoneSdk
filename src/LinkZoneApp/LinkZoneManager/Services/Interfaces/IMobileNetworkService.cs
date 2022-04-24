using LinkZoneManager.Infrastructure;

namespace LinkZoneManager.Services.Interfaces;

public interface IMobileNetworkService : IMobileNetworkManager, IMobileNetworkProvider, IDeviceSetting, IService
{
}