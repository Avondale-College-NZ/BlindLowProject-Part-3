using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlindLowVisionProject.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                  new Customer
                  {
                      Id = 1,
                      Name = "Nikhil",
                      Department = Dept.IT,
                      Email = "nikhil@gmail.com"

                  },
                  new Customer
                  {
                      Id = 2,
                      Name = "DrRohanPatel",
                      Department = Dept.HR,
                      Email = "DrRohanP@gmail.com"

                  }

              );
        }
    }
}
