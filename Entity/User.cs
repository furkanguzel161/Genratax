using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Genratax.Models;

namespace Genratax.Entity
{
    public class User
    {
        public int UserId {get;set;}
        public string? UserName {get;set;}
        public string? Email {get;set;}
        public string? Password {get;set;}
        public List<Post> Posts {get;set;} = new List<Post>();

    }
}