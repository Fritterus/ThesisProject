using CarSharing.Entities.Enums;

namespace CarSharing.Entities.Models
{
    public class RequestModel
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalCost { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public OrderStatus Status { get; set; }
    }
}
