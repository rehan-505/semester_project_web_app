using Microsoft.AspNetCore.Mvc;

namespace semester_project_web_app.Controllers
{
    public class Error : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
