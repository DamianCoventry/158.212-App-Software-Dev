namespace RugbyView
{
    /// <summary>
    /// These are the options for the find dialog, and the replace dialog.
    /// </summary>
    /// <remarks>
    /// <para>Author: Damian Coventry</para>
    /// <para>Date: Feb-May 2021</para>
    /// </remarks>
    public class FindReplaceOptions
    {
        /// <summary>
        /// Which string to find within the Rugby Union data set
        /// </summary>
        public string FindWhat { get; set; }
        /// <summary>
        /// Whether or not to match case for string compares
        /// </summary>
        public bool MatchCase { get; set; }
        /// <summary>
        /// Whether or not to the whole string for string compares
        /// </summary>
        public bool MatchWholeWord { get; set; }
        /// <summary>
        /// Whether or not to compile the FindWhat text as a RegEx and subsequently use it
        /// </summary>
        public bool UseRegularExpression { get; set; }
        /// <summary>
        /// Whether or not to search the Teams data
        /// </summary>
        public bool FindTeams { get; set; }
        /// <summary>
        /// Whether or not to search the Players data
        /// </summary>
        public bool FindPlayers { get; set; }
        /// <summary>
        /// Whether or not to search the Signed Players data
        /// </summary>
        public bool FindSignedPlayers { get; set; }
    }
}
