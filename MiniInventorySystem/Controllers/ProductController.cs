﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using MiniInventorySystem.Interface;
using MiniInventorySystem.Models;
using Newtonsoft.Json;

namespace MiniInventorySystem.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _repo;
        //private readonly string _jsonFilePath;

        public ProductController(IProductRepository repo/*, IWebHostEnvironment env*/)
        {
            _repo = repo;
            //_jsonFilePath = Path.Combine(env.WebRootPath, "data", "products.json");
        }


        //private List<Product> ReadFromFile()
        //{
        //    if (!System.IO.File.Exists(_jsonFilePath))
        //        return new List<Product>();

        //    var json = System.IO.File.ReadAllText(_jsonFilePath);
        //    return JsonConvert.DeserializeObject<List<Product>>(json) ?? new List<Product>();
        //}


        //private void WriteToFile(List<Product> products)
        //{
        //    var json = JsonConvert.SerializeObject(products, Formatting.Indented);
        //    System.IO.File.WriteAllText(_jsonFilePath, json);
        //}


        //public IActionResult Index()
        //{
        //    var products = ReadFromFile();
        //    return View(products);
        //}

        public IActionResult Index()
        {
            //var products = _repo.GetAll();
            //return View(products);
            var products = new List<Product>
        {
            new Product { ProductId = 1, ProductName = "Laptop", UnitPrice = 1200,CategoryName="IT" },
            new Product { ProductId = 2, ProductName = "Smartphone", UnitPrice = 800,CategoryName="Food"  },
            new Product { ProductId = 3, ProductName = "Tablet", UnitPrice = 400,CategoryName="Sports"  }
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
