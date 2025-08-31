using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restino.Domain.Entities;

namespace Restino.Persistence.Configurations
{
    public class AccommodationConfiguration : IEntityTypeConfiguration<Accommodation>
    {

        public void Configure(EntityTypeBuilder<Accommodation> builder)
        {
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
