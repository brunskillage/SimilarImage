using MetroFramework.Controls;
using MetroFramework.Forms;
using SimilarImage.Controllers;
using SimilarImage.Events;
using SimilarImage.Models;
using SimilarImage.Services;
using SimilarImage.Widgets;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SimilarImage.Ui
{

    public class FormComponents
    {
         
    }

    public partial class MetroMainForm : MetroForm
    {
        private readonly MainFormController _controller;
        private readonly DataGridViewService _dataGridViewService;
        private readonly ImageStatService _imageStatService;
        private readonly FormSettingsService _formSettingsService;
        private readonly MetroProgressBarWidget _progressBarWidget;

        private BackgroundWorker _cleanMissingWorker;
        private BackgroundWorker _getImagesWorker;
        private BackgroundWorker _getSetsWorker;
        private BackgroundWorker _reduceSetsWorker;

        public MetroMainForm(MainFormController controller, DataGridViewService dataGridViewService, ImageStatService imageStatService, FormSettingsService formSettingsService)
        {
            _controller = controller;
            _dataGridViewService = dataGridViewService;
            _imageStatService = imageStatService;
            _formSettingsService = formSettingsService;

            InitializeComponent();

            _progressBarWidget = new MetroProgressBarWidget(metroProgressBar1, progressPercent, progressLabel);

            // metroStyleManager1.Theme = metroStyleManager1.Theme == MetroThemeStyle.Light ? MetroThemeStyle.Dark : MetroThemeStyle.Light;
            // this.Theme = metroStyleManager1.Theme;
            // this.Refresh();
            // dependencies

            // handlers
            Messaging.OnTalk += OnTalkHandler;
            Messaging.OnProgress += OnProgressHandler;

            FormClosing += Form1_FormClosing;
            Load += form_Load;
            stopButton.Click += stopButton_Click;
            startbutton.Click += startbutton_Click;
            metroButton1.Click += cleanSetsButton_Click;
            analyseButton.Click += analyseButton_Click;
            extraSetAnalysisCheckbox.CheckedChanged += extraSetAnalysisCheckbox_CheckedChanged;
            statsAnalysisRange.ValueChanged += statsAnalysisRange_ValueChanged;
            minSimilarityScoreNumericUpDown.ValueChanged += minSimilarityScoreNumericUpDown_ValueChanged;
            imageSetsListbox.SelectedIndexChanged += imageSetsListbox_SelectedIndexChanged;
            imagesGridView.Click += imagesGridView_Click;
            imagesGridView.DoubleClick += imagesGridView_DoubleClick;

            // start methods
            _controller.TestConnection();
            SetImageCountText();
            imageSetsListbox.Items.Clear();
            LoadUiFromSavedOptions();

            // hacks for visual performance
            MakeDoubleBufferedHack(this, true);
            MakeDoubleBufferedHack(imagesGridView, true);
        }

        private void OnTalkHandler(object sender, EventArgs eventArgs)
        {
            var talkEventArgs = eventArgs as TalkEventArgs;

            Action act = () =>
            {
                if (talkEventArgs != null && !string.IsNullOrWhiteSpace(talkEventArgs.Words))
                {
                    toolStripStatusLabel1.Text = talkEventArgs.Words;
                    toolStripStatusLabel1.Invalidate();
                }
            };

            SafeUpdate(act);
        }

        // ============= UI Event handlers ==================

        private void OnProgressHandler(object sender, EventArgs args)
        {
            Action act = () =>
            {
                var progressBarArgs = args as ProgressBarEventArgs;

                if (progressBarArgs.EventKindOf == EventKind.Increment)
                    _progressBarWidget.Increment();

                if (progressBarArgs.EventKindOf == EventKind.ResetProgress)
                    _progressBarWidget.Reset(progressBarArgs.CurrentValue, progressBarArgs.Text);

                if (progressBarArgs.EventKindOf == EventKind.IncrementError)
                    _progressBarWidget.IncrementError();
            };

            SafeUpdate(act);
        }

        private void form_Load(object sender, EventArgs e)
        {
            minSimilarityScoreNumericUpDown.Enabled = extraSetAnalysisCheckbox.Checked;
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            _controller.SetCancelFlag(true);
            _getImagesWorker?.CancelAsync();
            SetImageCountText();
        }

        private void analyseButton_Click(object sender, EventArgs e)
        {
            RunGetSetsWorker();
        }


        private void startbutton_Click(object sender, EventArgs e)
        {
            var options = _formSettingsService.GetFormOptions(this);

            if (!Directory.Exists(options.ImagesSoureDirectory) ||
                string.IsNullOrWhiteSpace(options.ImagesSoureDirectory))
                selectFolder();

            options = _formSettingsService.GetFormOptions(this);

            if (!options.ClearDatabase || string.IsNullOrWhiteSpace(options.ImagesSoureDirectory))
            {
                Messaging.Talk("Clear database not checked, so nothing was run...");
                RunGetSetsWorker();
            }
            else
            {
                RunGetImagesWorker();
            }
        }

        private void imageSetsListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var list = (MetroListView)sender;
            if (list.SelectedIndices.Count == 0)
                return;

            var selected = (ImageStatGroup)list.SelectedItems[0].Tag;

            _controller.RenderGrid(imagesGridView, selected, _formSettingsService.GetFormOptions(this), _controller);
        }

        private void isDuplicateButton_Click(object sender, EventArgs e)
        {
            if (imagesGridView.SelectedCells.Count > 0)
                foreach (DataGridViewImageCell selectedCell in imagesGridView.SelectedCells)
                {
                    var data = _dataGridViewService.GetActiveCellData(selectedCell);
                    if (data != null)
                    {
                        data.IsDuplicate = 1;
                        _controller.SetDuplicateValue(data.Id, true);
                        _dataGridViewService.SetDuplicateDisplay(selectedCell);
                        Messaging.Talk($"{data.ImagePath} marked as Duplicate");
                    }
                }
        }

        private void clearDuplicateButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewImageCell selectedCell in imagesGridView.SelectedCells)
            {
                var data = _dataGridViewService.GetActiveCellData(selectedCell);
                if (data != null)
                {
                    data.IsDuplicate = 0;
                    _controller.SetDuplicateValue(data.Id, false);
                    _dataGridViewService.SetNotDuplicateDisplay(selectedCell);
                    Messaging.Talk($"{data.ImagePath} not a dulplicate");
                }
            }
        }

        private void moveDuplicatesButton_Click(object sender, EventArgs e)
        {
            if (!confirmCleanupCheckbox.Checked)
                return;

            _controller.MoveDuplicates(_formSettingsService.GetFormOptions(this));
        }
        
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.A)
            //    if (imageSetsListbox.SelectedIndex > 0)
            //        imageSetsListbox.SelectedIndex = imageSetsListbox.SelectedIndex - 1;

            //if (e.KeyCode == Keys.S)
            //    if (imageSetsListbox.SelectedIndex < imageSetsListbox.Items.Count - 1)
            //        imageSetsListbox.SelectedIndex = imageSetsListbox.SelectedIndex + 1;

            if (e.KeyCode == Keys.D)
                isDuplicateButton.PerformClick();

            e.Handled = true;
        }

        private void cleanMissingButton_Click(object sender, EventArgs e)
        {
            RunCleanMissingWorker();
        }

        // ============= UI Event handlers END ================== 

        private void SafeUpdate(Action act)
        {
            if (InvokeRequired)
                Invoke(act);
            else
                act();
        }


        private void RunGetImagesWorker()
        {
            var options = _formSettingsService.GetFormOptions(this);
            using (_getImagesWorker = new BackgroundWorker())
            {
                _getImagesWorker.WorkerReportsProgress = true;
                _getImagesWorker.WorkerSupportsCancellation = true;
                _getImagesWorker.DoWork += (o, args) =>
                {
                    SafeUpdate(() => _progressBarWidget.Reset(0, "Progress of image scan"));

                    _controller.SetCancelFlag(false);
                    _controller.ClearDatabase();

                    var imagePaths = _controller.GetImageList(options.ImagesSoureDirectory);
                    _controller.AnalyseImages(imagePaths, options);
                };

                _getImagesWorker.RunWorkerCompleted += (o, args) =>
                {
                    Messaging.Talk("Completed Scanning images...");
                    RunGetSetsWorker();
                };

                _getImagesWorker.RunWorkerAsync();
            }
        }

        private void SetImageCountText()
        {
            Action act = () => dbRecordCountLabel.Text = _controller.GetImageRecordCount() + " Database records";
            SafeUpdate(act);
        }

        private void selectFolder()
        {
            var folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                SafeUpdate(() => imagesDirectoryTextBox.Text = folderBrowserDialog1.SelectedPath);

            _formSettingsService.SaveFormOptionsToFile(this);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _formSettingsService.SaveFormOptionsToFile(this);
            _controller.CloseDatabaseConnection();
        }

        // Another hack for flickering problem
        //https://www.codeproject.com/Tips/390496/Reducing-flicker-blinking-in-DataGridView
        public void MakeDoubleBufferedHack(object dgv, bool setting)
        {
            var dgvType = dgv.GetType();
            var pi = dgvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }

        private void LoadUiFromSavedOptions()
        {
            _formSettingsService.LoadFormOptionsFromFile(this);
        }

        private void RunGetSetsWorker()
        {
            using (_getSetsWorker = new BackgroundWorker())
            {
                _getSetsWorker.WorkerSupportsCancellation = true;

                _getSetsWorker.DoWork += (o, args) =>
                {
                    Messaging.Talk("Finding similar images...");
                    SafeUpdate(() => imageSetsListbox.Items.Clear());
                    var options = _formSettingsService.GetFormOptions(this);
                    _controller.FindSimilarImages(options);
                };

                _getSetsWorker.RunWorkerCompleted += (o, args) =>
                {
                    Messaging.Talk("Finished finding similar images...");
                    FillImageGroupsListBox(_imageStatService.ImageSets.Cast<object>().ToArray());
                    RunCleanStatsWorker();
                };

                _getSetsWorker.RunWorkerAsync();
            }
        }

        private void RunCleanStatsWorker()
        {
            using (_reduceSetsWorker = new BackgroundWorker())
            {
                _reduceSetsWorker.DoWork += (o, args) =>
                {
                    Messaging.RaiseProgress(null, new ProgressBarEventArgs
                    {
                        EventKindOf = EventKind.ResetProgress,
                        Text = "Cleaning Sets",
                        CurrentValue = _imageStatService.ImageSets.Count
                    });
                    _controller.CleanImageGroupsUsingAlgorthm(Convert.ToDouble(_formSettingsService.GetFormOptions(this).MinimumSimilarityScore));
                };

                _reduceSetsWorker.RunWorkerCompleted += (o, args) =>
                {
                    Messaging.Talk("Finished reducing sets...");
                    FillImageGroupsListBox(_imageStatService.ImageSets.Cast<object>().ToArray());
                };

                _reduceSetsWorker.RunWorkerAsync();
            }
        }

        private void UpdateImageSetsText()
        {
            SafeUpdate(() =>
            {
                imageSetsGroupBox.Text = $"{imageSetsListbox.Items.Count} Sets ";
            });
        }


        private void RunCleanMissingWorker()
        {
            using (_cleanMissingWorker = new BackgroundWorker())
            {
                _cleanMissingWorker.DoWork += (o, args) =>
                {
                    _controller.CleanMissingFilesFromDatabase(_formSettingsService.GetFormOptions(this));
                };
                _cleanMissingWorker.RunWorkerCompleted += (o, args) =>
                {
                    Messaging.Talk("Finished cleaning missing files from database...");
                };
                _cleanMissingWorker.RunWorkerAsync();
            }
        }

        private void extraSetAnalysisCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            var checkbox = (CheckBox)sender;
            minSimilarityScoreNumericUpDown.Enabled = checkbox.Checked;
        }

        private void cleanSetsButton_Click(object sender, EventArgs e)
        {
            RunCleanStatsWorker();
        }

        private void FillImageGroupsListBox(object[] items)
        {
            SafeUpdate(() =>
            {
                imageSetsListbox.Items.Clear();
                imageSetsListbox.Items.AddRange(
                    items.Select(i => new ListViewItem { Text = i.ToString(), Tag = i }).ToArray());
            });

            UpdateImageSetsText();
        }

        private void imagesGridView_Click(object sender, EventArgs e)
        {
            if (imagesGridView.CurrentCell != null)
            {
                var cell = (DataGridViewImageCell)imagesGridView.CurrentCell;
                if (cell.Tag != null)
                {
                    var stat = (ImageStat)cell.Tag;
                    var finfo = new FileInfo(stat.ImagePath);

                    stat.DateCreated = finfo.CreationTime;
                    stat.DateModified = finfo.LastWriteTime;

                    // imageDetailPropertyGrid.SelectedObject = stat;
                }

                // HackPropertyGrid();
            }
        }

        private void imagesGridView_DoubleClick(object sender, EventArgs e)
        {
            if (imagesGridView.CurrentCell != null)
            {
                var cell = (DataGridViewImageCell)imagesGridView.CurrentCell;
                if (cell.Tag != null)
                {
                    var stat = (ImageStat)cell.Tag;
                    var finfo = new FileInfo(stat.ImagePath);
                    if (finfo.Exists)
                        Process.Start(finfo.FullName);
                }
            }
        }

        private void statsAnalysisRange_ValueChanged(object sender, EventArgs e)
        {
            metroLabel2.Text = ((MetroTrackBar)sender).Value.ToString();
        }

        private void minSimilarityScoreNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            metroLabel6.Text = ((MetroTrackBar)sender).Value.ToString();
        }
    }
}