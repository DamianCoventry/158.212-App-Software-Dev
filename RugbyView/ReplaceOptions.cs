namespace RugbyView
{
    /// <summary>
    /// This class extends the FindReplaceOptions class by adding a string that holds the replacement string value
    /// </summary>
    /// <remarks>
    /// <para>Author: Damian Coventry</para>
    /// <para>Date: Feb-May 2021</para>
    /// </remarks>
    public class ReplaceOptions : FindReplaceOptions
    {
        /// <summary>
        /// The value to replace found text with
        /// </summary>
        public string ReplaceWith { get; set; }
    }
}
