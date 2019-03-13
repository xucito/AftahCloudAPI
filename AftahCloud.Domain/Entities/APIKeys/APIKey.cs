using AftahCloud.Domain.Entities.Accounts;
using AftahCloud.Domain.Entities.ApplicationUsers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AftahCloud.Domain.Entities.APIKeys
{
    public class APIKey
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Key { get; set; }
        public string HashedSecret { get; set; }
        public DateTime CreatedOn { get; set; }
        public byte[] Salt { get; set; }

        public Guid CreatedById { get; set; }

        [ForeignKey("CreatedById")]
        public ApplicationUser CreatedBy { get; set; }

        [ForeignKey("Account")]
        public Guid AccountId { get; set; }
        public Account Account { get; set; }

        public bool IsDisabled { get; set; }
    }
}
