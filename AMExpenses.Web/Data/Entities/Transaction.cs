using System;

namespace AMExpenses.Web.Data.Entities
{
    public class Transaction
    {
        public long Id { get; set; }
        public DateTime DateTime { get; set; }
        public StoreUser User { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}