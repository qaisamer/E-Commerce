using E_Commerce.Data;
using E_Commerce.Models;

namespace E_Commerce.Services.CategoryService
{

    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext context;
        public CategoryService(ApplicationDbContext context)
        {
            this.context = context;
                
        }

        public List<Category> GetCategories()
        {
            return context.Categories.ToList();
        }
    }
}
