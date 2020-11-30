using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Data.Model
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public DateTime Time {get; set;}
        
        public CustomerEntity Customer { get; set; }
        public StoreEntity Store { get; set; }
    }
}
