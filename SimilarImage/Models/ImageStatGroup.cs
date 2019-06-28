using System.Collections.Generic;

namespace SimilarImage.Models
{
    public class ImageStatGroup
    {
        public ImageStatGroup()
        {
            ImageStats= new List<ImageStat>();
        }
        public string Name { get; set; }
        public List<ImageStat> ImageStats { get; set; }
        public override string ToString()
        {
            return $"{Name} ({ImageStats.Count} matches)";
        }
    }
}