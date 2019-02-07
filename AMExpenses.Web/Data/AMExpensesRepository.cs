using System.Collections.Generic;
using System.Linq;
using AMExpenses.Web.Data.Entities;
using Microsoft.Extensions.Logging;

namespace AMExpenses.Web.Data
{
    public class AMExpensesRepository : IAMExpensesRepository
    {
        private readonly AMExpensesContext _ctx;
        private readonly ILogger<AMExpensesRepository> _logger;

        public AMExpensesRepository(AMExpensesContext ctx, ILogger<AMExpensesRepository> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }
        public IEnumerable<Transaction> GetAllTransactions()
        {
            return _ctx.Transactions
                .OrderBy(t => t.DateTime)
                .ToList();
        }

        public void AddEntity(object model)
        {
            _ctx.Add(model);
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}