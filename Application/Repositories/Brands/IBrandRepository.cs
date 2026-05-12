namespace Application.Repositories.Brands;

public interface IBrandRepository
{
    Task<bool> BrandNameExists(string name);
    Task<int> AddNewBrandName(string name);
    Task UpdateBrandPhotoAddress(int brandId, string brandPhotoAddress);
    Task<bool> BrandByIdExists(int brandId);
}