using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListingAPI.Data
{
    public class ApiUser : IdentityUser
    {
        //if you need more custom fields than basic identity user class
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
