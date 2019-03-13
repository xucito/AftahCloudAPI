using AftahCloud.Domain.Entities.Accounts;
using AftahCloud.Domain.Entities.APIKeys;
using AftahCloud.Domain.Entities.Subscriptions;
using AftahCloud.Domain.Entities.ApplicationUsers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AftahCloudAPI.Persistence.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public DbSet<Account> Account { get; set; }
        public DbSet<Subscription> Subscription { get; set; }
        public DbSet<APIKey> APIKey { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("localhost;port=5432;Database=AftahDB");
        }*/

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
