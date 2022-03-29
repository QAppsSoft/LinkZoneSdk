using System.Threading;
using System.Threading.Tasks;

namespace LinkZoneManager.Services.Interfaces;

public interface IMobileNetworkController
{
    Task SwitchState(bool connect, CancellationToken cancellation);
}