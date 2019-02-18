using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;
using AMExpenses.Web.Data;
using AMExpenses.Web.Data.Entities;
using AMExpenses.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AMExpenses.Web.Controllers
{
    [Route("api/[Controller]")]
    public class TransactionsController : Controller
    {
        private readonly AMExpensesContext _ctx;
        private readonly UserManager<StoreUser> _userManager;

        public TransactionsController(AMExpensesContext ctx, UserManager<StoreUser> userManager)
        {
            _ctx = ctx;
            _userManager = userManager;
        }
        // GET
        [HttpPost]
        public async Task<IActionResult> Post(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            //Get form data from client side
            var requestFormData = Request.Form;
            var databaseList = await _ctx.Transactions.Where(t => t.User == user).ToListAsync();
            var lstItems = ConvertToModel(databaseList);

            var listItems = ProcessCollection(lstItems, requestFormData);
            var recFiltered = GetTotalRecordsFiltered(requestFormData, lstItems, listItems);

            // Custom response to bind information in client side
            dynamic response = new
            {
                data = listItems,
                draw = requestFormData["draw"],
                recordsFiltered = recFiltered,
                recordsTotal = lstItems.Count
            };
            return Ok(response);
        }
        private static PropertyInfo GetProperty(string name)
        {
            var properties = typeof(LogTransactionViewModel).GetProperties();
            PropertyInfo prop = null;
            foreach (var item in properties)
            {
                if (item.Name.ToLower().Equals(name.ToLower()))
                {
                    prop = item;
                    break;
                }
            }

            return prop;
        }

        private List<LogTransactionViewModel> ProcessCollection(List<LogTransactionViewModel> lstElements,
            Microsoft.AspNetCore.Http.IFormCollection requestFormData)
        {
            var searchText = string.Empty;
            Microsoft.Extensions.Primitives.StringValues tempOrder = new[] {""};
            if (requestFormData.TryGetValue("search[value]", out tempOrder))
            {
                searchText = requestFormData["search[value]"].ToString();
            }

            tempOrder = new[] {""};
            var skip = Convert.ToInt32(requestFormData["start"].ToString());
            var pageSize = Convert.ToInt32(requestFormData["length"].ToString());

            if (requestFormData.TryGetValue("order[0][column]", out tempOrder))
            {
                var columnIndex = requestFormData["order[0][column]"].ToString();
                var sortDirection = requestFormData["order[0][dir]"].ToString();
                tempOrder = new[] {""};
                if (requestFormData.TryGetValue($"columns[{columnIndex}][data]", out tempOrder))
                {
                    var columnName = requestFormData[$"columns[{columnIndex}][data]"].ToString();

                    if (pageSize > 0)
                    {
                        var prop = GetProperty(columnName);
                        if (sortDirection == "asc")
                        {
                            return lstElements
                                .Where(x => x.Description.ToLower().Contains(searchText.ToLower())
                                            || x.Description.ToLower().Contains(searchText.ToLower()))
                                .Skip(skip)
                                .Take(pageSize)
                                .OrderBy(prop.GetValue).ToList();
                        }
                        else
                            return lstElements
                                .Where(
                                    x => x.Description.ToLower().Contains(searchText.ToLower())
                                         || x.Description.ToLower().Contains(searchText.ToLower()))
                                .Skip(skip)
                                .Take(pageSize)
                                .OrderByDescending(prop.GetValue).ToList();
                    }
                    else
                        return lstElements;
                }
            }

            return null;
        }
        private static int GetTotalRecordsFiltered(IFormCollection requestFormData, ICollection lstItems,
            ICollection listProcessedItems)
        {
            var recFiltered = 0;
            Microsoft.Extensions.Primitives.StringValues tempOrder = new[] {""};
            if (requestFormData.TryGetValue("search[value]", out tempOrder))
            {
                if (string.IsNullOrEmpty(requestFormData["search[value]"].ToString().Trim()))
                {
                    recFiltered = lstItems.Count;
                }
                else
                {
                    recFiltered = listProcessedItems.Count;
                }
            }

            return recFiltered;
        }

        private static List<LogTransactionViewModel> ConvertToModel(IEnumerable<Transaction> transactions)
        {
            var newTransactions = new List<LogTransactionViewModel>();
            foreach (var transaction in transactions)
            {
                var newTransaction = new LogTransactionViewModel(transaction);
                newTransactions.Add(newTransaction);
            }

            return newTransactions;
        }
    }
}
