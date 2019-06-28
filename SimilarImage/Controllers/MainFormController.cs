using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Imaging;
using SimilarImage.Events;
using SimilarImage.Models;
using SimilarImage.Services;
using SimilarImage.Ui;

namespace SimilarImage.Controllers
{
    public class MainFormController
    {
        private readonly DatabaseService _databaseService;
        private readonly DataGridViewService _dataGridViewService;
        private readonly ImageStatService _imageStatService;
        private readonly ConfigService _configService;
        private readonly FileSystemService _fileSystemService;
        private readonly ImageService _imageService;
        private bool cancelAnalysis;

        public MainFormController(DatabaseService databaseService, DataGridViewService dataGridViewService, 
            ImageStatService imageStatService, ConfigService configService, FileSystemService fileSystemService, ImageService imageService)
        {
            _databaseService = databaseService;
            _dataGridViewService = dataGridViewService;
            _imageStatService = imageStatService;
            _configService = configService;
            _fileSystemService = fileSystemService;
            _imageService = imageService;
        }

        public MetroMainForm Form { get; set; }
        
        public void TestConnection()
        {
            Messaging.Talk("Connecting to Database...");
            _databaseService.TestConnection();
            _databaseService.GetOpenConnection();
            _databaseService.CreateTables();
            Messaging.Talk("Database connected...");
        }

        public void OpenDatabaseConnection()
        {
            Messaging.Talk("Connecting to Database...");
            _databaseService.GetOpenConnection();
            Messaging.Talk("Database connected...");
        }

        public void CloseDatabaseConnection()
        {
            Messaging.Talk("Disconnecting from Database...");
            _databaseService.CloseConnection();
        }

        public string[] GetImageList(string directory)
        {
            Messaging.Talk("Getting the image list...");

            var ext = new List<string> {".jpg", ".gif", ".png"};
            var files = Directory.EnumerateFiles(directory, "*.*", SearchOption.AllDirectories)
                .Where(s => ext.Contains(Path.GetExtension(s).ToLowerInvariant())
                            && Path.GetFullPath(s).IndexOf(_configService.DupesDirName, StringComparison.InvariantCultureIgnoreCase) == -1
                            && Path.GetFullPath(s).IndexOf("AppData",StringComparison.InvariantCultureIgnoreCase) == -1
                       ).ToArray();

            Messaging.Talk($"Found {files.Length} images...");
            Messaging.RaiseProgress(null, new ProgressBarEventArgs
            {
                EventKindOf = EventKind.ResetProgress, CurrentValue = files.Length, Text = "Getting image List..."
            });

            return files;
        }

        public void ClearDatabase()
        {
            _databaseService.DeleteImageStats();
        }

        public void AnalyseImages(string[] imageFullPaths, FormOptions options)
        {
            var actions = imageFullPaths.Select(imageFilePath => new Action(() =>
                {
                    if (cancelAnalysis)
                        return;

                    var imageStat = GetImageStats(imageFilePath, options);
                    if (imageStat != null)
                        _databaseService.InsertImageStat(imageStat);
                }
            )).ToArray();

            Parallel.Invoke(new ParallelOptions {MaxDegreeOfParallelism = 2}, actions);

            Messaging.Talk("Completed processing...");
        }

        public void SetCancelFlag(bool val)
        {
            Messaging.Talk($"Setting cancel flag to {val}... ");
            cancelAnalysis = val;
        }

        public int GetImageRecordCount()
        {
            return _databaseService.GetImageRecordCount();
        }

        public void ClearImageStats()
        {
            _imageStatService.ImageSets.Clear();
            _databaseService.DeleteImageStats();
            Messaging.RaiseProgress(null, new ProgressBarEventArgs { EventKindOf = EventKind.ResetProgress, Text = "Getting image List..." });

        }

        public ImageStat GetImageStats(string fullPath, FormOptions options)
        {
            if (cancelAnalysis)
                return null;

            Messaging.Talk($"{fullPath}");

            ImageStat res = null;

            try
            {
                res = new ImageStat();

                
                res.ImagePath = fullPath.Replace(options.ImagesSoureDirectory, string.Empty);
                res.Length = _fileSystemService.GetFileLength(fullPath);

                // load file
                using (var image = _imageService.GetThumb(fullPath, _configService.TempBitmapHeight))
                {
                    // hsl
                    //Talk($"{fullPath} HSL... ");
                    var statsHSL = new ImageStatisticsHSL(image);
                    res.Luminance = (decimal) statsHSL.Luminance.Median;
                    res.Pixels = statsHSL.PixelsCount;

                    // rgb
                    //Talk($"{fullPath} RGB... ");
                    var stats = new ImageStatistics(image);
                    res.Blue = stats.Blue.Median;
                    res.Red = stats.Red.Median;
                    res.Green = stats.Green.Median;
                    res.Pixels = stats.PixelsCount;

                    res.BlueMax = stats.Blue.Max;
                    res.RedMax = stats.Red.Max;
                    res.GreenMax = stats.Green.Max;

                    res.BlueMin = stats.Blue.Min;
                    res.RedMin = stats.Red.Min;
                    res.GreenMin = stats.Green.Min;

                    // YCbCr
                    // Talk($"{fullPath} YCbCr... ");
                    var statsYCbCr = new ImageStatisticsYCbCr(image);
                    res.Cb = statsYCbCr.Cb.Mean;
                    res.Cr = statsYCbCr.Cr.Mean;
                    res.Y = statsYCbCr.Y.Mean;


                }
            }
            catch (Exception e)
            {
                res = null;
                Messaging.Talk($"{fullPath} Error! {e.Message}... ");
                Messaging.RaiseProgress(null, new ProgressBarEventArgs { EventKindOf = EventKind.IncrementError});
            }
            finally
            {
                Messaging.RaiseProgress(null, new ProgressBarEventArgs { EventKindOf = EventKind.Increment });
            }

            return res;
        }

        public List<ImageStatGroup> FindSimilarImages(FormOptions options)
        {
            var allImageData = _databaseService.GetImageStatsNotMoved(options);

            Messaging.RaiseProgress(null, 
                new ProgressBarEventArgs {
                    EventKindOf = EventKind.ResetProgress,
                    Text = "Gathering sets",
                    CurrentValue = allImageData.Length
                });

            var SetId = 0;

            var RGBPercentUnit = 255 * 0.01m;
            var thresholdRGB = RGBPercentUnit * options.StatsAnalysisRange;
            var thresholdLuminance = 0.01m * options.StatsAnalysisRange;

            foreach (var imageStat in allImageData)
            {
                //var count = allImageData.Count(i => i.IsExcludedFromSearchSet);

                //if (id == 100)
                //    return sets;

                Messaging.RaiseProgress(null,
                new ProgressBarEventArgs
                {
                    EventKindOf = EventKind.Increment
                });

                if (imageStat.IsExcludedFromSearchSet)
                    continue;

                
                var setName = "Set-" + SetId.ToString("00000");
                var RedLow = imageStat.Red - thresholdRGB;
                var RedHigh = imageStat.Red + thresholdRGB;
                var GreenLow = imageStat.Green - thresholdRGB;
                var GreenHigh = imageStat.Green + thresholdRGB;
                var BlueLow = imageStat.Blue - thresholdRGB;
                var BlueHigh = imageStat.Blue + thresholdRGB;

                var RedMinLow = imageStat.RedMin - thresholdRGB;
                var RedMinHigh = imageStat.RedMin + thresholdRGB;
                var GreenMinLow = imageStat.GreenMin - thresholdRGB;
                var GreenMinHigh = imageStat.GreenMin + thresholdRGB;
                var BlueMinLow = imageStat.BlueMin - thresholdRGB;
                var BlueMinHigh = imageStat.BlueMin + thresholdRGB;

                var RedMaxLow = imageStat.RedMax - thresholdRGB;
                var RedMaxHigh = imageStat.RedMax + thresholdRGB;
                var GreenMaxLow = imageStat.GreenMax - thresholdRGB;
                var GreenMaxHigh = imageStat.GreenMax + thresholdRGB;
                var BlueMaxLow = imageStat.BlueMax - thresholdRGB;
                var BlueMaxHigh = imageStat.BlueMax + thresholdRGB;

                var LumLow = imageStat.Luminance - thresholdLuminance;
                var LumHigh = imageStat.Luminance + thresholdLuminance;

                var similarToOtherStats = allImageData.Where(
                    i => i.Luminance >= LumLow && i.Luminance <= LumHigh
                         && i.Red >= RedLow && i.Red <= RedHigh
                         && i.Green >= GreenLow && i.Green <= GreenHigh
                         && i.Blue >= BlueLow && i.Blue <= BlueHigh
                         && i.RedMin >= RedMinLow && i.RedMin <= RedMinHigh
                         && i.GreenMin >= GreenMinLow && i.GreenMin <= GreenMinHigh
                         && i.BlueMin >= BlueMinLow && i.BlueMin <= BlueMinHigh
                         && i.RedMax >= RedMaxLow && i.RedMax <= RedMaxHigh
                         && i.GreenMax >= GreenMaxLow && i.GreenMax <= GreenMaxHigh
                         && i.BlueMax >= BlueMaxLow && i.BlueMax <= BlueMaxHigh
                         && !i.IsExcludedFromSearchSet).ToList();

                if (similarToOtherStats.Count <= 1)
                    continue;

                foreach (var res in similarToOtherStats)
                {
                    res.SetName = setName;
                    res.IsExcludedFromSearchSet = true;
                }

                _imageStatService.ImageSets.Add(new ImageStatGroup {Name = setName,ImageStats = similarToOtherStats});

                SetId += 1;
            }

            return _imageStatService.ImageSets;
        }

        public void SetDuplicateValue(int id, bool val)
        {
            _databaseService.SetDuplicateValue(id, val);
        }

        public void MoveDuplicates(FormOptions options)
        {
            var stats = _databaseService.GetDuplicates(options);
            foreach (var stat in stats)
            {
                var src = stat.ImagePath;
                var dest = stat.ImagePath.Replace(options.ImagesSoureDirectory,
                    options.ImagesSoureDirectory + "\\_dupes");

                Messaging.Talk($"Moving {src} to {dest}... ");
                _fileSystemService.MoveFile(src, dest);
                _databaseService.SetMoved(stat.Id, true);
                Messaging.Talk($"Done... ");
            }
            Messaging.Talk($"Finished purging duplicates...");
        }

        public void DeleteImageStatById(int id)
        {
            _databaseService.DeleteImageStatById(id);
            Messaging.Talk($"Deleted image with id {id}...");
        }

        public void CleanMissingFilesFromDatabase(FormOptions options)
        {
            var images = _databaseService.GetImageStatsNotMoved(options);

            foreach (var imageStat in images)
            {
                Messaging.Talk($"Checking {imageStat.ImagePath}...");
                if (!File.Exists(imageStat.ImagePath))
                {
                    _databaseService.DeleteImageStatById(imageStat.Id);
                    Messaging.Talk($"Deleted {imageStat.ImagePath}...");
                }
                Messaging.Talk($"Finished removing missing file entries in Database...");
            }
        }

        

        public void RenderGrid(DataGridView imagesGridView, ImageStatGroup selected, FormOptions getFormOptions, MainFormController controller)
        {
            _dataGridViewService.RenderGrid(imagesGridView, selected, getFormOptions, controller);
        }

        public void CleanImageGroupsUsingAlgorthm(double threshold)
        {
            _imageStatService.CleanImageGroupsUsingAlgorthm(threshold);
        }
    }
}