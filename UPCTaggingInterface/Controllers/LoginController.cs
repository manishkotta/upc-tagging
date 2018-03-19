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
using Authentication;
using Common.CommonEntities;
using Common.CommonUtilities;

namespace UPCTaggingInterface.Controllers
{
    [Produces("application/json")]
    [Route("api/login")]
    public class LoginController : Controller
    {
        protected IUserService _userService;
        protected ITokenProvider _tokenProvider;
        public LoginController(IUserService userService,ITokenProvider tokenProvider)
        {

            _userService = userService;
            _tokenProvider = tokenProvider;
        }


        [HttpPost]
        [Route("authenticate-user")]
        public async Task<IActionResult> Login([FromBody]LoginCredentialsDTO login)
        {
            try
            {

                //await _userService.CreateUser(new Business.Entities.User { Email = "harshika.gupta@ggktech.com", Name = "Harshika Gupta", RoleID = 1, UserName = "harshikagupta", Password = "Harshika@9119" });
                var result = await _userService.AuthenticateUser(login.Email, login.Password);

                if (result.IsSuccessed)
                {
                    var user = result.Value;

                    var userToken = new UserToken
                    {
                        Id = user.UserID,
                        FullName = user.Name,
                        RoleID = user.RoleID
                    };

                    var token = _tokenProvider.CreateToken(userToken);

                    if (!token.IsSuccessed) BadRequest(token.GetErrorString());
                    
                    return Ok(new AuthTokenDTO { AuthToken = token.Value.Value, RoleName = Utilities.GetRoleName(user.RoleID) });
                }
                return BadRequest("User is not authenticated");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}