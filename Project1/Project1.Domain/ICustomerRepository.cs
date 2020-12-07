using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Domain
{
    public interface ICustomerRepository
    {
        void Create(string firstName, string lastName, List<Customer> customers);
        public IEnumerable<Customer> GetAll();
    }
}
