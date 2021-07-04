using System;
using System.Text.RegularExpressions;

namespace RugbyModel
{
    /// <summary>
    /// This class represents the core abstraction for a Team. Data members are defined as properties. Static
    /// methods are provided for file parsing. ICloneable is implemented so that deep cloning can occur in the
    /// Controller.
    /// </summary>
    /// <remarks>
    /// <para>Author: Damian Coventry</para>
    /// <para>Date: Feb-May 2021</para>
    /// </remarks>
    public class Team : ICloneable
    {
        private static readonly Regex _fileFormatRegex = new Regex(@"^(.+);(.+);(.+);(.+)$", RegexOptions.Compiled);
        private static readonly Regex _foundedFieldRegex = new Regex(@"Founded (.+?),(.+)$", RegexOptions.Compiled);
        private const int _expectedFieldCountWithinFile = 4;
        private const int _expectedYearFoundedCountWithinField = 2;

        /// <summary>
        /// The lower end of the acceptable year range
        /// </summary>
        public const int MinYearFounded = 1000;
        /// <summary>
        /// The higher end of the acceptable year range
        /// </summary>
        public const int MaxYearFounded = 3000;

        /// <summary>
        /// The name of the team. It is unique amongst its peers
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The name of the Home Ground
        /// </summary>
        public string HomeGround { get; set; }
        /// <summary>
        /// The fulul name of the Coach
        /// </summary>
        public string Coach { get; set; }
        /// <summary>
        /// The name of the Region the team represents
        /// </summary>
        public string Region { get; set; }
        /// <summary>
        /// The year in which this team was founded
        /// </summary>
        public int YearFounded { get; set; }

        /// <summary>
        /// Performs a deep clone of the object's state
        /// </summary>
        /// <returns>A new object with its own copy of this object's data</returns>
        public object Clone()
        {
            var clone = (Team)MemberwiseClone();
            clone.Name = string.Copy(Name);
            clone.HomeGround = string.Copy(HomeGround);
            clone.Coach = string.Copy(Coach);
            clone.Region = string.Copy(Region);
            return clone;
        }

        /// <summary>
        /// Returns whether or the passed string represents valid state for a Team
        /// </summary>
        /// <param name="line">A line of text</param>
        /// <returns>True if the line represents valid state for a Team, false otherwise</returns>
        public static bool IsValidFileString(string line)
        {
            if (string.IsNullOrEmpty(line))
                return false;
            var match = _fileFormatRegex.Match(line);
            if (match.Groups.Count != _expectedFieldCountWithinFile + 1) // The easy path
                return false;
            var foundedMatch = _foundedFieldRegex.Match(match.Groups[4].ToString().Trim()); // The year founded requires another pass
            return foundedMatch.Groups.Count == _expectedYearFoundedCountWithinField + 1 &&
                int.TryParse(foundedMatch.Groups[1].ToString().Trim(), out int _); // Reject non-integer data
        }

        /// <summary>
        /// Attempts to extract state from the passed line of text, then create a new Team
        /// object with that state.
        /// </summary>
        /// <param name="line">A line of text</param>
        /// <returns>A new Team object</returns>
        public static Team FromFileString(string line)
        {
            if (string.IsNullOrEmpty(line))
                throw new ArgumentNullException(nameof(line));

            var match = _fileFormatRegex.Match(line);
            if (match.Groups.Count != _expectedFieldCountWithinFile + 1) // A simple quick rejection
                BaseFile.ThrowUnrecognisableDataFormat();

            var team = new Team()
            {
                Name = match.Groups[1].ToString().Trim(),
                HomeGround = match.Groups[2].ToString().Trim(),
                Coach = match.Groups[3].ToString().Trim(),
            };

            var foundedMatch = _foundedFieldRegex.Match(match.Groups[4].ToString().Trim());
            if (foundedMatch.Groups.Count != _expectedYearFoundedCountWithinField + 1) // A simple quick rejection
                BaseFile.ThrowUnrecognisableDataFormat();

            if (!int.TryParse(foundedMatch.Groups[1].ToString().Trim(), out int year)) // Reject non-integer data
                BaseFile.ThrowUnrecognisableDataFormat();

            team.YearFounded = Math.Min(Math.Max(year, MinYearFounded), MaxYearFounded); // Clamp to our expected range
            team.Region = foundedMatch.Groups[2].ToString().Trim();

            return team;
        }

        /// <summary>
        /// Builds a string as the well-known format for a Team. The string is suitable for file I/O.
        /// </summary>
        /// <returns>A string formatted to a well-known format</returns>
        public string ToFileString()
        {
            return $"{Name}; {HomeGround}; {Coach}; Founded {YearFounded}, {Region}";
        }
    }
}
