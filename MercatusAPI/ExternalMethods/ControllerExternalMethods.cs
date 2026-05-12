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
        return controllerBase.StatusCode((int)operationResponse.Status, operationResponse);
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
        return controllerBase.StatusCode((int)operationResponse.Status, operationResponse);
    }
}