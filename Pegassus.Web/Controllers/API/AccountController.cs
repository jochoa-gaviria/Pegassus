using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pegasssus.Common.Models;
using Pegassus.Web.Data;
using Pegassus.Web.Data.Entities;
using Pegassus.Web.Helpers;

namespace Pegassus.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;
        private readonly IMailHelper _mailHelper;

        public AccountController(DataContext dataContext, IUserHelper userHelper, IMailHelper mailHelper)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
            _mailHelper = mailHelper;
        }

        [HttpPost]
        [Route("RegisterUser")]
        public async Task<IActionResult> PostUser([FromBody] UserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response<object>
                {
                    IsSuccess = false,
                    Message = "Bad Request"
                });
            }
            var user = await _userHelper.GetUserByEmailAsync(request.Email);
            if (user != null)
            {
                return BadRequest(new Response<object>
                {
                    IsSuccess = false,
                    Message = "This email is already registered."
                });
            }
            user = new User
            {
                Address = request.Address,
                Document = request.Document,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.Phone,
                UserName = request.Email
            };
            var result = await _userHelper.AddUserAsync(user, request.Password);
            if (result != IdentityResult.Success)
            {
                return BadRequest(result.Errors.FirstOrDefault().Description);
            }
            var userNew = await _userHelper.GetUserByEmailAsync(request.Email);
            await _userHelper.AddUserToRoleAsync(userNew, "Organizer");
            _dataContext.Organizers.Add(new Organizer { User = userNew });
            await _dataContext.SaveChangesAsync();

            var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
            var tokenLink = Url.Action("ConfirmEmail", "Account", new
            {
                userid = user.Id,
                token = myToken,
            }, protocol: HttpContext.Request.Scheme);

            _mailHelper.SendMail(request.Email, "Email confirmation", $"<h1>Email Confirmation</h1>" +
                $"To allow the user, " +
                $"Please click on this link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");
            return Ok(new Response<object>
            {
                IsSuccess = true,
                Message = "A confirmation email was sent. Please confirm your account and login into the App."
            });
        }

        [HttpPost]
        [Route("RecoverPassword")]
        public async Task<IActionResult> RecoverPassword([FromBody] EmailRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response<object>
                {
                    IsSuccess = false,
                    Message = "Bad Request"
                });
            }
            var user = await _userHelper.GetUserByEmailAsync(request.Email);
            if (user == null)
            {
                return BadRequest(new Response<object>
                {
                    IsSuccess = false,
                    Message = "The email is not assigned to any user."
                });
            }
            var myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);
            var link = Url.Action("ResetPassword", "Account", new
            {
                token = myToken
            }, protocol: HttpContext.Request.Scheme);
            _mailHelper.SendMail(request.Email, "Pegassus Password reset", $"<h1>Recover Password</h1> " +
                $"To reset the password click in this link:</br></br>" +
                $"<a href = \"{link}\">Reset Password</a>");

            return Ok(new Response<object>
            {
                IsSuccess = true,
                Message = "An email with instructions to change the password was sent."
            });
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("PutUser")]
        public async Task<IActionResult> PutUser([FromBody] UserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userEntity = await _userHelper.GetUserByEmailAsync(request.Email);
            if (userEntity == null)
            {
                return BadRequest("User not found");
            }
            userEntity.FirstName = request.FirstName;
            userEntity.LastName = request.LastName;
            userEntity.Address = request.Address;
            userEntity.PhoneNumber = request.Phone;
            userEntity.Document = request.Document;

            var response = await _userHelper.UpdateUserAsync(userEntity);
            if (!response.Succeeded)
            {
                return BadRequest(response.Errors.FirstOrDefault().Description);
            }
            var updateUser = await _userHelper.GetUserByEmailAsync(request.Email);
            return Ok(updateUser);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response<object>
                {
                    IsSuccess = false,
                    Message = "Bad Request"
                });
            }
            var user = await _userHelper.GetUserByEmailAsync(request.Email);
            if (user==null)
            {
                return BadRequest(new Response<object>
                {
                    IsSuccess = false,
                    Message = "This email is not assigned to any user"
                });
            }
            var result = await _userHelper.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest(new Response<object>
                {
                    IsSuccess = false,
                    Message = result.Errors.FirstOrDefault().Description
                });
            }
            return Ok(new Response<object>
            {
                IsSuccess = true,
                Message = "The Password has been changed successfully"
            });
        }
    }
}