using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KASHOP12.DAL.Data;
using KASHOP12.DAL.Data.DTO.Response;
using KASHOP12.DAL.Migrations;
using KASHOP12.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Product = KASHOP12.DAL.Models.Product;

namespace KASHOP12.DAL.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

    

        public async Task<Models.Product> AddAsync(Models.Product request)
        {
            await _context.AddAsync(request);
            await _context.SaveChangesAsync();
            return request;
        }

   

       public async Task<List<Models.Product>> GetAllAsync()
        {
            return await _context.Products.Include(c => c.Translations).Include(c => c.User).ToListAsync();
        }

        public async Task<Models.Product?> FindByIdAsync(int id)
        {
            return await _context.Products.Include(c => c.Translations).FirstOrDefaultAsync(c => c.Id == id);
        }

        public IQueryable<Product> Query()
        {
            return _context.Products.Include(p => p.Translations).AsQueryable();
        }

        public async Task<bool> DecreaseQuantitiesAsync(List<(int productId, int quantity)> items)
        {
            var productIds = items.Select(p => p.productId).ToList();
            var products = await _context.Products.Where(p => productIds.Contains(p.Id)).ToListAsync();

            foreach(var prduct in products)
            {
                var item = items.FirstOrDefault(p => p.productId == prduct.Id);
                if (prduct.Quantity<item.quantity)
                {
                    return false;

                }
                prduct.Quantity -= item.quantity;

            }

        
            await _context.SaveChangesAsync();
            return true;

          
        }
    }
}
