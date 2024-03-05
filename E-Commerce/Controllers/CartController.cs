using E_Commerce.Data;
using E_Commerce.Services.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Authorize(Roles = "User")]
    public class CartController : Controller
    {
        
        private readonly ShoppingCartActions _shoppingCartActions;

        public CartController(ShoppingCartActions shoppingCartActions)
        {
            _shoppingCartActions = shoppingCartActions;
        }

        public IActionResult Index()
        {
            ViewBag.Total = _shoppingCartActions.GetShoppingCartTotal();
            var cartItems = _shoppingCartActions.GetCartItems();
            return View(cartItems);
        }

        public IActionResult AddToCart(int Id)
        {
            _shoppingCartActions.AddToCart(Id);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int Id)
        {
            _shoppingCartActions.RemoveFromCart(Id);
            return RedirectToAction("Index");
        }
        public ActionResult ClearCart() { 
        _shoppingCartActions.ClearShoppingCart();
            return RedirectToAction("Index");
        }
        public ActionResult Checkout()
        {
            ClearCart();
        return View();
        }


    }
}
