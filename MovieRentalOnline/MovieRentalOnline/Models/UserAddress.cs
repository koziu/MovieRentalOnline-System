using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieRentalOnline.Models
{
    public class UserAddress
    {
        public UserAddress()
        { // jeden uzytkownik 2 adresy
        }
        public int UserAddressId { get; set; }   //klucz glowny
        public string UserId { get; set; }
        public bool IsPrimary { get; set; }
        public string Street { get; set; }
        public string HomeNo { get; set; }
        public string Postal { get; set; }
        public string CityPostal { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }

    }
}