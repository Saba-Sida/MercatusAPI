using Application.BlobStorage;

namespace Infrastructure.BlobStirage;

public class GenericBlobStorageManager : IGenericBlobStorageManager
{
    public async Task<string?> SaveFile(string addressToSave, string fullFileName, byte[]? content)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(addressToSave) ||
                string.IsNullOrWhiteSpace(fullFileName) ||
                content == null ||
                content.Length == 0)
            {
                return null;
            }

            if (!Directory.Exists(addressToSave))
            {
                Directory.CreateDirectory(addressToSave);
            }

            var fullPath = Path.Combine(addressToSave, fullFileName);

            await File.WriteAllBytesAsync(fullPath, content);

            return fullPath;
        }
        catch
        {
            return null;
        }
    }
}