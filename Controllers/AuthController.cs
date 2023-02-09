using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.EntityFrameworkCore;
using semester_project_web_app.Model;
using System.Numerics;
using System.Text;

namespace semester_project_web_app.Controllers
{
    public class AuthController : Controller
    {



        private readonly IWebHostEnvironment _webHostEnvironment;

        public AuthController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ViewResult signup(Model.User user, List<IFormFile> imageFile)
        {
            Console.WriteLine(user.Email);
            Console.WriteLine(user.Pass);
            Console.WriteLine(user.Image);

                string wwwPath = _webHostEnvironment.WebRootPath;

            Console.WriteLine("www path" + wwwPath);

                string path = Path.Combine(wwwPath, "Uploads");
            Console.WriteLine($"{path}");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }



            Console.WriteLine($"image list length {imageFile.Count}");





            foreach (var file in imageFile)
                {

                Console.WriteLine($"inside in loop, length {imageFile.Count}");

                    var fileName = Path.GetFileName(file.FileName);
                    user.Image = fileName;
                    var pathWithFileName = Path.Combine(path, fileName);

                Console.WriteLine($"path with file name: {pathWithFileName}");



                using (FileStream stream = new
                        FileStream(pathWithFileName,
                        FileMode.Create))
                    {
                    Console.WriteLine($"copying file");

                    file.CopyTo(stream);
                    }
                }



            UserRepository userRepository = new UserRepository();
            

            if (userRepository.getUsers().Any(e => e.Email.Equals(user.Email)))
            {

                Console.WriteLine("Email already registered");
                
                ViewBag.msg = "Email already registered";
                return View("../Error/Index");

            }


            saveUserInCookies(user);
            userRepository.addUser(user,_webHostEnvironment);
            ProductRepository productRepository = new ProductRepository();

            return View("../Home/Index", productRepository.getProducts());

            //return View("home");            
        }


        [HttpGet]
        public ViewResult login(Model.User user)
        {

            UserRepository userRepository = new UserRepository();

            if (userRepository.getUsers().Any(e => e.Email.Equals(user.Email) && e.Pass.Equals(user.Pass)))
            {
                ProductRepository productRepository= new ProductRepository();
                saveUserInCookies(userRepository.getUsers().Where(e => e.Email.Equals(user.Email) && e.Pass.Equals(user.Pass)).ToList().FirstOrDefault());
                return View("../Home/Index", productRepository.getProducts());
                //Response.Redirect("../Home",foodDbc.Products);
            }

            else
            {

                ViewBag.msg = "Invalid Credentials";


                return View("../Error/Index");

            }




        }

        private void loadDataAndNavigate()
        {
            FoodDbContext foodDbc = new FoodDbContext();
            foodDbc.Products.Load();


        }


        private void addData()
        {
            FoodDbContext foodDbc = new FoodDbContext();
            List<Product> products = new List<Product>();
            for (int i = 0; i < 9; i++)
            {
                String imgUrl = "https://images.unsplash.com/photo-1613588718956-c2e80305bf61?crop=entropy&cs=tinysrgb&fit=crop&fm=jpg&h=250&ixid=MnwxfDB8MXxyYW5kb218MHx8bW9iaWxlfHx8fHx8MTY3NTcwODE2Mw&ixlib=rb-4.0.3&q=80&utm_campaign=api-credit&utm_medium=referral&utm_source=unsplash_source&w=400";

                foodDbc.Products.Add(new Product
                {
                    Des = "Loren Ipsum ",
                    Image = imgUrl,
                    Price = 25,
                    Title = "Dummy Product"

                });

                Console.WriteLine(foodDbc.Products.Count());

                foodDbc.SaveChanges();

            }


        }


        private void saveUserInCookies(Model.User user)
        {
            HttpContext.Response.Cookies.Append("user", user.Id.ToString());
        }




    }
}
