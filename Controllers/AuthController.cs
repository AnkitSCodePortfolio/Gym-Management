using Gym_Management.DTO;
using Gym_Management.IRespository;
using Gym_Management.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Management.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> usermanager;
        private readonly ITokenRespository tokenrepo;

        public AuthController(UserManager<IdentityUser> usermanager, ITokenRespository tokenrepo)
        {
            this.usermanager = usermanager;
            this.tokenrepo = tokenrepo;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View("~/Views/Login/Register.cshtml");
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO register)
        {
            var identityuser = new IdentityUser
            {
                UserName = register.Username,
                Email = register.Username

            };
            var identityresult = await usermanager.CreateAsync(identityuser, register.Password);

            if (identityresult.Succeeded)
            {
                if (register.Roles != null && register.Roles.Any())
                {
                    identityresult = await usermanager.AddToRolesAsync(identityuser, register.Roles);

                    if (identityresult.Succeeded)
                    {
                        return Ok("User Registered Please Login");
                    }
                }
            }
            return BadRequest("Something went Wrong");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View("~/Views/Login/Login.cshtml");
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            var user = await usermanager.FindByEmailAsync(login.Email);
            if (user != null)
            {
                var checkpassword = await usermanager.CheckPasswordAsync(user, login.Password);

                if (checkpassword)
                {
                    var roles = await usermanager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        var token = tokenrepo.CreateToken(user, roles.ToList());
                        var response = new LoginResponseDTO
                        {
                            JwtToken = token,
                        };
                        return Ok(response);
                    }

                }

            }
            return BadRequest("Username and Password is Incorrect");
        }
    }
}
