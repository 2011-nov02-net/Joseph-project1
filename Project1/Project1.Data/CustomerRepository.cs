using Microsoft.EntityFrameworkCore;
using Project1.Data.Model;
using Project1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1.Data
{

    public class CustomerRepository : ICustomerRepository
    {
        private readonly DbContextOptions<P1DbContext> _contextOptions;
        public CustomerRepository(DbContextOptions<P1DbContext> contextOptions)
        {
            _contextOptions = contextOptions;
        }
        /// <summary>
        /// Enter a new customer into db, and app
        /// </summary>
        /// <param name="customerName"></param>
        public void Create(string firstName, string lastName, List<Customer> customers)
        {
            using var context = new P1DbContext(_contextOptions);
            int newId = context.Customers.OrderBy(x => x.Id).Last().Id + 1;

            //add to app
            Customer newCustomer = new Customer(firstName,lastName, newId);
            customers.Add(newCustomer);

            //add to db
            
            var dbCustomer = new CustomerEntity() { FirstName = firstName, LastName = lastName };
            context.Add(dbCustomer);
            context.SaveChanges();
        }
        public IEnumerable<Customer> GetAll()
        {
            using var context = new P1DbContext(_contextOptions);
            var dbCustomers = context.Customers.ToList();

            var result = dbCustomers.Select(c => new Customer(c.FirstName, c.LastName, c.Id));
            return result;
        }
    }
}
