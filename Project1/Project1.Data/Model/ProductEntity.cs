using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Data.Model
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public double Price{ get; set; }
        public string Name { get; set; }

        public List<OrderItemsEntity> Stocks { get; set; }
    }
}
