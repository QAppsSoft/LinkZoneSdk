using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using LinkZoneSdk.Enums;
using LinkZoneSdk.Models.Network;

namespace LinkZoneSdk
{
    public interface INetwork : IFluentInterface
    {
        Task<Result<NetworkSettings>> GetSettings(CancellationToken? cancellation = null);
        Task<Result<bool>> SetSettings(NetworkMode networkMode, NetworkSelection networkSelectionMode, CancellationToken? cancellation = null);
    }
}