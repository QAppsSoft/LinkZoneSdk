using FluentResults;
using LinkZoneSdk.Models.User;
using System.Threading.Tasks;

namespace LinkZoneSdk
{
    public interface IUser : IFluentInterface
    {
        Task<Result<LoginToken>> Login(string password);
        Task<Result<LoginToken>> Login(string userName, string password);
        Task<Result<bool>> Logout();
        Task<Result<LoginStateInfo>> GetLoginStatus();
        Task<Result<bool>> ChangePassword(string userName, string currentPassword, string newPassword);
        Task<Result> HeartBeat();
    }
}