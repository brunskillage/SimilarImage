using System;
using System.IO;
using System.Security.Policy;
using Newtonsoft.Json;
using SimilarImage.Models;
using SimilarImage.Ui;

namespace SimilarImage.Services
{
    public class FormSettingsService
    {
        private readonly ConfigService _configService;
        private readonly FileSystemService _fileSystemService;


        public FormSettingsService(ConfigService configService, FileSystemService fileSystemService)
        {

            _configService = configService;
            _fileSystemService = fileSystemService;
        }

        public FormOptions GetFormOptions(MetroMainForm _form)
        {
            var similarityScore = _form.minSimilarityScoreNumericUpDown.Value;

            var options = new FormOptions
            {
                ImagesSoureDirectory = _form.imagesDirectoryTextBox.Text,
                ClearDatabase = _form.clearDatabaseCheckbox.Checked,
                StatsAnalysisRange = _form.statsAnalysisRange.Value,
                ConfirmMoveDuplicates = _form.confirmCleanupCheckbox.Checked,
                ExtraSetAnalysis = _form.extraSetAnalysisCheckbox.Checked,
                MinimumSimilarityScore = similarityScore > 1 ? similarityScore*0.01m : similarityScore
            };

            return options;
        }

        public FormOptions LoadFormOptionsFromFile(MetroMainForm _form)
        {
            if (!_fileSystemService.FileExists(_configService.ConfigFile))
            {
                var defaultOptions = new FormOptions
                {
                    ImagesSoureDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                    ClearDatabase = false,
                    StatsAnalysisRange = 1,
                    ConfirmMoveDuplicates = false,
                    ExtraSetAnalysis = true,
                    MinimumSimilarityScore = 0.85m
                };
                File.WriteAllText(_configService.ConfigFile,
                    JsonConvert.SerializeObject(defaultOptions, Formatting.Indented));
            }

            var options = ReadFromFile();
            _form.imagesDirectoryTextBox.Text = options.ImagesSoureDirectory;
            _form.clearDatabaseCheckbox.Checked = options.ClearDatabase;
            _form.statsAnalysisRange.Value = (int) options.StatsAnalysisRange;
            _form.confirmCleanupCheckbox.Checked = options.ConfirmMoveDuplicates;
            _form.extraSetAnalysisCheckbox.Checked = options.ExtraSetAnalysis;
            _form.minSimilarityScoreNumericUpDown.Value = options.MinimumSimilarityScore < 1
                ? (int) (options.MinimumSimilarityScore*100)
                : (int) (options.MinimumSimilarityScore);
            return options;
        }

        private FormOptions ReadFromFile()
        {
            return JsonConvert.DeserializeObject<FormOptions>(_fileSystemService.GetText(_configService.ConfigFile));
        }

        public void SaveFormOptionsToFile(MetroMainForm _form)
        {
            var options = GetFormOptions(_form);
            _fileSystemService.Save(_configService.ConfigFile, JsonConvert.SerializeObject(options, Formatting.Indented));
        }
    }
}