using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KASHOP12.DAL.Data.DTO.Response;
using KASHOP12.DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace KASHOP12.BLL.MapsterConfigurations
{
  public static  class MapsterConfig
    {
        public static void MapsterConfigRegister()
        {
            TypeAdapterConfig<Category, CategoryResponse>.NewConfig()
                .Map(dest => dest.UserCreated,source=> source.User.UserName);

            TypeAdapterConfig<Product, ProductResponse>.NewConfig()
                .Map(dest => dest.MainImage, source => $" https://localhost:7245/Images/{source.MainImage}");

            TypeAdapterConfig<Product, ProductUserResponse>.NewConfig()
                 .Map(dest => dest.MainImage, source => $" https://localhost:7245/Images/{source.MainImage}")
              .Map(dest => dest.Name, source => source.Translations);

            TypeAdapterConfig<Product, ProductUserDetails>.NewConfig()
              .Map(dest => dest.Name, source => source.Translations.
              Where(t => t.Language == MapContext.Current.Parameters["lang"].ToString())
              .Select(t => t.Name).FirstOrDefault())
                 .Map(dest => dest.Description, source => source.Translations
                 .Where(t => t.Language == MapContext.Current.Parameters["lang"].ToString())
                 .Select(t=>t.Description).FirstOrDefault());






        }
    }
}
