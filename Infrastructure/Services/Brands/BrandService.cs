using Application.BlobStorage;
using Application.ExtensionMethods;
using Application.Models.Common;
using Application.Models.RequestModels;
using Application.Repositories.Brands;
using Application.Services.Brands;

namespace Infrastructure.Services.Brands;

public class BrandService : IBrandService
{
    private readonly IBrandRepository _brandRepository;
    private readonly IBrandPhotoStorageHelper _brandPhotoStorageHelper;

    public BrandService(
        IBrandRepository brandRepository,
        IBrandPhotoStorageHelper brandPhotoStorageHelper
    )
    {
        _brandRepository = brandRepository;
        _brandPhotoStorageHelper = brandPhotoStorageHelper;
    }

    public async Task<OperationResponse<int>> CreateNewBrand(AddNewBrandRequest request)
    {
        var newBrandOperationResponse = new OperationResponse<int>();

        if (await _brandRepository.BrandNameExists(request.Name))
        {
            newBrandOperationResponse.MakeConflictResponse("Brand name already exists");
        }
        else
        {
            var newBrandId = await _brandRepository.AddNewBrandName(request.Name);
            newBrandOperationResponse.Data = newBrandId;

            if (request.BrandPhoto is not null)
            {
                var brandPhotoAddress = 
                    await _brandPhotoStorageHelper
                        .SaveBrandPhoto(request.BrandPhoto.Value.Item1, request.BrandPhoto.Value.Item2);
                
                if (brandPhotoAddress is not null) 
                    await _brandRepository.UpdateBrandPhotoAddress(newBrandId, brandPhotoAddress);
            }
        }

        return newBrandOperationResponse;
    }
}