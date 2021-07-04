using System;
using System.Text.RegularExpressions;

namespace RugbyModel
{
    /// <summary>
    /// This class represents the core abstraction for a Player. Data members are defined as properties. Static
    /// methods are provided for file parsing. ICloneable is implemented so that deep cloning can occur in the
    /// Controller.
    /// </summary>
    /// <remarks>
    /// <para>Author: Damian Coventry</para>
    /// <para>Date: Feb-May 2021</para>
    /// </remarks>
    public class Player : ICloneable
    {
        private static readonly Regex _fileFormatRegex = new Regex(@"^(.+);(.+);(.+);(.+);(.+);(.+);(.+)$", RegexOptions.Compiled);
        private const int _expectedFieldCountWithinFile = 7;

        /// <summary>
        /// The lower end of the acceptable height range
        /// </summary>
        public const int MinHeight = 150;
        /// <summary>
        /// The higher end of the acceptable height range
        /// </summary>
        public const int MaxHeight = 300;
        /// <summary>
        /// The lower end of the acceptable weight range
        /// </summary>
        public const int MinWeight = 70;
        /// <summary>
        /// The higher end of the acceptable weight range
        /// </summary>
        public const int MaxWeight = 180;
        /// <summary>
        /// The lower end of the acceptable age range
        /// </summary>
        public const int MinAge = 18;
        /// <summary>
        /// The higher end of the acceptable age range
        /// </summary>
        public const int MaxAge = 40;

        /// <summary>
        /// The unique ID of the player
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The first name of the player
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// The last name of the player
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// The friendly name of the player
        /// </summary>
        public string DisplayName { get { return $"{FirstName} {LastName}"; } }
        /// <summary>
        /// The height of the player
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// The weight of the player
        /// </summary>
        public int Weight { get; set; }
        /// <summary>
        /// The date of birth of the player
        /// </summary>
        public DateTime DateOfBirth { get; set; }
        /// <summary>
        /// The place of birth of the player
        /// </summary>
        public string PlaceOfBirth { get; set; }

        /// <summary>
        /// Performs a deep clone of the object's state
        /// </summary>
        /// <returns>A new object with its own copy of this object's data</returns>
        public object Clone()
        {
            var clone = (Player)MemberwiseClone();
            clone.FirstName = string.Copy(FirstName);
            clone.LastName = string.Copy(LastName);
            clone.PlaceOfBirth = string.Copy(PlaceOfBirth);
            return clone;
        }

        /// <summary>
        /// Returns whether or the passed string represents valid state for a Player
        /// </summary>
        /// <param name="line">A line of text</param>
        /// <returns>True if the line represents valid state for a Player, false otherwise</returns>
        public static bool IsValidFileString(string line)
        {
            if (string.IsNullOrEmpty(line))
                return false;
            var match = _fileFormatRegex.Match(line);
            return match.Groups.Count == _expectedFieldCountWithinFile + 1 &&
                   int.TryParse(match.Groups[1].ToString().Trim(), out _) && // Reject non-integer data
                   DateTime.TryParse(match.Groups[4].ToString().Trim(), out _) && // Reject non-date data
                   int.TryParse(match.Groups[5].ToString().Trim(), out _) && // Reject non-integer data
                   int.TryParse(match.Groups[6].ToString().Trim(), out _); // Reject non-integer data
        }

        /// <summary>
        /// Attempts to extract state from the passed line of text, then create a new Player
        /// object with that state.
        /// </summary>
        /// <param name="line">A line of text</param>
        /// <returns>A new Player object</returns>
        public static Player FromFileString(string line)
        {
            if (string.IsNullOrEmpty(line))
                throw new ArgumentNullException(nameof(line));

            var match = _fileFormatRegex.Match(line);
            if (match.Groups.Count != _expectedFieldCountWithinFile + 1) // A simple quick rejection
                BaseFile.ThrowUnrecognisableDataFormat();

            if (!int.TryParse(match.Groups[1].ToString().Trim(), out int id)) // Reject non-integer data
                BaseFile.ThrowUnrecognisableDataFormat();

            if (!DateTime.TryParse(match.Groups[4].ToString().Trim(), out DateTime dateOfBirth)) // Reject non-date data
                BaseFile.ThrowUnrecognisableDataFormat();

            if (!int.TryParse(match.Groups[5].ToString().Trim(), out int height)) // Reject non-integer data
                BaseFile.ThrowUnrecognisableDataFormat();

            if (!int.TryParse(match.Groups[6].ToString().Trim(), out int weight)) // Reject non-integer data
                BaseFile.ThrowUnrecognisableDataFormat();

            var now = DateTime.Now;
            var age = Utility.DateDiffAsYears(dateOfBirth, now);
            if (age < MinAge)
                dateOfBirth = now.AddYears(-MinAge).Date; // Clamp to our expected range
            else if (age > MaxAge)
                dateOfBirth = now.AddYears(-MaxAge).Date; // Clamp to our expected range

            return new Player()
            {
                Id = id,
                FirstName = match.Groups[2].ToString().Trim(),
                LastName = match.Groups[3].ToString().Trim(),
                DateOfBirth = dateOfBirth,
                Height = Math.Min(Math.Max(height, MinHeight), MaxHeight), // Clamp to our expected range
                Weight = Math.Min(Math.Max(weight, MinWeight), MaxWeight), // Clamp to our expected range
                PlaceOfBirth = match.Groups[7].ToString().Trim(),
            };
        }

        /// <summary>
        /// Builds a string as the well-known format for a Player. The string is suitable for file I/O.
        /// </summary>
        /// <returns>A string formatted to a well-known format</returns>
        public string ToFileString()
        {
            return $"{Id}; {FirstName}; {LastName}; {DateOfBirth.ToShortDateString()}; {Height}; {Weight}; {PlaceOfBirth}";
        }
    }
}
