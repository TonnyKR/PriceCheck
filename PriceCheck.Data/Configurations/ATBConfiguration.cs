using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PriceCheck.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCheck.Data.Configurations
{
    internal class ATBConfiguration : IEntityTypeConfiguration<ATB>
    {
        public void Configure(EntityTypeBuilder<ATB> builder)
        {
            builder.HasKey(x => x.Id).HasName("Id");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.ProductName).HasMaxLength(200);
            builder.Property(x => x.ProductPrice).HasMaxLength(10);
            builder.Property(x => x.ProductLink).IsRequired().HasMaxLength(200);

        }
    }
}
