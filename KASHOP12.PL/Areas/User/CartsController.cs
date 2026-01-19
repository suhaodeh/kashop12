using System.Security.Claims;
using KASHOP12.BLL.Service;
using KASHOP12.DAL.Data.DTO.Request;
using KASHOP12.PL.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace KASHOP12.PL.Areas.User
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public CartsController(ICartService cartService,IStringLocalizer<SharedResource> localizer)
        {
            _cartService = cartService;
            _localizer = localizer;
        }
        [HttpPost("")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartService.AddToCartAsync(userId, request);
            return Ok(result);

        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartService.GetUserCartAsync(userId);
            return Ok(result);

        }

        [HttpDelete("")]
        public async Task<IActionResult> ClearCart()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartService.ClearCartAsync(userId);
            return Ok(result);

        }
}
}
