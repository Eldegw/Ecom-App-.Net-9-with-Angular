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
    public class DeliveryMethodConfigration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(m => m.Price).HasColumnType("decimal(18,2)");
            builder.HasData(new DeliveryMethod
            {
                Id = 1,
                DeliveryTime = "Only one week",
                Description = "The fast delivery in the world ",
                Name = "DHL",
                Price = 15
            },
             new DeliveryMethod
            {
                 Id = 2,
                 DeliveryTime = "Only take two week",
                 Description = "Make your product save",
                 Name = "XXD",
                 Price = 12




            });

        }
    }
}
