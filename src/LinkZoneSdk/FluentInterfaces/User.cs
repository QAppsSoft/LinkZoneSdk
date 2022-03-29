using FluentResults;
using LinkZoneSdk.Errors;
using LinkZoneSdk.Errors.Authentication;
using LinkZoneSdk.Errors.Password;
using LinkZoneSdk.Models.User;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LinkZoneSdk
{
    internal partial class Sdk : IUser
    {
        public Task<Result<LoginToken>> Login(string password, CancellationToken? cancellation = null)
        {
            return Login("admin", password, cancellation);
        }
        public async Task<Result<LoginToken>> Login(string userName, string password, CancellationToken? cancellation = null)
        {
            var result = await _apiService.RequestJsonRpcAsync<LoginToken, Dictionary<string, object>>("Login", "1.1",
                parameters =>
                {
                    parameters.Add("UserName", userName);
                    parameters.Add("Password", password);
                }, null, cancellation);

            if (result.IsFailed)
            {
                return result.ToResult();
            }

            var resultValue = result.Value;

            if (RequestJsonRpcIsOk(resultValue))
            {
                return Result.Ok(resultValue.ResultParameters);
            }

            var errorCode = resultValue.ErrorParameters["code"].ToString();

            return errorCode switch
            {
                ErrorCodes.WrongUserOrPassword => Result.Fail(new PasswordError(password)),
                ErrorCodes.LoginFailed => Result.Fail(new LoginFailedError(password)),
                ErrorCodes.LoginAttemptsExceeded => Result.Fail(new LoginAttemptsExceededError(password)),
                _ => throw new Exception("LOGIN_STATE_LOGIN_TIMES_USEDOUT")
            };
        }

        public async Task<Result<bool>> Logout(CancellationToken? cancellation = null)
        {
            var result = await _apiService.RequestJsonRpcAsync<Dictionary<string, object>, Dictionary<string, object>>("Logout", "1.2", null, cancellation);

            if (result.IsFailed)
            {
                return result.ToResult();
            }

            return RequestJsonRpcIsOk(result.Value) ? Result.Ok(true) : Result.Fail("Failed to logout");
        }

        public async Task<Result<LoginStateInfo>> GetLoginStatus(CancellationToken? cancellation = null)
        {
            var result = await _apiService.RequestJsonRpcAsync<LoginStateInfo, Dictionary<string, object>>("GetLoginState", "1.3", null, cancellation);

            if (result.IsFailed)
            {
                return result.ToResult();
            }

            if (RequestJsonRpcIsOk(result.Value))
            {
                return Result.Ok(result.Value.ResultParameters);
            }

            return Result.Fail("Failed to retrieve login status");
        }

        public async Task<Result<bool>> ChangePassword(string userName, string currentPassword, string newPassword, CancellationToken? cancellation = null)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return Result.Fail("Username can't be blank");
            }

            if (string.IsNullOrWhiteSpace(currentPassword))
            {
                return Result.Fail("Current password can't be blank");
            }

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                return Result.Fail("New password can't be blank");
            }

            var result = await _apiService.RequestJsonRpcAsync<Dictionary<string, object>, Dictionary<string, object>>("ChangePassword", "1.4", parameters =>
            {
                parameters.Add("UserName", userName);
                parameters.Add("CurrPassword", currentPassword);
                parameters.Add("NewPassword", newPassword);
            }, null, cancellation);

            if (result.IsFailed)
            {
                return result.ToResult();
            }

            var resultValue = result.Value;

            if (RequestJsonRpcIsOk(resultValue))
            {
                return Result.Ok(true);
            }

            var errorCode = resultValue.ErrorParameters["code"].ToString();

            return errorCode switch
            {
                ErrorCodes.PasswordChangeFailed => Result.Fail(
                    new PasswordChangeFailError(userName, currentPassword, newPassword)),
                ErrorCodes.WrongCurrentPassword => Result.Fail(
                    new WrongCurrentPasswordError(userName, currentPassword, newPassword)),
                _ => Result.Fail(new PasswordChangeFailError(userName, currentPassword, newPassword))
            };
        }

        public async Task<Result> HeartBeat(CancellationToken? cancellation = null)
        {
            var result = await _apiService.RequestJsonRpcAsync<Dictionary<string, object>, Dictionary<string, object>>(
                "HeartBeat", "1.5", null, cancellation);

            return result.ToResult();
        }
    }
}