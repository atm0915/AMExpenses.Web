using System;
using System.Threading.Tasks;
using AMExpenses.Web.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace AMExpenses.Web.Services
{
    public class BalanceUpdater : IBalanceUpdater
    {
        private readonly UserManager<StoreUser> _userManager;

        public BalanceUpdater(UserManager<StoreUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task UpdateBalanceAsync(Transaction transaction, StoreUser user)
        {
            switch (transaction.Type)
            {
                case TransactionType.Credit:
                {
                    user.Credit += transaction.Amount;
                    break;
                }
                case TransactionType.CreditOnAccount:
                {
                    user.CreditOnAccount += transaction.Amount;
                    break;
                }
                case TransactionType.DebitToCredit:
                {
                    user.Credit -= transaction.Amount;
                    break;
                }
                case TransactionType.DebitToCreditOnAccount:
                {
                    user.CreditOnAccount -= transaction.Amount;
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }

            await _userManager.UpdateAsync(user);
        }
    }
}