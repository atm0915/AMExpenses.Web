using System.Collections.Generic;
using AMExpenses.Web.Data.Entities;

namespace AMExpenses.Web.Data
{
    public interface IAMExpensesRepository
    {
        IEnumerable<Transaction> GetAllTransactions();

        void AddEntity(object model);

        bool SaveAll();

    }
}