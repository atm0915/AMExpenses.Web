using System.Threading.Tasks;
using AMExpenses.Web.Data.Entities;

namespace AMExpenses.Web.Services
{
    public interface IBalanceUpdater
    {
        Task UpdateBalanceAsync(Transaction transaction, StoreUser user);
    }
}