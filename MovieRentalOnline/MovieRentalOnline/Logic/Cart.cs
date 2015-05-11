using MovieRentalOnline.Models;
using MovieRentalOnline.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieRentalOnline.Logic
{
    public class Cart
    {
        public List<CartItem> CartItems { get; set; }
        public DateTime DeliveryTime { get; set; }
        public DateTime ReturnTime { get; set; }
        public decimal TotalCost { get; set; }
    }
}