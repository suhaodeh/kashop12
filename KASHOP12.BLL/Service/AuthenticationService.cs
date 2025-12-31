using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using KASHOP12.DAL.Data.DTO.Request;
using KASHOP12.DAL.Data.DTO.Response;
using KASHOP12.DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace KASHOP12.BLL.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthenticationService(UserManager<ApplicationUser> userManager, IConfiguration configuration, IEmailSender emailSender,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailSender = emailSender;
            _signInManager = signInManager;
        }
        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginRequest.Email);
                if (user is null)
                {
                    return new LoginResponse()
                    {
                        Success = false,
                        Message = "Invalid Email",

                    };

                }
                if (await _userManager.IsLockedOutAsync(user))
                {
                    return new LoginResponse()
                    {

                        Success = false,
                        Message = "Account is locked,try again later"
                    };
                }


                var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, true);


                if (!result.IsLockedOut)
                {
                    return new LoginResponse()
                    {
                        Success = false,
                        Message = "Account is locked"
                    };
                }
                else if (result.IsNotAllowed)
                {
                    return new LoginResponse()
                    {
                        Success = false,
                        Message = "please confirm your email"
                    };

                }

                if (!result.Succeeded)
                {
                    return new LoginResponse()
                    {
                        Success = false,
                        Message = "Invalid Password"
                    };

                }




                return new LoginResponse()
                {
                    Success = true,
                    Message = "login successfully",
                    AccessToken = await GenerateAccessToken(user),


                };


            }
            catch (Exception ex)
            {
                return new LoginResponse()
                {
                    Success = false,
                    Message = "Un Expected Error!",
                    Errors = new List<string> { ex.Message }
                };


            }
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest registerRequest)
        {
            try
            {
                var user = registerRequest.Adapt<ApplicationUser>();
                var result = await _userManager.CreateAsync(user, registerRequest.Password);

                if (!result.Succeeded)
                {
                    return new RegisterResponse()
                    {
                        Success = false,
                        Message = "user creation failed",
                        Errors = result.Errors.Select(e => e.Description).ToList()
                    };
                }
                await _userManager.AddToRoleAsync(user, "User");
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                token = Uri.EscapeDataString(token);
                var emailUrl = $"https://localhost:7245/api/auth/Account/ConfirmEmail?token={token}&userId={user.Id}";

                await _emailSender.SendEmailAsync(user.Email, "Welcome", $"<h1>Welcome ..{user.UserName}</h1> " +
                    $"<a href='{emailUrl}'>Confirm Email </a>");
                return new RegisterResponse()
                {
                    Success = true,
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new RegisterResponse()
                {
                    Success = false,
                    Message = "Un Expected Error!",
                    Errors = new List<string> { ex.Message }
                };

            }
        }

        public async Task<bool> ConfirmEmailAsync(string token, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null) return false;

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return false;
            }
            return true;



        }


        public async Task<string> GenerateAccessToken(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var userClaimes = new List<Claim>()
            { new Claim("id",user.Id),
            new Claim("userName",user.UserName),
            new Claim("email",user.Email),
            new Claim(ClaimTypes.Role,string.Join(',',roles))
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:secretkey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: userClaimes,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<ForgotPasswordResponse> RequestPasswordReset(ForgotPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                return new ForgotPasswordResponse
                {
                    Success = false,
                    Message = "Email not found"
                };
            }
                var random = new Random();
                var code = random.Next(1000, 9999).ToString();

                user.CodeResetPassword = code;
                user.CodeResetPassword = code;
                user.PasswordResetCodeExpiry = DateTime.UtcNow.AddMinutes(5);
                await _userManager.UpdateAsync(user);
                await _emailSender.SendEmailAsync(request.Email, "resetPassword", $"<p>code is {code}</p>");
              
                return new ForgotPasswordResponse
                {
                    Success = true,
                    Message = "code sent to your email"
                };
            }



        public async Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                return new ResetPasswordResponse
                {
                    Success = false,
                    Message = "Email not found"
                };
            }

            else if (user.CodeResetPassword != request.Code)
            {
                return new ResetPasswordResponse
                {
                    Success = false,
                    Message = "Invalid code"
                };

            }
            else if (user.PasswordResetCodeExpiry < DateTime.UtcNow)
            {
                return new ResetPasswordResponse
                {
                    Success = false,
                    Message = " code Expire"
                };
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);
            if (!result.Succeeded)
            {
                return new ResetPasswordResponse
                {
                    Success = false,
                    Message = "password reset failed",
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };
            }

            await _emailSender.SendEmailAsync(request.Email, "changePassword", $"<p> your password is changed</p>");

            return new ResetPasswordResponse
            {
                Success = true,
                Message = "Password reset successfully"
            };
        }

    }
    }

