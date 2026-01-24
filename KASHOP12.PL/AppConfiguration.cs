using KASHOP12.BLL.Service;
using KASHOP12.DAL.Repository;
using KASHOP12.DAL.Utils;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace KASHOP12.PL
{
    public static class AppConfiguration
    {
        public static void config(IServiceCollection Services)
        {

            Services.AddScoped<ICategoryRepository, CategoryRepository>();
         
            Services.AddScoped<ICategoryService, CategoryService>();


            Services.AddScoped<IAuthenticationService, AuthenticationService>();

           Services.AddScoped<ISeedData, RoleSeedData>();
           Services.AddScoped<ISeedData, UserSeedData>();
           Services.AddTransient<IEmailSender, EmailSender>();

            Services.AddScoped<IFileService,FileService>();
            Services.AddScoped<IProductService,ProductService>();
            Services.AddScoped<IProductRepository, ProductRepository>();
            Services.AddScoped<ITokenService, TokenService>();



            Services.AddScoped<ICartService , CartService>();
            Services.AddScoped<ICartRepository , CartRepository>();
            Services.AddScoped<ICheckoutService, CheckoutService>();
            Services.AddScoped<IOrderRepository, OrderRepository>();
            Services.AddScoped<IOrderItemRepository, OrderItemRepository>();







        }
    }
}
