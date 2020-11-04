using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlindLowVisionProject.Models
{
    public class SQLCustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext context;
        public SQLCustomerRepository(AppDbContext context)
        {
            this.context = context;
        }

        public Customer Add(Customer customer)
        {
            context.Customers.Add(customer);
            context.SaveChanges();
            return customer;
        }

        public Customer Delete(int id)
        {
            Customer customer = context.Customers.Find(id);
            if (customer != null)
            {
                context.Customers.Remove(customer);
                context.SaveChanges();
            }
            return customer;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return context.Customers;
        }

        public Customer GetCustomer(int Id)
        {
            return context.Customers.Find(Id);
        }

        public Customer Update(Customer customerChanges)
        {
           var customer = context.Customers.Attach(customerChanges);
            customer.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return customerChanges;
        }
    }
}
