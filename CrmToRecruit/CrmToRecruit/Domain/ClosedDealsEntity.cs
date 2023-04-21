using System.ComponentModel.DataAnnotations;

namespace CrmToRecruit.Domain
{
    public class ClosedDealsEntity
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string RecordId { get; set; }

        [MaxLength(100)]
        public string DealOwner { get; set; }

        [MaxLength(100)]
        public string DealName { get; set; }

        [MaxLength(100)]
        public string AccountName { get; set; }

        [MaxLength(100)]
        public string Stage { get; set; }

        public DateTime? ModifiedTime { get; set; }

        public DateTime? JobOpeningCreationDate { get; set; }

        public DateTime? ClosingDate { get; set; }

        public int? NumberOfRoles { get; set; }

        public int? NumberOfResources { get; set; }

        [MaxLength(500)]
        public string? LossReason { get; set; }

        [MaxLength(500)]
        public string? LossDescription { get; set; }
    }
}
