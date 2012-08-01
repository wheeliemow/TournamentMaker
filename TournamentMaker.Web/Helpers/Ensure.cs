using System;

namespace TournamentReport
{
    /// <summary>
    ///   Ensure input parameters
    /// </summary>
    public static class Ensure
    {
        /// <summary>
        ///   Checks an argument to ensure it isn't null.
        /// </summary>
        /// <param name = "value">The argument value to check.</param>
        /// <param name = "name">The name of the argument.</param>
        public static void ArgumentNotNull([ValidatedNotNull] object value, string name)
        {
            if (value != null) return;

            throw new ArgumentNullException(name);
        }

        public static void ArgumentNonNegative(int value, string name)
        {
            if (value > -1) return;

            throw new ArgumentException("The argument must be non-negative", name);
        }

        /// <summary>
        ///   Checks a string argument to ensure it isn't null or empty.
        /// </summary>
        /// <param name = "value">The argument value to check.</param>
        /// <param name = "name">The name of the argument.</param>
        public static void ArgumentNotNullOrEmptyString([ValidatedNotNull] string value, string name)
        {
            ArgumentNotNull(value, name);
            if (!string.IsNullOrWhiteSpace(value)) return;

            throw new ArgumentException("String cannot be empty", name);
        }
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class ValidatedNotNullAttribute : Attribute
    {
    }
}