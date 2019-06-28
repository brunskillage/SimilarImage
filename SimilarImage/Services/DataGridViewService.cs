using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SimilarImage.Controllers;
using SimilarImage.Events;
using SimilarImage.Models;

namespace SimilarImage.Services
{
    public class DataGridViewService
    {
        private readonly DatabaseService _databaseService;
        private readonly ImageStatService _imageStatService;
        private readonly ConfigService _configService;
        private readonly ImageService _imageService;

        public DataGridViewService(DatabaseService databaseService, ImageStatService imageStatService, ConfigService configService, ImageService imageService)
        {
            _databaseService = databaseService;
            _imageStatService = imageStatService;
            _configService = configService;
            _imageService = imageService;
        }

        public void RenderGrid(DataGridView view, ImageStatGroup statGroup, FormOptions options,
            MainFormController mainFormController)
        {
            // mark identical
            Messaging.Talk($"Looking for binary identical files...");

            view.SuspendLayout();

            if (options.ExtraSetAnalysis)
            {
                statGroup.ImageStats = _imageStatService.RemoveSimilar(statGroup, (double) options.SimilarityThreshold);
            }

            // add columns
            var requiredColumnsTotal = CalculateRequiredColumns(view);
            AddColumns(view, requiredColumnsTotal);

            //add rows
            var requiredRowsTotal = CalculateRequiredRows(requiredColumnsTotal, statGroup.ImageStats.Count);
            AddRows(view, requiredRowsTotal);

            // remove invalid
            RemoveStatsWithNoFile(statGroup.ImageStats, mainFormController);

            // prepare dataset
            var ordered = statGroup.ImageStats.OrderByDescending(i => i.Length).ToList();

            statGroup.ImageStats = ordered;


            // the current stat result
            var currentResultIndex = 0;

            // render grid iterating throught the rows
            foreach (DataGridViewRow row in view.Rows)
            foreach (DataGridViewColumn column in view.Columns)
            {
                var cell = row.Cells[column.Index] as DataGridViewImageCell;
                cell.Value = null;

                try
                {
                    // only render while we are processing images
                    if (currentResultIndex <= statGroup.ImageStats.Count - 1)
                    {
                        var stat = statGroup.ImageStats[currentResultIndex];

                        cell.Value = _imageService.GetThumb(stat.ImagePath, _configService.ThumbMaxHeight);

                        if (stat.IsDuplicate == 1)
                            SetDuplicateDisplay(cell);
                        else
                            SetNotDuplicateDisplay(cell);

                        cell.Tag = stat;
                        cell.Description = stat.ImagePath;

                        if (currentResultIndex == statGroup.ImageStats.Count - 1)
                        {
                                view.ClearSelection();
                                cell.Selected = true;
                        }
                            
                    }
                }
                finally
                {
                    cell.DataGridView.InvalidateCell(cell);
                    currentResultIndex += 1;
                    view.ResumeLayout();
                }
            }

            //GC.Collect();
            //GC.WaitForPendingFinalizers();
            //GC.Collect();

            view.Invalidate();
        }

        private void RemoveStatsWithNoFile(List<ImageStat> imageStats, MainFormController mainFormController)
        {
            foreach (var stat in imageStats)
                if (!File.Exists(stat.ImagePath))
                {
                    Messaging.Talk($"No physical file {stat.ImagePath}...");
                    mainFormController.DeleteImageStatById(stat.Id);
                    imageStats = imageStats.Where(s => s.Id != stat.Id).ToList();
                }
        }

        private void AddRows(DataGridView view, decimal requiredRowsTotal)
        {
            view.Rows.Clear();
            view.AllowUserToAddRows = true;
            view.DefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Padding = new Padding(5)
                
            };

            var cell = new DataGridViewImageCell();
            
            for (var i = 0; i < requiredRowsTotal; i++)
            {

                var rowCells =
                    Enumerable.Range(0, (int) requiredRowsTotal).Select(r => cell).ToArray();

                if (i > 0)
                    view.Rows.Add(rowCells);

                for (var j = 0; j < view.RowCount; j++)
                    view.Rows[j].Height = _configService.ThumbMaxHeight;
            }
        }

        private decimal CalculateRequiredRows(double columnCount, int imageStatCount)
        {
            var rows = Math.Ceiling(imageStatCount / (decimal) columnCount);
            return rows;
        }

        private void AddColumns(DataGridView view, double columns)
        {
            view.Columns.Clear();
            for (var i = 0; i < columns; i++)
            {
                var column = new DataGridViewImageColumn();
                column.Name = "Col" + i;
                column.HeaderText = "Col" + i;
                column.Width = _configService.ThumbMaxHeight;
                column.DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.White,
                    SelectionBackColor = Color.Cornsilk,
                    NullValue = null
                };
                view.Columns.Add(column);
            }
        }

        private double CalculateRequiredColumns(DataGridView view)
        {
            double columns = (int) Math.Floor((double) view.Width / _configService.ThumbMaxHeight);
            return columns;
        }

        public ImageStat GetActiveCellData(DataGridViewImageCell cell)
        {
            var data = cell?.Tag as ImageStat;
            return data;
        }

        public void SetDuplicateDisplay(DataGridViewImageCell cell)
        {
            var bm = (Bitmap) cell.Value;
            ImageService.MakeImageAsDuplicate(bm);
            cell.Value = bm;
            cell.DataGridView.InvalidateCell(cell);
        }

        public void SetNotDuplicateDisplay(DataGridViewImageCell cell)
        {
            cell.Style.BackColor = Color.White;
            cell.DataGridView.InvalidateCell(cell);
            cell.ImageLayout = DataGridViewImageCellLayout.Normal;

            
            var data = GetActiveCellData(cell);
            if (data != null)
                cell.Value = _imageService.GetThumb(data.ImagePath, _configService.ThumbMaxHeight);
            cell.DataGridView.InvalidateCell(cell);
        }
    }
}