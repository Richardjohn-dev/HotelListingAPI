using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListingAPI.Data
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions options) : base (options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }


        // what if they exist already?
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name = "Jamaica",
                    ShortName = "JM"
                },
                 new Country
                 {
                     Id = 2,
                     Name = "England",
                     ShortName = "Eng"
                 },
                  new Country
                  {
                      Id = 3,
                      Name = "Thailand",
                      ShortName = "Thai"
                  }
                );

            builder.Entity<Hotel>().HasData(
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
