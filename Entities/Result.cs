namespace KnowledgeControl.Entities
{
    public class Result
    {
        public int Id { get; set; }
        public int SolutionId { get; set; }
        public Solution Solution { get; set; }
        public int CorrectCount { get; set; }
        public int Count { get; set; }
    }
}