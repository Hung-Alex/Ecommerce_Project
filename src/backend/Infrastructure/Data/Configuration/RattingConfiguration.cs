using Domain.Entities.Rattings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class RattingConfiguration : IEntityTypeConfiguration<Ratting>
    {
        public void Configure(EntityTypeBuilder<Ratting> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
