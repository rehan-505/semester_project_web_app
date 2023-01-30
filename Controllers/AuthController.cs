using Microsoft.AspNetCore.Mvc;
using semester_project_web_app.Models;

namespace semester_project_web_app.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public void signup(Models.User user)
        {
            Console.WriteLine(user.Email);
            Console.WriteLine(user.Pass);

            //return View("home");            
        }
    }
}
