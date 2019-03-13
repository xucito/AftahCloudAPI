using AftahCloud.Domain.Entities.Accounts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AftahCloud.Domain.Entities.Subscriptions
{
    public class Subscription: TrackedEntity
    {
        [ForeignKey("Account")]
        public Guid AccountId { get; set; }
        public Account Account { get; set; }

        public string SubscriptionCode { get; set; }
        public string SubscriptionName { get; set; }
    }
}
