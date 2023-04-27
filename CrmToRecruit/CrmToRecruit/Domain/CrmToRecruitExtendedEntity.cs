namespace CrmToRecruit.Domain
{
    public class CrmToRecruitExtendedEntity : CrmToRecruitEntity
    {
        public DateTime? ClosingDate { get; set; }
        public string? Stage { get; set; }
    }
}
