using MercatusAPI.Models.ResponseModels.Shared;
using Microsoft.AspNetCore.Mvc;

namespace MercatusAPI.ExternalMethods;

public static class ControllerExternalMethods
{
    /// <summary>
    /// Returns api ready response type from ApiResponse Wrapper
    /// </summary>
    /// <param name="controllerBase"></param>
    /// <param name="apiResponse"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IActionResult FormatResponse<T>(this ControllerBase controllerBase, ApiResponse<T> apiResponse)
    {
        if (apiResponse.IsSuccess)
        {
            if (apiResponse.Data != null)
            {
                return controllerBase.StatusCode((int)apiResponse.StatusCode, apiResponse.Data);
            }

            return controllerBase.StatusCode((int)apiResponse.StatusCode);
        }

        return controllerBase.StatusCode((int)apiResponse.StatusCode, apiResponse.ErrorMessage);
    }
}