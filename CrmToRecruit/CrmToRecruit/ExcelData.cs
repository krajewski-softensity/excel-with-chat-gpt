using System.ComponentModel;

namespace CrmToRecruit
{
    public class ExcelData
    {
        public int Id { get; set; }
        public string? RecordId { get; set; }
        public string? AccountName { get; set; }
        public string? DealName { get; set; }
        public string? DealOwner { get; set; }
        public int NumberOfResources { get; set; }
        public string? StageOfOpen { get; set; }
        public string? StageOfClosed { get; set; }
        public DateTime JobOpeningCreationDate { get; set; }
        public DateTime LastActivityTime { get; set; }
        public string? MustHaveSkills { get; set; }
        public string? NiceToHaveSkills { get; set; }
        public string? ProjectDescription { get; set; }
        public string? RMOwnership { get; set; }
        public string? NotesPriority { get; set; }
        public string? SubmittedST { get; set; }
        public string? SubmittedVendor { get; set; }
        public string? InterviewedST { get; set; }
        public string? InterviewedVendor { get; set; }
        public string? ConfirmedST { get; set; }
        public string? ConfirmedVendor { get; set; }
        public string? RejectedST { get; set; }
        public string? RejectedVendor { get; set; }
    }
}
