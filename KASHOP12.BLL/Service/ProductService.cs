using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KASHOP12.DAL.Data.DTO.Request;
using KASHOP12.DAL.Data.DTO.Response;
using KASHOP12.DAL.Models;
using KASHOP12.DAL.Repository;
using Mapster;

namespace KASHOP12.BLL.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileService _fileService;

        public ProductService(IProductRepository productRepository, IFileService fileService)
        {
            _productRepository = productRepository;
            _fileService = fileService;
        }

        public async Task<ProductResponse> CreateProduct(ProductRequest request)
        {
            var product = request.Adapt<Product>();
            if (request.MainImage != null)
            {
                var imagePath = await _fileService.UploadAsync(request.MainImage);
                product.MainImage = imagePath;
            }

            await _productRepository.AddAsync(product);
            return product.Adapt<ProductResponse>();

        }

     

        public async Task<List<ProductUserResponse>> GetAllProductsForUser()
        {
            var products = await _productRepository.GetAllAsync();

            var response = products.Adapt<List<ProductUserResponse>>();
            return response;
        }

        public async Task<List<ProductResponse>> GetAllProductsforAdmin()
        {
            var products = await _productRepository.GetAllAsync();

            var response = products.Adapt<List<ProductResponse>>();
            return response;
        }

        public async Task<ProductUserDetails> GetAllProductDetailsForUser(int id,string lang = "en")
        {
            var product = await _productRepository.FindByIdAsync(id);
            var response = product.BuildAdapter().AddParameters("lang", lang).AdaptToType<ProductUserDetails>();
            return response;
        }
    }
}
