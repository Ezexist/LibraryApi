using Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.UserName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(128);
            builder.Property(x => x.PasswordHash)
                .IsRequired();
        }
    }
}
