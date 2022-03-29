using System;
using System.Threading;
using System.Threading.Tasks;
using LinkZoneManager.Services.Interfaces;
using LinkZoneSdk;

namespace LinkZoneManager.Services
{
    public class MobileNetworkController : IMobileNetworkController
    {
        private readonly ISdk _sdk;
        private readonly IBasicInfoReaderService _readerService;

        public MobileNetworkController(ISdk sdk, IBasicInfoReaderService readerService)
        {
            _sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
            _readerService = readerService ?? throw new ArgumentNullException(nameof(readerService));
        }

        public async Task SwitchState(bool connect, CancellationToken cancellation)
        {
            _readerService.StopListening();

            if (connect)
            {
                await _sdk.Connection().Connect(cancellation).ConfigureAwait(false);
            }
            else
            {
                await _sdk.Connection().Disconnect(cancellation).ConfigureAwait(false);
            }

            _readerService.StartListening();
        }

        public void SwitchNetworkType()
        {

        }
    }
}
