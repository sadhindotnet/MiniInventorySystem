using MiniInventorySystem.Models;

namespace MiniInventorySystem.Interface
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        void Add(Product product);
        void Update(Product product);
        void Delete(int productId);
    }
}
