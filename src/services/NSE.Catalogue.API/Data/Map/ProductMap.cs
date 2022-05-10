﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NSE.Catalogue.API.Models;

namespace NSE.Catalogue.API.Data.Map
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(x => x.Description)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(x => x.Image)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.ToTable("Products");
        }
    }
}
