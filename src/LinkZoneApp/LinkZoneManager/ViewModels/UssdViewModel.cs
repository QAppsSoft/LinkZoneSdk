using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using LinkZoneManager.Infrastructure;
using LinkZoneManager.Services.Interfaces;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace LinkZoneManager.ViewModels
{
    public sealed class UssdViewModel : PageViewModelBase
    {
        private readonly IUssdService _ussdService;

        public UssdViewModel(IUssdService ussdService, ISchedulerProvider schedulerProvider)
        {
            _ussdService = ussdService ?? throw new ArgumentNullException(nameof(ussdService));
            
            ExecuteCode = ReactiveCommand.CreateFromTask<string, string>(ExecuteCodeAsync);
            
            Cancel = ReactiveCommand.CreateFromTask(CancelAsync);

            var execution = ExecuteCode.Merge(Cancel).Publish().RefCount();

            execution.ToPropertyEx(this, vm => vm.UssdResponse, scheduler: schedulerProvider.Dispatcher);
            execution.ObserveOn(schedulerProvider.Dispatcher).Subscribe(_ => UssdValue = "");
        }

        private async Task<string> CancelAsync(CancellationToken cancellation)
        {
            await _ussdService.CancelCodeExecutionAsync(cancellation);
            return "";
        }

        private Task<string> ExecuteCodeAsync(string code, CancellationToken cancellation)
        {
            return _ussdService.ExecuteCodeAsync(code, cancellation);
        }


        [ObservableAsProperty]
        public string UssdResponse { get; }

        [Reactive]
        public string UssdValue { get; set; }

        public ReactiveCommand<string, string> ExecuteCode { get; }

        public ReactiveCommand<Unit, string> Cancel { get; }


        public override int Order => 2;
        public override string Name => "USSD";
        public override string Icon => "M10.5 4.9980469C6.9280619 4.9980469 4 7.9261087 4 11.498047L4 28.498047C4 32.069985 6.9280619 34.998047 10.5 34.998047L12 34.998047L12 40.498047C12 42.464037 14.427297 43.67893 16 42.5 A 1.50015 1.50015 0 0 0 16 42.498047L26 34.998047L37.5 34.998047C41.071576 34.998047 44 32.070691 44 28.498047L44 11.498047C44 7.9261557 41.071938 4.9980469 37.5 4.9980469L10.5 4.9980469 z M 10.5 7.9980469L37.5 7.9980469C39.450062 7.9980469 41 9.5479848 41 11.498047L41 28.498047C41 30.449402 39.450424 31.998047 37.5 31.998047L25.5 31.998047 A 1.50015 1.50015 0 0 0 24.599609 32.298828L15 39.498047L15 33.498047 A 1.50015 1.50015 0 0 0 13.5 31.998047L10.5 31.998047C8.5499381 31.998047 7 30.448109 7 28.498047L7 11.498047C7 9.5479848 8.5499381 7.9980469 10.5 7.9980469 z M 16.486328 15.978516 A 1.50015 1.50015 0 0 0 15.439453 16.439453L14 17.878906L12.560547 16.439453 A 1.50015 1.50015 0 0 0 11.484375 15.984375 A 1.50015 1.50015 0 0 0 10.439453 18.560547L11.878906 20L10.439453 21.439453 A 1.50015 1.50015 0 1 0 12.560547 23.560547L14 22.121094L15.439453 23.560547 A 1.50015 1.50015 0 1 0 17.560547 21.439453L16.121094 20L17.560547 18.560547 A 1.50015 1.50015 0 0 0 16.486328 15.978516 z M 26.486328 15.978516 A 1.50015 1.50015 0 0 0 25.439453 16.439453L24 17.878906L22.560547 16.439453 A 1.50015 1.50015 0 0 0 21.484375 15.984375 A 1.50015 1.50015 0 0 0 20.439453 18.560547L21.878906 20L20.439453 21.439453 A 1.50015 1.50015 0 1 0 22.560547 23.560547L24 22.121094L25.439453 23.560547 A 1.50015 1.50015 0 1 0 27.560547 21.439453L26.121094 20L27.560547 18.560547 A 1.50015 1.50015 0 0 0 26.486328 15.978516 z M 36.486328 15.978516 A 1.50015 1.50015 0 0 0 35.439453 16.439453L34 17.878906L32.560547 16.439453 A 1.50015 1.50015 0 0 0 31.484375 15.984375 A 1.50015 1.50015 0 0 0 30.439453 18.560547L31.878906 20L30.439453 21.439453 A 1.50015 1.50015 0 1 0 32.560547 23.560547L34 22.121094L35.439453 23.560547 A 1.50015 1.50015 0 1 0 37.560547 21.439453L36.121094 20L37.560547 18.560547 A 1.50015 1.50015 0 0 0 36.486328 15.978516 z";
    }
}
