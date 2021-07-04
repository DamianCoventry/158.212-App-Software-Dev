using System;

namespace RugbyModel
{
    /// <summary>
    /// A location to group utility functionality.
    /// </summary>
    /// <remarks>
    /// <para>Author: Damian Coventry</para>
    /// <para>Date: Feb-May 2021</para>
    /// </remarks>
    public static class Utility
    {
        /// <summary>
        /// Calculates the difference between two DateTime objects as units of Years.
        /// Unit tests are here: RugbyModel.Test\DateDiffAsYearsTest.cs
        /// </summary>
        /// <param name="oldestDate">A date from the past</param>
        /// <param name="now">The current date</param>
        /// <returns>The number of years between the two dates</returns>
        public static int DateDiffAsYears(DateTime oldestDate, DateTime now)
        {
            return (now.Year - oldestDate.Year - 1) +
                   (((now.Month > oldestDate.Month) ||
                   ((now.Month == oldestDate.Month) && (now.Day >= oldestDate.Day))) ? 1 : 0);
        }
    }
}
