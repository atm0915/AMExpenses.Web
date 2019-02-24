using System;
using AMExpenses.Web.Data.Entities;

namespace AMExpenses.Web.ViewModels
{
    public class LogTransactionViewModel
    {
        public LogTransactionViewModel(Transaction transaction)
        {
            Id = transaction.Id;
            Date = transaction.DateTime.ToString("yyyy-MM-dd");
            Amount = $"${transaction.Amount.ToString()}";
            Description = transaction.Description;
            switch (transaction.Type)
            {
                case TransactionType.Credit:
                    Type = "Credit";
                    break;
                case TransactionType.CreditOnAccount:
                    Type = "Credit On Account";
                    break;
                case TransactionType.DebitToCredit:
                    Type = "Debit To Credit";
                    break;
                case TransactionType.DebitToCreditOnAccount:
                    Type = "Debit To Credit On Account";
                    break;
            }
        }

        public long Id { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }
        public string Amount { get; set; }
        public string Description { get; set; }
    }
}