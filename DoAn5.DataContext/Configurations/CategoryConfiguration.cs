using DoAn5.DataContext.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using DoAn5.DataContext.Enums;

namespace DoAn5.DataContext.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(x => x.Id);
            builder.Property(x=> x.Name).HasColumnType("nvarchar(250)");
            builder.Property(x => x.Image).HasColumnType("varchar(500)");
        }
    }
}
