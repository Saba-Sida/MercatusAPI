namespace MercatusAPI.LayerSpecificHelpers;

public class PhotoConvertingHelper
{
    public async Task<(byte[], string)?> ConvertIFormFileToByteArrayAndFileExtension(IFormFile? file)
    {
        if (file == null) return null;
        
        using var ms = new MemoryStream();
        await file.CopyToAsync(ms);
        
        var fileByteArray = ms.ToArray();
        var fileExtension = Path.GetExtension(file.FileName);
        
        return (fileByteArray, fileExtension);
    }
}