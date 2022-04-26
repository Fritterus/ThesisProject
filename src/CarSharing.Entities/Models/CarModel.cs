using CarSharing.Entities.Enums;

namespace CarSharing.Entities.Models
{
    public class CarModel
    {
        public int CarId { get; set; }
        public string MarkName { get; set; }
        public string ModelName { get; set; }
        public CarStatus Status { get; set; }
        public string ImageURL { get; set; }
        public int ReleaseDate { get; set; }
        public decimal Cost { get; set; }
    }
}
