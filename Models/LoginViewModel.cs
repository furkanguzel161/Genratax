
using System.ComponentModel.DataAnnotations;

namespace Genratax.Models
{
    public class LoginViewModel
    {      
        [Required]
        [EmailAddress]
        [Display(Name="Eposta")]
        public string? Email{get;set;}

        [Required]
        [StringLength(10,ErrorMessage = "{0} alani en az {2} karakter olmalidir",MinimumLength =6)]
        [DataType(DataType.Password)]
        [Display(Name = "Parola")]
        public string? Password{get;set;}
    }
}