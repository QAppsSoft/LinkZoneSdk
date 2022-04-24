using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using LinkZoneSdk.Models.Ussd;

namespace LinkZoneSdk
{
    public interface IUssd : IFluentInterface
    {
        Task<Result> Send(string ussdCode, int ussdType, CancellationToken? cancellation = null);
        Task<Result<UssdResultData>> GetSendResult(CancellationToken? cancellation = null);
        Task<Result<bool>> SetEnd(CancellationToken? cancellation = null);
    }
}