using System;

namespace SimilarImage.Models
{
    public class ProgressBarEventArgs : EventArgs
    {
        public EventKind EventKindOf { get; set; }
        public int CurrentValue { get; set; }
        public string Text { get; set; }
    }
}