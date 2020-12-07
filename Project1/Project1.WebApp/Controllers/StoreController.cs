using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.WebApp.Controllers
{
    public class StoreController : Controller
    {
        private readonly IStoreRepository _storeRepo;
        public StoreController(IStoreRepository storeRepository)
        {
            _storeRepo = storeRepository;
        }
        // GET: StoreController
        public ActionResult Index()
        {
            var custList = new List<Customer>();
            var storeList = _storeRepo.GetStores(custList);
            return View(storeList);
        }

        // GET: StoreController/Details/5
        public ActionResult Details(int id)
        {
            var custList = new List<Customer>();
            var storeList = _storeRepo.GetStores(custList);
            var store = storeList.First(x => x.Id == id);
            return View(store);
        }
        public ActionResult OrderDetails(int id)
        {
            var custList = new List<Customer>();
            var storeList = _storeRepo.GetStores(custList);
            var order = _storeRepo.GetOrderById(custList, id);
            //var customer = custList.Where(x=>x.OrderHistory.Contains(y=>y.orderId == id)).First();
            return View(order);
        }
        // GET: StoreController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StoreController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: StoreController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StoreController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: StoreController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StoreController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
