using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IBusiness;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ViewModel.Entities;

namespace UPCTaggingInterface.Controllers
{
    [Produces("application/json")]
    [Route("api/login")]
    public class LoginController : Controller
    {
        protected IUserService _userService;
        public LoginController(IUserService userService)
        {

            _userService = userService;

        }


        [HttpPost]
        [Route("authenticate-user")]
        public async Task<IActionResult> Login([FromBody]LoginCredentialsDTO login)
        {
            try
            {

                //await _userService.CreateUser(new Business.Entities.User { Email = "manish.kotta@gmail.com", Name = "Manish Kumar", RoleID = 2, UserName = "manish9119", Password = "Manish@9119" });
                var result = await _userService.AuthenticateUser(login.Email, login.Password);

                if (result.IsSuccessed)
                {
                    var claims = new List<Claim>
                      {
                          new Claim(ClaimTypes.Name,login.Email),
                          new Claim("Role",result.Value?.RoleID.ToString())
                      };
                    var userIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(principal);

                    

                    return Ok(true);
                }
                return BadRequest("User not authenticated");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}