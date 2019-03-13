using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AftahCloud.Domain.Entities.Accounts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Identity;

namespace AftahCloud.Domain.Entities.ApplicationUsers
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [ForeignKey("OwningAccount")]
        public Guid? OwningAccountId { get; set; }
        public Account OwningAccount { get; set; }

        public bool IsPlatformUser { get; set; }
    }
}
