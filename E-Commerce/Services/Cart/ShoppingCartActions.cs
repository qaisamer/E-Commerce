using E_Commerce.Data;
using E_Commerce.Data.Migrations;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace E_Commerce.Services.Cart
{
    public class ShoppingCartActions
    {
        private string ShoppingCartId { get; set; }
        private ApplicationDbContext _db;
        public ShoppingCartActions(ApplicationDbContext _db)
        {
            this._db = _db;
        }
        public static ShoppingCartActions GetShoppingCart(IServiceProvider serviceProvider)
        {
            var session = serviceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCartActions(context) { ShoppingCartId = cartId };
        }
        public List<CartItem> GetCartItems()
        {
            return _db.cartItems.Where(c => c.CartId == ShoppingCartId).Include(x=> x.Product).ToList();
        }
        public void AddToCart(int id)
        {
            // Retrieve the product from the database.           
            var cartItem = _db.cartItems.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                && c.ProductId == id);
            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists.                 
                cartItem = new CartItem
                {
                    ItemId = Guid.NewGuid().ToString(),
                    ProductId = id,
                    Price= ((decimal)_db.Products.SingleOrDefault(p => p.Id == id).Price),
                    CartId = ShoppingCartId,
                    Product = _db.Products.SingleOrDefault(p => p.Id == id),
                    Quantity = 1,
                    DateCreated = DateTime.Now
                };
                _db.cartItems.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart,                  
                // then add one to the quantity.                 
                cartItem.Quantity++;
            }
            _db.SaveChanges();
        }

        public void RemoveFromCart(int id)
        {
            var cartitem = _db.cartItems.SingleOrDefault(
                    c => c.CartId == ShoppingCartId
                    && c.ProductId == id);
            if (cartitem.Quantity >= 2)
            {
                cartitem.Quantity--;
                _db.Update(cartitem);
                _db.SaveChanges();
            }
            else
            {
                _db.cartItems.Remove(cartitem);
                _db.SaveChanges();
            }
        }
        public double GetShoppingCartTotal()=> _db.cartItems.Where(x => x.CartId == ShoppingCartId).Select(x => x.Product.Price * x.Quantity).Sum();
        public void ClearShoppingCart()
        {
            var items = _db.cartItems.Where(x => x.CartId == ShoppingCartId).ToList();
            _db.cartItems.RemoveRange(items);
            _db.SaveChanges();
        }

    }
 


    
}
