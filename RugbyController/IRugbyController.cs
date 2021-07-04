using RugbyView;
using System.Collections.Generic;

namespace RugbyController
{
    /// <summary>
    /// This class is a contract between the Controller layer and the main window. The main window will use the methods within
    /// this interface to communicate to the Controller. These methods represent significant actions that the user will perform,
    /// therefore these actions are justified in having a dedicated method within this interface.
    /// This layer responds to most of the actions by notifying the View layer, this prevents the main window calling back into
    /// the Controller, or in other words, going around in cirlces. There are a few methods within this interface that directly
    /// return data for convenience.
    /// </summary>
    /// <remarks>
    /// <para>Author: Damian Coventry</para>
    /// <para>Date: Feb-May 2021</para>
    /// </remarks>
    public interface IRugbyController
    {
        /// <summary>
        /// Returns whether or not a Rugby Union document is open
        /// </summary>
        bool IsOpen { get; }
        /// <summary>
        /// Returns whether or not the Rugby Union document has been modified
        /// </summary>
        bool IsModified { get; }
        /// <summary>
        /// Returns the Rugby Union document's name
        /// </summary>
        string RugbyUnionName { get; }
        /// <summary>
        /// Returns whether or not the Rugby Union document has been saved
        /// </summary>
        bool HasBeenSaved { get; }
        /// <summary>
        /// Returns the Rugby Union document's pathname
        /// </summary>
        string PathName { get; }

        /// <summary>
        /// Returns a reference to the internal Teams container
        /// </summary>
        List<RugbyModel.Team> Teams { get; }
        /// <summary>
        /// Returns a reference to the internal Player container
        /// </summary>
        List<RugbyModel.Player> Players { get; }

        /// <summary>
        /// Creates a new Rugby Union document. Will report the result to the view layer.
        /// </summary>
        /// <param name="rugbyUnionName">The name to assign to the Rugby Union</param>
        void NewRugbyUnion(string rugbyUnionName);
        /// <summary>
        /// Sets the name of the Rugby Union document. Will report the result to the view layer.
        /// </summary>
        /// <param name="rugbyUnionName">The name to assign to the Rugby Union</param>
        void RenameRugbyUnion(string rugbyUnionName);
        /// <summary>
        /// Loads a Rugby Union from a file. Will report the result to the view layer.
        /// </summary>
        /// <param name="pathName">The pathname of a Rugby Union file</param>
        void OpenRugbyUnion(string pathName);
        /// <summary>
        /// Closes a Rugby Union document. Will report the result to the view layer.
        /// </summary>
        void CloseRugbyUnion();
        /// <summary>
        /// Saves a Rugby Union document using a previously set filename. Will report the result to the view layer.
        /// </summary>
        void SaveRugbyUnion();
        /// <summary>
        /// Saves a Rugby Union document into the file identified by the pathname. Will report the result to the view layer.
        /// </summary>
        /// <param name="pathName">The pathname of a file to write the Rugby Union into</param>
        void SaveAsRugbyUnion(string pathName);

        /// <summary>
        /// Adds a team into the Rugby Union document. Will report the result to the view layer.
        /// </summary>
        /// <param name="team">A reference to a team to clone</param>
        void AddTeam(RugbyModel.Team team);
        /// <summary>
        /// Edits an existring team within the Rugby Union document. Will report the result to the view layer.
        /// </summary>
        /// <param name="team">A reference to a team to clone</param>
        void EditTeam(RugbyModel.Team team);
        /// <summary>
        /// Renames an existring team within the Rugby Union document. Will report the result to the view layer.
        /// </summary>
        /// <param name="oldName">The old name of the team</param>
        /// <param name="newName">The new name of the team</param>
        void RenameTeam(string oldName, string newName);
        /// <summary>
        /// Deletes an existring team from the Rugby Union document. Will report the result to the view layer.
        /// </summary>
        /// <param name="teamName">The name of the team</param>
        void DeleteTeam(string teamName);
        /// <summary>
        /// Deletes existring teams from the Rugby Union document. Will report the result to the view layer.
        /// </summary>
        /// <param name="teamNames">The names of the teams</param>
        void DeleteTeams(List<string> teamNames);
        /// <summary>
        /// Deletes all existring teams from the Rugby Union document. Will report the result to the view layer.
        /// </summary>
        void DeleteAllTeams();
        /// <summary>
        /// Finds an existring team within the Rugby Union document.
        /// </summary>
        /// <param name="teamName">The name of the team</param>
        /// <returns>A reference to the team, or null</returns>
        RugbyModel.Team FindTeam(string teamName);
        /// <summary>
        /// Returns whether or not there are teams available within the Rugby Union document.
        /// </summary>
        bool TeamsAvailable { get; }

        /// <summary>
        /// Adds a player into the Rugby Union document. Will report the result to the view layer.
        /// </summary>
        /// <param name="player">A reference to a player to clone</param>
        /// <returns>The ID of the new player</returns>
        int AddPlayer(RugbyModel.Player player);
        /// <summary>
        /// Edits an existing player within the Rugby Union document. Will report the result to the view layer.
        /// </summary>
        /// <param name="player">A reference to a player to clone</param>
        void EditPlayer(RugbyModel.Player player);
        /// <summary>
        /// Deletes an existing player from the Rugby Union document. Will report the result to the view layer.
        /// </summary>
        /// <param name="playerId">The ID of the player</param>
        void DeletePlayer(int playerId);
        /// <summary>
        /// Deletes existing players from the Rugby Union document. Will report the result to the view layer.
        /// </summary>
        /// <param name="playerIds">The IDs of the players</param>
        void DeletePlayers(List<int> playerIds);
        /// <summary>
        /// Deletes all existing players from the Rugby Union document. Will report the result to the view layer.
        /// </summary>
        void DeleteAllPlayers();
        /// <summary>
        /// Finds an existing player within the Rugby Union document.
        /// </summary>
        /// <param name="playerId">The ID of the player</param>
        /// <returns>A reference to the player, or null</returns>
        RugbyModel.Player FindPlayer(int playerId);
        /// <summary>
        /// Finds an existing player within the Rugby Union document.
        /// </summary>
        /// <param name="firstName">The first name of the player</param>
        /// <param name="lastName">The last name of the player</param>
        /// <returns>A reference to the player, or null</returns>
        RugbyModel.Player FindPlayer(string firstName, string lastName);
        /// <summary>
        /// Returns whether or not there are players available within the Rugby Union document.
        /// </summary>
        bool PlayersAvailable { get; }

        /// <summary>
        /// Signs an existing player to an existing team within the Rugby Union document. Will report the result to the view layer.
        /// </summary>
        /// <param name="playerId">The ID of the player</param>
        /// <param name="teamName">The name of the team</param>
        void SignPlayerToTeam(int playerId, string teamName);
        /// <summary>
        /// Unsigns an existing player from an existing team within the Rugby Union document. Will report the result to the view layer.
        /// </summary>
        /// <param name="playerId">The ID of the player</param>
        /// <param name="teamName">The name of the team</param>
        void UnsignPlayerFromTeam(int playerId, string teamName);
        /// <summary>
        /// Unsigns existing players from existing teams within the Rugby Union document. Will report the result to the view layer.
        /// </summary>
        /// <param name="signedPlayers">A list of references to signed players</param>
        void UnsignPlayersFromTeams(List<RugbyModel.SignedPlayer> signedPlayers);
        /// <summary>
        /// Unsigns all existing players from an existing team within the Rugby Union document. Will report the result to the view layer.
        /// </summary>
        /// <param name="teamName">The name of the team</param>
        void UnsignAllPlayersFromTeam(string teamName);
        /// <summary>
        /// Unsigns all existing players from all existing teams within the Rugby Union document. Will report the result to the view layer.
        /// </summary>
        void UnsignAllPlayersFromAllTeams();
        /// <summary>
        /// Finds an existing signed player within the Rugby Union document.
        /// </summary>
        /// <param name="playerId">The ID of the player</param>
        /// <returns>A reference to the signed player, or null</returns>
        RugbyModel.SignedPlayer FindSignedPlayer(int playerId);
        /// <summary>
        /// Gets the list of all signed players to a team within the Rugby Union document.
        /// </summary>
        /// <param name="teamName">The name of the team</param>
        /// <returns>A list of references to the signed players, or null</returns>
        List<RugbyModel.SignedPlayer> GetPlayersSignedToTeam(string teamName);
        /// <summary>
        /// Returns whether or not there are signed players available within the Rugby Union document.
        /// </summary>
        bool SignedPlayersAvailable { get; }

        /// <summary>
        /// Finds all items that match the given find options. Will report the result to the view layer.
        /// </summary>
        /// <param name="findOptions">The options for the find operation</param>
        void Find(FindReplaceOptions findOptions);
        /// <summary>
        /// Finds all items that match the given find options, then replaces those items with the replace data. Will report the result to the view layer.
        /// </summary>
        /// <param name="replaceOptions">The options for the replace operation</param>
        void Replace(ReplaceOptions replaceOptions);
        /// <summary>
        /// Finds, and optionally replaces, all items that match the given advanced find options. Will report the result to the view layer.
        /// </summary>
        /// <param name="advancedFindReplaceOptions">The options for the find/replace operation</param>
        void AdvancedFindAndReplace(AdvancedFindReplaceOptions advancedFindReplaceOptions);
    }
}
