using CoffeeManagementSystem.Entities.Models;

namespace CoffeeManagementSystem.Repositories.Interfaces
{
    public interface ICategoreRepository
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category> GetCategoryById(int id);
        Task<Category> AddCategory(Category category);
        Task<Category> UpDateCategory(int id, Category category);
        Task DeleteCategory(int id);
    }
}
