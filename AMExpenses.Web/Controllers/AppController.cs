using AMExpenses.Web.ViewModels;
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
        [HttpGet("New")]
        public IActionResult NewTransaction()
        {
            return View();
        }

        [HttpPost("New")]
        public IActionResult NewTransaction(TransactionViewModel model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.UserMessage = $"Type: {model.Type}; Amount: {model.Amount}; Description: {model.Description}";
            }

            return View();
        }
    }
}