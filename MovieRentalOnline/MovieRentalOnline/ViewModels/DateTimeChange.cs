using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieRentalOnline.ViewModels
{
    public class DateTimeChange
    {
        public DateTimeModel DeliveryTime { get; set; }
        public DateTimeModel ReturnTime { get; set; }
    }

    public class DateTimeModel
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

        public int Hour { get; set; }
        public int Minute { get; set; }


       
    }
}