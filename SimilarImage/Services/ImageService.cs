using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using AForge.Imaging.Filters;
using Image = AForge.Imaging.Image;

namespace SimilarImage.Services
{
    public class ImageService
    {
        private readonly ConfigService _configService;

        public ImageService(ConfigService configService)
        {
            _configService = configService;
        }


        public Bitmap GetThumb(string path, int maxSize)
        {
            using (var original = Image.FromFile(path))
            {
                //if (original.Width <= maxSize && original.Height < maxSize)
                //    return original;

                // Figure out the ratio
                var ratioX = maxSize/(double) original.Width;
                var ratioY = maxSize/(double) original.Height;
                // use whichever multiplier is smaller
                var ratio = ratioX < ratioY ? ratioX : ratioY;

                // now we can get the new height and width
                var newHeight = Convert.ToInt32(original.Height*ratio);
                var newWidth = Convert.ToInt32(original.Width*ratio);

                // create filter
                var filter = new ResizeBilinear(newWidth, newHeight);
                // apply the filter
                var newImage = filter.Apply(original);

                return newImage;
            }
        }

        public Bitmap GetThumb(string path, int width, int height)
        {
            using (var original = Image.FromFile(path))
            {
                // create filter
                var filter = new ResizeBilinear(width, height);
                // apply the filter
                var newImage = filter.Apply(original);

                return newImage;
            }
        }

        public Bitmap GetMissingThumb()
        {
            using (var bm = new Bitmap(_configService.ThumbMaxHeight, _configService.ThumbMaxHeight))
            using (var g = Graphics.FromImage(bm))
            {
                var rectf = new RectangleF(0, 0, _configService.ThumbMaxHeight, _configService.ThumbMaxHeight);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.FillRectangle(new SolidBrush(Color.Red), rectf);
                g.Flush();
                return bm;
            }
        }

        public static void MakeImageAsDuplicate(Bitmap bm)
        {
            using (var g = Graphics.FromImage(bm))
            {
                var rectf = new RectangleF(bm.Width - 10, 6, 6, 6);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.FillRectangle(new SolidBrush(Color.Red), rectf);
                g.Flush();
            }
        }
    }
}