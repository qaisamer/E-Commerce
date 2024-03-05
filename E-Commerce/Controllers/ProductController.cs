using E_Commerce.Data;
using E_Commerce.Data.Repository;
using E_Commerce.Models;
using E_Commerce.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.ContentModel;

namespace E_Commerce.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IRepo<Product> repo;
        private readonly ApplicationDbContext context;

        public ProductController(IRepo<Product> repo, ApplicationDbContext context)
        {
            this.context = context;
            this.repo = repo;
        }
        public IActionResult Index()
        {
            return View(repo.show());
        }
        [HttpGet]
        public IActionResult Creat()
        {
            CreatProductViewModel viewModel = new() {
                catigories = context.Categories.Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem {Value=c.Id.ToString() ,Text=c.Name  }).ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Creat(CreatProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                var photoname = $"{model.Name}{Path.GetExtension(model.photo.FileName)}";
                var path = Path.Combine("C:\\Users\\qais\\Desktop\\sql\\E-Commerce\\E-Commerce\\wwwroot\\assets\\pictures\\", photoname);
                using var stream = System.IO.File.Create(path);
                model.photo.CopyTo(stream);
                stream.Dispose();
                Product p = new()
                {
                    CategoryId = model.CategoryId,
                    Name = model.Name,
                    Price = model.Price,
                    Description = model.Description,
                    photo = photoname,
                };
                repo.add_item(p);
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpGet]
        public IActionResult Edit(int Id) 
        {
            var product = repo.Find_by_id(Id);
            if (product == null)
            { return BadRequest(); }
            else {
                CreatProductViewModel p = new() { ProductId = product.Id,
                    Name=product.Name,
                    Price=product.Price,
                    Description=product.Description,
                };
                return View(p);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CreatProductViewModel model)
        {

            var Product = context.Products.FirstOrDefault(c => c.Id == model.ProductId);
            if (!ModelState.IsValid && Product == null)
            {
                return BadRequest(model);
            }
            var oldphoto = Product.photo;
            var hasnewphoto = model.photo != null;
            if (hasnewphoto)
            {
                var photoname = $"{model.Name}{Path.GetExtension(model.photo.FileName)}";
                var path = Path.Combine("C:\\Users\\qais\\Desktop\\sql\\E-Commerce\\E-Commerce\\wwwroot\\assets\\pictures\\", photoname);
                using var stream = System.IO.File.Create(path);
                model.photo.CopyTo(stream);
                stream.Dispose();
                Product.photo = photoname;
                Product.Name = model.Name;
                Product.Price = model.Price;
                Product.Description = model.Description;
                var deletephoto = Path.Combine("C:\\Users\\qais\\Desktop\\sql\\E-Commerce\\E-Commerce\\wwwroot\\assets\\pictures\\", oldphoto);
                System.IO.File.Delete(deletephoto);
                repo.update_item(Product);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Product.Name = model.Name;
                Product.Price = model.Price;
                Product.Description = model.Description;
                repo.update_item(Product);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var item = repo.Find_by_id(Id);
            if (item == null)
            {
                return NotFound();
            }
            repo.remove_item(item);
            return RedirectToAction(nameof(Index));
        }
    
    }
}
