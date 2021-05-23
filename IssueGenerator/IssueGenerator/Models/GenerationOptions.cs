namespace IssueGenerator.Models
{
    public class GenerationOptions
    {
        public int IssuesCount { get; set; }
        public int? WordsLimit { get; set; }
        public bool RandomWordsCount { get; set; }
        public int? TextSourceId { get; set; }
    }
}
