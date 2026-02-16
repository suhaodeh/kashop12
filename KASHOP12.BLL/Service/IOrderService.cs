using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KASHOP12.DAL.Data.DTO.Response;
using KASHOP12.DAL.Models;

namespace KASHOP12.BLL.Service
{
  public  interface IOrderService
    {
        Task<List<OrderResponse>> GetOrdersAsync(OrderStatusEnum status);
        Task<BaseResponse> UpdateOrderStatusAsync(int orderId, OrderStatusEnum newStatus);
        Task<Order?> GetOrderByIdAsync(int orderId);
    }
}
