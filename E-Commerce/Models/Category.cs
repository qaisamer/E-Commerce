using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models
{
    public class Category
    {
        [Required]
        public int Id { get; set; }

        [Required, MaxLength(256)]
        public string Name { get; set; } = string.Empty;

        public string photo { get; set; } = string.Empty;

    }
}
