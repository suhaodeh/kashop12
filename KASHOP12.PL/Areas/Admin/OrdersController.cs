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
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IStringLocalizer _localizer;

        public OrdersController(IOrderService orderService, 
            IStringLocalizer<SharedResource> localizer)
        {
            _orderService = orderService;
            _localizer = localizer;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetOrders([FromQuery] OrderStatusEnum status = OrderStatusEnum.Pending)
        {
            var orders = await _orderService.GetOrdersAsync(status);
            return Ok(orders);
        }


        [HttpPatch("{OrderId}")]
        public async Task<IActionResult> UpdateStatus([FromRoute]int OrderId, [FromBody] UpdateOrderStatusRequest request)
        {
            var result = await _orderService.UpdateOrderStatusAsync(OrderId, request.Status);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

    }
}
