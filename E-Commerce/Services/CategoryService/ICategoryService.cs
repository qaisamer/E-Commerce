using E_Commerce.Models;
using System.Web.Mvc;

namespace E_Commerce.Services.CategoryService
{
    public interface ICategoryService
    {
        List<Category> GetCategories();


    }
}
