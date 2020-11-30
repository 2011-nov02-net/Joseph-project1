using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Data.Model
{
    public class CustomerEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<OrderEntity> Orders { get; set;  }
    }
}
