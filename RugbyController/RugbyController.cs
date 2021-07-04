using RugbyModel;
using RugbyView;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace RugbyController
{
    /// <summary>
    /// This is an implementation of the IRugbyController interface and is therefore the Controller layer. This class provides the application
    /// logic and rules in terms of Teams, Players, and Signed Players. It does not provide logic and rules for presenting the data on the GUI.
    /// A reference to an IRugbyView and a reference to an IFileIo are passed to the constructor. Mock implementations of those interfaces are
    /// written and used in the unit tests over here: RugbyController.Test\RugbyControllerTest.cs
    /// </summary>
    /// <remarks>
    /// <para>Author: Damian Coventry</para>
    /// <para>Date: Feb-May 2021</para>
    /// </remarks>
    public class RugbyController : IRugbyController
    {
        private Regex _findReplaceRegex;
        private readonly IFileIo _fileIo;
        private readonly IRugbyView _view;
        private static int _nextPlayerId = 1;

        /// <summary>
        /// The one and only constructor that accepts a reference to the view layer and a reference to a file I/O implementation.
        /// </summary>
        /// <param name="view">A reference to the View layer</param>
        /// <param name="fileIo">A reference to a file I/O implementation</param>
        public RugbyController(IRugbyView view, IFileIo fileIo)
        {
            _fileIo = fileIo; // Save for later
            _view = view; // Save for later
        }

        /// <summary>
        /// Returns whether or not a Rugby Union document is open
        /// </summary>
        public bool IsOpen { get; private set; }
        /// <summary>
        /// Returns whether or not the Rugby Union document has been modified
        /// </summary>
        public bool IsModified { get; private set; }
        /// <summary>
        /// Returns the Rugby Union document's name
        /// </summary>
        public string RugbyUnionName { get; private set; }
        /// <summary>
        /// Returns whether or not the Rugby Union document has been saved
        /// </summary>
        public bool HasBeenSaved { get; private set; }
        /// <summary>
        /// Returns the Rugby Union document's pathname
        /// </summary>
        public string PathName { get; private set; }
        /// <summary>
        /// Returns a reference to the internal Teams container
        /// </summary>
        public List<Team> Teams { get; private set; }
        /// <summary>
        /// Returns a reference to the internal Player container
        /// </summary>
        public List<Player> Players { get; private set; }
        /// <summary>
        /// Returns a reference to the internal Signed Player container. Only used by the unit tests. Not available via IRugbyController.
        /// </summary>
        public List<SignedPlayer> SignedPlayers { get; private set; }

        /// <summary>
        /// Creates a new Rugby Union document. Will report the result to the view layer.
        /// </summary>
        /// <param name="rugbyUnionName">The name to assign to the Rugby Union</param>
        public void NewRugbyUnion(string rugbyUnionName)
        {
            if (string.IsNullOrEmpty(rugbyUnionName))
                throw new ArgumentNullException(nameof(rugbyUnionName));

            IsOpen = true;
            IsModified = true; // Because it's not saved to a file yet
            RugbyUnionName = rugbyUnionName;
            HasBeenSaved = false;
            PathName = null;
            Teams = null;
            Players = null;
            SignedPlayers = null;
            _view.OnRugbyUnionCreated(rugbyUnionName); // Notify the view layer
        }

        /// <summary>
        /// Loads a Rugby Union from a file. Will report the result to the view layer.
        /// </summary>
        /// <param name="pathName">The pathname of a Rugby Union file</param>
        public void OpenRugbyUnion(string pathName)
        {
            if (string.IsNullOrEmpty(pathName))
                throw new ArgumentNullException(nameof(pathName));

            var rugbyUnionFile = new RugbyUnionFile(pathName, _fileIo); // Load and parse. Throws on error.
            IsOpen = true;
            IsModified = false; // Because it is saved to a file
            RugbyUnionName = rugbyUnionFile.Name;
            HasBeenSaved = true;
            PathName = pathName;
            Teams = rugbyUnionFile.Teams;
            Players = rugbyUnionFile.Players;
            SignedPlayers = rugbyUnionFile.SignedPlayers;
            _nextPlayerId = DiscoverLargestPlayerId() + 1; // Our internal counter needs to be seeded from the file's data
            _view.OnRugbyUnionOpened(RugbyUnionName, PathName, Teams, Players, SignedPlayers); // Notify the view layer
        }

        /// <summary>
        /// Closes a Rugby Union document. Will report the result to the view layer.
        /// </summary>
        public void CloseRugbyUnion()
        {
            IsOpen = false;
            IsModified = false;
            RugbyUnionName = null;
            HasBeenSaved = false;
            PathName = null;
            Teams = null;
            Players = null;
            SignedPlayers = null;
            _view.OnRugbyUnionClosed(); // Notify the view layer
        }

        /// <summary>
        /// Sets the name of the Rugby Union document. Will report the result to the view layer.
        /// </summary>
        /// <param name="rugbyUnionName">The name to assign to the Rugby Union</param>
        public void RenameRugbyUnion(string rugbyUnionName)
        {
            if (string.IsNullOrEmpty(rugbyUnionName))
                throw new ArgumentNullException(nameof(rugbyUnionName));

            var oldName = string.Copy(RugbyUnionName); // Because we're about to overwrite our reference
            RugbyUnionName = rugbyUnionName;
            IsModified = true;
            _view.OnRugbyUnionRenamed(oldName, RugbyUnionName); // Notify the view layer
        }

        /// <summary>
        /// Saves a Rugby Union document using a previously set filename. Will report the result to the view layer.
        /// </summary>
        public void SaveRugbyUnion()
        {
            if (string.IsNullOrEmpty(PathName)) // Save As must be called first.
                throw new ArgumentNullException(nameof(PathName));

            var rugbyUnionFile = new RugbyUnionFile(_fileIo)
            {
                Name = RugbyUnionName,
                Teams = Teams,
                Players = Players,
                SignedPlayers = SignedPlayers
            };
            rugbyUnionFile.Save(PathName); // Write it out in the expected format. Throws on error.

            IsModified = false; // Because if we get here an exception wasn't thrown.
            HasBeenSaved = true;
            _view.OnRugbyUnionSaved(PathName); // Notify the view layer
        }

        /// <summary>
        /// Saves a Rugby Union document into the file identified by the pathname. Will report the result to the view layer.
        /// </summary>
        /// <param name="pathName">The pathname of a file to write the Rugby Union into</param>
        public void SaveAsRugbyUnion(string pathName)
        {
            if (string.IsNullOrEmpty(pathName))
                throw new ArgumentNullException(nameof(pathName));

            var rugbyUnionFile = new RugbyUnionFile(_fileIo)
            {
                Name = RugbyUnionName,
                Teams = Teams,
                Players = Players,
                SignedPlayers = SignedPlayers
            };
            rugbyUnionFile.Save(pathName); // Write it out in the expected format. Throws on error.

            IsModified = false; // Because if we get here an exception wasn't thrown.
            HasBeenSaved = true;
            PathName = pathName; // Update our private copy.
            _view.OnRugbyUnionSaved(PathName); // Notify the view layer
        }

        /// <summary>
        /// Adds a team into the Rugby Union document. Will report the result to the view layer.
        /// </summary>
        /// <param name="team">A reference to a team to clone</param>
        public void AddTeam(Team team)
        {
            if (team == null)
                throw new ArgumentNullException(nameof(team));
            if (string.IsNullOrEmpty(team.Name))
                throw new ArgumentNullException(nameof(team.Name));
            var existingTeam = FindTeam(team.Name); // Duplicates disallowed
            if (existingTeam != null)
                throw new ArgumentException("Team name already in use");

            if (Teams == null)
                Teams = new List<Team>();
            Team copy = (Team)team.Clone(); // We need our own version
            Teams.Add(copy);
            IsModified = true;
            _view.OnTeamAdded(copy); // Notify the view layer
        }

        /// <summary>
        /// Edits an existing team within the Rugby Union document. Will report the result to the view layer.
        /// </summary>
        /// <param name="team">A reference to a team to clone</param>
        public void EditTeam(Team team)
        {
            if (team == null)
                throw new ArgumentNullException(nameof(team));
            if (string.IsNullOrEmpty(team.Name))
                throw new ArgumentNullException(nameof(team.Name));
            var existingTeam = FindTeam(team.Name);
            if (existingTeam == null)
                throw new ArgumentException("No such player exists");
            existingTeam.HomeGround = string.Copy(team.HomeGround); // Updated everything but the name. The user must rename for that to occur.
            existingTeam.Coach = string.Copy(team.Coach);
            existingTeam.Region = string.Copy(team.Region);
            existingTeam.YearFounded = team.YearFounded;
            IsModified = true;
            _view.OnTeamEdited(existingTeam); // Notify the view layer
        }

        /// <summary>
        /// Renames an existing team within the Rugby Union document. Will report the result to the view layer.
        /// </summary>
        /// <param name="oldName">The old name of the team</param>
        /// <param name="newName">The new name of the team</param>
        public void RenameTeam(string oldName, string newName)
        {
            if (string.IsNullOrEmpty(oldName))
                throw new ArgumentNullException(nameof(oldName));
            if (string.IsNullOrEmpty(newName))
                throw new ArgumentNullException(nameof(newName));
            var existingTeam = FindTeam(oldName);
            if (existingTeam == null)
                throw new ArgumentException("No such player exists");
            var renamedTeam = FindTeam(newName); // Duplicates disallowed
            if (renamedTeam != null)
                throw new ArgumentException("Team name already in use");
            existingTeam.Name = string.Copy(newName);
            IsModified = true;
            _view.OnTeamRenamed(oldName, newName); // Notify the view layer
        }

        /// <summary>
        /// Deletes an existing team from the Rugby Union document. Will report the result to the view layer.
        /// </summary>
        /// <param name="teamName">The name of the team</param>
        public void DeleteTeam(string teamName)
        {
            if (string.IsNullOrEmpty(teamName))
                throw new ArgumentNullException(nameof(teamName));
            var existingTeam = FindTeam(teamName);
            if (existingTeam == null)
                throw new ArgumentException("No such player exists");
            Teams.Remove(existingTeam);
            if (SignedPlayers != null) // Need to remove all signed players for this team
            {
                SignedPlayers.RemoveAll(x => x.TeamName.Equals(teamName, StringComparison.OrdinalIgnoreCase));
                if (SignedPlayers.Count == 0)
                    SignedPlayers = null; // This is how we identify no signed players
            }
            IsModified = true;
            _view.OnTeamDeleted(teamName); // Notify the view layer
        }

        /// <summary>
        /// Deletes existing teams from the Rugby Union document. Will report the result to the view layer.
        /// </summary>
        /// <param name="teamNames">The names of the teams</param>
        public void DeleteTeams(List<string> teamNames)
        {
            if (teamNames == null)
                throw new ArgumentNullException(nameof(teamNames));
            if (Teams == null)
                return;
            var deletedTeamNames = new List<string>();
            foreach (var teamName in teamNames)
            {
                var team = FindTeam(teamName);
                if (team != null)
                {
                    Teams.Remove(team);
                    if (SignedPlayers != null) // Need to remove all signed players for this team
                        SignedPlayers.RemoveAll(x => x.TeamName.Equals(teamName, StringComparison.OrdinalIgnoreCase));
                    deletedTeamNames.Add(teamName);
                }
            }
            if (SignedPlayers != null && SignedPlayers.Count == 0)
                SignedPlayers = null; // This is how we identify no signed players
            IsModified = true;
            _view.OnTeamsDeleted(deletedTeamNames); // Notify the view layer
        }

        /// <summary>
        /// Deletes all existing teams from the Rugby Union document. Will report the result to the view layer.
        /// </summary>
        public void DeleteAllTeams()
        {
            if (Teams == null) // Don't bother if nothing has been created
                return;
            var deletedTeamNames = Teams.Select(x => x.Name).ToList(); // Save for the view layer callback
            Teams.Clear();
            Teams = null;
            if (SignedPlayers != null)
            {
                SignedPlayers.Clear();
                SignedPlayers = null; // This is how we identify no signed players
            }
            IsModified = true;
            _view.OnTeamsDeleted(deletedTeamNames); // Notify the view layer
        }

        /// <summary>
        /// Finds an existing team within the Rugby Union document.
        /// </summary>
        /// <param name="teamName">The name of the team</param>
        /// <returns>A reference to the team, or null</returns>
        public Team FindTeam(string teamName)
        {
            if (string.IsNullOrEmpty(teamName))
                throw new ArgumentNullException(nameof(teamName));
            if (Teams == null) // Don't bother if nothing has been created
                return null;
            return Teams.FirstOrDefault(x => x.Name.Equals(teamName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Returns whether or not there are teams available within the Rugby Union document.
        /// </summary>
        public bool TeamsAvailable
        {
            get { return Teams != null && Teams.Count > 0; }
        }

        /// <summary>
        /// Adds a player into the Rugby Union document. Will report the result to the view layer.
        /// </summary>
        /// <param name="player">A reference to a player to clone</param>
        /// <returns>The ID of the new player</returns>
        public int AddPlayer(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));
            if (string.IsNullOrEmpty(player.FirstName))
                throw new ArgumentNullException(nameof(player.FirstName));
            if (string.IsNullOrEmpty(player.LastName))
                throw new ArgumentNullException(nameof(player.LastName));
            var existingPlayer = FindPlayer(player.FirstName, player.LastName); // Duplicates disallowed
            if (existingPlayer != null)
                throw new ArgumentException("Player name already in use");

            if (Players == null)
                Players = new List<Player>();
            Player copy = (Player)player.Clone(); // We need our own copy
            copy.Id = _nextPlayerId++; // Allocate the next ID
            Players.Add(copy);
            IsModified = true;
            _view.OnPlayerAdded(copy); // Notify the view layer
            return copy.Id; // For convenience, let the caller know the ID
        }

        /// <summary>
        /// Edits an existing player within the Rugby Union document. Will report the result to the view layer.
        /// </summary>
        /// <param name="player">A reference to a player to clone</param>
        public void EditPlayer(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));
            if (player.Id < 1)
                throw new ArgumentOutOfRangeException("Invalid player ID");
            var existingPlayer = FindPlayer(player.Id);
            if (existingPlayer == null)
                throw new ArgumentException("No such player exists");
            existingPlayer.FirstName = string.Copy(player.FirstName); // Update everything except the ID. It can never be updated.
            existingPlayer.LastName = string.Copy(player.LastName);
            existingPlayer.PlaceOfBirth = string.Copy(player.PlaceOfBirth);
            existingPlayer.Height = player.Height;
            existingPlayer.Weight = player.Weight;
            existingPlayer.DateOfBirth = player.DateOfBirth; // DateTime is a value type
            IsModified = true;
            _view.OnPlayerEdited(existingPlayer); // Notify the view layer
        }

        /// <summary>
        /// Deletes an existing player from the Rugby Union document. Will report the result to the view layer.
        /// </summary>
        /// <param name="playerId">The ID of the player</param>
        public void DeletePlayer(int playerId)
        {
            if (playerId < 1)
                throw new ArgumentOutOfRangeException("Invalid player ID");
            if (Players == null)
                return;
            var existingPlayer = FindPlayer(playerId);
            if (existingPlayer == null)
                throw new ArgumentException("No such player exists");
            Players.Remove(existingPlayer);
            if (SignedPlayers != null)
            {
                SignedPlayers.RemoveAll(x => x.PlayerId == playerId); // Need to remove the signed players for this player
                if (SignedPlayers.Count == 0)
                    SignedPlayers = null; // This is how we identify no signed players
            }
            IsModified = true;
            _view.OnPlayerDeleted(playerId); // Notify the view layer
        }

        /// <summary>
        /// Deletes existing players from the Rugby Union document. Will report the result to the view layer.
        /// </summary>
        /// <param name="playerIds">The IDs of the players</param>
        public void DeletePlayers(List<int> playerIds)
        {
            if (playerIds == null)
                throw new ArgumentNullException(nameof(playerIds));
            if (Players == null)
                return;
            var deletedPlayerIds = new List<int>();
            foreach (var playerId in playerIds)
            {
                var player = FindPlayer(playerId);
                if (player != null)
                {
                    Players.Remove(player);
                    if (SignedPlayers != null)
                        SignedPlayers.RemoveAll(x => x.PlayerId == playerId); // Need to remove the signed players for this player
                    deletedPlayerIds.Add(playerId); // Save for the view layer
                }
            }
            if (SignedPlayers != null && SignedPlayers.Count == 0)
                SignedPlayers = null; // This is how we identify no signed players
            IsModified = true;
            _view.OnPlayersDeleted(deletedPlayerIds); // Notify the view layer
        }

        /// <summary>
        /// Deletes all existing players from the Rugby Union document. Will report the result to the view layer.
        /// </summary>
        public void DeleteAllPlayers()
        {
            if (Players == null)
                return;
            var deletedPlayerIds = Players.Select(x => x.Id).ToList(); // Save for the view layer
            Players.Clear();
            Players = null;
            if (SignedPlayers != null)
            {
                SignedPlayers.Clear();
                SignedPlayers = null; // This is how we identify no signed players
            }
            IsModified = true;
            _view.OnPlayersDeleted(deletedPlayerIds); // Notify the view layer
        }

        /// <summary>
        /// Finds an existing player within the Rugby Union document.
        /// </summary>
        /// <param name="playerId">The ID of the player</param>
        /// <returns>A reference to the player, or null</returns>
        public Player FindPlayer(int playerId)
        {
            if (playerId < 1)
                throw new ArgumentOutOfRangeException("Invalid player ID");
            if (Players == null) // Don't bother if nothing has been created
                return null;
            return Players.FirstOrDefault(x => x.Id == playerId);
        }

        /// <summary>
        /// Finds an existing player within the Rugby Union document.
        /// </summary>
        /// <param name="firstName">The first name of the player</param>
        /// <param name="lastName">The last name of the player</param>
        /// <returns>A reference to the player, or null</returns>
        public Player FindPlayer(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName))
                throw new ArgumentNullException(nameof(firstName));
            if (string.IsNullOrEmpty(lastName))
                throw new ArgumentNullException(nameof(lastName));
            if (Players == null) // Don't bother if nothing has been created
                return null;
            return Players.FirstOrDefault(x => x.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                                               x.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Returns whether or not there are players available within the Rugby Union document.
        /// </summary>
        public bool PlayersAvailable
        {
            get { return Players != null && Players.Count > 0; }
        }

        /// <summary>
        /// Signs an existing player to an existing team within the Rugby Union document. Will report the result to the view layer.
        /// </summary>
        /// <param name="playerId">The ID of the player</param>
        /// <param name="teamName">The name of the team</param>
        public void SignPlayerToTeam(int playerId, string teamName)
        {
            if (playerId < 1)
                throw new ArgumentOutOfRangeException("Invalid player ID");
            if (string.IsNullOrEmpty(teamName))
                throw new ArgumentNullException(nameof(teamName));
            var existingPlayer = FindPlayer(playerId);
            if (existingPlayer == null)
                throw new ArgumentException("No such player exists");
            var existingTeam = FindTeam(teamName);
            if (existingTeam == null)
                throw new ArgumentException("No such team exists");
            var existingSignedPlayer = FindSignedPlayer(playerId);
            if (existingSignedPlayer != null)
                existingSignedPlayer.TeamName = string.Copy(teamName); // Update the team in case it's changed
            else
            {
                // The player hasn't been signed to a team yet
                if (SignedPlayers == null)
                    SignedPlayers = new List<SignedPlayer>();
                SignedPlayers.Add(new SignedPlayer()
                {
                    PlayerId = playerId,
                    PlayerName = existingPlayer.DisplayName,
                    TeamName = teamName
                });
            }
            IsModified = true;
            _view.OnPlayerSignedToTeam(playerId, teamName); // Notify the view layer
        }

        /// <summary>
        /// Unsigns an existing player from an existing team within the Rugby Union document. Will report the result to the view layer.
        /// </summary>
        /// <param name="playerId">The ID of the player</param>
        /// <param name="teamName">The name of the team</param>
        public void UnsignPlayerFromTeam(int playerId, string teamName)
        {
            if (SignedPlayers == null)
                return;
            var signedPlayer = SignedPlayers.Find(x => x.PlayerId == playerId && x.TeamName.Equals(teamName, StringComparison.OrdinalIgnoreCase));
            if (signedPlayer == null)
                throw new ArgumentException($"No such signed player exists \"{playerId}/{teamName}\"");
            SignedPlayers.RemoveAll(x => x.PlayerId == playerId && x.TeamName.Equals(teamName, StringComparison.OrdinalIgnoreCase));
            if (SignedPlayers.Count == 0)
                SignedPlayers = null; // This is how we identify no signed players
            IsModified = true;
            _view.OnPlayerUnsignedFromTeam(playerId, teamName); // Notify the view layer
        }

        /// <summary>
        /// Unsigns existing players from existing teams within the Rugby Union document. Will report the result to the view layer.
        /// </summary>
        /// <param name="signedPlayers">A list of references to signed players</param>
        public void UnsignPlayersFromTeams(List<SignedPlayer> signedPlayers)
        {
            if (signedPlayers == null)
                throw new ArgumentNullException(nameof(signedPlayers));
            if (SignedPlayers == null) // Don't bother if no signings yet
                return;
            var deletedSignedPlayers = new List<SignedPlayer>();
            foreach (var s in signedPlayers)
            {
                var signedPlayer = FindSignedPlayer(s.PlayerId);
                if (signedPlayer != null)
                {
                    SignedPlayers.Remove(signedPlayer);
                    deletedSignedPlayers.Add(signedPlayer); // Save for the view layer
                }
            }
            if (SignedPlayers.Count == 0)
                SignedPlayers = null; // This is how we identify no signed players
            IsModified = true;
            _view.OnPlayersUnsignedFromTeams(deletedSignedPlayers); // Notify the view layer
        }

        /// <summary>
        /// Unsigns all existing players from an existing team within the Rugby Union document. Will report the result to the view layer.
        /// </summary>
        /// <param name="teamName">The name of the team</param>
        public void UnsignAllPlayersFromTeam(string teamName)
        {
            if (string.IsNullOrEmpty(teamName))
                throw new ArgumentNullException(nameof(teamName));
            if (SignedPlayers == null) // Don't bother if no signings yet
                return;
            var deletedSignedPlayers = new List<SignedPlayer>();
            var signedPlayersCopy = SignedPlayers.Where(x => x.TeamName.Equals(teamName, StringComparison.OrdinalIgnoreCase)).ToList();
            foreach (var signedPlayer in signedPlayersCopy)
            {
                SignedPlayers.Remove(signedPlayer);
                deletedSignedPlayers.Add(signedPlayer); // Save for the view layer
            }
            if (SignedPlayers.Count == 0)
                SignedPlayers = null; // This is how we identify no signed players
            IsModified = true;
            _view.OnPlayersUnsignedFromTeams(deletedSignedPlayers); // Notify the view layer
        }

        /// <summary>
        /// Unsigns all existing players from all existing teams within the Rugby Union document. Will report the result to the view layer.
        /// </summary>
        public void UnsignAllPlayersFromAllTeams()
        {
            if (Players == null)
                return;
            var deletedSignedPlayers = new List<SignedPlayer>(SignedPlayers); // Take a copy for the view layer
            if (SignedPlayers != null)
            {
                SignedPlayers.Clear();
                SignedPlayers = null; // This is how we identify no signed players
            }
            IsModified = true;
            _view.OnPlayersUnsignedFromTeams(deletedSignedPlayers); // Notify the view layer
        }

        /// <summary>
        /// Finds an existing signed player within the Rugby Union document.
        /// </summary>
        /// <param name="playerId">The ID of the player</param>
        /// <returns>A reference to the signed player, or null</returns>
        public SignedPlayer FindSignedPlayer(int playerId)
        {
            if (playerId < 1)
                throw new ArgumentOutOfRangeException("Invalid player ID");
            if (SignedPlayers == null) // Don't bother if no signings yet
                return null;
            return SignedPlayers.Find(x => x.PlayerId == playerId);
        }

        /// <summary>
        /// Returns whether or not there are signed players available within the Rugby Union document.
        /// </summary>
        public bool SignedPlayersAvailable
        {
            get { return SignedPlayers != null && SignedPlayers.Count > 0; }
        }

        /// <summary>
        /// Gets the list of all signed players to a team within the Rugby Union document.
        /// </summary>
        /// <param name="teamName">The name of the team</param>
        /// <returns>A list of references to the signed players, or null</returns>
        public List<SignedPlayer> GetPlayersSignedToTeam(string teamName)
        {
            if (FindTeam(teamName) == null) // FindTeam validates the parameter
                return null; // No such team
            if (SignedPlayers == null) // Don't bother if no signings yet
                return null;
            return SignedPlayers.Where(x => x.TeamName.Equals(teamName, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        /// <summary>
        /// Utility that finds the largest player ID within the Rugby Union document
        /// </summary>
        /// <returns>The largest player ID within the Rugby Union document</returns>
        private int DiscoverLargestPlayerId()
        {
            if (Players == null)
                return 0;
            int largestPlayerId = 0;
            foreach (var player in Players)
                largestPlayerId = Math.Max(largestPlayerId, player.Id);
            return largestPlayerId;
        }

        /// <summary>
        /// Finds all items that match the given find options. Will report the result to the view layer.
        /// </summary>
        /// <param name="findOptions">The options for the find operation</param>
        public void Find(FindReplaceOptions findOptions)
        {
            if (findOptions == null)
                throw new ArgumentNullException(nameof(findOptions));
            if (string.IsNullOrEmpty(findOptions.FindWhat))
                throw new ArgumentNullException(nameof(findOptions.FindWhat));

            try
            {
                _findReplaceRegex = BuildRegex(findOptions); // If not using a regex then this will return null
            }
            catch (ArgumentException regexError)
            {
                throw new Exception($"The supplied pattern does not constitute a valid Regular Expression.\n\nThe error returned from the system is \"{regexError.Message}\"");
            }

            var compare = BuildCompareFunction(_findReplaceRegex); // Build either a string compare function or a regex compare function.

            // Run all items through the compare function and bundle the results into a container that's passed to the view layer.
            var findResults = new List<FindResult>();
            if (findOptions.FindTeams)
                findResults.AddRange(FindTeams(findOptions, compare));
            if (findOptions.FindPlayers)
                findResults.AddRange(FindPlayers(findOptions, compare));
            if (findOptions.FindSignedPlayers)
                findResults.AddRange(FindSignedPlayers(findOptions, compare));

            _view.OnFindResults(findResults); // Notify the view layer
        }

        /// <summary>
        /// Finds all items that match the given find options, then replaces those items with the replace data. Will report the result to the view layer.
        /// </summary>
        /// <param name="replaceOptions">The options for the replace operation</param>
        public void Replace(ReplaceOptions replaceOptions)
        {
            if (replaceOptions == null)
                throw new ArgumentNullException(nameof(replaceOptions));
            if (string.IsNullOrEmpty(replaceOptions.FindWhat))
                throw new ArgumentNullException(nameof(replaceOptions.FindWhat));
            if (string.IsNullOrEmpty(replaceOptions.ReplaceWith))
                throw new ArgumentNullException(nameof(replaceOptions.ReplaceWith));

            try
            {
                _findReplaceRegex = BuildRegex(replaceOptions);
            }
            catch (ArgumentException regexError)
            {
                throw new Exception($"The supplied pattern does not constitute a valid Regular Expression.\n\nThe error returned from the system is \"{regexError.Message}\"");
            }

            var compare = BuildCompareFunction(_findReplaceRegex); // Build either a string compare function or a regex compare function.

            // Run all items through the compare function and bundle the results into a container that's passed to the view layer.
            var replaceResults = new List<ReplaceResult>();
            if (replaceOptions.FindTeams)
                replaceResults.AddRange(ReplaceTeams(replaceOptions, compare));
            if (replaceOptions.FindPlayers)
                replaceResults.AddRange(ReplacePlayers(replaceOptions, compare));
            if (replaceOptions.FindSignedPlayers)
                replaceResults.AddRange(ReplaceSignedPlayers(replaceOptions, compare));

            foreach (var result in replaceResults)
            {
                if (result.Item is Team team)
                    _view.OnTeamEdited(team); // Notify the view layer
                else if (result.Item is Player player)
                    _view.OnPlayerEdited(player); // Notify the view layer
                // Signed players are not modified via Replace
            }

            if (replaceResults.Count > 0)
                IsModified = true;
            _view.OnReplaceResults(replaceResults); // Notify the view layer
        }

        /// <summary>
        /// If the options are asking for a regex pattern to be used then a regex object is built and returned, otherwise
        /// null is returned.
        /// </summary>
        /// <param name="findOptions">The options for the find/replace operation</param>
        /// <returns>Either a regex object or null</returns>
        private Regex BuildRegex(FindReplaceOptions findOptions)
        {
            if (!findOptions.UseRegularExpression)
                return null;

            RegexOptions regexOptions = RegexOptions.Compiled; // Compilation is only done once
            if (!findOptions.MatchCase)
                regexOptions |= RegexOptions.IgnoreCase;

            string pattern;
            if (findOptions.MatchWholeWord)
                pattern = $"^({findOptions.FindWhat})$"; // Wrap it so that we can guarantee it's a whole-word match
            else
                pattern = findOptions.FindWhat; // Just use whatever the user entered

            return new Regex(pattern, regexOptions); // Will throw if the regex pattern is invalid
        }

        /// <summary>
        /// Create a function from a lambda that can be passed to other code within this file. This is a nice way of hiding
        /// the differences between a string compare and a regex compare.
        /// </summary>
        /// <param name="regex">If null then a string compare is created, otherwise a regex compare is created.</param>
        /// <returns>A function that encapsulates the type of compare to perform</returns>
        private Func<string, FindReplaceOptions, bool> BuildCompareFunction(Regex regex)
        {
            if (regex != null)
                return (textValue, options) => { return RegexCompare(textValue, options, regex); };
            return (textValue, options) => { return StringCompare(textValue, options); };
        }

        /// <summary>
        /// Passes each team though the compare function, collating the results as it goes.
        /// </summary>
        /// <param name="findOptions">The options to use for the find operation</param>
        /// <param name="compare">The compare function to use</param>
        /// <returns>A container of results from the compare function</returns>
        private IEnumerable<FindResult> FindTeams(FindReplaceOptions findOptions, Func<string, FindReplaceOptions, bool> compare)
        {
            var findResults = new List<FindResult>();
            if (Teams != null)
            {
                foreach (var team in Teams)
                {
                    var findResult = new FindResult() { Item = team };
                    if (FindTeam(team, findOptions, findResult, compare))
                        findResults.Add(findResult);
                }
            }
            return findResults;
        }

        /// <summary>
        /// Passes each team though the compare function, collating the results as it goes.
        /// </summary>
        /// <param name="replaceOptions">The options to use for the replace operation</param>
        /// <param name="compare">The compare function to use</param>
        /// <returns>A container of results from the compare function</returns>
        private IEnumerable<ReplaceResult> ReplaceTeams(ReplaceOptions replaceOptions, Func<string, FindReplaceOptions, bool> compare)
        {
            var replaceResults = new List<ReplaceResult>();
            if (Teams != null)
            {
                foreach (var team in Teams)
                {
                    var replaceResult = new ReplaceResult() { Item = team };
                    if (ReplaceTeam(team, replaceOptions, replaceResult, compare))
                        replaceResults.Add(replaceResult);
                }
            }
            return replaceResults;
        }

        /// <summary>
        /// Passes each field of the team object through the compare function. This function returns immediately after a match is found.
        /// This approach means the function implements a logical OR operation. It can be read as:
        ///     if the name matches OR the home ground matches OR the coach matches...etc then return true
        /// </summary>
        /// <param name="team">The team to search</param>
        /// <param name="findOptions">The options to use for the find operation</param>
        /// <param name="findResult">The result of the find operation</param>
        /// <param name="compare">The compare function to use</param>
        /// <returns>True if a match is found, false otherwise</returns>
        private bool FindTeam(Team team, FindReplaceOptions findOptions, FindResult findResult, Func<string, FindReplaceOptions, bool> compare)
        {
            return FindField(nameof(team.Name), team.Name, findOptions, findResult, compare) ||
                   FindField(nameof(team.HomeGround), team.HomeGround, findOptions, findResult, compare) ||
                   FindField(nameof(team.Coach), team.Coach, findOptions, findResult, compare) ||
                   FindField(nameof(team.Region), team.Region, findOptions, findResult, compare) ||
                   FindField(nameof(team.YearFounded), team.YearFounded.ToString(), findOptions, findResult, compare);
        }

        /// <summary>
        /// Passes each field of the team object through the compare function. For each field that matches a replace function is executed
        /// upon the field. The replace options determine whether the replace function is a string replace or a regex replace
        /// </summary>
        /// <param name="team">The team to search</param>
        /// <param name="replaceOptions">The options to use for the replace operation</param>
        /// <param name="replaceResult">The result of the replace operation</param>
        /// <param name="compare">The compare function to use</param>
        /// <returns>True if a replacement occurred, false otherwise</returns>
        private bool ReplaceTeam(Team team, ReplaceOptions replaceOptions, ReplaceResult replaceResult, Func<string, FindReplaceOptions, bool> compare)
        {
            bool replaced = ReplaceField(nameof(team.Name), team.Name, replaceOptions, replaceResult, compare, (findWhat, replaceWith) =>
                                {
                                    replaceResult.ReplaceMessage = $"Will not replace the unique identifier \"{team.Name}\""; // Disallow unique IDs to replaced
                                    return string.Empty;
                                });

            if (ReplaceField(nameof(team.HomeGround), team.HomeGround, replaceOptions, replaceResult, compare, (findWhat, replaceWith) =>
                {
                    team.HomeGround = ReplaceString(team.HomeGround, findWhat, replaceWith, replaceOptions);
                    return team.HomeGround;
                }))
            {
                replaced = true;
            }

            if (ReplaceField(nameof(team.Coach), team.Coach, replaceOptions, replaceResult, compare, (findWhat, replaceWith) =>
                {
                    team.Coach = ReplaceString(team.Coach, findWhat, replaceWith, replaceOptions);
                    return team.Coach;
                }))
            {
                replaced = true;
            }

            if (ReplaceField(nameof(team.Region), team.Region, replaceOptions, replaceResult, compare, (findWhat, replaceWith) =>
                {
                    team.Region = ReplaceString(team.Region, findWhat, replaceWith, replaceOptions);
                    return team.Region;
                }))
            {
                replaced = true;
            }

            if (ReplaceField(nameof(team.YearFounded), team.YearFounded.ToString(), replaceOptions, replaceResult, compare, (findWhat, replaceWith) =>
                {
                    var yearFounded = ReplaceString(team.YearFounded.ToString(), findWhat, replaceWith, replaceOptions);
                    if (int.TryParse(yearFounded, out int i))
                    {
                        team.YearFounded = Math.Min(Team.MaxYearFounded, Math.Max(Team.MinYearFounded, i)); // Clamp to the range
                        return team.YearFounded.ToString();
                    }
                    replaceResult.ReplaceMessage = $"The replacement would produce an invalid integer. Not replacing \"{team.YearFounded}\"";
                    return string.Empty;
                }))
            {
                replaced = true;
            }

            return replaced;
        }

        /// <summary>
        /// Passes each player though the compare function, collating the results as it goes.
        /// </summary>
        /// <param name="findOptions">The options to use for the find operation</param>
        /// <param name="compare">The compare function to use</param>
        /// <returns>A container of results from the compare function</returns>
        private IEnumerable<FindResult> FindPlayers(FindReplaceOptions findOptions, Func<string, FindReplaceOptions, bool> compare)
        {
            var findResults = new List<FindResult>();
            if (Players != null)
            {
                foreach (var player in Players)
                {
                    var findResult = new FindResult() { Item = player };
                    if (FindPlayer(player, findOptions, findResult, compare))
                        findResults.Add(findResult);
                }
            }
            return findResults;
        }

        /// <summary>
        /// Passes each player though the compare function, collating the results as it goes.
        /// </summary>
        /// <param name="replaceOptions">The options to use for the replace operation</param>
        /// <param name="compare">The compare function to use</param>
        /// <returns>A container of results from the compare function</returns>
        private IEnumerable<ReplaceResult> ReplacePlayers(ReplaceOptions replaceOptions, Func<string, FindReplaceOptions, bool> compare)
        {
            var replaceResults = new List<ReplaceResult>();
            if (Players != null)
            {
                foreach (var player in Players)
                {
                    var replaceResult = new ReplaceResult() { Item = player };
                    if (ReplacePlayer(player, replaceOptions, replaceResult, compare))
                        replaceResults.Add(replaceResult);
                }
            }
            return replaceResults;
        }

        /// <summary>
        /// Passes each field of the player object through the compare function. This function returns immediately after a match is found.
        /// This approach means the function implements a logical OR operation. It can be read as:
        ///     if the ID matches OR the first name matches OR the last name matches...etc then return true
        /// </summary>
        /// <param name="player">The player to search</param>
        /// <param name="findOptions">The options to use for the find operation</param>
        /// <param name="findResult">The result of the find operation</param>
        /// <param name="compare">The compare function to use</param>
        /// <returns>True if a match is found, false otherwise</returns>
        private bool FindPlayer(Player player, FindReplaceOptions findOptions, FindResult findResult, Func<string, FindReplaceOptions, bool> compare)
        {
            return FindField(nameof(player.Id), player.Id.ToString(), findOptions, findResult, compare) ||
                   FindField(nameof(player.FirstName), player.FirstName, findOptions, findResult, compare) ||
                   FindField(nameof(player.LastName), player.LastName, findOptions, findResult, compare) ||
                   FindField(nameof(player.Height), player.Height.ToString(), findOptions, findResult, compare) ||
                   FindField(nameof(player.Weight), player.Weight.ToString(), findOptions, findResult, compare) ||
                   FindField(nameof(player.DateOfBirth), player.DateOfBirth.ToString(), findOptions, findResult, compare) ||
                   FindField(nameof(player.PlaceOfBirth), player.PlaceOfBirth, findOptions, findResult, compare);
        }

        /// <summary>
        /// Passes each field of the player object through the compare function. For each field that matches a replace function is executed
        /// upon the field. The replace options determine whether the replace function is a string replace or a regex replace
        /// </summary>
        /// <param name="player">The player to search</param>
        /// <param name="replaceOptions">The options to use for the replace operation</param>
        /// <param name="replaceResult">The result of the replace operation</param>
        /// <param name="compare">The compare function to use</param>
        /// <returns>True if a replacement occurred, false otherwise</returns>
        private bool ReplacePlayer(Player player, ReplaceOptions replaceOptions, ReplaceResult replaceResult, Func<string, FindReplaceOptions, bool> compare)
        {
            bool replaced = ReplaceField(nameof(player.Id), player.Id.ToString(), replaceOptions, replaceResult, compare, (findWhat, replaceWith) =>
                                {
                                    replaceResult.ReplaceMessage = $"Will not replace the unique identifier \"{player.Id}\""; // Disallow unique IDs to replaced
                                    return string.Empty;
                                });

            if (ReplaceField(nameof(player.FirstName), player.FirstName, replaceOptions, replaceResult, compare, (findWhat, replaceWith) =>
                {
                    replaceResult.ReplaceMessage = $"Will not replace the unique identifier \"{player.FirstName}\""; // Disallow unique IDs to replaced
                    return string.Empty;
                }))
            {
                replaced = true;
            }

            if (ReplaceField(nameof(player.LastName), player.LastName, replaceOptions, replaceResult, compare, (findWhat, replaceWith) =>
                {
                    replaceResult.ReplaceMessage = $"Will not replace the unique identifier \"{player.LastName}\""; // Disallow unique IDs to replaced
                    return string.Empty;
                }))
            {
                replaced = true;
            }

            if (ReplaceField(nameof(player.Height), player.Height.ToString(), replaceOptions, replaceResult, compare, (findWhat, replaceWith) =>
                {
                    var h = ReplaceString(player.Height.ToString(), findWhat, replaceWith, replaceOptions);
                    if (int.TryParse(h, out int i))
                    {
                        player.Height = Math.Min(Player.MaxHeight, Math.Max(Player.MinHeight, i)); // Clamp to range
                        return player.Height.ToString();
                    }
                    replaceResult.ReplaceMessage = $"The replacement would produce an invalid integer. Not replacing \"{player.Height}\"";
                    return string.Empty;
                }))
            {
                replaced = true;
            }

            if (ReplaceField(nameof(player.Weight), player.Weight.ToString(), replaceOptions, replaceResult, compare, (findWhat, replaceWith) =>
                {
                    var w = ReplaceString(player.Weight.ToString(), findWhat, replaceWith, replaceOptions);
                    if (int.TryParse(w, out int i))
                    {
                        player.Weight = Math.Min(Player.MaxWeight, Math.Max(Player.MinWeight, i)); // Clamp to range
                        return player.Weight.ToString();
                    }
                    replaceResult.ReplaceMessage = $"The replacement would produce an invalid integer. Not replacing \"{player.Weight}\"";
                    return string.Empty;
                }))
            {
                replaced = true;
            }

            if (ReplaceField(nameof(player.DateOfBirth), player.DateOfBirth.ToString(), replaceOptions, replaceResult, compare, (findWhat, replaceWith) =>
                {
                    var dob = ReplaceString(player.DateOfBirth.ToString(), findWhat, replaceWith, replaceOptions);
                    if (DateTime.TryParse(dob, out DateTime dt))
                    {
                        var now = DateTime.Now;
                        var age = Utility.DateDiffAsYears(dt, now);
                        if (age < Player.MinAge)
                            dt = now.AddYears(-Player.MinAge); // Clamp to range
                        else if (age > Player.MaxAge)
                            dt = now.AddYears(-Player.MaxAge); // Clamp to range
                        player.DateOfBirth = dt;
                        return player.DateOfBirth.ToShortDateString();
                    }
                    replaceResult.ReplaceMessage = $"The replacement would produce an invalid date. Not replacing \"{player.DateOfBirth.ToShortDateString()}\"";
                    return string.Empty;
                }))
            {
                replaced = true;
            }

            if (ReplaceField(nameof(player.PlaceOfBirth), player.PlaceOfBirth, replaceOptions, replaceResult, compare, (findWhat, replaceWith) =>
                {
                    player.PlaceOfBirth = ReplaceString(player.PlaceOfBirth, findWhat, replaceWith, replaceOptions);
                    return player.PlaceOfBirth;
                }))
            {
                replaced = true;
            }

            return replaced;
        }

        /// <summary>
        /// Passes each signed player though the compare function, collating the results as it goes.
        /// </summary>
        /// <param name="findOptions">The options to use for the find operation</param>
        /// <param name="compare">The compare function to use</param>
        /// <returns>A container of results from the compare function</returns>
        private IEnumerable<FindResult> FindSignedPlayers(FindReplaceOptions findOptions, Func<string, FindReplaceOptions, bool> compare)
        {
            var findResults = new List<FindResult>();
            if (SignedPlayers != null)
            {
                foreach (var signedPlayer in SignedPlayers)
                {
                    var findResult = new FindResult() { Item = signedPlayer };
                    if (FindSignedPlayer(signedPlayer, findOptions, findResult, compare))
                        findResults.Add(findResult);
                }
            }
            return findResults;
        }

        /// <summary>
        /// Passes each signed player though the compare function, collating the results as it goes.
        /// </summary>
        /// <param name="replaceOptions">The options to use for the replace operation</param>
        /// <param name="compare">The compare function to use</param>
        /// <returns>A container of results from the compare function</returns>
        private IEnumerable<ReplaceResult> ReplaceSignedPlayers(ReplaceOptions replaceOptions, Func<string, FindReplaceOptions, bool> compare)
        {
            var replaceResults = new List<ReplaceResult>();
            if (SignedPlayers != null)
            {
                foreach (var signedPlayer in SignedPlayers)
                {
                    var replaceResult = new ReplaceResult() { Item = signedPlayer };
                    if (ReplaceSignedPlayer(signedPlayer, replaceOptions, replaceResult, compare))
                        replaceResults.Add(replaceResult);
                }
            }
            return replaceResults;
        }

        /// <summary>
        /// Passes each field of the signed player object through the compare function. This function returns immediately after a match is found.
        /// This approach means the function implements a logical OR operation. It can be read as:
        ///     if the player id matches or the player name matches or the last team name then return true
        /// </summary>
        /// <param name="signedPlayer">The signed player to search</param>
        /// <param name="findOptions">The options to use for the find operation</param>
        /// <param name="findResult">The result of the find operation</param>
        /// <param name="compare">The compare function to use</param>
        /// <returns>True if a match is found, false otherwise</returns>
        private bool FindSignedPlayer(SignedPlayer signedPlayer, FindReplaceOptions findOptions, FindResult findResult, Func<string, FindReplaceOptions, bool> compare)
        {
            return FindField(nameof(signedPlayer.PlayerId), signedPlayer.PlayerId.ToString(), findOptions, findResult, compare) ||
                   FindField(nameof(signedPlayer.PlayerName), signedPlayer.PlayerName, findOptions, findResult, compare) ||
                   FindField(nameof(signedPlayer.TeamName), signedPlayer.TeamName, findOptions, findResult, compare);
        }

        /// <summary>
        /// Passes each field of the signed player object through the compare function. For each field that matches a replace function is executed
        /// upon the field. The replace options determine whether the replace function is a string replace or a regex replace
        /// </summary>
        /// <param name="signedPlayer">The signed player to search</param>
        /// <param name="replaceOptions">The options to use for the replace operation</param>
        /// <param name="replaceResult">The result of the replace operation</param>
        /// <param name="compare">The compare function to use</param>
        /// <returns>True if a replacement occurred, false otherwise</returns>
        private bool ReplaceSignedPlayer(SignedPlayer signedPlayer, ReplaceOptions replaceOptions, ReplaceResult replaceResult, Func<string, FindReplaceOptions, bool> compare)
        {
            bool replaced = ReplaceField(nameof(signedPlayer.PlayerId), signedPlayer.PlayerId.ToString(), replaceOptions, replaceResult, compare, (findWhat, replaceWith) =>
                                {
                                    replaceResult.ReplaceMessage = $"Will not replace the unique identifier \"{signedPlayer.PlayerId}\""; // Disallow unique IDs to replaced
                                    return string.Empty;
                                });

            if (ReplaceField(nameof(signedPlayer.PlayerName), signedPlayer.PlayerName, replaceOptions, replaceResult, compare, (findWhat, replaceWith) =>
                {
                    replaceResult.ReplaceMessage = $"Will not replace the unique identifier \"{signedPlayer.PlayerName}\""; // Disallow unique IDs to replaced
                    return string.Empty;
                }))
            {
                replaced = true;
            }

            if (ReplaceField(nameof(signedPlayer.TeamName), signedPlayer.TeamName, replaceOptions, replaceResult, compare, (findWhat, replaceWith) =>
                {
                    replaceResult.ReplaceMessage = $"Will not replace the unique identifier \"{signedPlayer.TeamName}\""; // Disallow unique IDs to replaced
                    return string.Empty;
                }))
            {
                replaced = true;
            }

            return replaced;
        }

        /// <summary>
        /// Executes the compare function. If the compare passes then the result object is updated with the results.
        /// </summary>
        /// <param name="fieldName">The name of the field to compare</param>
        /// <param name="fieldValue">The value of the field to compare</param>
        /// <param name="findOptions">The options for the find operation</param>
        /// <param name="findResult">The results of the find operation</param>
        /// <param name="compare">The compare function</param>
        /// <returns>True if a match was made and the find result object updated</returns>
        private bool FindField(string fieldName,
                               string fieldValue,
                               FindReplaceOptions findOptions,
                               FindResult findResult,
                               Func<string, FindReplaceOptions, bool> compare)
        {
            if (compare(fieldValue, findOptions))
            {
                findResult.FieldName = fieldName;
                findResult.FieldValue = string.Copy(fieldValue);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Executes the compare function. If the compare passes then the replace function is executed. Both the result
        /// of the find and the result of the replace are written into the result object.
        /// </summary>
        /// <param name="fieldName">The name of the field to compare</param>
        /// <param name="fieldValue">The value of the field to compare</param>
        /// <param name="replaceOptions">The options for the replace operation</param>
        /// <param name="replaceResult">The results of the replace operation</param>
        /// <param name="compare">The compare function</param>
        /// <param name="replace">The replace function</param>
        /// <returns>True if a match was made and the replace result object updated</returns>
        private bool ReplaceField(string fieldName,
                                  string fieldValue,
                                  ReplaceOptions replaceOptions,
                                  ReplaceResult replaceResult,
                                  Func<string, FindReplaceOptions, bool> compare,
                                  Func<string, string, string> replace)
        {
            if (compare(fieldValue, replaceOptions))
            {
                replaceResult.FieldName = fieldName;
                replaceResult.FieldValue = string.Copy(fieldValue);
                replaceResult.ReplacedValue = replace(replaceOptions.FindWhat, replaceOptions.ReplaceWith);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Executes the regular expression
        /// </summary>
        /// <param name="textValue">The text that is passed to the regex engine</param>
        /// <param name="_">The unused find/replace options</param>
        /// <param name="regex">The regular expression</param>
        /// <returns>True if a match occurs, false otherwise</returns>
        private bool RegexCompare(string textValue, FindReplaceOptions _, Regex regex)
        {
            return regex.IsMatch(textValue); // The regex was initialised with the FindReplaceOptions earlier.
        }

        /// <summary>
        /// Executes either a substring compare or a whole string compare
        /// </summary>
        /// <param name="textValue">The text to match with</param>
        /// <param name="findOptions">The options for the find operation</param>
        /// <returns>True if a match occurs, false otherwise</returns>
        private bool StringCompare(string textValue, FindReplaceOptions findOptions)
        {
            var stringComparison = findOptions.MatchCase ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
            if (findOptions.MatchWholeWord)
                return textValue.Equals(findOptions.FindWhat, stringComparison);
            return textValue.IndexOf(findOptions.FindWhat, stringComparison) >= 0;
        }

        /// <summary>
        /// Executes a string replacement. The replacement will be performed either by the regex engine or by
        /// a string function.
        /// </summary>
        /// <param name="fieldValue"></param>
        /// <param name="findWhat"></param>
        /// <param name="replaceWith"></param>
        /// <param name="replaceOptions"></param>
        /// <returns>The string that resulted from the replace operation</returns>
        private string ReplaceString(string fieldValue, string findWhat, string replaceWith, ReplaceOptions replaceOptions)
        {
            if (replaceOptions.UseRegularExpression)
                return _findReplaceRegex.Replace(fieldValue, replaceWith); // The regex was initialised with the FindReplaceOptions earlier.

            if (replaceOptions.MatchCase)
                return fieldValue.Replace(findWhat, replaceWith);

            // There is no case insensitive string.Replace in .NET 4.7.2, so we'll use regex.
            return Regex.Replace(fieldValue, Regex.Escape(findWhat), replaceWith.Replace("$", "$$"), RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Finds, and optionally replaces, all items that match the given advanced find options. Will report the result to the view layer.
        /// </summary>
        /// <param name="advancedFindReplaceOptions">The options for the find/replace operation</param>
        public void AdvancedFindAndReplace(AdvancedFindReplaceOptions advancedFindReplaceOptions)
        {
            if (advancedFindReplaceOptions == null)
                throw new ArgumentNullException(nameof(advancedFindReplaceOptions));
            if (advancedFindReplaceOptions.FindFields == null)
                throw new ArgumentNullException(nameof(advancedFindReplaceOptions.FindFields));

            var advancedResults = new List<AdvancedResult>();
            switch (advancedFindReplaceOptions.FindWhat) // This find/replace algorithm uses only one dataset
            {
                case AdvancedFindReplaceOptions.What.Teams:
                    advancedResults.AddRange(FindTeams(advancedFindReplaceOptions));
                    break;
                case AdvancedFindReplaceOptions.What.Players:
                    advancedResults.AddRange(FindPlayers(advancedFindReplaceOptions));
                    break;
                case AdvancedFindReplaceOptions.What.SignedPlayers:
                    advancedResults.AddRange(FindSignedPlayers(advancedFindReplaceOptions));
                    break;
            }

            if (advancedFindReplaceOptions.ReplaceField != null) // is the user asking for a replacement to occur?
            {
                foreach (var result in advancedResults)
                {
                    if (result.Item is Team team)
                        _view.OnTeamEdited(team); // Notify the view layer
                    else if (result.Item is Player player)
                        _view.OnPlayerEdited(player); // Notify the view layer
                    // Signed players are not modified via Replace
                }

                if (advancedResults.Count > 0)
                    IsModified = true;
                _view.OnReplaceResults(advancedResults); // Notify the view layer
            }
            else
                _view.OnFindResults(advancedResults); // Notify the view layer
        }

        /// <summary>
        /// Passes each team though the compare function, collating the results as it goes.
        /// </summary>
        /// <param name="advancedFindReplaceOptions">The options to use for the find operation</param>
        /// <returns>A container of results from the find operation</returns>
        private IEnumerable<AdvancedResult> FindTeams(AdvancedFindReplaceOptions advancedFindReplaceOptions)
        {
            List<AdvancedResult> replaceResults = new List<AdvancedResult>();
            if (Teams != null)
            {
                foreach (var team in Teams)
                {
                    AdvancedResult advancedResult = new AdvancedResult() { Item = team };
                    if (FindTeam(team, advancedFindReplaceOptions, advancedResult))
                        replaceResults.Add(advancedResult);
                }
            }
            return replaceResults;
        }

        /// <summary>
        /// Passes each player though the compare function, collating the results as it goes.
        /// </summary>
        /// <param name="advancedFindReplaceOptions">The options to use for the find operation</param>
        /// <returns>A container of results from the find operation</returns>
        private IEnumerable<AdvancedResult> FindPlayers(AdvancedFindReplaceOptions advancedFindReplaceOptions)
        {
            List<AdvancedResult> replaceResults = new List<AdvancedResult>();
            if (Players != null)
            {
                foreach (var player in Players)
                {
                    AdvancedResult advancedResult = new AdvancedResult() { Item = player };
                    if (FindPlayer(player, advancedFindReplaceOptions, advancedResult))
                        replaceResults.Add(advancedResult);
                }
            }
            return replaceResults;
        }

        /// <summary>
        /// Passes each signed player though the compare function, collating the results as it goes.
        /// </summary>
        /// <param name="advancedFindReplaceOptions">The options to use for the find operation</param>
        /// <returns>A container of results from the find operation</returns>
        private IEnumerable<AdvancedResult> FindSignedPlayers(AdvancedFindReplaceOptions advancedFindReplaceOptions)
        {
            List<AdvancedResult> replaceResults = new List<AdvancedResult>();
            if (SignedPlayers != null)
            {
                foreach (var signedPlayer in SignedPlayers)
                {
                    AdvancedResult advancedResult = new AdvancedResult() { Item = signedPlayer };
                    if (FindSignedPlayer(signedPlayer, advancedFindReplaceOptions, advancedResult))
                        replaceResults.Add(advancedResult);
                }
            }
            return replaceResults;
        }

        /// <summary>
        /// Compares fields from the team object against the find options. The user chooses which fields to match
        /// against. Therefore, this function will return immediately if a field was chosen by the player and does
        /// not match.
        /// This approach means the function implements a logical AND operation. It can be read as:
        ///     if the name matches AND the home ground matches AND the coach matches...etc then return true
        /// </summary>
        /// <param name="team">The team whose fields are to be matched</param>
        /// <param name="advancedFindReplaceOptions">The options for the find operation</param>
        /// <param name="advancedResult">The result of the find operation</param>
        /// <returns>True if a match was made, false otherwise</returns>
        private bool FindTeam(Team team, AdvancedFindReplaceOptions advancedFindReplaceOptions, AdvancedResult advancedResult)
        {
            // Return immediately if the any of the fields were included by the user and didn't match the find options
            var nameResult = AdvancedCompare(nameof(team.Name), team.Name, advancedFindReplaceOptions, advancedResult);
            if (nameResult == AdvancedCompareResult.FieldIncludedDidntMatch)
                return false;
            var homeGroundResult = AdvancedCompare(nameof(team.HomeGround), team.HomeGround, advancedFindReplaceOptions, advancedResult);
            if (homeGroundResult == AdvancedCompareResult.FieldIncludedDidntMatch)
                return false;
            var coachResult = AdvancedCompare(nameof(team.Coach), team.Coach, advancedFindReplaceOptions, advancedResult);
            if (coachResult == AdvancedCompareResult.FieldIncludedDidntMatch)
                return false;
            var regionResult = AdvancedCompare(nameof(team.Region), team.Region, advancedFindReplaceOptions, advancedResult);
            if (regionResult == AdvancedCompareResult.FieldIncludedDidntMatch)
                return false;
            var yearFoundedResult = AdvancedCompare(nameof(team.YearFounded), team.YearFounded, advancedFindReplaceOptions, advancedResult);
            if (yearFoundedResult == AdvancedCompareResult.FieldIncludedDidntMatch)
                return false;

            if (advancedFindReplaceOptions.ReplaceField != null) // is the user asking for a replacement to occur?
            {
                advancedResult.ReplacedField = advancedFindReplaceOptions.ReplaceField.Name;

                if (advancedFindReplaceOptions.ReplaceField.Name == nameof(team.Name))
                    advancedResult.ReplaceMessage = $"Will not replace the unique identifier \"{team.Name}\""; // Disallow unique IDs to replaced
                else if (advancedFindReplaceOptions.ReplaceField.Name == nameof(team.HomeGround))
                {
                    team.HomeGround = advancedFindReplaceOptions.ReplaceField.Value as string;
                    advancedResult.ReplacedValue = team.HomeGround;
                }
                else if (advancedFindReplaceOptions.ReplaceField.Name == nameof(team.Coach))
                {
                    team.Coach = advancedFindReplaceOptions.ReplaceField.Value as string;
                    advancedResult.ReplacedValue = team.Coach;
                }
                else if (advancedFindReplaceOptions.ReplaceField.Name == nameof(team.Region))
                {
                    team.Region = advancedFindReplaceOptions.ReplaceField.Value as string;
                    advancedResult.ReplacedValue = team.Region;
                }
                else if (advancedFindReplaceOptions.ReplaceField.Name == nameof(team.YearFounded))
                {
                    team.YearFounded = (int)advancedFindReplaceOptions.ReplaceField.Value;
                    advancedResult.ReplacedValue = team.YearFounded.ToString();
                }
            }

            return true;
        }

        /// <summary>
        /// Compares fields from the player object against the find options. The user chooses which fields to match
        /// against. Therefore, this function will return immediately if a field was chosen by the player and does
        /// not match.
        /// This approach means the function implements a logical AND operation. It can be read as:
        ///     if the ID AND the first name matches AND the last name matches...etc then return true
        /// </summary>
        /// <param name="player">The player whose fields are to be matched</param>
        /// <param name="advancedFindReplaceOptions">The options for the find operation</param>
        /// <param name="advancedResult">The result of the find operation</param>
        /// <returns>True if a match was made, false otherwise</returns>
        private bool FindPlayer(Player player, AdvancedFindReplaceOptions advancedFindReplaceOptions, AdvancedResult advancedResult)
        {
            // Return immediately if the any of the fields were included by the user and didn't match the find options
            var idResult = AdvancedCompare(nameof(player.Id), player.Id, advancedFindReplaceOptions, advancedResult);
            if (idResult == AdvancedCompareResult.FieldIncludedDidntMatch)
                return false;
            var firstNameResult = AdvancedCompare(nameof(player.FirstName), player.FirstName, advancedFindReplaceOptions, advancedResult);
            if (firstNameResult == AdvancedCompareResult.FieldIncludedDidntMatch)
                return false;
            var lastNameResult = AdvancedCompare(nameof(player.LastName), player.LastName, advancedFindReplaceOptions, advancedResult);
            if (lastNameResult == AdvancedCompareResult.FieldIncludedDidntMatch)
                return false;
            var heightResult = AdvancedCompare(nameof(player.Height), player.Height, advancedFindReplaceOptions, advancedResult);
            if (heightResult == AdvancedCompareResult.FieldIncludedDidntMatch)
                return false;
            var weightResult = AdvancedCompare(nameof(player.Weight), player.Weight, advancedFindReplaceOptions, advancedResult);
            if (weightResult == AdvancedCompareResult.FieldIncludedDidntMatch)
                return false;
            var dateOfBirthResult = AdvancedCompare(nameof(player.DateOfBirth), player.DateOfBirth, advancedFindReplaceOptions, advancedResult);
            if (dateOfBirthResult == AdvancedCompareResult.FieldIncludedDidntMatch)
                return false;
            var placeOfBirthResult = AdvancedCompare(nameof(player.PlaceOfBirth), player.PlaceOfBirth, advancedFindReplaceOptions, advancedResult);
            if (placeOfBirthResult == AdvancedCompareResult.FieldIncludedDidntMatch)
                return false;

            if (advancedFindReplaceOptions.ReplaceField != null) // is the user asking for a replacement to occur?
            {
                advancedResult.ReplacedField = advancedFindReplaceOptions.ReplaceField.Name;

                if (advancedFindReplaceOptions.ReplaceField.Name == nameof(player.Id))
                    advancedResult.ReplaceMessage = $"Will not replace the unique identifier \"{player.Id}\""; // Disallow unique IDs to replaced
                else if (advancedFindReplaceOptions.ReplaceField.Name == nameof(player.FirstName))
                {
                    player.FirstName = advancedFindReplaceOptions.ReplaceField.Value as string;
                    advancedResult.ReplacedValue = player.FirstName;
                }
                else if (advancedFindReplaceOptions.ReplaceField.Name == nameof(player.LastName))
                {
                    player.LastName = advancedFindReplaceOptions.ReplaceField.Value as string;
                    advancedResult.ReplacedValue = player.LastName;
                }
                else if (advancedFindReplaceOptions.ReplaceField.Name == nameof(player.Height))
                {
                    player.Height = (int)advancedFindReplaceOptions.ReplaceField.Value;
                    advancedResult.ReplacedValue = player.Height.ToString();
                }
                else if (advancedFindReplaceOptions.ReplaceField.Name == nameof(player.Weight))
                {
                    player.Weight = (int)advancedFindReplaceOptions.ReplaceField.Value;
                    advancedResult.ReplacedValue = player.Weight.ToString();
                }
                else if (advancedFindReplaceOptions.ReplaceField.Name == nameof(player.DateOfBirth))
                {
                    if (advancedFindReplaceOptions.ReplaceField.Value is DateTime dateTime)
                    {
                        player.DateOfBirth = dateTime;
                        advancedResult.ReplacedValue = player.DateOfBirth.ToShortDateString();
                    }
                }
                else if (advancedFindReplaceOptions.ReplaceField.Name == nameof(player.PlaceOfBirth))
                {
                    player.PlaceOfBirth = advancedFindReplaceOptions.ReplaceField.Value as string;
                    advancedResult.ReplacedValue = player.PlaceOfBirth;
                }
            }

            return true;
        }

        /// <summary>
        /// Compares fields from the signed player object against the find options. The user chooses which fields to match
        /// against. Therefore, this function will return immediately if a field was chosen by the player and does
        /// not match.
        /// This approach means the function implements a logical AND operation. It can be read as:
        ///     if the player id matches and the player name matches and the team name matches then return true
        /// </summary>
        /// <param name="signedPlayer">The signed player whose fields are to be matched</param>
        /// <param name="advancedFindReplaceOptions">The options for the find operation</param>
        /// <param name="advancedResult">The result of the find operation</param>
        /// <returns>True if a match was made, false otherwise</returns>
        private bool FindSignedPlayer(SignedPlayer signedPlayer, AdvancedFindReplaceOptions advancedFindReplaceOptions, AdvancedResult advancedResult)
        {
            // Return immediately if the any of the fields were included by the user and didn't match the find options
            var playerIdResult = AdvancedCompare(nameof(signedPlayer.PlayerId), signedPlayer.PlayerId, advancedFindReplaceOptions, advancedResult);
            if (playerIdResult == AdvancedCompareResult.FieldIncludedDidntMatch)
                return false;
            var playerNameResult = AdvancedCompare(nameof(signedPlayer.PlayerName), signedPlayer.PlayerName, advancedFindReplaceOptions, advancedResult);
            if (playerNameResult == AdvancedCompareResult.FieldIncludedDidntMatch)
                return false;
            var teamNameResult = AdvancedCompare(nameof(signedPlayer.TeamName), signedPlayer.TeamName, advancedFindReplaceOptions, advancedResult);
            if (teamNameResult == AdvancedCompareResult.FieldIncludedDidntMatch)
                return false;

            if (advancedFindReplaceOptions.ReplaceField != null) // is the user asking for a replacement to occur?
            {
                advancedResult.ReplacedField = advancedFindReplaceOptions.ReplaceField.Name;
                advancedResult.ReplaceMessage = $"Will not replace the unique identifier \"{advancedFindReplaceOptions.ReplaceField.Name}\""; // Disallow unique IDs to replaced
            }

            return true;
        }

        /// <summary>
        /// Defines the possible return values for the AdvancedCompare method
        /// </summary>
        private enum AdvancedCompareResult
        {
            /// <summary>
            /// The user did not ask for this field to be searched
            /// </summary>
            FieldExcluded,
            /// <summary>
            /// The user asked for this field to be searched and it matched
            /// </summary>
            FieldIncludedMatched,
            /// <summary>
            /// The user asked for this field to be searched and it did not match
            /// </summary>
            FieldIncludedDidntMatch
        }

        /// <summary>
        /// Compares the passed in field value aginst the find/replace options. Not every field within an object is searched, therefore,
        /// if the options do not include the passed in field then no comparison occurs.
        /// </summary>
        /// <param name="fieldName">The name of the field to compare</param>
        /// <param name="fieldValue">The value to compare</param>
        /// <param name="advancedFindReplaceOptions">The options for the find/replace operation</param>
        /// <param name="advancedResult">The result of the find/replace operation</param>
        /// <returns>Whether or not the filed was included by the user, and if so, whether or not it matched</returns>
        private AdvancedCompareResult AdvancedCompare(string fieldName, object fieldValue, AdvancedFindReplaceOptions advancedFindReplaceOptions, AdvancedResult advancedResult)
        {
            var noSpaces = string.Copy(fieldName).Replace(" ", ""); // Strip spaces
            var findField = advancedFindReplaceOptions.FindFields.FirstOrDefault(x => x.Name.Equals(noSpaces, StringComparison.OrdinalIgnoreCase));
            if (findField == null) // The user did not ask for this field to be searched
                return AdvancedCompareResult.FieldExcluded;

            if (!AdvancedFieldCompare(fieldValue, findField))
                return AdvancedCompareResult.FieldIncludedDidntMatch; // End now if there was no match

            // A match occurred. Bundle up everything we know so that we can send it up to the view layer.
            string value;
            if (fieldValue is DateTime dateTime)
                value = dateTime.ToShortDateString();
            else
                value = fieldValue.ToString();

            if (advancedResult.Fields == null)
                advancedResult.Fields = new List<Tuple<string, string>>();
            advancedResult.Fields.Add(Tuple.Create(fieldName, value));

            return AdvancedCompareResult.FieldIncludedMatched;
        }

        /// <summary>
        /// Runs the compare operation by delegating to a type specific compare function.
        /// </summary>
        /// <param name="fieldValue">The value to compare</param>
        /// <param name="findField">The field within an object to inspect</param>
        /// <returns>True if the value matches the field, false otherwise</returns>
        private bool AdvancedFieldCompare(object fieldValue, FindField findField)
        {
            if (fieldValue == null)
                return false;
            if (findField.Type == typeof(DateTime))
            {
                if (fieldValue is DateTime dateTime)
                    return AdvancedDateTimeCompare(dateTime, findField);
            }
            if (findField.Type == typeof(int))
                return AdvancedIntegerCompare((int)fieldValue, findField);
            if (findField.Type == typeof(string))
                return AdvancedStringCompare(fieldValue as string, findField);
            return false;
        }

        /// <summary>
        /// Compares two dates using a user chosen find operation.
        /// </summary>
        /// <param name="fieldValue">The value to compare</param>
        /// <param name="findField">The field within an object to inspect</param>
        /// <returns>True if a match occurs, false otherwise</returns>
        private bool AdvancedDateTimeCompare(DateTime fieldValue, FindField findField)
        {
            if (!(findField.BeginValue is DateTime beginDate))
                return false; // Don't bother

            if (findField.Operation == FindOperation.EqualTo)
                return fieldValue == beginDate;

            if (findField.Operation == FindOperation.Before)
                return fieldValue < beginDate;

            if (findField.Operation == FindOperation.After)
                return fieldValue > beginDate;

            if (findField.Operation == FindOperation.Between)
            {
                if (findField.EndValue is DateTime endDate)
                    return fieldValue > beginDate && fieldValue < endDate;
            }

            return false;
        }

        /// <summary>
        /// Compares two intergers using a user chosen find operation.
        /// </summary>
        /// <param name="fieldValue">The value to compare</param>
        /// <param name="findField">The field within an object to inspect</param>
        /// <returns>True if a match occurs, false otherwise</returns>
        private bool AdvancedIntegerCompare(int fieldValue, FindField findField)
        {
            if (findField.Operation == FindOperation.EqualTo)
                return fieldValue == (int)findField.BeginValue;

            if (findField.Operation == FindOperation.FewerThan)
                return fieldValue < (int)findField.BeginValue;

            if (findField.Operation == FindOperation.GreaterThan)
                return fieldValue > (int)findField.BeginValue;

            if (findField.Operation == FindOperation.Between)
                return fieldValue > (int)findField.BeginValue && fieldValue < (int)findField.EndValue;

            return false;
        }

        /// <summary>
        /// Compares two strings using a user chosen find operation.
        /// </summary>
        /// <param name="fieldValue">The value to compare</param>
        /// <param name="findField">The field within an object to inspect</param>
        /// <returns>True if a match occurs, false otherwise</returns>
        private bool AdvancedStringCompare(string fieldValue, FindField findField)
        {
            if (findField.Operation == FindOperation.IsEqualTo)
                return fieldValue.Equals(findField.BeginValue as string, StringComparison.OrdinalIgnoreCase);

            if (findField.Operation == FindOperation.Contains)
                return CultureInfo.CurrentCulture.CompareInfo.IndexOf(fieldValue, findField.BeginValue as string, CompareOptions.IgnoreCase) >= 0;

            if (findField.Operation == FindOperation.StartsWith)
                return fieldValue.StartsWith(findField.BeginValue as string, StringComparison.OrdinalIgnoreCase);

            if (findField.Operation == FindOperation.EndsWith)
                return fieldValue.EndsWith(findField.BeginValue as string, StringComparison.OrdinalIgnoreCase);

            return false;
        }
    }
}
