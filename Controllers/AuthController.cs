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
            foodDbc.Products.Load();
            Response.Redirect ("../Home");

            //return View("home");            
        }


        [HttpGet]
        public ViewResult login(Models.User user)
        {

            FoodDbContext foodDbc = new FoodDbContext();

            foodDbc.Users.Load();


            Console.WriteLine (foodDbc.Users.Local.Count);

            if (foodDbc.Users.Any(e => e.Email.Equals(user.Email) && e.Pass.Equals(user.Pass)))
            {
                foodDbc.Products.Load();
                return View("../Home/Index", foodDbc.Products.ToList());
                //Response.Redirect("../Home",foodDbc.Products);
            }

            else
            {

                Console.WriteLine("data not found");
                return View("Auth");

            }




        }

        private void loadDataAndNavigate()
        {
            FoodDbContext foodDbc = new FoodDbContext();
            foodDbc.Products.Load();


        }


        //private void addData()
        //{
        //    FoodDbContext foodDbc = new FoodDbContext();
        //    List<Product> products = new List<Product>();
        //    for (int i = 0; i < 9; i++) {
        //        foodDbc.Products.Add(new Product {
        //            Des = "Loren Ipsum ",
        //            Image = "https://source.unsplash.com/400x250/?mobile",
        //            Price= 25,
        //            Title = "Dummy Product"

        //        });

        //        Console.WriteLine(foodDbc.Products.Count());

        //        foodDbc.SaveChanges();

        //    }


        //}




    }
}
