
using System.ComponentModel.DataAnnotations;

namespace Genratax.Models
{
    public class SignUpViewModel
    {      

        [Required]
        [Display(Name="UserName")]
        public string? UserName {get;set;}

        [Required]
        [EmailAddress]
        [Display(Name="Eposta")]
        public string? Email{get;set;}

        [Required]
        [StringLength(10,ErrorMessage = "{0} alani en az {2} karakter olmalidir",MinimumLength =6)]
        [DataType(DataType.Password)]
        [Display(Name = "Parola")]
        public string? Password{get;set;}

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage = "Parola Eslesmedi!")]
        [Display(Name = "Parola Tekrarla")]
        public string? ConfirmPassword{get;set;}
    }
}