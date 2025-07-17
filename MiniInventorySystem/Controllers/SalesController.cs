using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniInventorySystem.Interface;
using MiniInventorySystem.Models;

namespace MiniInventorySystem.Controllers
{
    public class SalesController : Controller
    {
        private readonly ISaleRepository _saleRepo;
        private readonly IProductRepository _productRepo;

        public SalesController(ISaleRepository saleRepo, IProductRepository productRepo)
        {
            _saleRepo = saleRepo;
            _productRepo = productRepo;
        }

        public IActionResult Index()
        {
            // Local product list (dropdown)
            var products = new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "Pen" },
        new SelectListItem { Value = "2", Text = "Notebook" },
        new SelectListItem { Value = "3", Text = "Mouse" }
    };

            // Local sales data with details
            var sales = new List<Sale>
    {
        new Sale
        {
            SaleId = 1,
            CustomerId = 101,
            CustomerName = "Abdullah",
            SaleDate = DateTime.Today.AddDays(-2),
            TotalAmount = 40,
            Details = new List<SaleDetail>
            {
                new SaleDetail { ProductId = 1, ProductName = "Pen", Quantity = 2, },
                new SaleDetail { ProductId = 2, ProductName = "Notebook", Quantity = 1, }
            }
        },
        new Sale
        {
            SaleId = 2,
            CustomerId = 102,
            CustomerName = "Rafi",
            SaleDate = DateTime.Today.AddDays(-1),
            TotalAmount = 100,
            Details = new List<SaleDetail>
            {
                new SaleDetail { ProductId = 3, ProductName = "Mouse", Quantity = 1 }
            }
        }
    };

            // ViewModel bind
            var viewModel = new SalesPageViewModel
            {
                Sale = new Sale(), // form er jonno blank
                Sales = sales,
                Products = products
            };

            return View(viewModel);
        }

        // List sales & form
        //public IActionResult Index()
        //{
        //    var sales = _saleRepo.GetAllSales();
        //    var products = _productRepo.GetAll()
        //        .Select(p => new SelectListItem
        //        {
        //            Value = p.ProductId.ToString(),
        //            Text = p.ProductName
        //        }).ToList();

        //    var viewModel = new SalesPageViewModel
        //    {
        //        Sale = new Sale(),
        //        Sales = sales,
        //        Products = products
        //    };

        //    return View(viewModel);
        //}

        // Show create form - optional if using Index for form
        public IActionResult Create()
        {
            var products = _productRepo.GetAll()
                .Select(p => new SelectListItem
                {
                    Value = p.ProductId.ToString(),
                    Text = p.ProductName
                }).ToList();

            var viewModel = new SalesPageViewModel
            {
                Sale = new Sale { SaleDate = System.DateTime.Today },
                Products = products,
                Sales = _saleRepo.GetAllSales()
            };

            return View("Index", viewModel);
        }

        // Handle POST for Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Sale sale)
        {
            if (!ModelState.IsValid)
            {
                var vm = new SalesPageViewModel
                {
                    Sale = sale,
                    Products = _productRepo.GetAll()
                        .Select(p => new SelectListItem
                        {
                            Value = p.ProductId.ToString(),
                            Text = p.ProductName
                        }).ToList(),
                    Sales = _saleRepo.GetAllSales()
                };
                //return View("Index", vm);
            }

            _saleRepo.InsertSale(sale);
            return RedirectToAction(nameof(Index));
        }

        // Show edit form
        public IActionResult Edit(int id)
        {
            var sale = _saleRepo.GetSaleById(id);
            if (sale == null)
                return NotFound();

            var products = _productRepo.GetAll()
                .Select(p => new SelectListItem
                {
                    Value = p.ProductId.ToString(),
                    Text = p.ProductName
                }).ToList();

            var viewModel = new SalesPageViewModel
            {
                Sale = sale,
                Products = products,
                Sales = _saleRepo.GetAllSales()
            };

            return View("Index", viewModel);
        }

        // Handle POST for Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Sale sale)
        {
            if (!ModelState.IsValid)
            {
                var vm = new SalesPageViewModel
                {
                    Sale = sale,
                    Products = _productRepo.GetAll()
                        .Select(p => new SelectListItem
                        {
                            Value = p.ProductId.ToString(),
                            Text = p.ProductName
                        }).ToList(),
                    Sales = _saleRepo.GetAllSales()
                };
                return View("Index", vm);
            }

            _saleRepo.UpdateSale(sale);
            return RedirectToAction(nameof(Index));
        }

        // Delete action
        public IActionResult Delete(int id)
        {
            _saleRepo.DeleteSale(id);
            return RedirectToAction(nameof(Index));
        }
    }


}

