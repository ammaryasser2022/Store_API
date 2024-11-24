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
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(O=>O.SubTotal).HasColumnType("decimal(18,2)");
            builder.Property(O=>O.Status).HasConversion(OStatus=>OStatus.ToString() , OStatus=> (OrderStatus)Enum.Parse(typeof(OrderStatus) , OStatus) );
            builder.HasOne(O => O.DeliveryMethod).WithMany().HasForeignKey(O=>O.DeliveryMethodId); // Relation between Order and Deleveiry table 

            builder.OwnsOne(O => O.ShippingAddress, SA => SA.WithOwner()); // Table Order has Part of Address Table 

        }
    }
}
