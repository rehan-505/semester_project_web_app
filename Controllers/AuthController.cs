using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.EntityFrameworkCore;
using semester_project_web_app.Models;
using System.Text;

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
            FoodDbContext foodDbc = new FoodDbContext();

            foodDbc.Users.Load();

            if (foodDbc.Users.Any(e => e.Email.Equals(user.Email)))
            {

                Console.WriteLine("Email already registered");
                Response.Redirect("../Auth");
                return;
                
            }



            foodDbc.Add(user);
            foodDbc.SaveChanges();
            Response.Redirect ("../Home");

            //return View("home");            
        }

        public void login(Models.User user)
        {

            FoodDbContext foodDbc = new FoodDbContext();

            foodDbc.Users.Load();


            Console.WriteLine (foodDbc.Users.Local.Count);

            if (foodDbc.Users.Any(e => e.Email.Equals(user.Email) && e.Pass.Equals(user.Pass)))
            {
                Response.Redirect("../Home");
            }

            else
            {

                Console.WriteLine("data not found");
                Response.Redirect("../Auth");
                Response.WriteAsync("<script>alert('login failed');</script>");

            }




        }

    }
}
