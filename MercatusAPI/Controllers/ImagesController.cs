using Application.BlobStorage;
using Microsoft.AspNetCore.Mvc;

namespace MercatusAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImagesController : ControllerBase
{
    private readonly IGenericBlobStorageManager _genericBlobStorageManager;

    public ImagesController(IGenericBlobStorageManager genericBlobStorageManager)
    {
        _genericBlobStorageManager = genericBlobStorageManager;
    }

    [HttpGet("{*relativePath}")]
    public async Task<IActionResult> GetImage(string relativePath)
    {
        var (bytes, contentType) = await _genericBlobStorageManager.LoadImageAsync(relativePath);

        if (bytes == null)
            return NotFound();

        return File(bytes, contentType!);
    }
}