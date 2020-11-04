using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlindLowVisionProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string City { get; set; }
        public string Country { get; set; }

        public string Suburb { get; set; }



    }
}
