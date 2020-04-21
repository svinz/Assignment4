using ElectroInternalControlSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ElectroInternalControlSystem.Controllers
{
    [AllowAnonymous] //Allow everyone to see the login and logout
    public class LoginController : Controller
    {
        public LoginController(SignInManager<ApplicationUser> signInManager)
        {
            SignInManager = signInManager;
        }
        public SignInManager<ApplicationUser> SignInManager { get; }
        /// <summary>
        /// Function for checking if user is logged in and redirect if logged in. If not logged in, direct to the login page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login() //function to display the login window
        {
            if (SignInManager.IsSignedIn(User)) //Check if user is logged in
            {
                return RedirectToAction("index", "home"); //If logged in, redirect to another page
            }
            return View(); //If not logged in, go to login page
        }
        /// <summary>
        /// Logges in the user if credentials is correct.
        /// </summary>
        /// <param name="model">Login credentials</param>
        /// <returns>View</returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (ModelState.IsValid) //Check if the data written into the form is valid.
            {   //Sign in the user using the SignInManagaer
                var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded) //If the user is successfully logged in
                {
                    return RedirectToAction("index", "home"); //Redirect user 
                }
                ModelState.AddModelError("", "Invalid login"); //Add an error to the model if the login is not successfull
            }
            return View(model); //return to same page with errors
        }
        /// <summary>
        /// Logs out the current user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync(); //Sign out the user
            return RedirectToAction("Login", "Login"); //Redirect back to the login page
        }
    }
}