using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project1.Domain;
using Project1.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.WebApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly IStoreRepository _storeRepo;
        private readonly ICustomerRepository _customerRepo;
        public CustomerController(IStoreRepository storeRepository, ILogger<CustomerController> logger,
            ICustomerRepository customerRepository)
        {
            _logger = logger;
            _storeRepo = storeRepository;
            _customerRepo = customerRepository;
        }
        // GET: CustomerController
        public IActionResult Index()
        {
            var custList = new List<Customer>();
            var storeList = _storeRepo.GetStores(custList);
            return View(custList);
        }

        // GET: CustomerController/Details/5
        public IActionResult Details(int id)
        {
            var custList = new List<Customer>();
            var storeList = _storeRepo.GetStores(custList);
            var customer = custList.First(x => x.Id == id);
            return View(customer);
        }

        // GET: CustomerController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CustomerViewModel viewModel)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View(viewModel);
                }
                var custList = new List<Customer>();
                var storeList = _storeRepo.GetStores(custList);
                _storeRepo.CreateCustomer(viewModel.FirstName, viewModel.LastName, custList);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "There was some problem");
                return View(viewModel);
            }
        }

        // GET: CustomerController/ChooseStore/5
        public IActionResult ChooseStore(int id)
        {
            var custList = new List<Customer>();
            var storeList = _storeRepo.GetStores(custList);
            TempData["customerId"] = id;
            return View(storeList);
            /*var custList = new List<Customer>();
            var storeList = _storeRepo.GetStores(custList);
            List<string> storeNames = storeList.Select(x => x.Name).ToList();
            var customer = custList.First(x => x.Id == id);
            TempData["customer"] = customer;


            var viewModel = new ChooseStoreViewModel {StoreNames = storeNames };

            return View(viewModel);*/
        }

        // POST: CustomerController/ChooseStore/5
/*        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChooseStore(ChooseStoreViewModel viewModel, int id)
        {
            try
            {
                var custList = new List<Customer>();
                var storeList = _storeRepo.GetStores(custList);
                List<string> storeNames = storeList.Select(x => x.Name).ToList();

                viewModel = new ChooseStoreViewModel { StoreNames = storeNames };
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }
                return RedirectToAction(nameof(CreateOrder));
            }
            catch
            {
                return View();
            }
        }*/
        // GET: CustomerController/CreateOrder/5
        public IActionResult CreateOrder(int id)
        {
            var custList = new List<Customer>();
            var storeList = _storeRepo.GetStores(custList);
            //get the Store and the Customer for this order
            int customerId = (int)TempData["customerId"];
            var customer = custList.First(x => x.Id == customerId);
            var store = storeList.First(x => x.Id == id);

            var viewModel = new CreateOrderViewModel()
            {
                ProductNames = store.Inventory.Select(x => x.Name).ToList()
            };

            return View(viewModel);
        }

        // POST: CustomerController/CreateOrder/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrder(CreateOrderViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }
                var custList = new List<Customer>();
                var storeList = _storeRepo.GetStores(custList);
                Order newOrder = new Order();

                //fillorder here
                _storeRepo.CreateOrder(newOrder);
                //_storeRepo.FillOrderDb();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
