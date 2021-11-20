using HotelListingAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListingAPI.Configurations.Entities
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
               new Hotel
               {
                   Id = 1,
                   Name = "Sandals Resort and Spa",
                   Address = "Negril",
                   CountryId = 1,
                   Rating = 4.5
               },
               new Hotel
               {
                   Id = 2,
                   Name = "The Green Resort",
                   Address = "Koh Chang..",
                   CountryId = 3,
                   Rating = 4
               },
               new Hotel
               {
                   Id = 3,
                   Name = "Airport Hotel",
                   Address = "Suvarnibhumi",
                   CountryId = 3,
                   Rating = 3.5
               },
               new Hotel
               {
                   Id = 4,
                   Name = "Premier Inn",
                   Address = "Manchester",
                   CountryId = 2,
                   Rating = 3
               }
             );
        }
    }
}
