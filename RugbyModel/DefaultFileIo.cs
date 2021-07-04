using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RugbyModel
{
    /// <summary>
    /// Defines state that the search algorithm uses to locate a field within the Rugby Union data set.
    /// </summary>
    /// <remarks>
    /// <para>Author: Damian Coventry</para>
    /// <para>Date: Feb-May 2021</para>
    /// </remarks>
    public class DefaultFileIo : IFileIo
    {
        /// <summary>
        /// Reads from the file identified by the pathname, using the given encoding
        /// </summary>
        /// <param name="path">A path to a file</param>
        /// <param name="encoding">Expected text encoding for the file</param>
        /// <returns>All data from the file split into lines</returns>
        public string[] ReadAllLines(string path, Encoding encoding)
        {
            return File.ReadAllLines(path, encoding);
        }

        /// <summary>
        /// Writes the given string to the file identified by the pathname, using the given encoding
        /// </summary>
        /// <param name="path">A path to a file</param>
        /// <param name="contents">A container of strings to write to the file</param>
        /// <param name="encoding">Expected text encoding for the file</param>
        public void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding)
        {
            File.WriteAllLines(path, contents, encoding);
        }
    }
}
