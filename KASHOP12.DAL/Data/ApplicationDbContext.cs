using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KASHOP12.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace KASHOP12.DAL.Data
{
 public   class ApplicationDbContext :DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryTranslation> categoryTranslations { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
        {
        }
    }
}
