using System.Net;

namespace MercatusAPI.Models.ResponseModels.Shared;

public record ApiResponse<T>(
    T? Data,
    bool IsSuccess,
    HttpStatusCode StatusCode,
    string ErrorMessage
    );