using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLibrary.Models.Identity;

namespace WebLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public bool isBooked { get; set; }
        public string WhoBooked { get; set; }
        public string CurrentHolder { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
