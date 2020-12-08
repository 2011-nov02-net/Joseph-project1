using Project1.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Domain
{
    public interface IStoreRepository
    {
        //void CreateCustomer(string customerName, List<Customer> customers);
        void CreateOrder(Order appOrder);
        Store FillOrderDb(Store storeChoice, Order appOrder);
        Order GetOrder(int orderId, Store appStore, Customer appCustomer);
        int GetStoreCustomer(string customerName, Store store);
        List<Store> GetStores(List<Customer> allCustomers);
        public IEnumerable<Customer> GetAllCustomers();
        public void CreateCustomer(string firstName, string lastName, List<Customer> customers);
        public Order GetOrderById(List<Customer> customers, int orderId);

        public double GetPrice(string productName);
        public int GetLastOrderId();
    }
}
