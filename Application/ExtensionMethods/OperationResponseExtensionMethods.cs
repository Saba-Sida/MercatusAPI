using Application.Models.Common;

namespace Application.ExtensionMethods;

public static class OperationResponseExtensionMethods
{
    public static void MakeGenerallyFailedResponse<T>(this T operationResponse, string errorMessage) where T : OperationResponse
    {
        operationResponse.ErrorMessage = errorMessage;
        operationResponse.IsSuccess = false;
        operationResponse.Status = OperationResponseStatus.ValidationError;
    }
}