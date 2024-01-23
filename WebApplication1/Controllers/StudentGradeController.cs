using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class StudentGradeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }
    }
}
