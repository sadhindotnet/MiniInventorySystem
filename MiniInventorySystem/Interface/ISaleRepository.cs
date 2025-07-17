using MiniInventorySystem.Models;

namespace MiniInventorySystem.Interface
{
    public interface ISaleRepository
    {
        IEnumerable<Sale> GetAllSales();
        Sale GetSaleById(int saleId);
        void InsertSale(Sale sale);
        void UpdateSale(Sale sale);
        void DeleteSale(int saleId);
    }
}
