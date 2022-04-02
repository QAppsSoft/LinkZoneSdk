using System;
using System.Reactive;
using System.Reactive.Linq;

namespace LinkZoneManager.Infrastructure.Extensions;

public static class ReactiveEx
{
    public static IObservable<Unit> ToUnit<T>(this IObservable<T> observable)
    {
        return observable.Select(_ => Unit.Default);
    }
}