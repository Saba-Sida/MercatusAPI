namespace Application.Models.Common;

public enum OperationResponseStatus
{
    Success = 200,
    NotFound = 404,
    ValidationError = 400,
    Conflict = 409,
    Unauthorized = 401,
    Forbidden = 403,
    ServerError = 500
}