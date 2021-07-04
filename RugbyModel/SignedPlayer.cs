using System;
using System.Text.RegularExpressions;

namespace RugbyModel
{
    /// <summary>
    /// This class represents the core abstraction for a Signed Player. Data members are defined as
    /// properties. Static methods are provided for file parsing. ICloneable is implemented so that
    /// deep cloning can occur in the Controller.
    /// </summary>
    /// <remarks>
    /// <para>Author: Damian Coventry</para>
    /// <para>Date: Feb-May 2021</para>
    /// </remarks>
    public class SignedPlayer : ICloneable
    {
        private static readonly Regex _fileFormatRegex = new Regex(@"^(.+);(.+)$", RegexOptions.Compiled);
        private const int _expectedFieldCountWithinFile = 2;

        /// <summary>
        /// The unique ID of the player
        /// </summary>
        public int PlayerId { get; set; }
        /// <summary>
        /// The friendly name of the player
        /// </summary>
        public string PlayerName { get; set; }
        /// <summary>
        /// The unique name of the team
        /// </summary>
        public string TeamName { get; set; }
        /// <summary>
        /// A string suitable to display on the GUI
        /// </summary>
        public string DisplayName { get { return $"{PlayerName} ({PlayerId}, {TeamName})"; } }

        /// <summary>
        /// Performs a deep clone of the object's state
        /// </summary>
        /// <returns>A new object with its own copy of this object's data</returns>
        public object Clone()
        {
            var clone = (SignedPlayer)MemberwiseClone();
            clone.PlayerName = string.Copy(PlayerName);
            clone.TeamName = string.Copy(TeamName);
            return clone;
        }

        /// <summary>
        /// Returns whether or the passed string represents valid state for a SignedPlayer
        /// </summary>
        /// <param name="line">A line of text</param>
        /// <returns>True if the line represents valid state for a Player, false otherwise</returns>
        public static bool IsValidFileString(string line)
        {
            if (string.IsNullOrEmpty(line))
                return false;
            var match = _fileFormatRegex.Match(line);
            return match.Groups.Count == _expectedFieldCountWithinFile + 1 &&
                   int.TryParse(match.Groups[1].ToString().Trim(), out _); // Reject non-integer data
        }

        /// <summary>
        /// Attempts to extract state from the passed line of text, then create a new SignedPlayer
        /// object with that state.
        /// </summary>
        /// <param name="line">A line of text</param>
        /// <returns>A new SignedPlayer object</returns>
        public static SignedPlayer FromFileString(string line)
        {
            if (string.IsNullOrEmpty(line))
                throw new ArgumentNullException(nameof(line));

            var match = _fileFormatRegex.Match(line);
            if (match.Groups.Count != _expectedFieldCountWithinFile + 1) // A simple quick rejection
                BaseFile.ThrowUnrecognisableDataFormat();

            if (!int.TryParse(match.Groups[1].ToString().Trim(), out int playerId)) // Reject non-integer data
                BaseFile.ThrowUnrecognisableDataFormat();

            return new SignedPlayer()
            {
                PlayerId = playerId,
                TeamName = match.Groups[2].ToString().Trim()
            };
        }

        /// <summary>
        /// Builds a string as the well-known format for a SignedPlayer. The string is suitable for file I/O.
        /// </summary>
        /// <returns>A string formatted to a well-known format</returns>
        public string ToFileString()
        {
            return $"{PlayerId}; {TeamName}";
        }
    }
}
