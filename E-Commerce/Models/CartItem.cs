using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models
{
    public class CartItem
    {
        [Key]
        public string ItemId { get; set; }

        public string CartId { get; set; } = string.Empty;

        public int Quantity { get; set; } = 0;

        public decimal Price { get; set; }

        public System.DateTime DateCreated { get; set; }= DateTime.Now;

        public int ProductId { get; set; }

        public virtual Product Product { get; set; } = default!;
    }
}
