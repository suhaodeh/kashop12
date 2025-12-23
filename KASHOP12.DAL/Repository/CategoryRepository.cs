using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KASHOP12.DAL.Data;
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

        public Category Create(Category Request)
        {
            _context.Add(Request);
            _context.SaveChanges();
            return Request;
        }

        public List<Category> GetAll()
        {
             return _context.Categories.Include(c => c.Translations).ToList();
        }
    }
}
