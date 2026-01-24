using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KASHOP12.DAL.Data.DTO.Request;
using KASHOP12.DAL.Data.DTO.Response;

namespace KASHOP12.BLL.Service
{
  public  interface IProductService
    {
        Task<ProductResponse> CreateProduct(ProductRequest request);
        Task<List<ProductResponse>> GetAllProductsforAdmin();
        Task<List<ProductUserResponse>> GetAllProductsForUser(int page = 1, int limit = 3);
        Task<ProductUserDetails> GetAllProductDetailsForUser(int id, string lang = "en");
    }
}
