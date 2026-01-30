using Ecom.Core.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Data.Config
{
    public class OrderConfigration : IEntityTypeConfiguration<Orders>
    {
        public void Configure(EntityTypeBuilder<Orders> builder)
        {
            builder.OwnsOne(x => x.shippingAddress, n => { n.WithOwner(); });
            builder.HasMany(x=>x.orderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.Property(x => x.status).HasConversion(o => o.ToString(), o => (Status)Enum.Parse(typeof(Status), o));
            builder.Property(x => x.SubTotal).HasColumnType("decimal(18,2)");
        }
    }
}
