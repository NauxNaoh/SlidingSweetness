namespace N.Utils
{
    public static class MathfExtensions
    {
        /// <summary>
        /// Loops an integer value within a specified range.
        /// If the result exceeds the maximum, it wraps around to the minimum, and vice versa.
        /// Useful for rotating indices, looping through arrays, or any cyclic numerical logic.
        /// </summary>
        /// <param name="curValue">The current integer value.</param>
        /// <param name="delta">The amount to add or subtract.</param>
        /// <param name="min">The minimum value of the range.</param>
        /// <param name="max">The maximum value of the range.</param>
        /// <returns>The new value, looped within the range [min, max].</returns>

        public static int LoopIndex(this int curValue, int delta, int min, int max)
        {
            int range = max - min + 1;
            int raw = curValue - min + delta;

            return ((raw % range + range) % range) + min;
        }
    }
}
