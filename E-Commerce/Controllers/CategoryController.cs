using E_Commerce.Data;
using E_Commerce.Data.Repository;
using E_Commerce.Models;
using E_Commerce.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace E_Commerce.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {
        private readonly IRepo<Category> repo;
        private readonly ApplicationDbContext context;
        public CategoryController(IRepo<Category> repo, ApplicationDbContext context)
        {
            this.repo = repo;
            this.context = context;
            
                
        }
        public IActionResult Index()
        {
            return View(repo.show());
        }
        [HttpGet]
        public IActionResult Creat()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Creat(CreatCategoryViewModel model )
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var photoname = $"{model.Name}{Path.GetExtension(model.photo.FileName)}";
            var path = Path.Combine("C:\\Users\\qais\\Desktop\\sql\\E-Commerce\\E-Commerce\\wwwroot\\assets\\CategoryPics\\", photoname);
            using var stream = System.IO.File.Create(path);
            model.photo.CopyTo(stream);
            stream.Dispose();
            Category c= new Category {
            
                Name = model.Name,
                photo=photoname
            };
            repo.add_item(c);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var category = repo.Find_by_id(Id);
            CreatCategoryViewModel c = new() {Id=category.Id, Name = category.Name };
            return View(c);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Edit(CreatCategoryViewModel model)
        {
            var category = context.Categories.FirstOrDefault(c => c.Id == model.Id);
            if (!ModelState.IsValid && category == null)
            {
                return BadRequest(model);
            }
            var oldphoto = category.photo;
            var hasnewphoto = model.photo != null;
            if (hasnewphoto)
            {
                var photoname = $"{model.Name}{Path.GetExtension(model.photo.FileName)}";
                var path = Path.Combine("C:\\Users\\qais\\Desktop\\sql\\E-Commerce\\E-Commerce\\wwwroot\\assets\\CategoryPics\\", photoname);
                using var stream = System.IO.File.Create(path);
                category.photo = photoname;
                category.Name = model.Name;
                var deletephoto = Path.Combine("C:\\Users\\qais\\Desktop\\sql\\E-Commerce\\E-Commerce\\wwwroot\\assets\\CategoryPics\\", oldphoto);
                System.IO.File.Delete(deletephoto);
                repo.update_item(category);
                return RedirectToAction(nameof(Index));
            }
            else {
                category.Name = model.Name;
                repo.update_item(category);
                return RedirectToAction(nameof(Index));
            }
           
        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var item =repo.Find_by_id(Id);
            if (item == null)
            {
                return NotFound();
            }
            repo.remove_item(item);
            return RedirectToAction(nameof(Index));
        }


    }
}
