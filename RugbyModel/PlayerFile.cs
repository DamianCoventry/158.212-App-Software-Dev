using System;
using System.Collections.Generic;
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
    public class PlayerFile : BaseFile
    {
        private readonly IFileIo _fileIo;
        private const string FileHeader = "Player File Format";
        private const string PlayersHeader = "Players";

        /// <summary>
        /// The currently supported version of the file format
        /// </summary>
        public const string SupportedFileVersion = "1.0";

        /// <summary>
        /// Constructor that does not load and parse a file.
        /// </summary>
        /// <param name="fileIo">File input/output interface</param>
        public PlayerFile(IFileIo fileIo) // File access pushed to an interface for unit testing
        {
            _fileIo = fileIo;
        }

        /// <summary>
        /// Constructor that loads and parses a file.
        /// </summary>
        /// <param name="pathName">The path to a file that's expected to implment the expected file format</param>
        /// <param name="fileIo">File input/output interface</param>
        public PlayerFile(string pathName, IFileIo fileIo) // File access pushed to an interface for unit testing
        {
            _fileIo = fileIo;
            Load(pathName);
        }

        /// <summary>
        /// The version of the file format actually read from the file
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// The container of players read from the file, or to be written to the file
        /// </summary>
        public List<Player> Players { get; set; }

        /// <summary>
        /// Reads players from the file identified by the passed pathname. The data will be stored into a private member
        /// for later retrieval by a public property. Throws an exception when any kind of error is encountered.
        /// </summary>
        /// <param name="pathName">The path to file that's expected to use the required format</param>
        public void Load(string pathName)
        {
            if (string.IsNullOrEmpty(pathName))
                throw new ArgumentNullException(nameof(pathName));

            Clear();

            string[] lines = _fileIo.ReadAllLines(pathName, Encoding.UTF8); // Delegate to the interface
            if (lines == null || lines.Length == 0)
                throw new Exception($"Unable to read data from the file \"{pathName}\"");

            // Start with a well-known header and a version
            int i = 0;
            if (GetNextLine(lines, ref i) != FileHeader)
                ThrowUnrecognisableDataFormat();

            if (GetNextLine(lines, ref i) != SupportedFileVersion)
                ThrowUnrecognisableDataFormat();
            Version = SupportedFileVersion;

            if (GetNextLine(lines, ref i) != PlayersHeader)
                ThrowUnrecognisableDataFormat();

            if (!(int.TryParse(GetNextLine(lines, ref i), out int playerCount)))
                ThrowUnrecognisableDataFormat();

            // Go get them all
            Players = new List<Player>();
            for (int j = 0; j < playerCount; ++j)
                Players.Add(Player.FromFileString(GetNextLine(lines, ref i)));
        }

        /// <summary>
        /// Writes players to the file identified by the passed pathname. The data are expected to be stored into
        /// a private member ahead of time. Throws an exception when any kind of error is encountered.
        /// </summary>
        /// <param name="pathName">The path to file that will be created/overwritten. It's data will be formatted as the required format</param>
        public void Save(string pathName)
        {
            if (string.IsNullOrEmpty(pathName))
                throw new ArgumentNullException(nameof(pathName));

            List<string> lines = new List<string>()
            {
                FileHeader,                 // Well-known header
                SupportedFileVersion        // Currently supported version
            };

            lines.Add(PlayersHeader);
            if (Players == null)
                lines.Add("0");
            else
            {
                lines.Add(Players.Count.ToString());
                lines.AddRange(Players.ConvertAll(x => x.ToFileString()));
            }

            _fileIo.WriteAllLines(pathName, lines, Encoding.UTF8); // Delegate to the interface
        }

        /// <summary>
        /// Resets internal state so that this object can be re-used
        /// </summary>
        public void Clear()
        {
            Version = null;
            Players = null;
        }
    }
}
