using System.ComponentModel.DataAnnotations;

namespace Application.Models.RequestModels;

public record AddNewCategoryRequest(
    [Required] string NewCategoryName,
    int? CategoryParentId = null
    );