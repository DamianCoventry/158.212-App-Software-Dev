namespace RugbyView
{
    /// <summary>
    /// This class holds state for a field whose value has been found by the search mechanism.
    /// </summary>
    /// <remarks>
    /// <para>Author: Damian Coventry</para>
    /// <para>Date: Feb-May 2021</para>
    /// </remarks>
    public class FindResult
    {
        /// <summary>
        /// The name of the field found
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// The value of the field found
        /// </summary>
        public string FieldValue { get; set; }
        /// <summary>
        /// A reference to the object whose field has been found
        /// </summary>
        public object Item { get; set; }

        /// <summary>
        /// Builds a user friendly string that summarises what field was found.
        /// </summary>
        /// <returns>A single line summary string</returns>
        public override string ToString()
        {
            if (Item == null)
                return string.Empty;
            if (Item is RugbyModel.Team team)
                return $"Team \"{team.Name}\", Field \"{FieldName}\", Value \"{FieldValue}\"";
            if (Item is RugbyModel.Player player)
                return $"Player \"{player.DisplayName}\", Field \"{FieldName}\", Value \"{FieldValue}\"";
            if (Item is RugbyModel.SignedPlayer signedPlayer)
                return $"Signed Player \"{signedPlayer.DisplayName}\", Field \"{FieldName}\", Value \"{FieldValue}\"";
            return string.Empty;
        }
    }
}
