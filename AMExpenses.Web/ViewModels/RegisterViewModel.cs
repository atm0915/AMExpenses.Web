using System.ComponentModel.DataAnnotations;

namespace AMExpenses.Web.ViewModels
{
    public class RegisterViewModel
    {
        [Required] public string Username { get; set; }
        [Required] public string Password { get; set; }
        [Required] [EmailAddress] public string Email { get; set; }
        [Required] public decimal Credit { get; set; }
        [Required] public decimal CreditOnAccount { get; set; }
    }
}