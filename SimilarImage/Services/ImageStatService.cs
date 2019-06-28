using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimilarImage.Algorithms;
using SimilarImage.Events;
using SimilarImage.Models;

namespace SimilarImage.Services
{
    public class ImageStatService
    {
        private readonly DatabaseService _databaseService;
        private readonly FileSystemService _fileSystemService;
        private readonly ImageService _imageService;

        public ImageStatService(DatabaseService databaseService, FileSystemService fileSystemService,
            ImageService imageService)
        {
            _databaseService = databaseService;
            _fileSystemService = fileSystemService;
            _imageService = imageService;
            ImageSets = new List<ImageStatGroup>();
        }

        public List<ImageStatGroup> ImageSets { get; set; }

        public List<ImageStat> RemoveSimilar(ImageStatGroup imageStatGroup, double similarityThreshold)
        {
            Messaging.Talk($"Performing similarity scores on {imageStatGroup.Name} matrix...");

            // var threshold = similarityThreshold*0.01d;

            // grab comparable images

            var comparableImages = new ConcurrentDictionary<int, ComparableImage>();

            foreach (var imageStat in imageStatGroup.ImageStats)
            {
                var bm = _imageService.GetThumb(imageStat.ImagePath, 100, 100);
                comparableImages.TryAdd(imageStat.Id, new ComparableImage(bm));
            }

            Parallel.ForEach(imageStatGroup.ImageStats, imageStatLeft =>
            {
                foreach (var imageStatRight in imageStatGroup.ImageStats)
                {
                    if (imageStatLeft.Id == imageStatRight.Id)
                        continue;

                    //Messaging.Talk($"Performing similarity scores on set matrix on {Path.GetFileName(imageStatLeft.ImagePath)}");
                    var source = comparableImages[imageStatLeft.Id];
                    var destination = comparableImages[imageStatRight.Id];
                    var similarity = source.CalculateSimilarity(destination);

                    if (similarity >= imageStatLeft.Similarity)
                        imageStatLeft.Similarity = similarity;
                }
            });


            // remove not similar
            imageStatGroup.ImageStats = imageStatGroup.ImageStats.Where(i => i.Similarity >= similarityThreshold).ToList();

            Messaging.Talk($"Finished performing similarity scores on {imageStatGroup.Name} matrix...");

            return imageStatGroup.ImageStats.ToList();
        }

        public List<ImageStat> FindBinaryIdentical(string setName, List<ImageStat> imageStatSet)
        {
            foreach (var imageStat in imageStatSet)
            {
                var group = imageStatSet.Where(i => imageStat.Length == i.Length);
                if (group.Count() > 1)
                    foreach (var stat in group)
                    {
                        stat.MD5 = _fileSystemService.GetFileHash(stat.ImagePath);
                        stat.DateCreated = _fileSystemService.GetDateCreated(stat.ImagePath);
                    }

                var md5Group = group.Where(s => s.MD5 == imageStat.MD5);
                if (md5Group.Count() > 1)
                {
                    md5Group = md5Group.OrderBy(g => g.DateCreated);
                    foreach (var stat in md5Group.Skip(1))
                    {
                        stat.IsDuplicate = 1;
                        _databaseService.SetDuplicateValue(stat.Id, true);
                    }
                }
            }

            return imageStatSet;
        }

        public void CleanImageGroupsUsingAlgorthm(double similarityThreshold)
        {
            foreach (var imageSet in ImageSets)
            {
                RemoveSimilar(imageSet, similarityThreshold);

                Messaging.RaiseProgress(null, new ProgressBarEventArgs
                {
                    EventKindOf = EventKind.Increment
                });
            }

            ImageSets = ImageSets.Where(i => i.ImageStats.Count > 0).ToList();
        }
    }
}