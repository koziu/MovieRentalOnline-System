using MovieRentalOnline.DAL;
using MovieRentalOnline.Logic;
using MovieRentalOnline.Models;
using MovieRentalOnline.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace MovieRentalOnline.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        private ISessionManager sessionManager { get; set; }
        private CartManager cartManager;
        private RentalContext db = new RentalContext();

        public CartController()
        {
            this.sessionManager = new SessionManager();
            this.cartManager = new CartManager(this.sessionManager, this.db);
        }

        [Authorize(Roles = "Client")]
        public ActionResult Index(DateTimeChange DateTimeChange = null)
        {
            if (Request.IsAjaxRequest())
            {
                var movies = db.Movies.ToList();
                if (DateTimeChange != null)
                {
                    var DeliveryTime = new DateTime(DateTimeChange.DeliveryTime.Year, DateTimeChange.DeliveryTime.Month, DateTimeChange.DeliveryTime.Day, DateTimeChange.DeliveryTime.Hour, DateTimeChange.DeliveryTime.Minute, 0);
                    var ReturnTime = new DateTime(DateTimeChange.ReturnTime.Year, DateTimeChange.ReturnTime.Month, DateTimeChange.ReturnTime.Day, DateTimeChange.ReturnTime.Hour, DateTimeChange.ReturnTime.Minute, 0);

                    cartManager.ChangeDateTime(DeliveryTime, ReturnTime);
                    return CartMenu();
                }
            }


            var cart = cartManager.GetCart();
            return View(cart);
        }

        public ActionResult AddToCart(int productId)
        {
            var product = db.Products.Where(a => a.ProductId == productId).FirstOrDefault();
            if (product != null)
                cartManager.AddToCart(product);
            return RedirectToAction("Index", "Cart");
        }

        public ActionResult RemoveFromCart(int productId)
        {
            cartManager.RemoveFromCart(productId);
            return RedirectToAction("Index", "Cart");
        }
        
        public ActionResult AddToCartMenu(int  movieId = 0)
        {
            
            var products = db.Products.Where(a => a.Movie.MovieId == movieId).ToList();
            if (products == null)
                return View("List", "Rental");
            var viewModel = new List<CartItem>();
            foreach (var product in products)
            {
                var cart = cartManager.GetCart();
                var vmp = new CartItem();
                vmp.Product = product;
                vmp.Cost = cartManager.SingleCost(product);
                if(cartManager.AvailabilePhysicalProductId(product, cart.DeliveryTime, cart.ReturnTime) == -1)
                    vmp.IsAvailable = false;
                else
                    vmp.IsAvailable = true;
                viewModel.Add(vmp);
            }
            return View("_AddToCartMenu", viewModel);
        }

        [ChildActionOnly]
        public ActionResult CartMenu()
        {
            Cart cart = cartManager.GetCart();
            return View("_CartMenu", cart);
        }
       
        public ActionResult Order()
        {
            Cart cart = cartManager.GetCart();
            var order = new Order()
            {
                ClientId = User.Identity.GetUserId(),
                DeliveryTime = cart.DeliveryTime,
                ReturnTime = cart.ReturnTime,
                OrderStatus = OrderStatus.InProgress,
                Rents = new List<Rent>()
               
            };
            foreach (var rent in cart.CartItems)
            {
                var r = new Rent()
                {
                    PhysicalProduct = db.PhysicalProducts.Find(cartManager.AvailabilePhysicalProductId(rent.Product, cart.DeliveryTime, cart.ReturnTime)),
                    Order = order,
                    SingleCost = (double)rent.Cost,
                    RentStatus = RentStatus.Waiting
                };
                order.Rents.Add(r);
            }
            db.Orders.Add(order);
            db.SaveChanges();

            cart = new Cart();
            cart.CartItems = new List<CartItem>();
            var now = DateTime.Now.Date.AddHours(DateTime.Now.Hour);
            cart.DeliveryTime = now.AddHours(2);
            cart.ReturnTime = now.AddDays(1);
            cart.TotalCost = 0;

            this.sessionManager.Set("CartData", cart);          
            
            return View("_SuccesOrder", order);
        }
        
    }
}