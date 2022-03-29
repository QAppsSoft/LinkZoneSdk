using System.Threading;
using FluentResults;
using LinkZoneSdk.Models.Connection;
using System.Threading.Tasks;

namespace LinkZoneSdk
{
    public interface IConnection : IFluentInterface
    {
        Task<Result<ConnectionState>> GetConnectionState(CancellationToken? cancellation = null);

        Task<Result<bool>> Connect(CancellationToken? cancellation = null);

        Task<Result<bool>> Disconnect(CancellationToken? cancellation = null);
    }
}
