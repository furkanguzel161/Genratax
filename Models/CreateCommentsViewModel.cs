
using System.ComponentModel.DataAnnotations;
using Genratax.Entity;

namespace Genratax.Models
{
    public class CreateCommentsViewModel
    {      
        public int PostId{get;set;}
        public int CommentId {get;set;}

        [Required]
        [Display(Name="Icerik")]
        public string? Text{get;set;}



    }
}