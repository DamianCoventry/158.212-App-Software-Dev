namespace RugbyView
{
    /// <summary>
    /// This class holds state for a field whose value has been replaced by the find/replace mechanism. The difference between
    /// this class and the AdvancedResult class is that this class contains a single field + value. The AdvancedResult class
    /// contains a list of fields and values.
    /// </summary>
    /// <remarks>
    /// <para>Author: Damian Coventry</para>
    /// <para>Date: Feb-May 2021</para>
    /// </remarks>
    public class ReplaceResult : FindResult
    {
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
        /// Builds a user friendly string that summarises what field was replaced, and what value replaced the original value
        /// </summary>
        /// <returns>A single line summary string</returns>
        public override string ToString()
        {
            if (Item == null) // Unlikely, but safe
                return string.Empty;

            string text;
            if (Item is RugbyModel.Team team)
                text = $"Team \"{team.Name}\"";
            else if (Item is RugbyModel.Player player)
                text = $"Player \"{player.DisplayName}\"";
            else if (Item is RugbyModel.SignedPlayer signedPlayer)
                text = $"Signed Player \"{signedPlayer.DisplayName}\"";
            else // Unlikely, but safe
                return string.Empty;

            text += $", Field \"{FieldName}\"";

            if (!string.IsNullOrEmpty(ReplaceMessage))
                return text + $", {ReplaceMessage}";

            if (!string.IsNullOrEmpty(ReplacedField))
                return text + $", Value within field \"{ReplacedField}\" replaced with \"{ReplacedValue}\"";

            return text + $", Value \"{FieldValue}\" replaced with \"{ReplacedValue}\"";
        }
    }
}
