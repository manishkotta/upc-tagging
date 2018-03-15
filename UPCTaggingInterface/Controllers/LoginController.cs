using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly UserManager<Repositories.Entities.User> _userManager;
        private readonly SignInManager<Repositories.Entities.User> _signInManager;

        public LoginController(UserManager<Repositories.Entities.User> userManager,
            SignInManager<Repositories.Entities.User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("authenticate-user")]
        public async Task<IActionResult> Login([FromBody]LoginCredentialsDTO login)
        {
           
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(login.Email,login.Password,true,false);
            if (result.Succeeded)
                return Ok("Login Success");
            else if (result.IsNotAllowed)
                return BadRequest("Username password mismatch");
            else
                return BadRequest();
        }
    }
}