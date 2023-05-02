namespace CrmToRecruit.Domain
{
    public class Vendor
    {
        public string Name { get; set; }
        public int Submitted { get; set; }
        public int Interviewed { get; set; }
        public int Confirmed { get; set; }
        public int Rejected { get; set; }
        public double Efficiency { get; set; }
        public double InterviewEfficiency { get; set; }
    }

}
