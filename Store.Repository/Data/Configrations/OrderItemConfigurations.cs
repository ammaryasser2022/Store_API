using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Core.Entities.OrderEntities;

namespace Store.Repository.Data.Configrations
{
    public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(OI => OI.Product, PIO => PIO.WithOwner());
            builder.Property(OI => OI.Price).HasColumnType("decimal(18,2)");
        }
    }
}
