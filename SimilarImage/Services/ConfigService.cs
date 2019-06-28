using SimilarImage.Models;

namespace SimilarImage.Services
{
    public class ConfigService
    {
        public readonly int ThumbMaxHeight = 200;
        public readonly int TempBitmapHeight = 400;
        public readonly string DupesDirName = "_dupes";
        public readonly string SavedSetsFileName = "sets.json";
        public readonly string ConfigFile = "config.json";
    }
}