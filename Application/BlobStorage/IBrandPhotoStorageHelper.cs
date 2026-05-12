namespace Application.BlobStorage;

public interface IBrandPhotoStorageHelper
{
    Task<string?> SaveBrandPhoto(byte[] bytes, string extension);
}