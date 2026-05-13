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

    /// <remarks>
    /// if we move the storages (and it is going to be a good approach) to some cloud hosted blob storage
    /// so it will be out of this server, then no longer necessity of this endpoint, for now, to develop the product,
    /// it will be that way
    /// </remarks>
    [HttpGet("{*relativePath}")]
    public async Task<IActionResult> GetImage(string relativePath)
    {
        var (bytes, contentType) = await _genericBlobStorageManager.LoadImageAsync(relativePath);

        if (bytes == null)
            return NotFound();

        return File(bytes, contentType!);
    }
}