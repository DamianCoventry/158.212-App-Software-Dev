using System.Collections.Generic;

namespace RugbyController.Test
{
    public class MockRugbyView : RugbyView.IRugbyView
    {
        public bool _onFindResultsCalled = false;
        public List<RugbyView.FindResult> _findResults;
        public void OnFindResults(List<RugbyView.FindResult> findResults)
        {
            _onFindResultsCalled = true;
            _findResults = findResults;
        }

        public bool _onPlayerAddedCalled = false;
        public RugbyModel.Player _newPlayerAdded;
        public void OnPlayerAdded(RugbyModel.Player copy)
        {
            _onPlayerAddedCalled = true;
            _newPlayerAdded = copy;
        }

        public bool _onPlayerDeletedCalled = false;
        public void OnPlayerDeleted(int playerId)
        {
            _onPlayerDeletedCalled = true;
        }

        public bool _onPlayerEditedCalled = false;
        public void OnPlayerEdited(RugbyModel.Player existingPlayer)
        {
            _onPlayerEditedCalled = true;
        }

        public bool _onPlayersDeletedCalled = false;
        public List<int> _playerIdsDeleted;
        public void OnPlayersDeleted(List<int> playerIds)
        {
            _onPlayersDeletedCalled = true;
            _playerIdsDeleted = playerIds;
        }

        public bool _onPlayerSignedToTeamCalled = false;
        public int _playerIdSignedToTeam;
        public string _teamNameSignedToTeam;
        public void OnPlayerSignedToTeam(int playerId, string teamName)
        {
            _onPlayerSignedToTeamCalled = true;
            _playerIdSignedToTeam = playerId;
            _teamNameSignedToTeam = teamName;
        }

        public bool _onPlayersUnsignedFromTeamsCalled = false;
        public List<RugbyModel.SignedPlayer> _unsignedPlayers;
        public void OnPlayersUnsignedFromTeams(List<RugbyModel.SignedPlayer> signedPlayers)
        {
            _onPlayersUnsignedFromTeamsCalled = true;
            _unsignedPlayers = signedPlayers;
        }

        public bool _onPlayerUnsignedFromTeamCalled = false;
        public void OnPlayerUnsignedFromTeam(int playerId, string teamName)
        {
            _onPlayerUnsignedFromTeamCalled = true;
        }

        public bool _onReplaceResultsCalled = false;
        public List<RugbyView.ReplaceResult> _replaceResults;
        public void OnReplaceResults(List<RugbyView.ReplaceResult> replaceResults)
        {
            _onReplaceResultsCalled = true;
            _replaceResults = replaceResults;
        }

        public bool _onRugbyUnionClosedCalled = false;
        public void OnRugbyUnionClosed()
        {
            _onRugbyUnionClosedCalled = true;
        }

        public bool _onRugbyUnionCreatedCalled = false;
        public string _newRugbyUnionName;
        public void OnRugbyUnionCreated(string rugbyUnionName)
        {
            _onRugbyUnionCreatedCalled = true;
            _newRugbyUnionName = rugbyUnionName;
        }

        public bool _onRugbyUnionOpenedCalled = false;
        public string _rugbyUnionPathName;
        public List<RugbyModel.Team> _teams;
        public List<RugbyModel.Player> _players;
        public List<RugbyModel.SignedPlayer> _signedPlayers;
        public void OnRugbyUnionOpened(string name, string pathName, List<RugbyModel.Team> teams, List<RugbyModel.Player> players, List<RugbyModel.SignedPlayer> signedPlayers)
        {
            _onRugbyUnionOpenedCalled = true;
            _newRugbyUnionName = name;
            _rugbyUnionPathName = pathName;
            _teams = teams;
            _players = players;
            _signedPlayers = signedPlayers;
        }

        public bool _onRugbyUnionRenamedCalled = false;
        public string _oldRugbyUnionName;
        public void OnRugbyUnionRenamed(string oldName, string newName)
        {
            _onRugbyUnionRenamedCalled = true;
            _oldRugbyUnionName = oldName;
            _newRugbyUnionName = newName;
        }

        public bool _onRugbyUnionSavedCalled = false;
        public void OnRugbyUnionSaved(string pathName)
        {
            _onRugbyUnionSavedCalled = true;
        }

        public bool _onTeamAddedCalled = false;
        public RugbyModel.Team _newTeamAdded;
        public void OnTeamAdded(RugbyModel.Team team)
        {
            _onTeamAddedCalled = true;
            _newTeamAdded = team;
        }

        public bool _onTeamDeletedCalled = false;
        public void OnTeamDeleted(string teamName)
        {
            _onTeamDeletedCalled = true;
        }

        public bool _onTeamEditedCalled = false;
        public void OnTeamEdited(RugbyModel.Team team)
        {
            _onTeamEditedCalled = true;
        }

        public bool _onTeamRenamedCalled = false;
        public void OnTeamRenamed(string oldName, string newName)
        {
            _onTeamRenamedCalled = true;
        }

        public bool _onTeamsDeletedCalled = false;
        public List<string> _teamNamesDeleted;
        public void OnTeamsDeleted(List<string> teamNames)
        {
            _onTeamsDeletedCalled = true;
            _teamNamesDeleted = teamNames;
        }

        public bool _onAdvancedFindResultsCalled = false;
        public List<RugbyView.AdvancedResult> _advancedFindResults;
        public void OnFindResults(List<RugbyView.AdvancedResult> advancedResults)
        {
            _onAdvancedFindResultsCalled = true;
            _advancedFindResults = advancedResults;
        }

        public bool _onAdvancedReplaceResultsCalled = false;
        public List<RugbyView.AdvancedResult> _advancedReplaceResults;
        public void OnReplaceResults(List<RugbyView.AdvancedResult> advancedResults)
        {
            _onAdvancedReplaceResultsCalled = true;
            _advancedReplaceResults = advancedResults;
        }
    }
}
