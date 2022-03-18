using System.Collections.Generic;
using System.Threading.Tasks;
using FluentResults;
using LinkZoneSdk.Models.System;

namespace LinkZoneSdk
{
    public interface ISystem : IFluentInterface
    {
        public Task<Result<Dictionary<string, object>>> GetSystemInfo();
        public Task<Result<SystemStatus>> GetSystemStatus();
        public Task<Result<bool>> RebootDevice();
    }
}