using Microsoft.AspNetCore.Mvc.Rendering;

namespace MiniInventorySystem.Models
{
    public class Sale
    {
        public int SaleId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime SaleDate { get; set; }
        public int TotalAmount { get; set; }
        public List<SaleDetail> Details { get; set; } = new List<SaleDetail>();
    }
    public class SalesPageViewModel
    {
      
            public Sale Sale { get; set; }
            public IEnumerable<Sale> Sales { get; set; }
            public List<SelectListItem> Products { get; set; } = new List<SelectListItem>();
        
    }
}
