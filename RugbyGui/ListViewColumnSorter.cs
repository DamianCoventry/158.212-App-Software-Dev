using System.Collections;
using System.Windows.Forms;

namespace RugbyGui
{
    /// <summary>
    /// Concrete implementation of the IComparer interface
    /// </summary>
    /// <remarks>
    /// <para>Author: Damian Coventry</para>
    /// <para>Date: Feb-May 2021</para>
    /// </remarks>
    public class ListViewColumnSorter : IComparer
    {
        private readonly CaseInsensitiveComparer _objectCompare = new CaseInsensitiveComparer();

        /// <summary>
        /// The index of the column to use for sorting
        /// </summary>
        public int SortColumn { get; set; } = 0;
        /// <summary>
        /// The type of sorting requested: none, ascending, descending
        /// </summary>
        public SortOrder SortOrder { get; set; } = SortOrder.None;

        /// <summary>
        /// Compares the two passed in objects with one another
        /// </summary>
        /// <param name="a">The first object</param>
        /// <param name="b">The second object</param>
        /// <returns>A positive number if a > b, a negative number if b > a, or 0 if a == b</returns>
        public int Compare(object a, object b)
        {
            var listViewA = (ListViewItem)a;
            var listViewB = (ListViewItem)b;
            if (SortColumn < 0 || SortColumn > listViewA.SubItems.Count - 1 || SortColumn > listViewB.SubItems.Count - 1) // Bounds check
                return 0;

            int compareResult = _objectCompare.Compare(listViewA.SubItems[SortColumn].Text, listViewB.SubItems[SortColumn].Text);
            if (SortOrder == SortOrder.Ascending)
                return compareResult;
            if (SortOrder == SortOrder.Descending)
                return -compareResult;
            return 0;
        }
    }
}
