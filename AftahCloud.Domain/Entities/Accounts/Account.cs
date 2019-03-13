using AftahCloud.Domain.Entities.APIKeys;
using AftahCloud.Domain.Entities.Subscriptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AftahCloud.Domain.Entities.Accounts
{
    public class Account: TrackedEntity
    {
        public string Name { get; set; }
        public Boolean IsActive { get; set; }
        [ForeignKey("ParentAccount")]
        public Guid? ParentAccountId { get; set; }
        public Account ParentAccount { get; set; }
        public string PrimaryContactEmail { get; set; }
        public string PrimaryContactName { get; set; }
        public string PrimaryContactPhone { get; set; }
        public DateTime? OnboardDate { get; set; }
        public DateTime? OffboardDate { get; set; }
        [Required]
        public string CustomerCode { get; set; }
        public List<APIKey> APIKeys { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }
        [Required]
        public bool? IsPartner { get; set; }
        public string TypeOfAccount { get; set; }
    }
}
