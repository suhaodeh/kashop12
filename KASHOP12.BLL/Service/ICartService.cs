using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KASHOP12.DAL.Data.DTO.Request;
using KASHOP12.DAL.Data.DTO.Response;

namespace KASHOP12.BLL.Service
{
 public   interface ICartService
    {
        Task<BaseResponse> AddToCartAsync(string userId, AddToCartRequest request);
        Task<CartSummaryResponse> GetUserCartAsync(string userId);
        Task<BaseResponse> ClearCartAsync(string userId);
    }
}
