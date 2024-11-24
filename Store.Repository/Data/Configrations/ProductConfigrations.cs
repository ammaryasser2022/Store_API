using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Core.Entities;

namespace Store.Repository.Data.Configrations
{
    public class ProductConfigrations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(200).IsRequired();
            builder.Property(p=>p.PictureUrl).IsRequired();
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            //relationShip
            builder.HasOne(P => P.Brand).WithMany() // fadya 3shan mafesh prop 3nd brand
                   .HasForeignKey(p => p.BrandId)
                   .OnDelete(DeleteBehavior.SetNull);


            builder.HasOne(P => P.Type).WithMany() // fadya 3shan mafesh prop 3nd brand
                   .HasForeignKey(p => p.TypeId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.Property(p=>p.BrandId).IsRequired(false);
            builder.Property(p=>p.TypeId).IsRequired(false);


            //KOL DA ASKN BY CONVENTION M3MOOL BS MORAG3AAA

        }
    }
}
