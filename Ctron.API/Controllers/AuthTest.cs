using Ctron.API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ctron.API.Controllers
{
    public class AuthTest : BaseApiController
    {
        public AuthTest() { }
        
        [HttpGet("AdminTest")]
        [Authorize(Roles = Entities.Roles.Admin)]
        public IActionResult AdminTest()
        {
            return Ok("Verified as Admin");
        }

        [HttpGet("UserTest")]
        [Authorize(Roles = Entities.Roles.User)]
        public IActionResult UserTest()
        {
            return Ok("Verified as User");
        }

        [HttpGet("AdminUserTest")]
        [Authorize(Roles = Entities.Roles.Admin + "," + Entities.Roles.User)]
        public IActionResult All()
        {
            return Ok("Verified as User&Admin");
        }

        [HttpGet("UnAuth")]
        [Authorize(Roles = "UnAuth")]
        public IActionResult UnAuth()
        {
            return Ok();
        }

        [HttpGet("Any")]
        [Authorize]
        public IActionResult Any()
        {
            return Ok("Verified");
        }
    }
}