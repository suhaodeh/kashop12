using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP12.DAL.Models
{
   public class Category :BaseModel
    {


        public List<CategoryTranslation> Translations { get; set; }

        public List<Product> Products { get; set; }
    }
}
