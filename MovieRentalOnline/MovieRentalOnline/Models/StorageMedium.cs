﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieRentalOnline.Models
{
    public class StorageMedium
    {
        public StorageMedium() { }
        public int StorageMediumId { get; set; } //klucz glowny
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; } //polaczone z wielomaproduktami
    }
}