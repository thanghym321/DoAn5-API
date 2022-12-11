using DoAn5.DataContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoAn5.DataContext.Configurations
{
    public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.ToTable("Providers");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasColumnType("nvarchar(250)");
            builder.Property(x => x.Address).HasColumnType("nvarchar(1500)");
            builder.Property(x => x.Phone).HasColumnType("varchar(20)");
            builder.Property(x => x.Email).HasColumnType("varchar(50)");
        }
    }
}
