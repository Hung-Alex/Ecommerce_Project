using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Data.Configuration
{
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasQueryFilter(x => !x.IsDeleted);
            builder.HasIndex(r => r.Type).IsUnique()
                     .HasFilter("IsDeleted = 0");
            builder.HasIndex(r => r.IsDeleted)
                     .HasFilter("IsDeleted = 0");
            builder.Property(c => c.IsDeleted).HasDefaultValue(false);
            builder.HasMany(x => x.Orders)
                .WithOne(x => x.Status)
                .HasForeignKey(x => x.StatusId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.Payments)
                .WithOne(x => x.Status)
                .HasForeignKey(x => x.StatusId)
                .OnDelete(DeleteBehavior.Restrict);
            #region status data
                        builder.HasData(
                                           new Status
                                           {
                                               Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                                               Type = "Order",
                                               Display = "Pending",
                                               Code = "PENDING",
                                               CreatedAt = DateTimeOffset.Now,
                                               UpdatedAt = DateTimeOffset.Now,
                                               IsDeleted = false
                                           },
                                           new Status
                                           {
                                               Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                                               Type = "Order",
                                               Display = "Processing",
                                               Code = "PROCESSING",
                                               CreatedAt = DateTimeOffset.Now,
                                               UpdatedAt = DateTimeOffset.Now,
                                               IsDeleted = false
                                           },
                                           new Status
                                           {
                                               Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                                               Type = "Order",
                                               Display = "Completed",
                                               Code = "COMPLETED",
                                               CreatedAt = DateTimeOffset.Now,
                                               UpdatedAt = DateTimeOffset.Now,
                                               IsDeleted = false
                                           },
                                           new Status
                                           {
                                               Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                                               Type = "Order",
                                               Display = "Cancelled",
                                               Code = "CANCELLED",
                                               CreatedAt = DateTimeOffset.Now,
                                               UpdatedAt = DateTimeOffset.Now,
                                               IsDeleted = false
                                           },
                                            new Status
                                            {
                                                Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                                                Type = "Payment",
                                                Display = "Pending",
                                                Code = "PENDING",
                                                CreatedAt = DateTimeOffset.Now,
                                                UpdatedAt = DateTimeOffset.Now,
                                                IsDeleted = false
                                            },                              
                                              new Status
                                              {
                                                  Id = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                                                  Type = "Payment",
                                                  Display = "Completed",
                                                  Code = "COMPLETED",
                                                  CreatedAt = DateTimeOffset.Now,
                                                  UpdatedAt = DateTimeOffset.Now,
                                                  IsDeleted = false
                                              },
                                              new Status
                                              {
                                                  Id = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                                                  Type = "Order",
                                                  Display = "Failed",
                                                  Code = "FAILED",
                                                  CreatedAt = DateTimeOffset.Now,
                                                  UpdatedAt = DateTimeOffset.Now,
                                                  IsDeleted = false
                                              },
                                                new Status
                                                {
                                                    Id = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                                                    Type = "Payment",
                                                    Display = "Failed",
                                                    Code = "FAILED",
                                                    CreatedAt = DateTimeOffset.Now,
                                                    UpdatedAt = DateTimeOffset.Now,
                                                    IsDeleted = false
                                                });
            #endregion
        }
    }
}
