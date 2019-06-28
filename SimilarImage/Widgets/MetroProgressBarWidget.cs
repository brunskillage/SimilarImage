using MetroFramework.Controls;

namespace SimilarImage.Widgets
{
    public class MetroProgressBarWidget
    {
        private readonly MetroProgressBar _barControl;
        private readonly MetroLabel _titleControl;
        private readonly MetroLabel _statusControl;
        private int ErrorCount { get; set; }

        public MetroProgressBarWidget(MetroProgressBar barControl, MetroLabel titleControl, MetroLabel statusControl)
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