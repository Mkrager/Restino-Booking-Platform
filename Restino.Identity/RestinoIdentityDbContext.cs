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

        public DbSet<UserTwoFactorCode> UserTwoFactorCodes { get; set; }
        public DbSet<UserResetPasswordCode> UserResetPasswordCodes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserTwoFactorCode>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(u => u.CreatedBy);

            modelBuilder.Entity<UserResetPasswordCode>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(u => u.CreatedBy);
        }
    }
}
