using System;
using System.Threading;
using System.Threading.Tasks;
using LinkZoneManager.Services.Interfaces;
using LinkZoneSdk;

namespace LinkZoneManager.Services;

public sealed class UssdService : IUssdService
{
    private readonly ISdk _sdk;
    private int ussdType = 1; // Is 1 in first USSD call and 2 in each nested menu call

    public UssdService(ISdk sdk)
    {
        _sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
    }

    public async Task<string> ExecuteCodeAsync(string ussdCode, CancellationToken cancellation)
    {
        var sendUssdResult = await _sdk.Ussd().Send(ussdCode, ussdType, cancellation).ConfigureAwait(false);

        if (sendUssdResult.IsFailed)
        {
            // TODO: Localize error
            return "An exception has been throw during USSD code execution.";
        }

        await Task.Delay(TimeSpan.FromSeconds(5), cancellation).ConfigureAwait(false);

        var resultData = await _sdk.Ussd().GetSendResult(cancellation).ConfigureAwait(false);

        if (resultData.IsFailed)
        {
            // TODO: Localize error
            return "An exception has been throw retrieving USSD code result";
        }

        var resultValue = resultData.Value;

        ussdType = resultValue.UssdType;

        // TODO: Localize error
        return resultValue.SendState switch
        {
            2 => resultValue.UssdContent,
            3 => "An exception has been throw, try to change NetworkMode to 3G or Auto",
            _ => "An exception has been throw, try again."
        };
    }

    public async Task CancelCodeExecutionAsync(CancellationToken cancellation)
    {
        ussdType = 1;
        var _ = await _sdk.Ussd().SetEnd(cancellation).ConfigureAwait(false);
    }
}