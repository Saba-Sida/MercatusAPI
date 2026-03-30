namespace Application.Models.Common;

public class OperationResponse
{
    public bool IsSuccess { get; set; } = true;
    public string ErrorMessage { get; set; } = string.Empty;
    public OperationResponseStatus Status { get; set; } = OperationResponseStatus.Success;
}

public class OperationResponse<T>: OperationResponse
{
    public T? Data { get; set; }
}