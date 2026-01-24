using KASHOP12.BLL.Service;
using KASHOP12.PL.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace KASHOP12.PL.Areas.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IStringLocalizer _localizer;

        public ProductsController(IProductService productService, IStringLocalizer<SharedResource> localizer)
        {
            _productService = productService;
            _localizer = localizer;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index([FromQuery] int page = 1, [FromQuery] int limit = 3)
        {
            var response = await _productService.GetAllProductsForUser(page,limit);
            return Ok(new { message = _localizer["Success"].Value, response });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Index([FromRoute] int id ,[FromQuery] string lang ="en")
        {
            var response = await _productService.GetAllProductDetailsForUser(id,lang);
            return Ok(new { message = _localizer["Success"].Value, response });
        }
    }
}
