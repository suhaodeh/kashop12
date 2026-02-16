using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KASHOP12.DAL.Models;

namespace KASHOP12.DAL.Data.DTO.Response
{
  public  class OrderResponse
    {
        public int Id { get; set; }
        public  OrderStatusEnum OrderStatus { get; set; }
        public PaymentStatusEnum PaymentStatus{ get; set; }

        public decimal AmountPaid { get; set; }

        public string UserName{ get; set; }

      
    }
}
