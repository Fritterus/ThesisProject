using CarSharing.Entities.Enums;

namespace CarSharing.Entities.DataBaseModels
{
    public class Car : BaseEntities
    {
        public decimal Cost { get; set; }
        public int ReleaseDate { get; set; }
        public string ImageURL { get; set; }
        public CarStatus Status { get; set; }
        public int MarkId { get; set; }
        public int CarModelId { get; set; }

    }
}
