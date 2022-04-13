using System.Reactive.Concurrency;
using Avalonia.Threading;

namespace LinkZoneManager.Infrastructure;

public class SchedulerProvider : ISchedulerProvider
{
    public IScheduler NewThread => NewThreadScheduler.Default;
    public IScheduler TaskPool => TaskPoolScheduler.Default.DisableOptimizations(typeof(ISchedulerLongRunning));
    public IScheduler TaskPoolLongRunning => TaskPoolScheduler.Default;
    public IScheduler CurrentThread => Scheduler.CurrentThread;
    public IScheduler Dispatcher => AvaloniaScheduler.Instance;
    public IScheduler Immediate => Scheduler.Immediate;
}