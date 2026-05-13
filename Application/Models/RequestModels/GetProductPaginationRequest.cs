using System.ComponentModel.DataAnnotations;

namespace Application.Models.RequestModels;

public record GetProductPaginationRequest(
    [Required] int PageNumber,
    [Required] int PageSize
    );