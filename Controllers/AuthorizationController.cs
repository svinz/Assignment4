using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectroInternalControlSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ElectroInternalControlSystem.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public AuthorizationController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleModel createRoleModel)
        {
            var role = new IdentityRole { Name = createRoleModel.Rolename };
            await roleManager.CreateAsync(role);
            return View();
        }
        [HttpGet]
        public IActionResult ListRoles()
        {
            
            var roles = new List<ListRolesViewModel>();

            foreach(var i in roleManager.Roles)
            {
                var role = new ListRolesViewModel();
                role.RoleID = i.Id;
                role.RoleName = i.Name;
                roles.Add(role);
            }
            return View(roles);
        }
    }
}
