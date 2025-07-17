using Microsoft.AspNetCore.Mvc;
using MiniInventorySystem.Interface;
using MiniInventorySystem.Models;

namespace MiniInventorySystem.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _repo;
        public ProductController(IProductRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            //var products = _repo.GetAll();
            //return View(products);
            var products = new List<Product>
    {
        new Product { ProductId = 1, ProductName = "Laptop", UnitPrice = 1200 },
        new Product { ProductId = 2, ProductName = "Smartphone", UnitPrice = 800 },
        new Product { ProductId = 3, ProductName = "Tablet", UnitPrice = 400 }
    };

            return View(products);

            
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(product);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _repo.Update(product);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int productId)
        {
            _repo.Delete(productId);
            return RedirectToAction(nameof(Index));
        }
    }
}
