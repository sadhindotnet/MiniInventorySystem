using Microsoft.AspNetCore.Mvc;
using MiniInventorySystem.Interface;
using MiniInventorySystem.Models;

namespace MiniInventorySystem.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepo;

        public CustomerController(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public IActionResult Index()
        {
            //var customers = _customerRepo.GetAllCustomers();
            var customers = new List<Customer>
    {
        new Customer { CustomerId = 1, CustomerName = "Abdullah", Address = "Dhaka", ContactNo = "01700000001" },
        new Customer { CustomerId = 2, CustomerName = "Rafi", Address = "Chattogram", ContactNo = "01700000002" },
        new Customer { CustomerId = 3, CustomerName = "Sumi", Address = "Khulna", ContactNo = "01700000003" },
        new Customer { CustomerId = 4, CustomerName = "Jamal", Address = "Rajshahi", ContactNo = "01700000004" }
    };
            return View(customers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerRepo.InsertCustomer(customer);
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var customer = _customerRepo.GetCustomerById(id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerRepo.UpdateCustomer(customer);
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        public IActionResult Delete(int id)
        {
            _customerRepo.DeleteCustomer(id);
            return RedirectToAction("Index");
        }
    }
}
