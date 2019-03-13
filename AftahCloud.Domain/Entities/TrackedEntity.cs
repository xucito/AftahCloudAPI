using AftahCloud.Domain.Entities.ApplicationUsers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AftahCloud.Domain.Entities
{
    public class TrackedEntity: BaseEntity
    {
        [Timestamp]
        public byte[] Timestamp { get; set; }

        [ForeignKey("CreatedBy")]
        public Guid CreatedById { get; set; }

        public virtual ApplicationUser CreatedBy { get; set; }
    }
}
