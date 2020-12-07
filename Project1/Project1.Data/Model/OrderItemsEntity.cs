using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Data.Model
{
    public class OrderItemsEntity
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        //foriegn keys and navigation properties
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public OrderEntity Order { get; set; }
        public ProductEntity Product { get; set; }
    }
}
