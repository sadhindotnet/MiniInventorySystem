using MiniInventorySystem.Models;

namespace MiniInventorySystem.Interface
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(int id);
        void InsertCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int id);
       
    }
}
