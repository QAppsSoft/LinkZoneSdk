﻿using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using LinkZoneManager.Services.Interfaces;
using LinkZoneSdk.Enums;
using LinkZoneSdk.Models.System;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace LinkZoneManager.ViewModels;

public class HomeViewModel : PageViewModelBase, IActivatableViewModel
{
    public HomeViewModel(IBasicInfoReaderService infoReader, IMobileNetworkController networkController)
    {
        Activator = new ViewModelActivator();
        this.WhenActivated(disposables =>
        {
            HandleActivation();

            Disposable.Create(HandleDeactivation)
                .DisposeWith(disposables);

            var setNetworkStatus = infoReader.MobilNetworkStatus
                .Subscribe(value => MobilNetworkStatus = value)
                .DisposeWith(disposables);

            var setNetworkName = infoReader.MobilNetworkName
                .ToPropertyEx(this, vm => vm.MobilNetworkName)
                .DisposeWith(disposables);

            var setNetworkType = infoReader.MobilNetworkType
                .ToPropertyEx(this, vm => vm.MobilNetworkType)
                .DisposeWith(disposables);

            var setSignalLevel = infoReader.SignalLevel
                .ToPropertyEx(this, vm => vm.SignalStrength)
                .DisposeWith(disposables);

            var setBatteryStatus = infoReader.BatteryStatus
                .ToPropertyEx(this, vm => vm.BatteryStatus)
                .DisposeWith(disposables);

            var setBatteryCapacity = infoReader.BatteryCapacity
                .ToPropertyEx(this, vm => vm.BatteryCapacity)
                .DisposeWith(disposables);

            var setConnectedUsers = infoReader.ConnectedUsers
                .ToPropertyEx(this, vm => vm.ConnectedUsers)
                .DisposeWith(disposables);

            var waitToStart = Observable.Timer(TimeSpan.FromSeconds(5)).Publish().RefCount();

            var switchNetworkStatus = this.WhenAnyValue(vm => vm.MobilNetworkStatus)
                .SkipUntil(waitToStart)
                .Select(value =>
                    Observable.FromAsync(cancellation => networkController.SwitchState(value, cancellation)))
                .Switch()
                .Subscribe()
                .DisposeWith(disposables);

            var setNetworkMode = networkController.NetworkMode
                .Subscribe(value => NetworkMode = value)
                .DisposeWith(disposables);

            var changeNetworkMode = this.WhenAnyValue(vm => vm.NetworkMode)
                .SkipUntil(waitToStart)
                .Select(value =>
                    Observable.FromAsync(cancellation => networkController.SwitchNetworkMode(value, cancellation)))
                .Switch()
                .Subscribe()
                .DisposeWith(disposables);
        });
    }

    [Reactive]
    public bool MobilNetworkStatus { get; set; }

    [ObservableAsProperty]
    public string MobilNetworkName { get; }

    [ObservableAsProperty]
    public string MobilNetworkType { get; }

    [ObservableAsProperty]
    public int SignalStrength { get; }

    [ObservableAsProperty]
    public ChargeState BatteryStatus { get; }

    [ObservableAsProperty]
    public int BatteryCapacity { get; }

    [ObservableAsProperty]
    public int ConnectedUsers { get; }

    [Reactive]
    public  NetworkMode NetworkMode { get; set; }

    public override int Order => 1;

    public override string Name => "Home";
    
    public override string Icon => "M23.951172 4 A 1.50015 1.50015 0 0 0 23.072266 4.3222656L8.859375 15.519531C7.0554772 16.941163 6 19.113506 6 21.410156L6 40.5C6 41.863594 7.1364058 43 8.5 43L18.5 43C19.863594 43 21 41.863594 21 40.5L21 30.5C21 30.204955 21.204955 30 21.5 30L26.5 30C26.795045 30 27 30.204955 27 30.5L27 40.5C27 41.863594 28.136406 43 29.5 43L39.5 43C40.863594 43 42 41.863594 42 40.5L42 21.410156C42 19.113506 40.944523 16.941163 39.140625 15.519531L24.927734 4.3222656 A 1.50015 1.50015 0 0 0 23.951172 4 z M 24 7.4101562L37.285156 17.876953C38.369258 18.731322 39 20.030807 39 21.410156L39 40L30 40L30 30.5C30 28.585045 28.414955 27 26.5 27L21.5 27C19.585045 27 18 28.585045 18 30.5L18 40L9 40L9 21.410156C9 20.030807 9.6307412 18.731322 10.714844 17.876953L24 7.4101562 z";
    
    public ViewModelActivator Activator { get; }
    
    private void HandleActivation() { }

    private void HandleDeactivation() { }
}