using MovieRentalOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieRentalOnline.ViewModels
{
    public class CartItem
    {
        public Product Product { get; set; }
        public bool IsAvailable { get; set; }
        public Decimal Cost { get; set; }
    }

}