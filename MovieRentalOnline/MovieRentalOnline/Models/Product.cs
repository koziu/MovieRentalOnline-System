using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieRentalOnline.Models
{
    public class Product
    {
        public Product()
        { //polaczenia wiele do wielu
            this.Languages = new HashSet<Language>();
            this.VideoTechnologys = new HashSet<VideoTechnology>();
            this.SoundTechnologys = new HashSet<SoundTechnology>();

        }
        public int ProductId { get; set; } //klucz glowny
        public int MovieId { get; set; } //polaczone z jednym filmem
        public int StorageMediumId { get; set; }
        public float Cost { get; set; }


        public virtual ICollection<Language> Languages { get; set; } //polaczone z wieloma jezykami
        public virtual ICollection<VideoTechnology> VideoTechnologys { get; set; }  //polaczone z wieloma technologiami obrazu
        public virtual ICollection<SoundTechnology> SoundTechnologys { get; set; }  //polaczone z wieloma technologiami dzwieku
        public virtual ICollection<PhysicalProduct> PhysicalProducts { get; set; }  //polaczone z wieloma fizycznymi produktami
        public virtual Movie Movie { get; set; } //polaczone z jednym filmem
        public virtual StorageMedium StorageMedium { get; set; } //polaczone z jednym filmem

    }
}