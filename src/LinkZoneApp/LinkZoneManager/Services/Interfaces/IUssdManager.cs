using System.Threading;
using System.Threading.Tasks;

namespace LinkZoneManager.Services.Interfaces;

public interface IUssdManager
{
    Task<string> ExecuteCodeAsync(string ussdCode, CancellationToken cancellation);
    Task CancelCodeExecutionAsync(CancellationToken cancellation);
}