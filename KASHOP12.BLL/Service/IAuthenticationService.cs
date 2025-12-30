using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KASHOP12.DAL.Data.DTO.Request;
using KASHOP12.DAL.Data.DTO.Response;

namespace KASHOP12.BLL.Service
{
  public  interface IAuthenticationService
    {
        Task<RegisterResponse> RegisterAsync(RegisterRequest registerRequest);
        Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
        Task<bool> ConfirmEmailAsync(string token,string userId);
        Task<ForgotPasswordResponse> RequestPasswordReset(ForgotPasswordRequest request);
        Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest request);
    }
}
