using System;
using System.Collections.Generic;

namespace SimilarImage.Algorithms
{
    /// <summary>
    ///     Represents a comparable images class.
    /// </summary>
    public class SimilarityImages : IComparer<SimilarityImages>, IComparable
    {
        private readonly double similarity;

        public SimilarityImages(ComparableImage source, ComparableImage destination, double similarity)
        {
            Source = source;
            Destination = destination;
            this.similarity = similarity;
        }

        public ComparableImage Source { get; }
        public ComparableImage Destination { get; }

        public double Similarity
        {
            get { return Math.Round(similarity*100, 1); }
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            var other = (SimilarityImages) obj;
            return Compare(this, other);
        }

        #endregion IComparable Members

        #region IComparer<SimilarityImages> Members

        public int Compare(SimilarityImages x, SimilarityImages y)
        {
            return x.similarity.CompareTo(y.similarity);
        }

        #endregion IComparer<SimilarityImages> Members

        public static int operator !=(SimilarityImages value, SimilarityImages compare)
        {
            return value.CompareTo(compare);
        }

        public static int operator <(SimilarityImages value, SimilarityImages compare)
        {
            return value.CompareTo(compare);
        }

        public static int operator ==(SimilarityImages value, SimilarityImages compare)
        {
            return value.CompareTo(compare);
        }

        public static int operator >(SimilarityImages value, SimilarityImages compare)
        {
            return value.CompareTo(compare);
        }

        public override string ToString()
        {
            return string.Format("{0}, {1} --> {2}", Source.File.Name, Destination.File.Name, similarity);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (SimilarityImages) obj;

            var equals = Source.File.FullName.Equals(other.Source.File.FullName,
                StringComparison.InvariantCultureIgnoreCase);

            if (!equals)
            {
                return false;
            }

            equals = Destination.File.FullName.Equals(other.Destination.File.FullName,
                StringComparison.InvariantCultureIgnoreCase);

            if (!equals)
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return string.Format("{0};{1}", Source.File.FullName, Destination.File.FullName).GetHashCode();
        }
    }
}