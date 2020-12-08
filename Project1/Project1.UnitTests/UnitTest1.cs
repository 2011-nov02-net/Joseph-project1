using Project1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Project1.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void StoreConstructorTest()
        {
            Product product2 = new Product("test product2", 5,10);
            Product product1 = new Product("test product1", 2,12);
            List<Product> testInventory = new List<Product>() { product1, product2 };
            Store testStore = new Store(testInventory, "123 Fake Street", "Test Store",300);

            Assert.True(testStore.Inventory == testInventory);
            Assert.True(testStore.Name == "Test Store");
            Assert.True(testStore.Address == "123 Fake Street");
        }

        [Theory]
        [InlineData("bananas", 10)]
        [InlineData("bananananas", 0)]//handled exception
        public void StoreAddItemTest(string itemName, int quantity)
        {
            List<Product> testInventory = new List<Product>();
            Store testStore = new Store(testInventory, "123 Fake Street", "Test Store",300);

            testStore.AddItem(itemName, quantity,10);

            Assert.True(itemName == testStore.Inventory.First().Name);
        }

        [Fact]
        public void StoreAddCustomerTest()
        {
            Customer customer = new Customer("test",  "Customer",3);
            Store testStore = new Store("123 Fake Street", "Test Store",300);

            testStore.AddCustomer(customer);

            Assert.True(testStore.Customers.First() == customer);
        }

        [Fact]
        public void StoreRemoveCustomerTest()
        {
            Customer customer = new Customer("test", "Customer", 3);
            Store testStore = new Store("123 Fake Street", "Test Store", 300)
            {
                Customers = { customer }
            };

            testStore.RemoveCustomer(customer);

            Assert.True(testStore.Customers.FirstOrDefault() == null);
        }

        [Fact]
        public void StoreRemoveItemTest()
        {
            string testItemName = "testItem";
            Product product = new Product(testItemName, 2,10);
            Store testStore = new Store("123 Fake Street", "Test Store",300)
            {
                Inventory = { product }
            };

            testStore.RemoveItem(testItemName);

            Assert.True(testStore.Inventory.FirstOrDefault() == null);
        }

        [Fact]
        public void StoreFillOrderTest()
        {
            Product product1 = new Product("test product1", 2,5);
            Customer testOrderer = new Customer("test" ,"orderer",5);
            List<Product> testSelections = new List<Product>() { product1 };
            Store testStore = new Store("123 Fake Street", "Test Store",300)
            {
                Inventory = { new Product("test product1", 10,12.99) }
            };
            Order testOrder = new Order(testStore, testOrderer, testSelections, 5);

            List<bool> ordersFilled = testStore.FillOrder(testOrder);

            Assert.True(ordersFilled.First() == true);
            Assert.True(testStore.Inventory.First().Quantity < 10);
        }

        [Fact]
        public void StoreFillOrderTest2()//will fail
        {
            Product product2 = new Product("test product2", 5,5);
            Customer testOrderer = new Customer("test", "orderer", 5);
            List<Product> testSelections = new List<Product>() { product2 };
            Store testStore = new Store("123 Fake Street", "Test Store",300)
            {
                Inventory = { new Product("test product1", 10,12.99) }
            };
            Order testOrder = new Order(testStore, testOrderer, testSelections,5);

            List<bool> ordersFilled = testStore.FillOrder(testOrder);

            Assert.True(ordersFilled.First() == false);
            Assert.True(testStore.Inventory.First().Quantity == 10);
        }

        [Fact]
        public void CustomerAddToOrderHistoryTest1()
        {
            Product product2 = new Product("test product2", 5,5.99);
            Product product1 = new Product("test product1", 2,6.99);
            Customer testOrderer = new Customer("test", "orderer", 5); ;
            List<Product> testSelections = new List<Product>() { product1, product2 };
            Store testStore = new Store("123 Fake Street", "Test Store",300)
            {
                Inventory = { new Product("test product1", 10,7.99) }
            }; ;
            Order testOrder = new Order(testStore, testOrderer, testSelections, 5);

            testOrderer.AddToOrderHistory(testOrder);

            Assert.True(testOrderer.OrderHistory.First() == testOrder);
        }
    }
}
