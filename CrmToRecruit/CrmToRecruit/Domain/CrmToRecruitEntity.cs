using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrmToRecruit.Domain
{
    [Table("CrmToRecruit")]
    public class CrmToRecruitEntity
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string? RecordId { get; set; }

        [MaxLength(100)]
        public string? AccountName { get; set; }

        [MaxLength(100)]
        public string? DealName { get; set; }

        [MaxLength(100)]
        public string? DealOwner { get; set; }

        public int? NumberOfResources { get; set; }

        [MaxLength(100)]
        public string? StageOfOpen { get; set; }

        [MaxLength(100)]
        public string? StageOfClosed { get; set; }
        //{
        //    get => StageOfClosed;
        //    set
        //    {
        //        if (value != null && value.Length > 100)
        //            value = value[..100];

        //        StageOfClosed = value;
        //    }
        //}

        public DateTime? JobOpeningCreationDate { get; set; }

        public DateTime? LastActivityTime { get; set; }

        public string? MustHaveSkills { get; set; }

        public string? NiceToHaveSkills { get; set; }

        public string? ProjectDescription { get; set; }

        [MaxLength(100)]
        public string? RmOwnership { get; set; }

        public string? NotesPriority { get; set; }

        [MaxLength(100)]
        public string? SubmittedST { get; set; }

        [MaxLength(100)]
        public string? SubmittedVendor { get; set; }

        [MaxLength(100)]
        public string? InterviewedST { get; set; }

        [MaxLength(100)]
        public string? InterviewedVendor { get; set; }

        [MaxLength(100)]
        public string? ConfirmedST { get; set; }

        [MaxLength(100)]
        public string? ConfirmedVendor { get; set; }

        [MaxLength(100)]
        public string? RejectedST { get; set; }

        [MaxLength(100)]
        public string? RejectedVendor { get; set; }
    }
}
