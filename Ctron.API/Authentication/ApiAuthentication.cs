using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Ctron.API.DTO;
using Ctron.API.Extensions;
using Ctron.API.Services;
using Ctron.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Ctron.API.Authentication
{
    public class ApiAuthentication : IApiAuthentication
    {
        private readonly IJwtAuthentication _jwtAuth;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;
        public ApiAuthentication(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config, IJwtAuthentication jwtAuth)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _config = config;
            _jwtAuth = jwtAuth;
        }

        public async Task<ApiResponse<VerifiedUser>> AuthenticateUser(UserDTO userModel)
        {
            //Get user 
            var user = await _userManager.FindByNameAsync(userModel.UserName);

            //Verify if _userManager returned user and check his password
            if(user != null && await _userManager.CheckPasswordAsync(user, userModel.Password))
            {
                //Get roles
                var userRoles = await _userManager.GetRolesAsync(user);

                //Create user
                var verifiedUser = new VerifiedUser { UserName = user.UserName, Role = userRoles };

                //Generate and assign JWT token to verified user
                verifiedUser = _jwtAuth.GenerateJsonWebToken(verifiedUser);
                return Response<VerifiedUser>.Create(verifiedUser, true, "Logged in");
            }

            return Response<VerifiedUser>.Create(null, false, "Couldn't find that user");
        }

        public async Task<ApiResponse<int>> Register(RegisterModel userModel) 
        {
            //check if user exists
            var userExists = await _userManager.FindByNameAsync(userModel.Username);
            if (userExists != null)
            {
                return Response<int>.Create(0, false, "This user already exists");
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = userModel.Email,
                UserName = userModel.Username,
            };

            //create user
            var result = await _userManager.CreateAsync(user, userModel.Password);
            if (!result.Succeeded)
                return Response<int>.Create(0, false, "Couldn't create user");

            return Response<int>.Create(0, true, "Created new user");
        }

        //public async Task AddRoles()
        //{
        //    var x = await _userManager.FindByNameAsync("tester");
        //    await _userManager.AddToRoleAsync(x, "Admin");
        //    await _userManager.AddToRoleAsync(x, "User");
        //}
    }
}