using Microsoft.AspNetCore.Mvc;

namespace AMExpenses.Web.Controllers
{
    public class AppController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}