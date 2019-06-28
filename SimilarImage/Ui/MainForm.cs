//using System;
//using System.ComponentModel;
//using System.Diagnostics;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using System.Windows.Forms;
//using SimilarImage.Controllers;
//using SimilarImage.Events;
//using SimilarImage.Models;
//using SimilarImage.Services;
//using SimilarImage.Widgets;

//namespace SimilarImage.Ui
//{
//    public partial class MainForm : Form
//    {
//        private readonly DataGridViewService _dataGridViewService;
//        private readonly ImageStatService _imageStatService;
//        private readonly MainFormController _mainFormController;
//        private readonly ProgressBarWidget _progressBarWidget;
//        private BackgroundWorker _cleanMissingWorker;
//        private BackgroundWorker _getImagesWorker;
//        private BackgroundWorker _getSetsWorker;
//        private BackgroundWorker _reduceSetsWorker;

//        public MainForm(MainFormController mainFormController, DataGridViewService dataGridViewService,
//            ImageStatService imageStatService)
//        {
//            _mainFormController.Form = this;
//            _mainFormController = mainFormController;
//            _dataGridViewService = dataGridViewService;
//            _imageStatService = imageStatService;

//            InitializeComponent();

//            _progressBarWidget = new ProgressBarWidget(progressBar1, progressGroupBox, progressLabel);

//            // metroStyleManager1.Theme = metroStyleManager1.Theme == MetroThemeStyle.Light ? MetroThemeStyle.Dark : MetroThemeStyle.Light;
//            // this.Theme = metroStyleManager1.Theme;
//            // this.Refresh();
//            // dependencies

//            // handlers
//            Messaging.OnTalk += OnTalkHandler;
//            Messaging.OnProgress += OnProgressHandler;

//            // start methods
//            mainFormController.TestConnection();
//            SetImageCountText();
//            imageSetsListbox.Items.Clear();

//            // hacks for visual performance
//            MakeDoubleBufferedHack(this, true);
//            MakeDoubleBufferedHack(imagesGridView, true);
//        }

//        private void OnTalkHandler(object sender, EventArgs eventArgs)
//        {
//            var talkEventArgs = eventArgs as TalkEventArgs;

//            Action act = () =>
//            {
//                if (talkEventArgs != null && !string.IsNullOrWhiteSpace(talkEventArgs.Words))
//                {
//                    toolStripStatusLabel1.Text = talkEventArgs.Words;
//                    toolStripStatusLabel1.Invalidate();
//                }
//            };

//            SafeUpdate(act);
//        }

//        private void HackPropertyGrid()
//        {
//            SetLabelColumnWidth(imageDetailPropertyGrid, 100);
//        }

//        public void SetLabelColumnWidth(PropertyGrid grid, int width)
//        {
//            if (grid == null)
//                throw new ArgumentNullException("grid");

//            // get the grid view
//            var view = (Control) grid.GetType().GetField("gridView", BindingFlags.Instance | BindingFlags.NonPublic)
//                .GetValue(grid);

//            // set label width
//            var fi = view.GetType().GetField("labelWidth", BindingFlags.Instance | BindingFlags.NonPublic);
//            fi.SetValue(view, width);

//            // refresh
//            view.Invalidate();
//        }

//        private void SetImageCountText()
//        {
//            Action act = () => dbRecordCountLabel.Text = _mainFormController.GetImageRecordCount() + " Database records";
//            SafeUpdate(act);
//        }

//        private void OnProgressHandler(object sender, EventArgs args)
//        {
//            Action act = () =>
//            {
//                var progressBarArgs = args as ProgressBarEventArgs;

//                if (progressBarArgs.EventKindOf == EventKind.Increment)
//                    _progressBarWidget.Increment();

//                if (progressBarArgs.EventKindOf == EventKind.ResetProgress)
//                    _progressBarWidget.Reset(progressBarArgs.CurrentValue, progressBarArgs.Text);

//                if (progressBarArgs.EventKindOf == EventKind.IncrementError)
//                    _progressBarWidget.IncrementError();
//            };

//            SafeUpdate(act);
//        }

//        private void SafeUpdate(Action act)
//        {
//            if (InvokeRequired)
//                Invoke(act);
//            else
//                act();
//        }

//        private void startbutton_Click(object sender, EventArgs e)
//        {
//            var options = GetFormOptions();
//            if (!options.ClearDatabase || string.IsNullOrWhiteSpace(options.ImagesSoureDirectory))
//            {
//                Messaging.Talk("Clear databse not checked, so nothing was run...");
//                RunGetSetsWorker();
//            }
//            else
//                RunGetImagesWorker();
//        }

//        private void RunGetImagesWorker()
//        {
//            var options = GetFormOptions();
//            using (_getImagesWorker = new BackgroundWorker())
//            {
//                _getImagesWorker.WorkerReportsProgress = true;
//                _getImagesWorker.WorkerSupportsCancellation = true;
//                _getImagesWorker.DoWork += (o, args) =>
//                {
//                    SafeUpdate(() => _progressBarWidget.Reset(0, "Progress of image scan"));

//                    _mainFormController.SetCancelFlag(false);
//                    _mainFormController.ClearDatabase();

//                    var imagePaths = _mainFormController.GetImageList(options.ImagesSoureDirectory);
//                    _mainFormController.AnalyseImages(imagePaths, options);
//                };

//                _getImagesWorker.RunWorkerCompleted += (o, args) =>
//                {
//                    Messaging.Talk("Completed Scanning images...");
//                    RunGetSetsWorker();
//                };

//                _getImagesWorker.RunWorkerAsync();
//            }
//        }

//        private void selectDirectoryButton_Click(object sender, EventArgs e)
//        {
//            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
//                imagesDirectoryTextBox.Text = folderBrowserDialog1.SelectedPath;

//            SaveFormOptions();
//        }

//        private void SaveFormOptions()
//        {
//        }

//        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
//        {
//            SaveFormOptions();
//            _mainFormController.CloseDatabaseConnection();
//        }

//        // Another hack for flickering problem
//        //https://www.codeproject.com/Tips/390496/Reducing-flicker-blinking-in-DataGridView
//        public void MakeDoubleBufferedHack(object dgv, bool setting)
//        {
//            var dgvType = dgv.GetType();
//            var pi = dgvType.GetProperty("DoubleBuffered",
//                BindingFlags.Instance | BindingFlags.NonPublic);
//            pi.SetValue(dgv, setting, null);
//        }

//        private void Form1_Load(object sender, EventArgs e)
//        {
//            similarityThresholdnumericUpDown.Enabled = extraSetAnalysisCheckbox.Checked;
//        }

//        private FormOptions GetFormOptions()
//        {
//            var model = new FormOptions
//            {
//                ImagesSoureDirectory = imagesDirectoryTextBox.Text,
//                ClearDatabase = clearDatabaseCheckbox.Checked,
//                StatsAnalysisRange = similarityRangePercentagenumericUpDown.Value,
//                ConfirmMoveDuplicates = confirmCleanupCheckbox.Checked,
//                ExtraSetAnalysis = extraSetAnalysisCheckbox.Checked,
//                SimilarityThreshold = similarityThresholdnumericUpDown.Value,
//                MinimumSimilarityScore = similarityThresholdnumericUpDown.Value
//            };
//            return model;
//        }

//        private void stopButton_Click(object sender, EventArgs e)
//        {
//            _mainFormController.SetCancelFlag(true);
//            _getImagesWorker?.CancelAsync();
//            SetImageCountText();
//        }

//        private void analyseButton_Click(object sender, EventArgs e)
//        {
//            RunGetSetsWorker();
//        }

//        private void RunGetSetsWorker()
//        {
//            using (_getSetsWorker = new BackgroundWorker())
//            {
//                _getSetsWorker.WorkerSupportsCancellation = true;

//                _getSetsWorker.DoWork += (o, args) =>
//                {
//                    Messaging.Talk("Finding similar images...");
//                    SafeUpdate(() => imageSetsListbox.Items.Clear());
//                    var options = GetFormOptions();
//                    _mainFormController.FindSimilarImages(options);
//                };

//                _getSetsWorker.RunWorkerCompleted += (o, args) =>
//                {
//                    Messaging.Talk("Finished finding similar images...");
//                    FillImageGroupsListBox(_imageStatService.ImageSets.Cast<object>().ToArray());
//                    RunCleanStatsWorker();
//                };

//                _getSetsWorker.RunWorkerAsync();
//            }
//        }

//        private void RunCleanStatsWorker()
//        {
//            using (_reduceSetsWorker = new BackgroundWorker())
//            {
//                _reduceSetsWorker.DoWork += (o, args) =>
//                {
//                    Messaging.RaiseProgress(null, new ProgressBarEventArgs
//                    {
//                        EventKindOf = EventKind.ResetProgress,
//                        Text = "Cleaning Sets",
//                        CurrentValue = _imageStatService.ImageSets.Count
//                    });
//                    _mainFormController.CleanImageGroupsUsingAlgorthm(Convert.ToDouble(GetFormOptions().SimilarityThreshold));
//                };

//                _reduceSetsWorker.RunWorkerCompleted += (o, args) =>
//                {
//                    Messaging.Talk("Finished reducing sets...");
//                    FillImageGroupsListBox(_imageStatService.ImageSets.Cast<object>().ToArray());
//                };

//                _reduceSetsWorker.RunWorkerAsync();
//            }
//        }

//        private void UpdateImageSetsText()
//        {
//            imageSetsGroupBox.Text = $"{imageSetsListbox.Items.Count} Sets ";
//        }

//        private void imageSetsListbox_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            var list = (ListBox) sender;
//            if (list.SelectedItem == null)
//                return;

//            var selected = (ImageStatGroup) list.SelectedItem;

//            _mainFormController.RenderGrid(imagesGridView, selected, GetFormOptions(), _mainFormController);
//        }

//        private void imagesGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
//        {
//            if (imagesGridView.CurrentCell != null)
//            {
//                var cell = (DataGridViewImageCell) imagesGridView.CurrentCell;
//                if (cell.Tag != null)
//                {
//                    var stat = (ImageStat) cell.Tag;
//                    var finfo = new FileInfo(stat.ImagePath);

//                    stat.DateCreated = finfo.CreationTime;
//                    stat.DateModified = finfo.LastWriteTime;

//                    imageDetailPropertyGrid.SelectedObject = stat;
//                }

//                HackPropertyGrid();
//            }
//        }

//        private void isDuplicateButton_Click(object sender, EventArgs e)
//        {
//            if (imagesGridView.SelectedCells.Count > 0)
//                foreach (DataGridViewImageCell selectedCell in imagesGridView.SelectedCells)
//                {
//                    var data = _dataGridViewService.GetActiveCellData(selectedCell);
//                    if (data != null)
//                    {
//                        data.IsDuplicate = 1;
//                        _mainFormController.SetDuplicateValue(data.Id, true);
//                        _dataGridViewService.SetDuplicateDisplay(selectedCell);
//                        Messaging.Talk($"{data.ImagePath} marked as Duplicate");
//                    }
//                }
//        }

//        private void clearDuplicateButton_Click(object sender, EventArgs e)
//        {
//            foreach (DataGridViewImageCell selectedCell in imagesGridView.SelectedCells)
//            {
//                var data = _dataGridViewService.GetActiveCellData(selectedCell);
//                if (data != null)
//                {
//                    data.IsDuplicate = 0;
//                    _mainFormController.SetDuplicateValue(data.Id, false);
//                    _dataGridViewService.SetNotDuplicateDisplay(selectedCell);
//                    Messaging.Talk($"{data.ImagePath} not a dulplicate");
//                }
//            }
//        }

//        private void moveDuplicatesButton_Click(object sender, EventArgs e)
//        {
//            if (!confirmCleanupCheckbox.Checked)
//                return;

//            _mainFormController.MoveDuplicates(GetFormOptions());
//        }

//        private void Form1_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.A)
//                if (imageSetsListbox.SelectedIndex > 0)
//                    imageSetsListbox.SelectedIndex = imageSetsListbox.SelectedIndex - 1;

//            //if (e.KeyCode == Keys.S)
//            //    if (imageSetsListbox.SelectedIndex < imageSetsListbox.Items.Count - 1)
//            //        imageSetsListbox.SelectedIndex = imageSetsListbox.SelectedIndex + 1;

//            if (e.KeyCode == Keys.D)
//                isDuplicateButton.PerformClick();

//            e.Handled = true;
//        }

//        private void cleanMissingButton_Click(object sender, EventArgs e)
//        {
//            RunCleanMissingWorker();
//        }

//        private void RunCleanMissingWorker()
//        {
//            using (_cleanMissingWorker = new BackgroundWorker())
//            {
//                _cleanMissingWorker.DoWork +=
//                    (o, args) => { _mainFormController.CleanMissingFilesFromDatabase(GetFormOptions()); };
//                _cleanMissingWorker.RunWorkerCompleted +=
//                    (o, args) => { Messaging.Talk("Finished cleaning missing files from database..."); };
//                _cleanMissingWorker.RunWorkerAsync();
//            }
//        }

//        private void extraSetAnalysisCheckbox_CheckedChanged(object sender, EventArgs e)
//        {
//            var checkbox = (CheckBox) sender;
//            similarityThresholdnumericUpDown.Enabled = checkbox.Checked;
//        }

//        private void imagesGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
//        {
//            if (imagesGridView.CurrentCell != null)
//            {
//                var cell = (DataGridViewImageCell) imagesGridView.CurrentCell;
//                if (cell.Tag != null)
//                {
//                    var stat = (ImageStat) cell.Tag;
//                    var finfo = new FileInfo(stat.ImagePath);
//                    if (finfo.Exists)
//                        Process.Start(finfo.FullName);
//                }
//            }
//        }

//        private void cleanSetsButton_Click(object sender, EventArgs e)
//        {
//            RunCleanStatsWorker();
//        }

//        private void FillImageGroupsListBox(object[] items)
//        {
//            imageSetsListbox.Items.Clear();
//            imageSetsListbox.Items.AddRange(items);
//            UpdateImageSetsText();
//        }
//    }
//}