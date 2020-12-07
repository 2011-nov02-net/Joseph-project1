using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Data.Model
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public DateTime Time {get; set;}

        //foriegn keys and navigation properties
        public int StoreId { get; set; }
        public int CustomerId { get; set; }
        public CustomerEntity Customer { get; set; }
        public StoreEntity Store { get; set; }
        public List<OrderItemsEntity> OrderItems { get; set; }
    }
}
