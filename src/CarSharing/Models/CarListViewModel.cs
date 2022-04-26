using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarSharing.Models
{
    public class CarListViewModel
    {
        public int CarId { get; set; }
        public string MarkName { get; set; }
        public string ModelName { get; set; }
        public int Status { get; set; }
        public string ImageURL { get; set; }
        public int ReleaseDate { get; set; }
        public decimal Cost { get; set; }

    }
}
