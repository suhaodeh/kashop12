using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KASHOP12.DAL.Models;

namespace KASHOP12.DAL.Data.DTO.Response
{
   public class CategoryResponse
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public  string UserCreated { get; set; }


        public List<CategoryTranslationResponse> Translations { get; set; }
      
    }
}
