using MovieRentalOnline.DAL;
using MovieRentalOnline.Models;
using MovieRentalOnline.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieRentalOnline.Logic
{
    public class CartManager
    {
        public const string CartSessionKey = "CartData";
        private RentalContext db;
        private ISessionManager session;

        public CartManager(ISessionManager session, RentalContext db)
        {
            this.session = session;
            this.db = db;
            //
        }

        public void AddToCart(Product product)
        {
            var cart = this.GetCart();
            var vmp = new CartItem();
            vmp.Product = product;
            vmp.Cost = SingleCost(product);
            if (AvailabilePhysicalProductId(product, cart.DeliveryTime, cart.ReturnTime) == -1)
                vmp.IsAvailable = false;
            else
                vmp.IsAvailable = true;


            if (!cart.CartItems.Any(a => a.Product.ProductId == product.ProductId))
            {
                cart.CartItems.Add(vmp);

                session.Set(CartSessionKey, cart);
                cart.TotalCost = TotalCost();
                session.Set(CartSessionKey, cart);
            }
        }

        public void RemoveFromCart(int ProductId)
        {
            var cart = this.GetCart();
            var cartItem = cart.CartItems.Where(a => a.Product.ProductId == ProductId).FirstOrDefault();
            cart.CartItems.Remove(cartItem);

            session.Set(CartSessionKey, cart);
            cart.TotalCost = TotalCost();
            session.Set(CartSessionKey, cart);

        }

        public Cart GetCart()
        {
            Cart cart;

            if (session.Get<Cart>(CartSessionKey) == null)
            {
                cart = new Cart();
                cart.CartItems = new List<CartItem>();
                var now = DateTime.Now.Date.AddHours(DateTime.Now.Hour);
                cart.DeliveryTime = now.AddHours(2);
                cart.ReturnTime = now.AddDays(1);
                cart.TotalCost = 0;

            }
            else
            {
                cart = session.Get<Cart>(CartSessionKey) as Cart;
            }

            return cart;
        }

        public int AvailabilePhysicalProductId(Product product, DateTime DeliveryTime, DateTime ReturnTime)
        {
            if (DeliveryTime > DateTime.Now.AddHours(1) && ReturnTime > DeliveryTime.AddHours(1))
            {
                List<PhysicalProduct> physicalProductList = new List<PhysicalProduct>();
                var physicalProducts = db.PhysicalProducts.Where(a => a.Product.ProductId == product.ProductId);
                if (physicalProducts.Count() > 0)
                    physicalProductList = physicalProducts.ToList();




                foreach (var physicalProduct in physicalProductList)
                {

                    bool availabile = true;

                    List<Order> orderList = new List<Order>();
                    var orders = physicalProduct.Rents.Select(a => a.Order).Where(b => b.OrderStatus != OrderStatus.Canceled && b.OrderStatus != OrderStatus.Returned);
                    if (orders.Count() > 0)
                        orderList = orders.ToList();

                    
                    {
                        foreach (var order in orders)
                        {

                            if (!(order.ReturnTime.AddMinutes(40) > DeliveryTime && order.DeliveryTime.AddMinutes(-40) < ReturnTime))
                            {
                                availabile = false;
                                break;
                            }
                        }

                        if (!availabile)
                            break;
                    }

                    if (availabile)
                        return physicalProduct.PhysicalProductId;
                }
            }
            return -1;
        }

        public void ChangeDateTime(DateTime DeliveryTime, DateTime ReturnTime)
        {
            var cart = GetCart();
            cart.DeliveryTime = DeliveryTime;
            cart.ReturnTime = ReturnTime;
            foreach (var cartItem in cart.CartItems)
            {
                if (AvailabilePhysicalProductId(cartItem.Product, DeliveryTime, ReturnTime) == -1)
                    cartItem.IsAvailable = false;
                else
                    cartItem.IsAvailable = true;
            }
            cart.TotalCost = TotalCost();
            session.Set(CartSessionKey, cart);
        }

        public decimal SingleCost(Product product)
        {
            if (product != null)
            {
                var t = (GetCart().ReturnTime - GetCart().DeliveryTime).TotalHours;
                return (decimal)(t * product.Cost);
            }
            else
                return 0;
        }

        public decimal TotalCost()
        {
            var cart = GetCart();
            decimal totalPrice = 0;
            foreach(var cartItem in cart.CartItems)
            {
                totalPrice += SingleCost(cartItem.Product);
            }
            return totalPrice;
        }

    }
}