using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnglerNet.Models
{
    public class ApplicationUser: IdentityUser
    {
        public ApplicationUser() : base() { }

     public string User { get; set; }
     public string City { get; set; }
     public string State { get; set; }
     }
}
