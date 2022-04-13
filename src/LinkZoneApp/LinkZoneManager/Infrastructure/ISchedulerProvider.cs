using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Text;

namespace LinkZoneManager.Infrastructure
{
    public interface ISchedulerProvider
    {
        IScheduler CurrentThread { get; }
        IScheduler Dispatcher { get; }
        IScheduler Immediate { get; }
        IScheduler NewThread { get; }
        IScheduler TaskPoolLongRunning { get; }
        IScheduler TaskPool { get; }
    }
}
