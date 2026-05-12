using Application.BlobStorage;
using Application.ExtensionMethods;
using Application.Models.Common;
using Application.Models.DTOs;
using Application.Models.RequestModels;
using Application.Repositories.Brands;
using Application.Repositories.Categories;
using Application.Repositories.ProductPhotos;
using Application.Repositories.Products;
using Application.Services.Products;

namespace Infrastructure.Services.Products;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductPhotoStorageHelper _productPhotoStorageHelper;
    private readonly IProductPhotoRepository _productPhotoRepository;

    public ProductService(
        IProductRepository productRepository,
        IBrandRepository brandRepository,
        ICategoryRepository categoryRepository,
        IProductPhotoStorageHelper productPhotoStorageHelper,
        IProductPhotoRepository productPhotoRepository
    )
    {
        _productRepository = productRepository;
        _brandRepository = brandRepository;
        _categoryRepository = categoryRepository;
        _productPhotoStorageHelper = productPhotoStorageHelper;
        _productPhotoRepository = productPhotoRepository;
    }

    public async Task<OperationResponse> AddNewProduct(AddNewProductCompleteRequest request)
    {
        OperationResponse productCreatingOperationResponse = new OperationResponse();

        if (!await _categoryRepository.CategoryByIdExists(request.CategoryId))
        {
            productCreatingOperationResponse
                .MakeGenerallyFailedResponse("Such category id was not found for product creation");
        }
        else if (!await _brandRepository.BrandByIdExists(request.BrandId))
        {
            productCreatingOperationResponse
                .MakeGenerallyFailedResponse("Such brand id was not found for product creation");
        }
        else
        {
            var productToAddModel = new AddableProductModel(
                ProductName: request.ProductName,
                ProductDescription: request.ProductDescription,
                Price: request.Price,
                InStockCount: request.InStockCount,
                BrandId: request.BrandId,
                CategoryId: request.CategoryId
            );
            var newProductId = await _productRepository.AddNewProduct(productToAddModel);
            
            List<string> savedPhotosAddresses = 
                await _productPhotoStorageHelper.SaveProductPhotos(newProductId, request.Photos);

            await _productPhotoRepository.AddProductPhotosByProductId(newProductId, savedPhotosAddresses);
        }

        return productCreatingOperationResponse;
    }
}