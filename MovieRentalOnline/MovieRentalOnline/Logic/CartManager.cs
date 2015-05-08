using MovieRentalOnline.DAL;
using MovieRentalOnline.Models;
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

        public void AddToCart(int productId, DateTime deliveryTime, DateTime returnTime)
        {
            var cart = this.GetCart();
            if (!cart.Products.Contains(productId) && db.Products.Where(a => a.ProductId == productId).FirstOrDefault() != null)
            {
                if (AvailabilePgysicalProductId(productId, deliveryTime, returnTime) != -1)
                {
                    cart.Products.Add(productId);
                    session.Set(CartSessionKey, cart);
                }
            }
        }

        public void RemoveFromCart(int productId)
        {
            var cart = this.GetCart();
            cart.Products.Remove(productId);
            session.Set(CartSessionKey, cart);

        }

        public Cart GetCart()
        {
            Cart cart;

            if (session.Get<Cart>(CartSessionKey) == null)
            {
                cart = new Cart();
            }
            else
            {
                cart = session.Get<Cart>(CartSessionKey) as Cart;
            }

            return cart;
        }

        public int AvailabilePgysicalProductId(int productId, DateTime DeliveryTime, DateTime ReturnTime)
        {
            var physicalProducts = db.PhysicalProducts.Where(a => a.ProductId == productId).ToList();
            foreach (var physicalProduct in physicalProducts)
            {

                bool availabile = true;
                var orders = physicalProduct.Rents.Select(a => a.Order).Where(b => b.OrderStatus != OrderStatus.Canceled && b.OrderStatus != OrderStatus.Returned).ToList();
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
            return -1;
        }

        public List<int> TryChangeDateTime(DateTime DeliveryTime, DateTime ReturnTime)
        {
            var cart = GetCart();
            List<int> NoAvailableProductIds = new List<int>();
            foreach(var productId in cart.Products)
            {
                if(AvailabilePgysicalProductId(productId, DeliveryTime, ReturnTime) == -1)
                    NoAvailableProductIds.Add(productId);
            }

            if(NoAvailableProductIds.Count == 0)
            {
                cart.DeliveryTime = DeliveryTime;
                cart.ReturnTime = ReturnTime;
            }
            return NoAvailableProductIds;
        }



    }
}