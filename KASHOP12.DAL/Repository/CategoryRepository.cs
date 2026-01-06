using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KASHOP12.DAL.Data;
using KASHOP12.DAL.Data.DTO.Response;
using KASHOP12.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace KASHOP12.DAL.Repository
{
  public  class CategoryRepository :ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateAsync(Category Request)
        {
          await  _context.AddAsync(Request);
          await  _context.SaveChangesAsync();
            return Request;
        }

     

        public async Task<Category?> FindByIdAsync(int id)
        {
            return await _context.Categories.Include(c => c.Translations).FirstOrDefaultAsync(c => c.Id==id);
        }

        public async Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.Include(c => c.Translations).Include(c => c.User).ToListAsync();
        }
    }
}
