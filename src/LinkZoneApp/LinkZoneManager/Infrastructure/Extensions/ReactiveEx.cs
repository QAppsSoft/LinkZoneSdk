using System;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace LinkZoneManager.Infrastructure.Extensions;

public static class ReactiveEx
{
    public static IObservable<Unit> ToUnit<T>(this IObservable<T> observable)
    {
        return observable.Select(_ => Unit.Default);
    }

    public static IObservable<bool> Invert(this IObservable<bool> observable)
    {
        return observable.Select(value => !value);
    }

    public static IObservable<bool> DelayOnTrue(this IObservable<bool> observable, TimeSpan delay, IScheduler? scheduler = default)
    {
        return observable.Select(value => value ? Observable.Return(value).Delay(delay, scheduler ?? Scheduler.Default) : Observable.Return(value))
            .Switch()
            .Publish()
            .RefCount();
    }
}