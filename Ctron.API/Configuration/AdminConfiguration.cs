using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Ctron.API.Entities;
using Ctron.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;

namespace Ctron.API.Configuration
{
    public class AdminConfiguration
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        public AdminConfiguration(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAdminAndRoles()
        {
            //Get role name constants from Roles class
            var codeSideRoles = typeof(Roles).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly).ToList();

            //Check if role exists and create if it's necessary 
            foreach(var role in codeSideRoles)
            {
                var managerRole = await _roleManager.FindByNameAsync(role.GetRawConstantValue() as string);
                if(managerRole == null)
                    await _roleManager.CreateAsync(new IdentityRole { Name = (string)role.GetRawConstantValue() });
            }
            
            //check if admin exists and his roles
            var admin = await _userManager.FindByNameAsync("admin");
            if(admin != null)
            {
                if(await _userManager.IsInRoleAsync(admin, "admin"))
                {
                    return;
                }
                else
                {
                    await _userManager.AddToRoleAsync(admin, "admin");
                    return;
                }
            }
            
            //create admin
            admin = new ApplicationUser { Email = "admin@ctron.com", UserName = "admin" };
            var adminResult = await _userManager.CreateAsync(admin, "Admin1232"); //You should change password if you gonna use it
            
            //add admin to role
            var roleResult = await _userManager.AddToRoleAsync(admin, "admin");
        }
    }
}