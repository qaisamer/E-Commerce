using E_Commerce.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Commerce.ViewModel
{
    public class CreatProductViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public float Price { get; set; }
        public IFormFile? photo { get; set; } = default!;
        public IEnumerable<SelectListItem> catigories { get; set; }= Enumerable.Empty<SelectListItem>();
    }
}
