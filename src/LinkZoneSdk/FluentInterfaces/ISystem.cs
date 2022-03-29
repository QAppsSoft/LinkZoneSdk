using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using LinkZoneSdk.Models.System;

namespace LinkZoneSdk
{
    public interface ISystem : IFluentInterface
    {
        public Task<Result<Dictionary<string, object>>> GetSystemInfo(CancellationToken? cancellation = null);
        public Task<Result<SystemStatus>> GetSystemStatus(CancellationToken? cancellation = null);
        public Task<Result<bool>> RebootDevice(CancellationToken? cancellation = null);
    }
}