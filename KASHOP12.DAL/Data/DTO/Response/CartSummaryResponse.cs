using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP12.DAL.Data.DTO.Response
{
  public  class CartSummaryResponse
    {
        public List<CartResponse> Items { get; set; }
        public decimal CartTotal => Items.Sum(I => I.TotalPrice);
    }
}
