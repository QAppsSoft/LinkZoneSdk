using System.Threading;
using FluentResults;
using LinkZoneSdk.Models.User;
using System.Threading.Tasks;

namespace LinkZoneSdk
{
    public interface IUser : IFluentInterface
    {
        Task<Result<LoginToken>> Login(string password, CancellationToken? cancellation = null);
        Task<Result<LoginToken>> Login(string userName, string password, CancellationToken? cancellation = null);
        Task<Result<bool>> Logout(CancellationToken? cancellation = null);
        Task<Result<LoginStateInfo>> GetLoginStatus(CancellationToken? cancellation = null);
        Task<Result<bool>> ChangePassword(string userName, string currentPassword, string newPassword, CancellationToken? cancellation = null);
        Task<Result> HeartBeat(CancellationToken? cancellation = null);
    }
}