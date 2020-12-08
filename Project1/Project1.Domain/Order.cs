using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Domain
{
    /// <summary>
    /// Provides data fields representing an order in
    /// the form of a list of Products
    /// </summary>
    public class Order
    {
        public Store TargetStore { get; set;  }
        public Customer Orderer { get; set; }
        public int Id { get; set; }

        public DateTime Time { get; set; }
        public List<Product> Selections { get; set; } = new List<Product>();

        public Order(Store targetStore, Customer orderer, List<Product> selections, int id)
        {
            this.TargetStore = targetStore;
            this.Orderer = orderer;
            this.Selections = selections;
            this.Id = id;
            this.Time = DateTime.Now;
        }

        /// <summary>
        /// for stock orders, having no ordering customers
        /// </summary>
        public Order()
        {
            this.Time = DateTime.Now;
        }

        public decimal Total()
        {
            double total = 0;
            foreach(var selection in this.Selections)
            {
                total += selection.Price * selection.Quantity;
            }
            return (decimal)total;
        }
    }
}
