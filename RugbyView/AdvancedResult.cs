using System;
using System.Collections.Generic;

namespace RugbyView
{
    /// <summary>
    /// This class holds state for a field whose value has been replaced by the find/replace mechanism. The difference between
    /// this class and the ReplaceResult class is that this class contains contains a list of fields and values. The ReplaceResult
    /// class contains a single field + value.
    /// </summary>
    /// <remarks>
    /// <para>Author: Damian Coventry</para>
    /// <para>Date: Feb-May 2021</para>
    /// </remarks>
    public class AdvancedResult
    {
        /// <summary>
        /// A reference to the object that has had a field value replaced. This will be a Team, Player, or SignedPlayer
        /// </summary>
        public object Item { get; set; }
        /// <summary>
        /// A list of fields and values that caused the search operation to make a match. This represents a logical AND, that is,
        /// Fields[0] matched all of the search criteria, AND Fields[1] matched all of the search criteria, AND so on...
        /// </summary>
        public List<Tuple<string, string>> Fields { get; set; }
        /// <summary>
        /// The name of the field whose value was replaced
        /// </summary>
        public string ReplacedField { get; set; }
        /// <summary>
        /// The value that replaced the field's original value
        /// </summary>
        public string ReplacedValue { get; set; }
        /// <summary>
        /// A message explaining why this field's value was replaced
        /// </summary>
        public string ReplaceMessage { get; set; }

        /// <summary>
        /// Builds a user friendly string that summarises what field was founnd/replaced.
        /// </summary>
        /// <returns>A single line summary string</returns>
        public override string ToString()
        {
            if (Item == null || Fields == null)
                return string.Empty;

            var message = string.Empty;
            if (Item is RugbyModel.Team team)
                message = $"Team \"{team.Name}\"";
            else if (Item is RugbyModel.Player player)
                message = $"Player \"{player.DisplayName}\"";
            else if (Item is RugbyModel.SignedPlayer signedPlayer)
                message = $"Signed Player \"{signedPlayer.DisplayName}\"";

            foreach (var field in Fields)
                message += $", Field=[\"{field.Item1}\" - \"{field.Item2}\"]";

            if (!string.IsNullOrEmpty(ReplaceMessage))
                return message + $", {ReplaceMessage}";

            if (!string.IsNullOrEmpty(ReplacedField) && !string.IsNullOrEmpty(ReplacedValue))
                return message + $", Value within field \"{ReplacedField}\" replaced with \"{ReplacedValue}\"";

            return message;
        }
    }
}
