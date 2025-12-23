using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KASHOP12.DAL.Models;

namespace KASHOP12.DAL.Repository
{
  public  interface ICategoryRepository
    {
        List<Category> GetAll();
        Category Create(Category Request);
    }
}
