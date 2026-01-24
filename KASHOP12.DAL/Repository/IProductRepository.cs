using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KASHOP12.DAL.Data.DTO.Response;
using KASHOP12.DAL.Models;

namespace KASHOP12.DAL.Repository
{
  public  interface IProductRepository
    {
        Task<Models.Product> AddAsync(Models.Product request);
        Task<List<Product>> GetAllAsync();
        Task<bool> DecreaseQuantitiesAsync(List<(int productId, int quantity)> items);
        Task<Models.Product?> FindByIdAsync(int id);
        IQueryable<Product> Query();



    }
}
