using System.IO;

namespace RugbyModel
{
    /// <summary>
    /// This class exists to remove duplication. Both methods are used by the derived classes RugbyUnionFile,
    /// TeamFile, and PlayerFile. No need to repeat this implementation thrice.
    /// </summary>
    /// <remarks>
    /// <para>Author: Damian Coventry</para>
    /// <para>Date: Feb-May 2021</para>
    /// </remarks>
    public class BaseFile
    {
        /// <summary>
        /// A utility method to make the calling code easier to read by hiding the if statement.
        /// </summary>
        /// <param name="lines">An array of lines read from a text file</param>
        /// <param name="i">An in/out parameter. On entry it's the index to read from, on exit it will have been incremented</param>
        /// <returns>The next line of text</returns>
        protected string GetNextLine(string[] lines, ref int i)
        {
            if (i >= lines.Length)
                ThrowUnrecognisableDataFormat();
            return lines[i++];
        }

        /// <summary>
        /// A utility method to prevent the string constant from being repeated in multiple files multiple times.
        /// </summary>
        public static void ThrowUnrecognisableDataFormat()
        {
            throw new InvalidDataException("The data within the file are in an unrecognisable format");
        }
    }
}
