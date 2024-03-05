using E_Commerce.Data;
using E_Commerce.Data.Repository;
using E_Commerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace E_Commerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext _context)
        {
            this._context = _context;


        }

        public IActionResult Index()
        {
            var categories =_context.Categories.ToList();
            return View(categories);
        }
        public IActionResult Products(int Id)
        {
            var products = _context.Products.Include(c => c.Category).Where(x=> x.Category.Id == Id);

            return View(products);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
