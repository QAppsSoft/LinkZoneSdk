using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using LinkZoneManager.Services;
using LinkZoneManager.Services.Interfaces;
using LinkZoneManager.ViewModels;
using LinkZoneManager.ViewModels.Interfaces;
using LinkZoneManager.Views;
using LinkZoneSdk.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace LinkZoneManager
{
    public partial class App : Application
    {
        public static IServiceProvider Services = null!; // Initialized before use

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            ConfigureServiceProvider();

            var viewModel = Services.GetService<MainWindowViewModel>();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = viewModel
                };
            }
            else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            {
                singleViewPlatform.MainView = new MainWindow
                {
                    DataContext = viewModel
                };
            }

            base.OnFrameworkInitializationCompleted();
        }

        private static void ConfigureServiceProvider()
        {
            var services = ConfigureServices();
            Services = services.BuildServiceProvider();
        }

        private static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();
            
            services.AddTransient<MainWindowViewModel>();
            services.AddSingleton<IBasicInfoReaderService, BasicInfoReaderService>();
            services.AddTransient<IMobileNetworkController, MobileNetworkController>();

            services.Scan(scan =>
            {
                scan.FromCallingAssembly()
                    .AddClasses(classes => classes.AssignableTo<IPageViewModel>())
                    .AsImplementedInterfaces()
                    .WithSingletonLifetime();
            });

            services.AddLinkZoneSdk();

            return services;
        }
    }
}