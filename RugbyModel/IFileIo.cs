using System.Collections.Generic;
using System.Text;

namespace RugbyModel
{
    /// <summary>
    /// This class exists to enable unit tests to be predictable. It pushes file I/O behind an interface so that the unit
    /// tests don't have to open/close files. It's possible that file I/O can fail at runtime (due to a full disk, missing
    /// files, etc) and that unpredictability detracts the purpose of the unit tests.
    /// The main application uses the DefaultFileIo implementation of this interface.
    /// The unit tests use the MockFileIo implementation of this interface.
    /// </summary>
    /// <remarks>
    /// <para>Author: Damian Coventry</para>
    /// <para>Date: Feb-May 2021</para>
    /// </remarks>
    public interface IFileIo
    {
        /// <summary>
        /// Reads all lines of text from an implementation defined source.
        /// </summary>
        /// <param name="path">A unique identifier for the source. Typically a pathname.</param>
        /// <param name="encoding">The expected text encoding for the source.</param>
        /// <returns>All lines read from the source</returns>
        string[] ReadAllLines(string path, Encoding encoding);
        /// <summary>
        /// Writes all string from the passed container to an implementation defined destination.
        /// </summary>
        /// <param name="path">A unique identifier for the destination. Typically a pathname.</param>
        /// <param name="contents">A container of strings to write to the destination.</param>
        /// <param name="encoding">The text encoding for the destination.</param>
        void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding);
    }
}
