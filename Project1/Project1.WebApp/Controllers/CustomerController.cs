using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IStoreRepository _storeRepo;
        private readonly ICustomerRepository _customerRepo;
        public CustomerController(IStoreRepository storeRepository,
            ICustomerRepository customerRepository)
        {
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

        // GET: CustomerController/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
