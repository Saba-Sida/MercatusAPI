using Application.BlobStorage;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.BlobStirage;

public class GenericBlobStorageManager : IGenericBlobStorageManager
{
    private readonly string _rootPath;

    public GenericBlobStorageManager(IConfiguration configuration)
    {
        _rootPath = configuration["BlobStorage:BaseUri"]!; // absolute path: C:/MercatusStorage
    }

    public async Task<string?> SaveFile(string relativeDirectory, string fileName, byte[]? content)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(relativeDirectory) ||
                string.IsNullOrWhiteSpace(fileName) ||
                content == null || content.Length == 0)
                return null;

            var absoluteDirectory = Path.Combine(_rootPath, relativeDirectory);

            if (!Directory.Exists(absoluteDirectory))
                Directory.CreateDirectory(absoluteDirectory);

            var absoluteFilePath = Path.Combine(absoluteDirectory, fileName);

            await File.WriteAllBytesAsync(absoluteFilePath, content);

            return Path.Combine(relativeDirectory, fileName).Replace("\\", "/");
        }
        catch
        {
            return null;
        }
    }

    public async Task<(byte[]? bytes, string? contentType)> LoadImageAsync(string relativePath)
    {
        try
        {
            var fullPath = Path.Combine(_rootPath, relativePath);

            if (!File.Exists(fullPath))
                return (null, null);

            var bytes = await File.ReadAllBytesAsync(fullPath);

            var contentType = GetContentType(fullPath);

            return (bytes, contentType);
        }
        catch
        {
            return (null, null);
        }
    }

    private string GetContentType(string path) =>
        Path.GetExtension(path).ToLower() switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".webp" => "image/webp",
            ".gif" => "image/gif",
            _ => "application/octet-stream"
        };
}