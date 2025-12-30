using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KASHOP12.DAL.Data.DTO.Response;

namespace KASHOP12.DAL.Data.DTO.Request
{
   public class ResetPasswordRequest
    {
        public string Code { get; set; }
        public string NewPassword { get; set; }
        public string Email { get; set; }
    }
}
