using System.ComponentModel;

namespace AMExpenses.Web.Data.Entities
{
    public enum TransactionType
    {
        [Description("Credit")]
        Credit,
        [Description("Credit On Account")]
        CreditOnAccount,
        [Description("Debit To Credit")]
        DebitToCredit,
        [Description("Debit To Credit On Account")]
        DebitToCreditOnAccount
    }
}