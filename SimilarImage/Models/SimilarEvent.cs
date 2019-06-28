using System.Collections.Generic;

namespace SimilarImage.Models
{
    public class SimilarEvent
    {
        public List<ImageStat> ImageStatSet;

        public SimilarEvent()
        {
            ImageStatSet = new List<ImageStat>();
        }

        public string SetName { get; set; }

        public override string ToString()
        {
            return $"{SetName} ({ImageStatSet.Count} Results)";
        }
    }
}