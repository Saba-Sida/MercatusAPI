namespace Application.Models.Common;

public class OperationResponse
{
    public bool IsSuccess { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
    public OperationResponseStatus Status { get; set; }
}

public class OperationResponse<T>: OperationResponse
{
    public T? Data { get; set; } = default;
}