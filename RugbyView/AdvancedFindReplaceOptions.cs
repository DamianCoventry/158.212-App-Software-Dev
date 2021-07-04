using System;
using System.Collections.Generic;

namespace RugbyView
{
    /// <summary>
    /// Defines state that the search algorithm uses to locate a field within the Rugby Union data set.
    /// </summary>
    /// <remarks>
    /// <para>Author: Damian Coventry</para>
    /// <para>Date: Feb-May 2021</para>
    /// </remarks>
    public class FindField
    {
        /// <summary>
        /// The name of the field to find
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The type of the field to find
        /// </summary>
        public Type Type { get; set; }
        /// <summary>
        /// What operation to use to find the field
        /// </summary>
        public string Operation { get; set; }
        /// <summary>
        /// The value that must match. If the operation is "between" then this value is the beginning of a range.
        /// </summary>
        public object BeginValue { get; set; }
        /// <summary>
        /// If the operation is "between" then this value is the end of a range. Unused otherwise.
        /// </summary>
        public object EndValue { get; set; }
    }

    /// <summary>
    /// Defines state that the replace algorithm uses to replace a field's value.
    /// </summary>
    /// <remarks>
    /// <para>Author: Damian Coventry</para>
    /// <para>Date: Feb-May 2021</para>
    /// </remarks>
    public class ReplaceField
    {
        /// <summary>
        /// The name of the field to replace
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The type of the field to replace
        /// </summary>
        public Type Type { get; set; }
        /// <summary>
        /// The value to replace the field's original value with
        /// </summary>
        public object Value { get; set; }
    }

    /// <summary>
    /// A container that encapsulates all state required to run an advanced find/replace algorithm
    /// </summary>
    /// <remarks>
    /// <para>Author: Damian Coventry</para>
    /// <para>Date: Feb-May 2021</para>
    /// </remarks>
    public class AdvancedFindReplaceOptions
    {
        /// <summary>
        /// The set of objects the can be found/replaced
        /// </summary>
        public enum What
        {
            /// <summary>
            /// Search within the Teams data
            /// </summary>
            Teams,
            /// <summary>
            /// Search within the Players data
            /// </summary>
            Players,
            /// <summary>
            /// Search within the Signed Players data
            /// </summary>
            SignedPlayers
        }
        /// <summary>
        /// The instance of What the defines what is being searched for
        /// </summary>
        public What FindWhat { get; set; }
        /// <summary>
        /// The list of fields that are being searched for
        /// </summary>
        public List<FindField> FindFields { get; set; }
        /// <summary>
        /// If this is a replace operation, then this is the which field to replace, and the value to use as a replacement
        /// </summary>
        public ReplaceField ReplaceField { get; set; }
    }
}
