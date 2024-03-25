
using System.ComponentModel.DataAnnotations;
using Genratax.Entity;

namespace Genratax.Models
{
    public class CreatePostViewModel
    {      
        public int PostId{get;set;}
        [Required]
        [Display(Name="Baslik")]
        public string? Title{get;set;}

        [Required]
        [Display(Name="Icerik")]
        public string? Content{get;set;}

        [Required]
        [Display(Name="Icerik")]
        public string? Image{get;set;}

        public string? Url{get;set;}


    }
}