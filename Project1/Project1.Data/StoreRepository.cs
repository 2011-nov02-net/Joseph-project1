using Project1.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Project1.Data
{
    public class StoreRepository
    {
        private readonly P1DbContext _context;
        public StoreRepository(P1DbContext context)
        {
            _context = context;
        }

        public Order GetOrder(int orderId, Store appStore, Customer appCustomer)
        {
            //using var context = new P1DbContext(_dbContextOptions);
            var entities = _context.Orders.ToList();

            //create a product list for the order
            List<Product> selections = new List<Product>();
            var dbItems = _context.OrderItems
                .Include(i => i.Product)
                .Where(i => i.Id == orderId);
            foreach (var item in dbItems)
            {
                if (selections.Count == 0)
                    selections.Add(new Product(item.Product.Name, (int)item.Quantity));

                for (int i = 0; i < selections.Count; ++i)
                {
                    if (item.Product.Name != selections.ElementAt(i).Name)
                        selections.Add(new Product(item.Product.Name, (int)item.Quantity));
                }
            }

            //create the order
            var dbOrder = _context.Orders
                .First(x => x.Id == orderId);

            Order newOrder = new Order()
            {
                TargetStore = appStore,
                Orderer = appCustomer,
                Time = dbOrder.Time,
                Selections = selections
            };
            return newOrder;
        }
    }
}
