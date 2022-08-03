using BusinessLayer.Contract;
using CommonLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Multitierapp.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomer CustomerService;
        public CustomerController(ICustomer customer)
        {
            CustomerService = customer;
        }
        public IActionResult Index()
        {
            var Customers = CustomerService.GetCustomers();
            return View(Customers);
        }
        public IActionResult Create()
        {
            return View();
        }
    [HttpPost]
    public IActionResult Create(Customer obj)
        {
            if(ModelState.IsValid)
            {
                var result = CustomerService.CreateCustomer(obj);
                if(result != null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error in customer create !");
                    return View(obj);
                }
            }
            return View(obj);
        }

        public IActionResult Delete(int id)
        {
            var result = CustomerService.DeleteCustomer(id);
            if(result)
            { return RedirectToAction("Index");  }
            else
            {
                ViewBag.message = "Error in delete!";
                return RedirectToAction("Index");
            }
        }
        
        public IActionResult Edit(int id)
        {
            var r = CustomerService.GetCustomerById(id);
                return View(r);
        }

        [HttpPost]
        public IActionResult Edit(Customer cust)
        {

            var r = CustomerService.UpdateCustomer(cust);
            if (r != null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error in customer create !");
                return View(r);
            }
        }

    }
}
