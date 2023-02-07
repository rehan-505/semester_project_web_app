using Microsoft.AspNetCore.Mvc;
using semester_project_web_app.Model;

namespace semester_project_web_app.Controllers
{
    public class DetailController : Controller
    {
        [Route("Index/{itemid:int}")]
        public IActionResult Index(String itemid)
        {
            FoodDbContext foodDbContext = new FoodDbContext();
            Product p = foodDbContext.Products.ToList().Where(p => p.Id.Equals(int.Parse(itemid))).FirstOrDefault();
            Console.WriteLine(p.Id);


            return View("~/Views/detail/Index.cshtml",p);
        }
    }
}
