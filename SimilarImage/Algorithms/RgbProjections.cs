using System;
using System.Collections.Generic;
using System.Linq;

namespace SimilarImage.Algorithms
{
    public class RgbProjections
    {
        internal RgbProjections(double[][] projections)
            : this(projections[0], projections[1])
        {
        }

        internal RgbProjections(double[] horizontalProjection, double[] verticalProjection)
        {
            HorizontalProjection = horizontalProjection;
            VerticalProjection = verticalProjection;
        }

        public double[] HorizontalProjection { get; }
        public double[] VerticalProjection { get; }

        /// <summary>
        ///     Calculate the similarity between two RGB projections, horizontal and vertical.
        /// </summary>
        /// <param name="compare">The RGB projection to compare with.</param>
        /// <returns>Return the max similarity value betweem horizontal and vertical RGB projections.</returns>
        public double CalculateSimilarity(RgbProjections compare)
        {
            var horizontalSimilarity = CalculateProjectionSimilarity(HorizontalProjection, compare.HorizontalProjection);
            var verticalSimilarity = CalculateProjectionSimilarity(VerticalProjection, compare.VerticalProjection);
            return Math.Max(horizontalSimilarity, verticalSimilarity);
        }

        /// <summary>
        ///     Calculate the similarity to another RGB projection.
        /// </summary>
        /// <param name="source">The source RGB projection.</param>
        /// <param name="compare">The RGB projection to compare with.</param>
        /// <returns>Return a value from 0 to 1 that is the similarity.</returns>
        private static double CalculateProjectionSimilarity(double[] source, double[] compare)
        {
            if (source.Length != compare.Length)
            {
                throw new ArgumentException();
            }

            var frequencies = new Dictionary<double, int>();

            ////Calculate frequencies
            for (var i = 0; i < source.Length; i++)
            {
                var difference = source[i] - compare[i];
                difference = Math.Round(difference, 2);
                difference = Math.Abs(difference);
                if (frequencies.ContainsKey(difference))
                {
                    frequencies[difference] = frequencies[difference] + 1;
                }
                else
                {
                    frequencies.Add(difference, 1);
                }
            }

            var deviation = frequencies.Sum(value => (value.Key*value.Value));

            ////Calculate "weighted mean"
            ////http://en.wikipedia.org/wiki/Weighted_mean
            deviation /= source.Length;

            ////Maximize scale
            deviation = (0.5 - deviation)*2;

            return deviation;
        }
    }
}