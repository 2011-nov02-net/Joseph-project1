using System;
using System.Collections.Generic;
using System.Linq;

namespace Project1.Domain
{
    /// <summary>
    /// Provides data fields representing a customer
    /// </summary>
    public class Customer
    {
        public string FirstName { get; }
        public string LastName { get; }
        public int Id { get; set; }
        public List<Order> OrderHistory {get;set;}

        //TODO: add default (preferred) store
        public Customer(string firstName, string lastName, int Id)
        {
            this.OrderHistory = new List<Order>();
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Id = Id;
        }

        /// <summary>
        /// adds an order to the customer's history
        /// </summary>
        public void AddToOrderHistory(Order order)
        {
            //may want to only track OrderIds
            if (order != null)
                this.OrderHistory.Add(order);
        }
        /// <summary>
        /// Finds an order in the customer's order history
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Order GetOrder(int orderId)
        {
            //may want to only track OrderIds
            if (orderId != 0 && this.OrderHistory.Exists(x=> x.Id == orderId))
            {
                Order order = this.OrderHistory.First(x => x.Id == orderId);
                return order;
            }
            else
            {
                return null;
            }
        }
    }
}
