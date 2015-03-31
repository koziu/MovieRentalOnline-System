using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieRentalOnline.Models
{
    public class Actor
    {
        public Actor() { }
        public int ActorId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Biography { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}