using System;
using System.Threading.Tasks;
using AMExpenses.Web.Data;
using AMExpenses.Web.Data.Entities;
using AMExpenses.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AMExpenses.Web.Controllers
{
    [Authorize]
    public class AppController : Controller
    {
        private readonly IAMExpensesRepository _repository;
        private readonly UserManager<StoreUser> _userManager;

        public AppController(IAMExpensesRepository repository, UserManager<StoreUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }
        // GET
        public async Task<IActionResult> Balance()
        {
            var user = _userManager.FindByNameAsync(_userManager.GetUserName(User)).Result;
            var model = new BalanceViewModel(){Credit = user.Credit, CreditOnAccount = user.CreditOnAccount, Total = user.Credit + user.CreditOnAccount};
            return View(model);
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
                var transaction = new Transaction()
                {
                    Amount = model.Amount,
                    Description = model.Description,
                    Type = model.Type
                };
                if (transaction.DateTime == DateTime.MinValue)
                {
                    transaction.DateTime = DateTime.Now;
                }
                ViewBag.UserMessage = $"Type: {model.Type}; Amount: {model.Amount}; Description: {model.Description}";
            }

            return View();
        }
    }
}