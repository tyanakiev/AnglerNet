using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnglerNet.Models
{
    public class ApplicationRole: IdentityRole
    {
        public ApplicationRole(): base()
        {
                
        }

        public ApplicationRole(string roleName): base(roleName)
        {

        }

        public ApplicationRole(string roleName, string description, DateTime creationDate): base(roleName)
        {
            this.description = description;
            this.creationDate = creationDate;
        }

        public string description { get; set; }
        public DateTime creationDate { get; set; }
    }
}
