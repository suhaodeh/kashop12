using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KASHOP12.DAL.Data;
using KASHOP12.DAL.Migrations;
using KASHOP12.DAL.Models;
using Microsoft.EntityFrameworkCore;

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
    }
}
