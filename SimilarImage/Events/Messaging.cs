using System;
using SimilarImage.Models;

namespace SimilarImage.Events
{
    //http://csharpindepth.com/Articles/Chapter2/Events.aspx
    public static class Messaging
    {
        private static readonly object eLock = new object();
        public static event EventHandler OnTalk;
        public static event EventHandler OnProgress;

        private static void RaiseTalk(object sender, TalkEventArgs args)
        {
            var evt = OnTalk;
            evt?.Invoke(sender, args);
        }

        public static void RaiseProgress(object sender, ProgressBarEventArgs args)
        {
            var evt = OnProgress;
            evt?.Invoke(sender, args);
        }

        public static void Talk(string words)
        {
            if (string.IsNullOrWhiteSpace(words))
                return;

            if (words.Length > 100)
                words = "..." + words.Substring(words.Length - 100);

            RaiseTalk(null, new TalkEventArgs {Words = words});
        }
    }
}