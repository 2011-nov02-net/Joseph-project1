using Project1.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Project1.Data
{
    public class StoreRepository : IStoreRepository
    {
        private readonly DbContextOptions<P1DbContext> _contextOptions;
        public StoreRepository(DbContextOptions<P1DbContext> contextOptions)
        {
            _contextOptions = contextOptions;
        }

        /// <summary>
        /// if customer has ordered from store return appCustomerid
        /// </summary>
        /// <param name="customerName"></param>
        /// <param name="store"></param>
        /// <returns>customerId</returns>
        public int GetStoreCustomer(string customerName, Store store)
        {
            //return of 0 indicates customer wasnt found
            using var context = new P1DbContext(_contextOptions);
            int customerId = 0;

            var dbOrders = context.Orders
               .Include(o => o.Customer)
               .ToList();
            foreach (var order in dbOrders)
            {
                //TODO: seperate into first and last name
                if (order.Store.Id == store.Id && order.Customer.FirstName+" "+ order.Customer.LastName == customerName)
                    customerId = order.Customer.Id;
            }
            return customerId;
        }
        /// <summary>
        /// populates initial db stores into the application
        /// </summary>
        /// <param name="allCustomers"></param>
        /// <returns>A list of Stores</returns>
        public List<Store> GetStores(List<Customer> allCustomers)
        {
            using var context = new P1DbContext(_contextOptions);
            List<Store> storeList = new List<Store>();

            var dbStores = context.Stores
                .Include(s => s.Inventories)
                    .ThenInclude(i => i.Product)
                .Include(s => s.Orders)
                    .ThenInclude(o => o.Customer)
                .ToList();
            var dbCustomers = context.Customers.ToList();
            //creates an appstore for each store in the db
            foreach (var dbStore in dbStores)
            {
                
                Store appStore = new Store(dbStore.Location, dbStore.Name, dbStore.Id);
                foreach (var inventory in dbStore.Inventories)
                {
                    appStore.AddItem(inventory.Product.Name, (int)inventory.Quantity, inventory.Product.Price);
                }
                foreach (var order in dbStore.Orders)
                {
                    //check if customer is already created
                    bool created = false;
                    foreach (var customer in allCustomers)
                    {
                        if (customer.Id == order.Customer.Id)
                        {
                            created = true;
                            appStore.AddCustomer(customer);
                            customer.AddToOrderHistory(GetOrder(order.Id, appStore, customer));
                        }
                    }
                    //if not, create new customer
                    if (!created)
                    {
                        int newId = order.CustomerId;
                        Customer newCustomer = new Customer(order.Customer.FirstName, order.Customer.LastName, newId);
                        appStore.AddCustomer(newCustomer);
                        allCustomers.Add(newCustomer);
                        newCustomer.AddToOrderHistory(GetOrder(order.Id, appStore, newCustomer));
                    }
                }
                //if a customer has no orders, put them in the customer list anyway
                foreach(var dbCustomer in dbCustomers)
                {
                    if (allCustomers.Exists(c => c.FirstName == dbCustomer.FirstName && c.LastName == dbCustomer.LastName))
                        continue;
                    else
                        allCustomers.Add(new Customer(dbCustomer.FirstName,dbCustomer.LastName,dbCustomer.Id));
                }
                storeList.Add(appStore);
            }
            //could return allCustomers
            return storeList;
        }
        /// <summary>
        /// creates an appOrder from the db
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="appStore"></param>
        /// <param name="appCustomer"></param>
        /// <returns></returns>
        public Order GetOrder(int orderId, Store appStore, Customer appCustomer)
        {
            using var context = new P1DbContext(_contextOptions);

            //create a product list for the order
            List<Product> selections = new List<Product>();
            var dbItems = context.OrderItems
                .Include(i => i.Product)
                .Where(i => i.OrderId == orderId);
            foreach (var item in dbItems)
            {
                if (selections.Count == 0)
                    selections.Add(new Product(item.Product.Name, (int)item.Quantity, item.Product.Price));

                for (int i = 0; i < selections.Count; ++i)
                {
                    if (item.Product.Name != selections.ElementAt(i).Name)
                        selections.Add(new Product(item.Product.Name, (int)item.Quantity, item.Product.Price));
                }
            }

            //create the order
            var dbOrder = context.Orders
                .First(x => x.Id == orderId);

            Order newOrder = new Order()
            {
                TargetStore = appStore,
                Orderer = appCustomer,
                Time = dbOrder.Time,
                Selections = selections,
                Id = dbOrder.Id
            };
            return newOrder;
        }

        /// <summary>
        /// Enter a new customer into db, and app
        /// </summary>
        /// <param name="customerName"></param>
        public void CreateCustomer(string firstName, string lastName, List<Customer> customers)
        {
            using var context = new P1DbContext(_contextOptions);
            int newId = context.Customers.OrderBy(x => x.Id).Last().Id + 1;

            //add to app
            Customer newCustomer = new Customer(firstName, lastName, newId);
            customers.Add(newCustomer);

            //add to db

            var dbCustomer = new CustomerEntity() { FirstName = firstName, LastName = lastName };
            context.Add(dbCustomer);
            context.SaveChanges();
        }
        /// <summary>
        /// Enter a new order into db
        /// </summary>
        /// <param name="appOrder"></param>
        public void CreateOrder(Order appOrder)
        {
            using var context = new P1DbContext(_contextOptions);
            //map library order to db
            var dbOrder = new OrderEntity()
            {
                Store = context.Stores.First(s => s.Id == appOrder.TargetStore.Id),
                Customer = context.Customers.First(c => c.Id == appOrder.Orderer.Id),
                Time = appOrder.Time
            };

            //map all items in the order to db
            foreach (Product selection in appOrder.Selections)
            {
                //create a new item, Store = null unless item is part of an inventory
                var dbOrderItems = new OrderItemsEntity()
                {
                    Product = context.Products.First(p => p.Name == selection.Name),
                    Quantity = selection.Quantity,
                    Order = dbOrder
                };
                context.Add(dbOrderItems);
            }
            context.Add(dbOrder);
            context.SaveChanges();
        }
        /// <summary>
        /// //execute order in db and update passed Store
        /// </summary>
        /// <param name="storeChoice"></param>
        /// <param name="appOrder"></param>
        /// <returns></returns>
        public Store FillOrderDb(Store storeChoice, Order appOrder)
        {
            using var context = new P1DbContext(_contextOptions);
            List<bool> successList = storeChoice.FillOrder(appOrder);
            int successListIndex = 0;

            //go ahead and grab everything
            var dbStore = context.Stores
                .Include(s => s.Inventories)
                    .ThenInclude(i => i.Product)
                .Include(s => s.Orders)
                    .ThenInclude(o => o.Customer)
                .First(s => s.Id == storeChoice.Id);

            //get store inventory
            var dbItems = context.Inventories.Where(s => s.Store.Id == storeChoice.Id);

            //find all items in inventory matching product.Name appOrderId then decrement quantity and update appStore
            foreach (var product in appOrder.Selections)
            {
                //find first item in dbItems that has the same name, and is an inventory
                var dbInvItem = dbItems.FirstOrDefault(i => i.Product.Name == product.Name);

                //update the db when fillOrder is successful
                if (successList.ElementAt(successListIndex)) 
                {
                    dbInvItem.Quantity -= product.Quantity;
                    context.Update(dbInvItem);
                }
                successListIndex++;
            }
            context.SaveChanges();
            return storeChoice;
        }
        public IEnumerable<Customer> GetAllCustomers()
        {
            using var context = new P1DbContext(_contextOptions);
            var dbCustomers = context.Customers.
                Include(c => c.Orders).ToList();

            var dbOrders = context.Orders.
                Include(c => c.OrderItems).ToList();

            var customers = dbCustomers.Select(c => new Customer(c.FirstName, c.LastName, c.Id));
            /*foreach (var order in dbOrders)
            {
                bool created = false;
                foreach (var customer in customers)
                {
                    if (customer.Id == order.CustomerId)
                    {
                        created = true;
                        Store appStore = order.Store;
                        appStore.AddCustomer(customer);
                        customer.AddToOrderHistory(GetOrder(order.Id, appStore, customer));
                    }
                }
                //if not, create new customer
                if (!created)
                {
                    int newId = dbCustomers.Max(c => c.Id) + 1;
                    Customer newCustomer = new Customer(order.Customer.FirstName, order.Customer.LastName, newId);
                    appStore.AddCustomer(newCustomer);
                    allCustomers.Add(newCustomer);
                    newCustomer.AddToOrderHistory(GetOrder(order.Id, appStore, newCustomer));
                }
            }*/
            return customers;
        }
        /// <summary>
        /// gets the price of a product
        /// </summary>
        /// <param name="productName"></param>
        /// <returns>double price</returns>
        public double GetPrice(string productName)
        {
            double price = 0;
            using var context = new P1DbContext(_contextOptions);

            var dbProduct = context.Products.First(x=>x.Name == productName);
            price = dbProduct.Price;

            return price;
        }
        /// <summary>
        /// used to retrieve the last order Id so a new Order can be created without reapeating the Id
        /// </summary>
        /// <returns>Id of last order</returns>
        public int GetLastOrderId()
        {
            int id = 0;
            using var context = new P1DbContext(_contextOptions);

            id = context.Orders.Max(x => x.Id);

            return id;
        }
        /// <summary>
        /// Helper method to find an order in the customer list
        /// </summary>
        /// <param name="customers"></param>
        /// <param name="orderId"></param>
        /// <returns>an app Order on success, null on failure</returns>
        public Order GetOrderById(List<Customer> customers, int orderId)
        {
            Order order = null;
            foreach (var customer in customers)
            {
                if(customer.GetOrder(orderId) != null)
                    order = customer.GetOrder(orderId);
            }
            return order;
        }
    }
}
