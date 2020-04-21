using ElectroInternalControlSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ElectroInternalControlSystem.Controllers
{
    [Authorize(Roles = "admin")] //Ensure that only users with role admin gets access
    public class UserAccountController : Controller
    {
        /// <summary>
        /// Constructor for setting up the UserAccountController
        /// </summary>
        /// <param name="userManager">UserManager </param>
        public UserAccountController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }
        /// <summary>
        /// Reads all users from the user database and returns them into the read.cshtml page
        /// </summary>
        public UserManager<ApplicationUser> UserManager { get; }
        [HttpGet]
        public IActionResult Read()
        {
            IEnumerable<ApplicationUser> users = UserManager.Users.AsEnumerable(); //Read in all users
            if (users == null)
            {
                return View();
            }

            var UsersModel = new List<ListUsersViewModel>(); //Set up a list of all users in ListUsersViewModel
            foreach (ApplicationUser i in users) //Loop through all users 
            {
                ListUsersViewModel user = new ListUsersViewModel //set up an instance of ListUsersViewModel
                {//Assign values from i to user
                    UserID = i.Id,
                    Firstname = i.FirstName,
                    Lastname = i.LastName,
                    Username = i.UserName
                };
                UsersModel.Add(user); //add the user to the UsersModel Instance
            }
            return View(UsersModel); //return the List of ListUsersViewModel to the Read.cshtml 
        }
        /// <summary>
        /// Show an empty register page
        /// </summary>
        /// <returns>View of the register page</returns>
        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }
        /// <summary>
        /// Function for recieving userdata and generating a new user
        /// </summary>
        /// <param name="model">User data</param>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid) //chech if the model is valid
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.Firstname,
                    LastName = model.Lastname
                };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password); //create a new user

                if (result.Succeeded) //if user is successfully created
                {
                    return RedirectToAction("Read"); //redirect and load the read page
                }

                foreach (IdentityError error in result.Errors) //read out all errors recieved from UserManager
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }
            return View(); //Return back to register page with error from the model
        }
        /// <summary>
        /// HTTP POST that removes user based on userID
        /// </summary>
        /// <param name="id">UserID</param>
        /// <returns>A View or RedirectToAction</returns>
        [HttpPost]
        public async Task<ActionResult> RemoveUser(string id)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {

                ViewBag.ErrorMessage = $"User with {id} can not be found";
                return View("Error");
            }
            IdentityResult result = await UserManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Read", "UserAccount");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View("Read");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">UserID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with {id} can not be found";
                return View("Error");
            }

            EditUserModel model = new EditUserModel
            {
                UserID = user.Id,
                Firstname = user.FirstName,
                Lastname = user.LastName,
                Username = user.UserName
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserModel model)
        {

            ApplicationUser user = await UserManager.FindByIdAsync(model.UserID);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with {model.UserID} can not be found";
                return View("Error");
            }
            user.FirstName = model.Firstname;
            user.LastName = model.Lastname;
            user.UserName = model.Username;

            IdentityResult result = await UserManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Read");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View();
        }
    }
}

