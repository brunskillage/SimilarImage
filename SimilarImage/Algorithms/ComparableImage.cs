using System;
using System.Drawing;
using System.IO;

namespace SimilarImage.Algorithms
{
    public class ComparableImage
    {
        public ComparableImage(FileInfo file)
        {
            if (file == null)
            {
                throw new ArgumentNullException("file");
            }

            if (!file.Exists)
            {
                throw new FileNotFoundException();
            }

            File = file;

            using (var bitmap = ImageUtility.ResizeBitmap(new Bitmap(file.FullName), 100, 100))
            {
                Projections = new RgbProjections(ImageUtility.GetRgbProjections(bitmap));
            }
        }

        // AB this is prefered becuase there were unknown memory issues using the other constructor. 
        // I used aforge resize - dospsed of memory bettter/ more often
        public ComparableImage(Bitmap bitmap)
        {
            if (bitmap == null)
                throw new ArgumentNullException("bitmap");
            Projections = new RgbProjections(ImageUtility.GetRgbProjections(bitmap));
            bitmap.Dispose();
        }

        public FileInfo File { get; }
        public RgbProjections Projections { get; }

        /// <summary>
        ///     Calculate the similarity to another image.
        /// </summary>
        /// <param name="compare">The image to compare with.</param>
        /// <returns>Return a value from 0 to 1 that is the similarity.</returns>
        public double CalculateSimilarity(ComparableImage compare)
        {
            return Projections.CalculateSimilarity(compare.Projections);
        }

        public override string ToString()
        {
            return File.Name;
        }
    }
}