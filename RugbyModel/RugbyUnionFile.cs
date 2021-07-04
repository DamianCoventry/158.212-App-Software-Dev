using System;
using System.Collections.Generic;
using System.Text;

namespace RugbyModel
{
    /// <summary>
    /// Encapsulates the required file reading and writing logic for the application's main file data source.
    /// </summary>
    /// <remarks>
    /// <para>Author: Damian Coventry</para>
    /// <para>Date: Feb-May 2021</para>
    /// </remarks>
    public class RugbyUnionFile : BaseFile
    {
        private readonly IFileIo _fileIo;
        private const string FileHeader = "Rugby Union File Format";
        private const string TeamsHeader = "Teams";
        private const string PlayersHeader = "Players";
        private const string SignedPlayersHeader = "Signed Players";

        /// <summary>
        /// The currently supported version of the file format
        /// </summary>
        public const string SupportedFileVersion = "1.0";

        /// <summary>
        /// Constructor that does not load and parse a file.
        /// </summary>
        /// <param name="fileIo">File input/output interface</param>
        public RugbyUnionFile(IFileIo fileIo)
        {
            _fileIo = fileIo; // Save for later
        }

        /// <summary>
        /// Constructor that loads and parses a file.
        /// </summary>
        /// <param name="pathName">The path to a file that's expected to implment the expected file format</param>
        /// <param name="fileIo">File input/output interface</param>
        public RugbyUnionFile(string pathName, IFileIo fileIo)
        {
            _fileIo = fileIo; // Save for later
            Load(pathName);
        }

        /// <summary>
        /// The name of the Rugby Union
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The version of the file format actually read from the file
        /// </summary>
        public string Version { get; set; } = SupportedFileVersion;
        /// <summary>
        /// The container of teams read from the file, or to be written to the file
        /// </summary>
        public List<Team> Teams { get; set; }
        /// <summary>
        /// The container of players read from the file, or to be written to the file
        /// </summary>
        public List<Player> Players { get; set; }
        /// <summary>
        /// The container of signed players read from the file, or to be written to the file
        /// </summary>
        public List<SignedPlayer> SignedPlayers { get; set; }

        /// <summary>
        /// Reads teams, players, and signed players from the file identified by the passed pathname. The
        /// data will be stored into private members for later retrieval by the public properties. Throws
        /// an exception when any kind of error is encountered.
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

            // Name of the Rugby Union next
            Name = GetNextLine(lines, ref i);
            if (string.IsNullOrEmpty(Name))
                ThrowUnrecognisableDataFormat();

            // Teams up first
            if (GetNextLine(lines, ref i) != TeamsHeader)
                ThrowUnrecognisableDataFormat();

            if (!(int.TryParse(GetNextLine(lines, ref i), out int teamCount)))
                ThrowUnrecognisableDataFormat();

            Teams = new List<Team>();
            for (int j = 0; j < teamCount; ++j)
                Teams.Add(Team.FromFileString(GetNextLine(lines, ref i)));

            // Then players
            if (GetNextLine(lines, ref i) != PlayersHeader)
                ThrowUnrecognisableDataFormat();

            if (!(int.TryParse(GetNextLine(lines, ref i), out int playerCount)))
                ThrowUnrecognisableDataFormat();

            Players = new List<Player>();
            for (int j = 0; j < playerCount; ++j)
                Players.Add(Player.FromFileString(GetNextLine(lines, ref i)));

            if (GetNextLine(lines, ref i) != SignedPlayersHeader)
                ThrowUnrecognisableDataFormat();

            if (!(int.TryParse(GetNextLine(lines, ref i), out int signedPlayerCount)))
                ThrowUnrecognisableDataFormat();

            // Then signed players
            SignedPlayers = new List<SignedPlayer>();
            for (int j = 0; j < signedPlayerCount; ++j)
            {
                var signedPlayer = SignedPlayer.FromFileString(GetNextLine(lines, ref i));
                var player = Players.Find(x => x.Id == signedPlayer.PlayerId);
                if (player != null)
                    signedPlayer.PlayerName = player.DisplayName;
                else
                    signedPlayer.PlayerName = "(unrecognised)"; // User can correct using GUI
                SignedPlayers.Add(signedPlayer);
            }
        }

        /// <summary>
        /// Writes teams, players, and signed players to the file identified by the passed pathname. The
        /// data are expected to be stored into private members ahead of time. Throws an exception when
        /// any kind of error is encountered.
        /// </summary>
        /// <param name="pathName">The path to file that will be created/overwritten. It's data will be formatted as the required format</param>
        public void Save(string pathName)
        {
            if (string.IsNullOrEmpty(pathName))
                throw new ArgumentNullException(nameof(pathName));

            List<string> lines = new List<string>()
            {
                FileHeader,                 // Well-known header
                SupportedFileVersion,       // Currently supported version
                Name                        // Name of the Rugby Union
            };

            // Teams up first
            lines.Add(TeamsHeader);
            if (Teams == null)
                lines.Add("0");
            else
            {
                lines.Add(Teams.Count.ToString());
                lines.AddRange(Teams.ConvertAll(x => x.ToFileString()));
            }

            // Then players
            lines.Add(PlayersHeader);
            if (Players == null)
                lines.Add("0");
            else
            {
                lines.Add(Players.Count.ToString());
                lines.AddRange(Players.ConvertAll(x => x.ToFileString()));
            }

            // Then signed players
            lines.Add(SignedPlayersHeader);
            if (SignedPlayers == null)
                lines.Add("0");
            else
            {
                lines.Add(SignedPlayers.Count.ToString());
                lines.AddRange(SignedPlayers.ConvertAll(x => x.ToFileString()));
            }

            _fileIo.WriteAllLines(pathName, lines, Encoding.UTF8); // Delegate to the interface
        }

        /// <summary>
        /// Resets internal state so that this object can be re-used
        /// </summary>
        public void Clear()
        {
            Name = null;
            Version = null;
            Teams = null;
            Players = null;
            SignedPlayers = null;
        }
    }
}
