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
            TypeAdapterConfig<Category, CategoryResponse>.NewConfig().Map(dest => dest.UserCreated,source=> source.User.UserName);
        }
    }
}
