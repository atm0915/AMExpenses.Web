using System.ComponentModel.DataAnnotations;
using AMExpenses.Web.Data.Entities;
namespace AMExpenses.Web.ViewModels
{
    public class TransactionViewModel
    {
        [Required]
        public TransactionType Type { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
        [Required]
        [MaxLength(7000)]
        public string Description { get; set; }
    }
}