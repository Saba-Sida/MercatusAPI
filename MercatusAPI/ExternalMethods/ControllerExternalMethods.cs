using Application.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace MercatusAPI.ExternalMethods;

public static class ControllerExternalMethods
{
    /// <summary>
    /// Returns api ready response type from OperationResponse Wrapper
    /// </summary>
    /// <param name="controllerBase"></param>
    /// <param name="operationResponse"></param>
    /// <returns></returns>
    public static IActionResult FormatResponse(this ControllerBase controllerBase,
        OperationResponse operationResponse)
    {
        if (operationResponse.IsSuccess)
        {
            return controllerBase.StatusCode((int)operationResponse.Status);
        }

        return controllerBase.StatusCode((int)operationResponse.Status, operationResponse.ErrorMessage);
    }
    
    /// <summary>
    /// Returns api ready response type from OperationResponse Wrapper
    /// </summary>
    /// <param name="controllerBase"></param>
    /// <param name="operationResponse"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IActionResult FormatResponse<T>(this ControllerBase controllerBase,
        OperationResponse<T> operationResponse)
    {
        if (operationResponse.IsSuccess)
        {
            return controllerBase.StatusCode((int)operationResponse.Status, operationResponse.Data);
        }

        return controllerBase.StatusCode((int)operationResponse.Status, operationResponse.ErrorMessage);
    }
}