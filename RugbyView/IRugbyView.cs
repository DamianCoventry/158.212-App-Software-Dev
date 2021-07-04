using RugbyModel;
using System.Collections.Generic;

namespace RugbyView
{
    /// <summary>
    /// This class is a contract between the Controller layer and the View layer. The Controller layer will use the methods within
    /// this interface to communicate to the layer above. These methods represent significant events that add value to the user experience,
    /// therefore, they are justified in having a dedicated method within this interface.
    /// The main window of the application implements this interface, copying the passed values into GUI controls for the user to view.
    /// </summary>
    /// <remarks>
    /// <para>Author: Damian Coventry</para>
    /// <para>Date: Feb-May 2021</para>
    /// </remarks>
    public interface IRugbyView
    {
        /// <summary>
        /// Reports that a Rugby Union has been created.
        /// </summary>
        /// <param name="rugbyUnionName">The name of the Rugby Union</param>
        void OnRugbyUnionCreated(string rugbyUnionName);
        /// <summary>
        /// Reports that a Rugby Union has been opened.
        /// </summary>
        /// <param name="name">The name of the Rugby Union</param>
        /// <param name="pathName">The path name of the Rugby Union file</param>
        /// <param name="teams">The list of teams within the Rugby Union</param>
        /// <param name="players">The list of players within the Rugby Union</param>
        /// <param name="signedPlayers">The list of signed players within the Rugby Union</param>
        void OnRugbyUnionOpened(string name, string pathName, List<Team> teams, List<Player> players, List<SignedPlayer> signedPlayers);
        /// <summary>
        /// Reports that a Rugby Union has been closed.
        /// </summary>
        void OnRugbyUnionClosed();
        /// <summary>
        /// Reports that a Rugby Union has been rename.
        /// </summary>
        /// <param name="oldName">The old name of the Rugby Union</param>
        /// <param name="newName">The new name of the Rugby Union</param>
        void OnRugbyUnionRenamed(string oldName, string newName);
        /// <summary>
        /// Reports that a Rugby Union has been saved.
        /// </summary>
        /// <param name="pathName">The path name of the Rugby Union file</param>
        void OnRugbyUnionSaved(string pathName);
        /// <summary>
        /// Reports that a Team has been added.
        /// </summary>
        /// <param name="team">A reference to the team</param>
        void OnTeamAdded(Team team);
        /// <summary>
        /// Reports that a Team has been edited.
        /// </summary>
        /// <param name="team">A reference to the team</param>
        void OnTeamEdited(Team team);
        /// <summary>
        /// Reports that a Team has been rename.
        /// </summary>
        /// <param name="oldName">The old name of the Team</param>
        /// <param name="newName">The new name of the Team</param>
        void OnTeamRenamed(string oldName, string newName);
        /// <summary>
        /// Reports that a Team has been deleted.
        /// </summary>
        /// <param name="teamName">The name of the Team</param>
        void OnTeamDeleted(string teamName);
        /// <summary>
        /// Reports that many Teams have been deleted.
        /// </summary>
        /// <param name="teamNames">The name of the Teams</param>
        void OnTeamsDeleted(List<string> teamNames);
        /// <summary>
        /// Reports that a Player has been created.
        /// </summary>
        /// <param name="player">A reference to the player</param>
        void OnPlayerAdded(Player player);
        /// <summary>
        /// Reports that a Player has been delete.
        /// </summary>
        /// <param name="playerId">The ID of the player</param>
        void OnPlayerDeleted(int playerId);
        /// <summary>
        /// Reports that a Player has been edited.
        /// </summary>
        /// <param name="player">A reference to the player</param>
        void OnPlayerEdited(Player player);
        /// <summary>
        /// Reports that many Players have been deleted.
        /// </summary>
        /// <param name="playerIds">The IDs of the players</param>
        void OnPlayersDeleted(List<int> playerIds);
        /// <summary>
        /// Reports that a Player has been signed to a Team.
        /// </summary>
        /// <param name="playerId">The ID of the Player</param>
        /// <param name="teamName">The name of the Team</param>
        void OnPlayerSignedToTeam(int playerId, string teamName);
        /// <summary>
        /// Reports that a Player has been unsigned to a Team.
        /// </summary>
        /// <param name="playerId">The ID of the Player</param>
        /// <param name="teamName">The name of the Team</param>
        void OnPlayerUnsignedFromTeam(int playerId, string teamName);
        /// <summary>
        /// Reports that many Players have been unsigned from Teams.
        /// </summary>
        /// <param name="signedPlayers">Reference to the unsigned players</param>
        void OnPlayersUnsignedFromTeams(List<SignedPlayer> signedPlayers);
        /// <summary>
        /// Reports that a find operation has completed.
        /// </summary>
        /// <param name="findResults">The set of results from the find</param>
        void OnFindResults(List<FindResult> findResults);
        /// <summary>
        /// Reports that an advanced find operation has completed.
        /// </summary>
        /// <param name="advancedResults">The set of results from the advanced find</param>
        void OnFindResults(List<AdvancedResult> advancedResults);
        /// <summary>
        /// Reports that a find and replace operation has completed.
        /// </summary>
        /// <param name="replaceResults">The set of results from the find and replace</param>
        void OnReplaceResults(List<ReplaceResult> replaceResults);
        /// <summary>
        /// Reports that an advanced find and replace operation has completed.
        /// </summary>
        /// <param name="advancedResults">The set of results from the advanced find and replace</param>
        void OnReplaceResults(List<AdvancedResult> advancedResults);
    }
}
