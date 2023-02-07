using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using semester_project_web_app.Model;
using semester_project_web_app.Models;
using System.Diagnostics;

namespace semester_project_web_app.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ViewResult AddToCart( int productId) {

            String userId = HttpContext.Request.Cookies["user"]!;

            FoodDbContext foodDbContext = new FoodDbContext();
            foodDbContext.CartItems.Load();
            foodDbContext.CartItems.Add(new CartItem {
            ProductId = productId,
            UserId= int.Parse(userId),
            });
        foodDbContext.SaveChanges();

            return View("../Home/Index", foodDbContext.Products.ToList());

        }

        public ViewResult goToCart()
        {
            String userId = HttpContext.Request.Cookies["user"]!;

            FoodDbContext foodDbContext = new FoodDbContext();
            foodDbContext.CartItems.Load();

            List<CartItem> cartItems = foodDbContext.CartItems.Where(item => item.UserId.Equals(int.Parse(userId))).ToList();

            List<Model.Product> products = foodDbContext.Products.ToList().Where(p => cartItems.Any(c=>c.UserId.Equals(int.Parse(userId)) &&  c.ProductId.Equals(p.Id))).ToList();   
            return View("../Cart/Index", products);

        }





    }
}