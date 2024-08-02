using Domain.Entities.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.PaymentMethod)
                .HasConversion<int>();
            builder.HasQueryFilter(x => !x.IsDeleted);
            builder.HasIndex(r => r.IsDeleted)
                     .HasFilter("IsDeleted = 0");
            builder.Property(c => c.IsDeleted).HasDefaultValue(false);
        }
    }
}
