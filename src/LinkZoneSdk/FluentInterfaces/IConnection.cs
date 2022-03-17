using FluentResults;
using LinkZoneSdk.Models.Connection;
using System.Threading.Tasks;

namespace LinkZoneSdk
{
    public interface IConnection : IFluentInterface
    {
        Task<Result<ConnectionState>> GetConnectionState();

        Task<Result<bool>> Connect();

        Task<Result<bool>> Disconnect();
    }
}
