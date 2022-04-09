using MessageRoomSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageRoomSolution.Data.Configuration
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("AppUsers");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Gender).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Dob).IsRequired();
            builder.Property(x => x.Phone).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(200).IsUnicode(false);
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.IsAdmin).IsRequired().HasDefaultValue(false);
            builder.Property(x => x.IsLocked).IsRequired().HasDefaultValue(false);
            builder.Property(x => x.IsDeleted).IsRequired().HasDefaultValue(false);
            builder.Property(x => x.LastAccess).IsRequired().HasDefaultValue(DateTime.Now);
            builder.Property(x => x.CreatedAt).IsRequired().HasDefaultValue(DateTime.Now);
            builder.Property(x => x.UpdatedAt).IsRequired().HasDefaultValue(DateTime.Now);
        }
    }
}