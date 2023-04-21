namespace CrmToRecruit.Domain
{
    public class ClosedDealsDto
    {
        public string RecordId { get; set; }
        public string DealOwner { get; set; }
        public string DealName { get; set; }
        public string AccountName { get; set; }
        public string Stage { get; set; }
        public DateTime ModifiedTime { get; set; }
        public DateTime? JobOpeningCreationDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public int? NumberOfRoles { get; set; }
        public int? NumberOfResources { get; set; }
        public string LossReason { get; set; }
        public string LossDescription { get; set; }
    }

}
