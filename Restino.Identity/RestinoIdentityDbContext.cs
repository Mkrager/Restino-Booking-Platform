using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restino.Domain.Entities;
using Restino.Identity.Models;

namespace Restino.Identity
{
    public class RestinoIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public RestinoIdentityDbContext()
        {

        }

        public RestinoIdentityDbContext(DbContextOptions<RestinoIdentityDbContext> options) 
            : base(options)
        {

        }

        DbSet<UserTwoFactor> UserTwoFactors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserTwoFactor>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(u => u.CreatedBy);
        }
    }
}
