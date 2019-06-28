using System;

namespace SimilarImage.Models
{
    public class TalkEventArgs : EventArgs
    {
        public TalkEventArgs()
        {
            Words = string.Empty;
        }

        public string Words { get; set; }
    }
}