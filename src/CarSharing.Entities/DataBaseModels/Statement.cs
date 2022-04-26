namespace CarSharing.Entities.DataBaseModels
{
    public class Statement : BaseEntities
    {
        public string EmployeeId { get; set; }
        public int OrderId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
