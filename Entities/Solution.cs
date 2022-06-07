namespace KnowledgeControl.Entities
{
    public class Solution
    {
        public int Id {get ; set;}
        public string Answers {get ; set;}
        public int TestId {get ; set;}
        public Test Test {get ; set;}
    }
}