using KASHOP12.BLL.Service; 
using KASHOP12.DAL.Data.DTO.Request;
using KASHOP12.DAL.Models;
using KASHOP12.PL.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace KASHOP12.PL.Areas.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public ProductsController(IProductService productService,
            IStringLocalizer<SharedResource> localizer
            )
        {
            _productService = productService;
            _localizer = localizer;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var response = await _productService.GetAllProductsforAdmin();
            return Ok(new { message = _localizer["Success"].Value, response });
        }


        [HttpPost("")]
        public async Task<IActionResult> Create([FromForm]ProductRequest request)
        {
            var response = await _productService.CreateProduct(request);
            return Ok(new { message = _localizer["Success"].Value, response });
        }

     
    }
}
