using System;
using Microsoft.AspNetCore.Identity;

namespace AMExpenses.Web.Data.Entities
{
    public class StoreUser : IdentityUser<long>
    {
        public decimal Credit { get; set; }
        public decimal CreditOnAccount { get; set; }
    }
}