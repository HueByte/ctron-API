using Ctron.API.Authentication;
using Ctron.API.Extensions;
using Ctron.Infrastructure.Models;
using Ctron.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Ctron.API.Controllers
{
    public class Admin : BaseApiController
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private IConfiguration _configuration; 
        private IJwtAuthentication _auth;
        public Admin(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IJwtAuthentication jwtAuth) 
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _auth = jwtAuth;
        }

        [HttpGet]
        public IActionResult Test()
        {
            return Ok("Test");
        }


        //GET: api/Admin/Users
        [HttpGet("Users")]
        [Authorize(Roles = Entities.Roles.Admin)]
        public IActionResult GetUsers()
        {
            var result = _userManager.Users;
            if(result != null)
                return Ok(Response<IQueryable<ApplicationUser>>.Create(result, true, ""));
            return BadRequest(Response<int>.Create(0, false, "Something went wrong check logs"));
        }

        
    }
}