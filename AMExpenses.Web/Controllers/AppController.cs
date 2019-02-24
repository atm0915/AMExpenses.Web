using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using AMExpenses.Web.Data;
using AMExpenses.Web.Data.Entities;
using AMExpenses.Web.Services;
using AMExpenses.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AMExpenses.Web.Controllers
{
    [Authorize]
    public class AppController : Controller
    {
        private readonly IBalanceUpdater _balanceUpdater;
        private readonly AMExpensesContext _ctx;
        private readonly UserManager<StoreUser> _userManager;

        public AppController(UserManager<StoreUser> userManager, IBalanceUpdater balanceUpdater, AMExpensesContext ctx)
        {
            _userManager = userManager;
            _balanceUpdater = balanceUpdater;
            _ctx = ctx;
        }

        // GET
        public IActionResult Balance()
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
        public async Task<IActionResult> NewTransaction(TransactionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByNameAsync(_userManager.GetUserName(User)).Result;
                var transaction = new Transaction()
                {
                    Amount = model.Amount,
                    Description = model.Description,
                    Type = model.Type,
                    DateTime = model.DateTime,
                    User = user
                };
                if (transaction.DateTime == DateTime.MinValue)
                {
                    transaction.DateTime = DateTime.Now;
                }
                await _ctx.AddAsync(transaction);
                await _balanceUpdater.UpdateBalanceAsync(transaction, user);
                await _ctx.SaveChangesAsync();
                ViewBag.UserMessage = "Transaction Saved!";
            }

            return View();
        }

        [HttpGet("Log")]
        public async Task<IActionResult> Log()
        {
            var user = await _userManager.FindByNameAsync(_userManager.GetUserName(User));
            var databaseList = await _ctx.Transactions.Where(t => t.User == user).ToListAsync();
            var transactions = new List<LogTransactionViewModel>();
            foreach (var transaction in databaseList)
            {
                var newTransaction = new LogTransactionViewModel(transaction);
                transactions.Add(newTransaction);
            }

            return View(transactions);
        }
    }
}