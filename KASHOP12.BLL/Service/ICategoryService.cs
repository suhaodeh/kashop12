using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KASHOP12.DAL.Data.DTO.Request;
using KASHOP12.DAL.Data.DTO.Response;
using KASHOP12.DAL.Models;

namespace KASHOP12.BLL.Service
{
   public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetAllCategories();
      Task<CategoryResponse> CreateCategory(CategoryRequest Request);
        Task<BaseResponse> DeleteCategoryAsync(int id);
        Task<BaseResponse> UpdateCategoryAsync(int id, CategoryRequest request);
        Task<BaseResponse> ToggleStatus(int id);

    }
}
