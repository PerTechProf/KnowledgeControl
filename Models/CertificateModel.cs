namespace KnowledgeControl.Models
{
    public class CertificateModel
    {
        public int Id { get; set; }
        public int SolutionId { get; set; }
        public string TestName { get; set; }
        public string PersonName { get; set; }
        public double Percentage { get; set; }
    }
}