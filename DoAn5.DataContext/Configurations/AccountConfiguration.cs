using DoAn5.DataContext.Entities;
using DoAn5.DataContext.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoAn5.DataContext.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");
            builder.HasKey(x => x.Id);
            builder.Property(x=> x.UserName).IsRequired().HasColumnType("varchar(100)");
            builder.Property(x => x.Password).IsRequired().HasColumnType("varchar(100)");
            builder.Property(x => x.Status).HasDefaultValue(Status.Active);

            builder.HasOne<User>().WithMany().HasForeignKey(fk => fk.User_Id);
        }
    }
}
