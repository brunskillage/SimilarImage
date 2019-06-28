using System.Windows.Forms;

namespace SimilarImage.Widgets
{
    public class ProgressBarWidget
    {
        private readonly ProgressBar _barControl;
        private readonly GroupBox _titleControl;
        private readonly Label _statusControl;
        private int ErrorCount { get; set; }

        public ProgressBarWidget(ProgressBar barControl, GroupBox titleControl, Label statusControl)
        {
            _barControl = barControl;
            _titleControl = titleControl;
            _statusControl = statusControl;
        }

        public void Reset(int max, string title)
        {
            _titleControl.Text = title;
            _barControl.Maximum = max;
            _barControl.Value = 0;
            _barControl.Step = 1;
            ErrorCount = 0;
        }

        public void SetTitle(string title)
        {
            _titleControl.Text = title;
        }

        public void SetStatus(string text)
        {
            _statusControl.Text = text;
        }

        public void Increment()
        {
            _barControl.Value += 1;

            _statusControl.Text = GetPercent(_barControl.Value, _barControl.Maximum).ToString("F1") + "% - " +
                                  _barControl.Value + " of " + _barControl.Maximum + " with " +
                                  ErrorCount + " Errors";
        }

        public void IncrementError()
        {
            ErrorCount += 1;
        }

        public decimal GetPercent(decimal current, decimal total)
        {
            if (current == 0 || total == 0)
                return 0;           

            return current / total * 100;
        }
    }
}

//if (progressBar1.Maximum == 0)
//    progressBar1.Maximum = (int)counter.CounterServiceObj.Total;

//progressBar1.Value = counter.CounterServiceObj.Current;
//progressLabel.Text = counter.CounterServiceObj.GetPercent().ToString("F1") + "% - " +
//                     counter.CounterServiceObj.Current + " of " + counter.CounterServiceObj.Total + " with " +
//                     counter.CounterServiceObj.Errors + " Errors";
