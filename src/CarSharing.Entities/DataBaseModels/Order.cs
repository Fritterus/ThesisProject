using CarSharing.Entities.Enums;

namespace CarSharing.Entities.DataBaseModels
{
    public class Order : BaseEntities
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalCost { get; set; }
        public OrderStatus Status { get; set; }
        public int CarId { get; set; }
        public string UserId { get; set; }
    }
}
