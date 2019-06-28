namespace SimilarImage.Models
{
    public class FormOptions
    {
        public string ImagesSoureDirectory { get; set; }
        public bool ClearDatabase { get; set; }
        public decimal StatsAnalysisRange { get; set; }
        public bool ConfirmMoveDuplicates { get; set; }
        public bool ExtraSetAnalysis { get; set; }
        public decimal MinimumSimilarityScore { get; set; }
        public decimal SimilarityThreshold { get; set; }
    }
}