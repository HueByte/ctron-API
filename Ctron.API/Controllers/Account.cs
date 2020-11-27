using System;
using System.Threading.Tasks;
using Ctron.API.Authentication;
using Ctron.API.DTO;
using Ctron.API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ctron.API.Controllers
{
    public class Account : BaseApiController
    {
        private IApiAuthentication _auth;
        public Account(IApiAuthentication auth)
        {
            _auth = auth;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Test()
        {
            return Ok();
        }
        
        //POST: api/Account/login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginService([FromBody] UserDTO login)
        {
            IActionResult response = Unauthorized();
            try
            {
                var user = await _auth.AuthenticateUser(login);
                if (user != null)
                {
                    return Ok(user);
                }
            }
            catch
            {
                response = StatusCode(500);
            }

            return response;   
        }
        
        //POST: api/Account/register
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterService([FromBody] RegisterModel user)
        {
            var result = await _auth.Register(user);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}