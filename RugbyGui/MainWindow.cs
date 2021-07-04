using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using RugbyGui.Dialogs;
using RugbyModel;
using RugbyView;

namespace RugbyGui
{
    /// <summary>
    /// This is the application's desktop window. It holds all other windows and controls, and is the parent for all modal dialog boxes that
    /// are invoked during runtime. When this window is closed the application closes too.
    /// The user manages all Rugby Union data using this window, including loading data from a file, and saving data to a file.
    /// </summary>
    public partial class MainWindow : Form, IRugbyView
    {
        private const string TreeRootNodeText = "Rugby Union";
        private const string ApplicationTitle = "Rugby Union Signing Application";
        private const string UserManualFileName = "Rugby Union Signing Application.pdf";
        private const string RecentFilesFileName = "Recent Files.txt";

        private const int MaxRecentFiles = 10;
        private const int MaxRecentFileStringLength = 50;

        private readonly RugbyController.IRugbyController _controller;
        private readonly ListViewColumnSorter _listViewColumnSorter = new ListViewColumnSorter();

        private enum InputContext { Nothing, RugbyUnionTreeView, TeamsListView, PlayersListView, SignedPlayersListView }
        private InputContext _inputContext = InputContext.Nothing;
        private ReplaceOptions _findReplaceOptions = new ReplaceOptions();
        private AdvancedFindReplaceOptions _advancedFindReplaceOptions = new AdvancedFindReplaceOptions();

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            // Our one and only instance of the controller. Notice the concrete file I/O class we give it: DefaultFileIo.
            _controller = new RugbyController.RugbyController(this, new DefaultFileIo());
        }

        /// <summary>
        /// The controller layer has created a new Rugby Union.
        /// </summary>
        /// <param name="rugbyUnionName">The name of the new Rugby Union</param>
        public void OnRugbyUnionCreated(string rugbyUnionName)
        {
            SetupGuiForRugbyUnion();
            EnableMenuItems();
            EnableToolbarButtons();
            UpdateWindowTitle();
            UpdateTreeRootNode();
            RugbyUnionTreeView.Nodes[0].Expand();
            StatusBarMessageLabel.Text = $"Created a new Rugby Union named \"{rugbyUnionName}\"";
        }

        /// <summary>
        /// The controller layer has opened an existing Rugby Union.
        /// </summary>
        /// <param name="name">The name of the existing Rugby Union</param>
        /// <param name="pathName">The pathname of the existing Rugby Union</param>
        /// <param name="teams">The teams from the existing Rugby Union</param>
        /// <param name="players">The players from the existing Rugby Union</param>
        /// <param name="signedPlayers">The signed players from the existing Rugby Union</param>
        public void OnRugbyUnionOpened(string name, string pathName, List<Team> teams, List<Player> players, List<SignedPlayer> signedPlayers)
        {
            Utility.SuspendDrawing(RugbyUnionTreeView);
            Utility.SuspendDrawing(TeamsListView);
            Utility.SuspendDrawing(PlayersListView);
            Utility.SuspendDrawing(SignedPlayersListView);

            SetupGuiForRugbyUnion();

            CopyTeamsIntoGui(teams);
            CopyPlayersIntoGui(players);
            CopySignedPlayersIntoGui(signedPlayers);

            Utility.ResumeDrawing(RugbyUnionTreeView);
            Utility.ResumeDrawing(TeamsListView);
            Utility.ResumeDrawing(PlayersListView);
            Utility.ResumeDrawing(SignedPlayersListView);

            EnableMenuItems();
            EnableToolbarButtons();
            UpdateWindowTitle();
            UpdateTreeRootNode();
            AddRecentFile(pathName);

            RugbyUnionTreeView.Nodes[0].Expand();
            StatusBarMessageLabel.Text = $"Opened an existing Rugby Union named \"{name}\" from file \"{pathName}\"";
        }

        /// <summary>
        /// The controller layer has closed an opened Rugby Union.
        /// </summary>
        public void OnRugbyUnionClosed()
        {
            SetupGuiForEmptyWorkspace();
            EnableMenuItems();
            EnableToolbarButtons();
            UpdateWindowTitle();
            UpdateTreeRootNode();
            StatusBarMessageLabel.Text = "Closed the Rugby Union";
        }

        /// <summary>
        /// The controller layer has renamed an opened Rugby Union.
        /// </summary>
        /// <param name="oldName">The old name of the Rugby Union</param>
        /// <param name="newName">The new name of the Rugby Union</param>
        public void OnRugbyUnionRenamed(string oldName, string newName)
        {
            EnableMenuItems();
            EnableToolbarButtons();
            UpdateWindowTitle();
            UpdateTreeRootNode();
            StatusBarMessageLabel.Text = $"Renamed the Rugby Union from \"{oldName}\" to \"{newName}\"";
        }

        /// <summary>
        /// The controller layer has saved an opened Rugby Union.
        /// </summary>
        /// <param name="pathName">The pathname of the opened Rugby Union</param>
        public void OnRugbyUnionSaved(string pathName)
        {
            UpdateWindowTitle();
            AddRecentFile(pathName);
            StatusBarMessageLabel.Text = $"Saved the Rugby Union to file \"{pathName}\"";
        }

        /// <summary>
        /// The controller layer has added a new team
        /// </summary>
        /// <param name="team">The new team</param>
        public void OnTeamAdded(Team team)
        {
            CopyTeamIntoGui(team);
            EnableMenuItems();
            EnableToolbarButtons();
            UpdateWindowTitle();
            StatusBarMessageLabel.Text = $"Added a new team named \"{team.Name}\"";
        }

        /// <summary>
        /// The controller layer has edited an existing team
        /// </summary>
        /// <param name="team">The existing team</param>
        public void OnTeamEdited(Team team)
        {
            UpdateTeamWithinGui(team);
            EnableMenuItems();
            EnableToolbarButtons();
            UpdateWindowTitle();
            StatusBarMessageLabel.Text = $"Edited a team named \"{team.Name}\"";
        }

        /// <summary>
        /// The controller layer has renamed an existing team
        /// </summary>
        /// <param name="oldName">The old name of the existing team</param>
        /// <param name="newName">The new name of the existing team</param>
        public void OnTeamRenamed(string oldName, string newName)
        {
            RenameTeamWithinGui(oldName, newName);
            // Don't bother with the other usual calls because 'OnTeamEdited' will be called straight after this method.
        }

        /// <summary>
        /// The controller layer has deleted an existing team
        /// </summary>
        /// <param name="teamName">The deleted team</param>
        public void OnTeamDeleted(string teamName)
        {
            DeleteTeamFromGui(teamName);
            EnableMenuItems();
            EnableToolbarButtons();
            UpdateWindowTitle();
            StatusBarMessageLabel.Text = $"Deleted a team named \"{teamName}\"";
        }

        /// <summary>
        /// The controller layer has deleted multiple existing teams
        /// </summary>
        /// <param name="teamNames">The existing teams</param>
        public void OnTeamsDeleted(List<string> teamNames)
        {
            Utility.SuspendDrawing(RugbyUnionTreeView);
            Utility.SuspendDrawing(TeamsListView);

            foreach (var teamName in teamNames)
                DeleteTeamFromGui(teamName);

            Utility.ResumeDrawing(RugbyUnionTreeView);
            Utility.ResumeDrawing(TeamsListView);

            EnableMenuItems();
            EnableToolbarButtons();
            UpdateWindowTitle();
            StatusBarMessageLabel.Text = $"Deleted {teamNames.Count} teams";
        }

        /// <summary>
        /// The controller layer has added a new player
        /// </summary>
        /// <param name="player">The new player</param>
        public void OnPlayerAdded(Player player)
        {
            CopyPlayerIntoGui(player);
            EnableMenuItems();
            EnableToolbarButtons();
            UpdateWindowTitle();
            StatusBarMessageLabel.Text = $"Added a new player named \"{player.DisplayName}\"";
        }

        /// <summary>
        /// The controller layer has edited an existing player
        /// </summary>
        /// <param name="player">The existing player</param>
        public void OnPlayerEdited(Player player)
        {
            UpdatePlayerWithinGui(player);
            EnableMenuItems();
            EnableToolbarButtons();
            UpdateWindowTitle();
            StatusBarMessageLabel.Text = $"Edited a player named \"{player.DisplayName}\"";
        }

        /// <summary>
        /// The controller layer has deleted an existing player
        /// </summary>
        /// <param name="playerId">The ID of the existing player</param>
        public void OnPlayerDeleted(int playerId)
        {
            DeletePlayerFromGui(playerId);
            EnableMenuItems();
            EnableToolbarButtons();
            UpdateWindowTitle();
            StatusBarMessageLabel.Text = $"Deleted a player with the ID \"{playerId}\"";
        }

        /// <summary>
        /// The controller layer has deleted multiple existing players
        /// </summary>
        /// <param name="playerIds">The IDs of the delete players</param>
        public void OnPlayersDeleted(List<int> playerIds)
        {
            Utility.SuspendDrawing(RugbyUnionTreeView);
            Utility.SuspendDrawing(PlayersListView);

            foreach (var playerId in playerIds)
                DeletePlayerFromGui(playerId);

            Utility.ResumeDrawing(RugbyUnionTreeView);
            Utility.ResumeDrawing(PlayersListView);

            EnableMenuItems();
            EnableToolbarButtons();
            UpdateWindowTitle();
            StatusBarMessageLabel.Text = $"Deleted {playerIds.Count} players";
        }

        /// <summary>
        /// The controller layer has signed a player to a team
        /// </summary>
        /// <param name="playerId">The ID of the player</param>
        /// <param name="teamName">The name of the team</param>
        public void OnPlayerSignedToTeam(int playerId, string teamName)
        {
            CopyIntoOrUpdateSignedPlayerWithinGui(playerId, teamName);
            EnableMenuItems();
            EnableToolbarButtons();
            UpdateWindowTitle();
            StatusBarMessageLabel.Text = $"Signed player \"{playerId}\" to team \"{teamName}\"";
        }

        /// <summary>
        /// The controller layer has unsigned a player from a team
        /// </summary>
        /// <param name="playerId">The ID of the player</param>
        /// <param name="teamName">The name of the team</param>
        public void OnPlayerUnsignedFromTeam(int playerId, string teamName)
        {
            DeleteSignedPlayerFromGui(playerId);
            EnableMenuItems();
            EnableToolbarButtons();
            UpdateWindowTitle();
            StatusBarMessageLabel.Text = $"Unsigned player \"{playerId}\" from team \"{teamName}\"";
        }

        /// <summary>
        /// The controller layer has unsigned multiple players from teams
        /// </summary>
        /// <param name="signedPlayers">The unsigned players</param>
        public void OnPlayersUnsignedFromTeams(List<SignedPlayer> signedPlayers)
        {
            Utility.SuspendDrawing(RugbyUnionTreeView);
            Utility.SuspendDrawing(SignedPlayersListView);

            foreach (var signedPlayer in signedPlayers)
                DeleteSignedPlayerFromGui(signedPlayer.PlayerId);

            Utility.ResumeDrawing(RugbyUnionTreeView);
            Utility.ResumeDrawing(SignedPlayersListView);

            EnableMenuItems();
            EnableToolbarButtons();
            UpdateWindowTitle();
            StatusBarMessageLabel.Text = $"Deleted {signedPlayers.Count} signed players";
        }

        /// <summary>
        /// The event handler that's called when the window loads
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void MainWindow_Load(object sender, EventArgs e)
        {
            ChartDropList.SelectedIndex = 0;
            TeamsListView.ListViewItemSorter = _listViewColumnSorter;
            PlayersListView.ListViewItemSorter = _listViewColumnSorter;
            SignedPlayersListView.ListViewItemSorter = _listViewColumnSorter;

            _findReplaceOptions.FindTeams = true;
            _findReplaceOptions.FindPlayers = true;
            _findReplaceOptions.FindSignedPlayers = true;

            SetTreeViewTheme(RugbyUnionTreeView.Handle);
            SetupGuiForEmptyWorkspace();
            RebuildRecentFilesMenu();

            EnableMenuItems();
            EnableToolbarButtons();
        }

        /// <summary>
        /// The event handler that's called when the window is about to close
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = AskUserAboutUnsavedChanges(); // Cancel the "form closing operation" if required
        }

        /// <summary>
        /// The event handler that's called when the user clicks File -> New
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void NewRugbyUnion_Click(object sender, EventArgs e)
        {
            if (AskUserAboutUnsavedChanges()) // Handle an already opened document that's been modified
                return;
            var rugbyUnionDialog = new RugbyUnionDialog(_controller.RugbyUnionName);
            if (rugbyUnionDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _controller.NewRugbyUnion(rugbyUnionDialog.RugbyUnionName); // Delegate the work to the layer below
                }
                catch (Exception ex)
                {
                    Error($"Unable to create a new Rugby Union.\r\n\r\n{ex.Message}");
                }
            }
        }

        /// <summary>
        /// The event handler that's called when the user clicks File -> Open
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void OpenRugbyUnion_Click(object sender, EventArgs e)
        {
            string pathName = ChooseOpenFileName();
            if (!string.IsNullOrEmpty(pathName))
                OpenRugbyUnionImpl(pathName);
        }

        /// <summary>
        /// The event handler that's called when the user clicks File -> Close
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void CloseRugbyUnion_Click(object sender, EventArgs e)
        {
            if (AskUserAboutUnsavedChanges()) // Handle an already opened document that's been modified
                return;
            _controller.CloseRugbyUnion(); // Delegate the work to the layer below
        }

        /// <summary>
        /// The event handler that's called when the user clicks File -> Save
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void SaveRugbyUnion_Click(object sender, EventArgs e)
        {
            SaveRugbyUnionImpl();
            EnableMenuItems();
            EnableToolbarButtons();
        }

        /// <summary>
        /// The event handler that's called when the user clicks File -> Save As
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void SaveAsRugbyUnion_Click(object sender, EventArgs e)
        {
            SaveAsRugbyUnionImpl();
            EnableMenuItems();
            EnableToolbarButtons();
        }

        /// <summary>
        /// The event handler that's called when the user clicks File -> Project Properties
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void RugbyUnionProperties_Click(object sender, EventArgs e)
        {
            EditRugbyUnionProperties(_controller.RugbyUnionName);
        }

        /// <summary>
        /// The event handler that's called when the user clicks File -> Exit
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void ApplicationExit_Click(object sender, EventArgs e)
        {
            if (AskUserAboutUnsavedChanges()) // Handle an already opened document that's been modified
                return;
            Application.Exit();
        }

        /// <summary>
        /// The event handler that's called when the user clicks Edit -> Cut
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void CutSelectedItemsToClipboard_Click(object sender, EventArgs e)
        {
            if (CopySelectedItemsToClipboardImpl())
                DeleteSelectedItems_Click(sender, e);
        }

        /// <summary>
        /// The event handler that's called when the user clicks Edit -> Copy
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void CopySelectedItemsToClipboard_Click(object sender, EventArgs e)
        {
            CopySelectedItemsToClipboardImpl();
        }

        /// <summary>
        /// The event handler that's called when the user clicks Edit -> Paste
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void PasteFromClipboard_Click(object sender, EventArgs e)
        {
            if (!Clipboard.ContainsText())
                return;
            switch (_inputContext) // Delegate based on the last context the user interacted with
            {
                case InputContext.RugbyUnionTreeView:
                    PasteIntoTreeViewFromClipboard();
                    break;
                case InputContext.TeamsListView:
                    PasteTeamsIntoGuiFromClipboard();
                    break;
                case InputContext.PlayersListView:
                    PastePlayersIntoGuiFromClipboard();
                    break;
                case InputContext.SignedPlayersListView:
                    PasteSignedPlayersIntoGuiFromClipboard();
                    break;
            }
        }

        /// <summary>
        /// The event handler that's called when the user clicks Edit -> Delete
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void DeleteSelectedItems_Click(object sender, EventArgs e)
        {
            switch (_inputContext) // Delegate based on the last context the user interacted with
            {
                case InputContext.RugbyUnionTreeView:
                    DeleteSelectedTreeViewItem();
                    break;
                case InputContext.TeamsListView:
                    DeleteSelectedTeamsListViewItems();
                    break;
                case InputContext.PlayersListView:
                    DeleteSelectedPlayersListViewItems();
                    break;
                case InputContext.SignedPlayersListView:
                    DeleteSelectedSignedPlayersListViewItems();
                    break;
            }
        }

        /// <summary>
        /// The event handler that's called when the user clicks Edit -> Select All
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void SelectAllItems_Click(object sender, EventArgs e)
        {
            switch (_inputContext) // Delegate based on the last context the user interacted with
            {
                case InputContext.TeamsListView:
                    SelectAllListViewItems(TeamsListView);
                    break;
                case InputContext.PlayersListView:
                    SelectAllListViewItems(PlayersListView);
                    break;
                case InputContext.SignedPlayersListView:
                    SelectAllListViewItems(SignedPlayersListView);
                    break;
            }
        }

        /// <summary>
        /// The event handler that's called when the user clicks Edit -> Unselect All
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void UnselectAllItems_Click(object sender, EventArgs e)
        {
            switch (_inputContext) // Delegate based on the last context the user interacted with
            {
                case InputContext.TeamsListView:
                    SelectAllListViewItems(TeamsListView, false);
                    break;
                case InputContext.PlayersListView:
                    SelectAllListViewItems(PlayersListView, false);
                    break;
                case InputContext.SignedPlayersListView:
                    SelectAllListViewItems(SignedPlayersListView, false);
                    break;
            }
        }

        /// <summary>
        /// The event handler that's called when the user clicks Edit -> Find and Replace -> Find
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void FindItems_Click(object sender, EventArgs e)
        {
            try
            {
                var findDialog = new FindDialog() { FindOptions = _findReplaceOptions }; // Initialise with previous state
                if (findDialog.ShowDialog() == DialogResult.OK)
                {
                    // Save the user's choices for the next invocation
                    _findReplaceOptions.FindWhat = findDialog.FindOptions.FindWhat;
                    _findReplaceOptions.MatchCase = findDialog.FindOptions.MatchCase;
                    _findReplaceOptions.MatchWholeWord = findDialog.FindOptions.MatchWholeWord;
                    _findReplaceOptions.UseRegularExpression = findDialog.FindOptions.UseRegularExpression;
                    _findReplaceOptions.FindTeams = findDialog.FindOptions.FindTeams;
                    _findReplaceOptions.FindPlayers = findDialog.FindOptions.FindPlayers;
                    _findReplaceOptions.FindSignedPlayers = findDialog.FindOptions.FindSignedPlayers;

                    _controller.Find(_findReplaceOptions); // Delegate the work to the layer below

                    ViewFindResults_Click(null, null); // Open the find results window
                }
            }
            catch (Exception ex)
            {
                Error($"Unable to run the find for items.\r\n\r\n{ex.Message}");
            }
        }

        /// <summary>
        /// The event handler that's called when the user clicks Edit -> Find and Replace -> Replace
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void ReplaceItems_Click(object sender, EventArgs e)
        {
            try
            {
                var replaceDialog = new ReplaceDialog() { ReplaceOptions = _findReplaceOptions }; // Initialise with previous state
                if (replaceDialog.ShowDialog() == DialogResult.OK)
                {
                    _findReplaceOptions = replaceDialog.ReplaceOptions; // Save the user's choices for the next invocation

                    _controller.Replace(_findReplaceOptions); // Delegate the work to the layer below

                    ViewFindResults_Click(null, null); // Open the find results window
                }
            }
            catch (Exception ex)
            {
                Error($"Unable to run the find and replace for items.\r\n\r\n{ex.Message}");
            }
        }

        /// <summary>
        /// The event handler that's called when the user clicks Edit -> Find and Replace -> Advanced
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void AdvancedFindAndReplace_Click(object sender, EventArgs e)
        {
            var advancedFindAndReplaceDialog = new AdvancedFindAndReplaceDialog() { FindReplaceOptions = _advancedFindReplaceOptions }; // Initialise with previous state
            if (advancedFindAndReplaceDialog.ShowDialog() == DialogResult.OK)
            {
                _advancedFindReplaceOptions = advancedFindAndReplaceDialog.FindReplaceOptions; // Save the user's choices for the next invocation

                _controller.AdvancedFindAndReplace(_advancedFindReplaceOptions); // Delegate the work to the layer below

                ViewFindResults_Click(null, null);
            }
        }

        /// <summary>
        /// The event handler that's called when the user clicks View -> Rugby Union
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void ViewRugbyUnion_Click(object sender, EventArgs e)
        {
            MainSplitContainer.Panel1Collapsed = false;
            MainSplitContainer.Panel1.Show();
            RugbyUnionTreeView.Focus();
        }

        /// <summary>
        /// The event handler that's called when the user clicks View -> Teams
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void ViewTeams_Click(object sender, EventArgs e)
        {
            MainTabControl.SelectedTab = TeamsTabPage;
            TeamsListView.Focus();
        }

        /// <summary>
        /// The event handler that's called when the user clicks View -> Players
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void ViewPlayers_Click(object sender, EventArgs e)
        {
            MainTabControl.SelectedTab = PlayersTabPage;
            PlayersListView.Focus();
        }

        /// <summary>
        /// The event handler that's called when the user clicks View -> Signed Players
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void ViewSignedPlayers_Click(object sender, EventArgs e)
        {
            MainTabControl.SelectedTab = SignedPlayersTabPage;
            SignedPlayersListView.Focus();
        }

        /// <summary>
        /// The event handler that's called when the user clicks View -> Find Results
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void ViewFindResults_Click(object sender, EventArgs e)
        {
            FindResultsSplitContainer.Panel2Collapsed = false;
            FindResultsSplitContainer.Panel2.Show();
            RugbyUnionTreeView.Focus();
        }

        /// <summary>
        /// The event handler that's called when the user clicks View -> Charts
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void ViewCharts_Click(object sender, EventArgs e)
        {
            MainTabControl.SelectedTab = ChartsTabPage;
            MainChart.Focus();
        }

        /// <summary>
        /// The event handler that's called when the user clicks View -> Status Bar
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void ViewStatusBar_Click(object sender, EventArgs e)
        {
            MainStatusBar.Visible = !MainStatusBar.Visible;
            ViewStatusBar.Checked = MainStatusBar.Visible;
        }

        /// <summary>
        /// The event handler that's called when the user clicks Team -> New
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void NewTeam_Click(object sender, EventArgs e)
        {
            AddNewTeam(null);
        }

        /// <summary>
        /// The event handler that's called when the user clicks Team -> Edit
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void EditTeam_Click(object sender, EventArgs e)
        {
            var teams = GetSelectedTeams();
            if (teams != null && teams.Count > 0)
                EditExistingTeam(teams[0]);
        }

        /// <summary>
        /// The event handler that's called when the user clicks Team -> Sign Player to Team
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void SignPlayerToTeam_Click(object sender, EventArgs e)
        {
            var teams = GetSelectedTeams();
            if (teams != null && teams.Count > 0)
                EditSignedPlayer(teams[0].Name, null);
        }

        /// <summary>
        /// The event handler that's called when the user clicks Player -> New
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void NewPlayer_Click(object sender, EventArgs e)
        {
            AddNewPlayer(null);
        }

        /// <summary>
        /// The event handler that's called when the user clicks Player -> Edit
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void EditPlayer_Click(object sender, EventArgs e)
        {
            var players = GetSelectedPlayers();
            if (players != null && players.Count > 0)
                EditExistingPlayer(players[0]);
        }

        /// <summary>
        /// The event handler that's called when the user clicks Player -> Sign Player with Team
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void SignPlayerWithTeam_Click(object sender, EventArgs e)
        {
            var players = GetSelectedPlayers();
            if (players != null && players.Count > 0)
            {
                var signedPlayer = _controller.FindSignedPlayer(players[0].Id); // Delegate the work to the layer below
                EditSignedPlayer(signedPlayer?.TeamName, players[0].Id);
            }
        }

        /// <summary>
        /// The event handler that's called when the user clicks Help -> User Manual
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void OpenUserManual_Click(object sender, EventArgs e)
        {
            // Multiple paths make it easier during development
            string[] potentialPathsToUserManual =
            {
                Path.Combine(AssemblyDirectory, "..", "..", UserManualFileName),
                Path.Combine(AssemblyDirectory, "..", UserManualFileName),
                Path.Combine(AssemblyDirectory, UserManualFileName),
            };
            bool processStarted = false;
            for (int i = 0; i < potentialPathsToUserManual.Length && !processStarted; ++i)
            {
                if (File.Exists(potentialPathsToUserManual[i]))
                    processStarted = Process.Start(potentialPathsToUserManual[i]) != null;
            }
        }

        /// <summary>
        /// The event handler that's called when the user clicks Help -> About
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void ViewAbout_Click(object sender, EventArgs e)
        {
            var aboutDialog = new AboutDialog();
            aboutDialog.ShowDialog();
        }

        /// <summary>
        /// The event handler that's called when the user closes the tree control
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void TreeViewCloseButton_Click(object sender, EventArgs e)
        {
            MainSplitContainer.Panel1Collapsed = true;
            MainSplitContainer.Panel1.Hide();
        }

        /// <summary>
        /// The event handler that's called when the user closes the find results
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void FindResultsCloseButton_Click(object sender, EventArgs e)
        {
            FindResultsSplitContainer.Panel2Collapsed = true;
            FindResultsSplitContainer.Panel2.Hide();
        }

        /// <summary>
        /// Implements the open existing file logic. This is called from more than one place within this file.
        /// </summary>
        /// <param name="pathName">The pathname of the file to open</param>
        private void OpenRugbyUnionImpl(string pathName)
        {
            if (string.IsNullOrEmpty(pathName))
                return;
            if (_controller.IsOpen && pathName.Equals(_controller.PathName) && !Confirm("That file is already open. Do you want to reload its contents?"))
                return;
            if (AskUserAboutUnsavedChanges()) // Handle an already opened document that's been modified
                return;

            try
            {
                _controller.OpenRugbyUnion(pathName); // Delegate the work to the layer below
            }
            catch (Exception ex)
            {
                Error($"Unable to open the file \"{pathName}\"\r\n\r\n{ex.Message}");
            }
        }

        /// <summary>
        /// Function pointer to an internal Windows function that sets the theme of a window
        /// </summary>
        /// <param name="hwnd">The handle of a window</param>
        /// <param name="pszSubAppName">The application name</param>
        /// <param name="pszSubIdList">List of CLSIDs, or null</param>
        /// <returns>If this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code</returns>
        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);

        /// <summary>
        /// Hides the specifics of setting a tree control theme
        /// </summary>
        /// <param name="treeHandle">The tree control window handle</param>
        private static void SetTreeViewTheme(IntPtr treeHandle)
        {
            SetWindowTheme(treeHandle, "explorer", null);
        }

        /// <summary>
        /// Defines what possible states the tree control selection can be in.
        /// </summary>
        private enum TreeViewSelection
        {
            /// <summary>
            /// No node is selected
            /// </summary>
            Nothing,
            /// <summary>
            /// The Rugby Union node is selected
            /// </summary>
            RugbyUnion,
            /// <summary>
            /// The Teams container is selected
            /// </summary>
            TeamsContainer,
            /// <summary>
            /// The Players container is selected
            /// </summary>
            PlayersContainer,
            /// <summary>
            /// The Signed Players container is selected
            /// </summary>
            SignedPlayersContainer,
            /// <summary>
            /// A Team is selected
            /// </summary>
            Team,
            /// <summary>
            /// A Player is selected
            /// </summary>
            Player,
            /// <summary>
            /// A Signed Player is selected
            /// </summary>
            SignedPlayer
        }

        /// <summary>
        /// Determines the state of the selected item within the tree control
        /// </summary>
        /// <returns>The state of the tree control selection</returns>
        private TreeViewSelection ClassifyTreeViewSelection()
        {
            if (RugbyUnionTreeView.SelectedNode == null)
                return TreeViewSelection.Nothing;
            if (RugbyUnionTreeView.SelectedNode.Parent == null)
                return TreeViewSelection.RugbyUnion;

            if (RugbyUnionTreeView.SelectedNode.Parent.Parent == null)
            {
                if (RugbyUnionTreeView.SelectedNode.Name == "TeamsNode")
                    return TreeViewSelection.TeamsContainer;
                if (RugbyUnionTreeView.SelectedNode.Name == "PlayersNode")
                    return TreeViewSelection.PlayersContainer;
                if (RugbyUnionTreeView.SelectedNode.Name == "SignedPlayersNode")
                    return TreeViewSelection.SignedPlayersContainer;
                return TreeViewSelection.Nothing;
            }

            if (RugbyUnionTreeView.SelectedNode.Parent.Name == "TeamsNode")
                return TreeViewSelection.Team;
            if (RugbyUnionTreeView.SelectedNode.Parent.Name == "PlayersNode")
                return TreeViewSelection.Player;
            if (RugbyUnionTreeView.SelectedNode.Parent.Name == "SignedPlayersNode")
                return TreeViewSelection.SignedPlayer;

            return TreeViewSelection.Nothing;
        }

        /// <summary>
        /// Determines whether or not a team is selected within the tree control or teams list view
        /// </summary>
        private bool IsTeamSelected
        {
            get
            {
                if (_inputContext == InputContext.RugbyUnionTreeView)
                    return ClassifyTreeViewSelection() == TreeViewSelection.Team;
                if (_inputContext == InputContext.TeamsListView)
                    return TeamsListView.SelectedItems.Count > 0;
                return false;
            }
        }

        /// <summary>
        /// Gets the list of selected teams, if any
        /// </summary>
        /// <returns>The list of selected teams</returns>
        private List<Team> GetSelectedTeams()
        {
            if (_inputContext == InputContext.RugbyUnionTreeView)
            {
                if (ClassifyTreeViewSelection() == TreeViewSelection.Team && RugbyUnionTreeView.SelectedNode.Tag is Team team)
                    return new List<Team>() { team };
            }
            else if (_inputContext == InputContext.TeamsListView)
                return GetSelectedTeamsFromListView();
            return null;
        }

        /// <summary>
        /// Determines whether or not a player is selected within the tree control or players list view
        /// </summary>
        private bool IsPlayerSelected
        {
            get
            {
                if (_inputContext == InputContext.RugbyUnionTreeView)
                    return ClassifyTreeViewSelection() == TreeViewSelection.Player;
                if (_inputContext == InputContext.PlayersListView)
                    return PlayersListView.SelectedItems.Count > 0;
                return false;
            }
        }

        /// <summary>
        /// Gets the list of selected players, if any
        /// </summary>
        /// <returns>The list of selected players</returns>
        private List<Player> GetSelectedPlayers()
        {
            if (_inputContext == InputContext.RugbyUnionTreeView)
            {
                if (ClassifyTreeViewSelection() == TreeViewSelection.Player && RugbyUnionTreeView.SelectedNode.Tag is Player player)
                    return new List<Player>() { player };
            }
            else if (_inputContext == InputContext.PlayersListView)
                return GetSelectedPlayersFromListView();
            return null;
        }

        /// <summary>
        /// Determines whether or not a signed player is selected within the tree control or signed players list view
        /// </summary>
        private bool IsSignedPlayerSelected
        {
            get
            {
                if (_inputContext == InputContext.RugbyUnionTreeView)
                    return ClassifyTreeViewSelection() == TreeViewSelection.SignedPlayer;
                if (_inputContext == InputContext.SignedPlayersListView)
                    return SignedPlayersListView.SelectedItems.Count > 0;
                return false;
            }
        }

        /// <summary>
        /// Gets the list of selected teams from the teams list view, if any
        /// </summary>
        /// <returns>The list of selected teams</returns>
        private List<Team> GetSelectedTeamsFromListView()
        {
            List<Team> teams = new List<Team>();
            for (int i = 0; i < TeamsListView.SelectedItems.Count; ++i)
            {
                if (TeamsListView.SelectedItems[i].Tag is Team team)
                    teams.Add(team);
            }
            return teams;
        }

        /// <summary>
        /// Gets the list of selected players from the players list view, if any
        /// </summary>
        /// <returns>The list of selected players</returns>
        private List<Player> GetSelectedPlayersFromListView()
        {
            List<Player> teams = new List<Player>();
            for (int i = 0; i < PlayersListView.SelectedItems.Count; ++i)
            {
                if (PlayersListView.SelectedItems[i].Tag is Player player)
                    teams.Add(player);
            }
            return teams;
        }

        /// <summary>
        /// Gets the list of selected signed players from the signed players list view, if any
        /// </summary>
        /// <returns>The list of selected signed players</returns>
        private List<SignedPlayer> GetSelectedSignedPlayersFromListView()
        {
            List<SignedPlayer> teams = new List<SignedPlayer>();
            for (int i = 0; i < SignedPlayersListView.SelectedItems.Count; ++i)
            {
                if (SignedPlayersListView.SelectedItems[i].Tag is SignedPlayer signedPlayer)
                    teams.Add(signedPlayer);
            }
            return teams;
        }

        /// <summary>
        /// Hides the details of retrieving the teams tree node
        /// </summary>
        /// <returns>The teams tree node</returns>
        private TreeNode GetTeamsTreeNode()
        {
            return GetTreeNode("TeamsNode");
        }

        /// <summary>
        /// Hides the details of retrieving the players tree node
        /// </summary>
        /// <returns>The players tree node</returns>
        private TreeNode GetPlayersTreeNode()
        {
            return GetTreeNode("PlayersNode");
        }

        /// <summary>
        /// Hides the details of retrieving the signed players tree node
        /// </summary>
        /// <returns>The signed players tree node</returns>
        private TreeNode GetSignedPlayersTreeNode()
        {
            return GetTreeNode("SignedPlayersNode");
        }

        /// <summary>
        /// Finds a named tree node within the tree control
        /// </summary>
        /// <param name="name">The name of the tree node to find</param>
        /// <returns>A tree node or null</returns>
        private TreeNode GetTreeNode(string name)
        {
            var nodes = RugbyUnionTreeView.Nodes.Find(name, true);
            if (nodes == null || nodes.Length != 1)
                return null;
            return nodes[0];
        }

        /// <summary>
        /// Implements the save opened document logic. This is called from more than one place within this file.
        /// </summary>
        /// <returns>True if the save succeeded, false otherwise</returns>
        private bool SaveRugbyUnionImpl()
        {
            if (!_controller.HasBeenSaved) // If the data aren't backed by a file then ask the user for a filename
                return SaveAsRugbyUnionImpl();

            bool saved = false;
            try
            {
                _controller.SaveRugbyUnion(); // Delegate the work to the layer below
                UpdateWindowTitle();
                saved = true;
            }
            catch (Exception ex)
            {
                Error($"Unable to save the file \"{_controller.PathName}\"\r\n\r\n{ex.Message}");
            }
            return saved; // Allow the caller to make a decision based on whether or not the save worked.
        }

        /// <summary>
        /// Implements the save opened document as logic. This is called from more than one place within this file.
        /// </summary>
        /// <returns>True if the save succeeded, false otherwise</returns>
        private bool SaveAsRugbyUnionImpl()
        {
            string pathName = ChooseSaveAsFileName($"{_controller.RugbyUnionName}.txt"); // Ask the user for a filename
            if (string.IsNullOrEmpty(pathName))
                return false;

            bool saved = false;
            try
            {
                _controller.SaveAsRugbyUnion(pathName); // Delegate the work to the layer below
                UpdateWindowTitle();
                saved = true;
            }
            catch (Exception ex)
            {
                Error($"Unable to save the file \"{pathName}\"\r\n\r\n{ex.Message}");
            }
            return saved;
        }

        /// <summary>
        /// If the Rugby Union document has unsaved changes, then this method asks the user if they want to save those changes.
        /// </summary>
        /// <returns>True if the user wants to cancel whatever operation caused this prompt to occur, false otherwise.</returns>
        private bool AskUserAboutUnsavedChanges()
        {
            if (!_controller.IsModified)
                return false;

            bool userCancelled = false;
            switch (Query("Save changes before closing this Rugby Union?"))
            {
                case QueryResult.Yes:
                    userCancelled = !SaveRugbyUnionImpl();
                    break;
                case QueryResult.No:
                    break;
                case QueryResult.Cancel:
                    userCancelled = true;
                    break;
            }
            return userCancelled;
        }

        /// <summary>
        /// Solicit a filename from the user that's for a file suitable to save the Rugby Union data into
        /// </summary>
        /// <param name="defaultFileName">A filename to initialise the dialog box with</param>
        /// <returns>A filename or null</returns>
        private string ChooseSaveAsFileName(string defaultFileName)
        {
            var saveFileDialog = new SaveFileDialog()
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                FilterIndex = 1,
                OverwritePrompt = true,
                DefaultExt = "txt",
                CheckPathExists = true,
                FileName = defaultFileName
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                return saveFileDialog.FileName;
            return null;
        }

        /// <summary>
        /// Solicit a filename from the user that's for a file containing Rugby Union data
        /// </summary>
        /// <returns>A filename or null</returns>
        private string ChooseOpenFileName()
        {
            var openFileDialog = new OpenFileDialog()
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                FilterIndex = 1,
                CheckPathExists = true,
                CheckFileExists = true
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                return openFileDialog.FileName;
            return null;
        }

        /// <summary>
        /// Shows an empty workspace to the user i.e. just a big grey box
        /// </summary>
        private void SetupGuiForEmptyWorkspace()
        {
            EmptyWorkspacePanel.Visible = true;
            MainSplitContainer.Visible = false;
            RemoveRugbyUnionDataFromGui();
            UpdateWindowTitle();
            UpdateTreeRootNode();
        }

        /// <summary>
        /// Shows a Rugby Union workspace to the user i.e. tree control, splitter, toolbars, etc
        /// </summary>
        private void SetupGuiForRugbyUnion()
        {
            MainSplitContainer.Visible = true;
            EmptyWorkspacePanel.Visible = false;
            RemoveRugbyUnionDataFromGui();
            UpdateWindowTitle();
            UpdateTreeRootNode();
            FindResultsCloseButton_Click(null, null);
        }

        /// <summary>
        /// Copies a list of teams into the teams list view
        /// </summary>
        /// <param name="teams">The list of teams</param>
        private void CopyTeamsIntoGui(List<Team> teams)
        {
            foreach (var team in teams)
                CopyTeamIntoGui(team);
            Utility.AutoSizeListViewColumns(TeamsListView);
        }

        /// <summary>
        /// Copies a single team into the teams list view
        /// </summary>
        /// <param name="team">The team</param>
        private void CopyTeamIntoGui(Team team)
        {
            var node = GetTeamsTreeNode().Nodes.Add(team.Name, team.Name, 1, 1);
            node.Tag = team;

            var item = TeamsListView.Items.Add(team.Name, 1);
            item.ImageIndex = 1;
            item.Tag = team;
            item.SubItems.Add(team.HomeGround);
            item.SubItems.Add(team.Coach);
            item.SubItems.Add(team.YearFounded.ToString());
            item.SubItems.Add(team.Region);
        }

        /// <summary>
        /// Ensures both the tree control and listview get updated with the same data
        /// </summary>
        /// <param name="team">The team that's been updated</param>
        private void UpdateTeamWithinGui(Team team)
        {
            UpdateTeamWithinTreeView(team);
            UpdateTeamWithinTeamsListView(team);
        }

        /// <summary>
        /// Locates a team within the tree control, then updates its data with the supplied data
        /// </summary>
        /// <param name="team">The team that's been updated</param>
        private void UpdateTeamWithinTreeView(Team team)
        {
            var teamsNode = GetTeamsTreeNode();
            int index = -1;
            for (int i = 0; i < teamsNode.Nodes.Count && index == -1; ++i)
            {
                if (teamsNode.Nodes[i].Tag is Team x)
                {
                    if (x.Name.Equals(team.Name, StringComparison.OrdinalIgnoreCase))
                        index = i;
                }
            }
            if (index != -1) // Don't bother if not found
            {
                teamsNode.Nodes[index].Name = team.Name;
                teamsNode.Nodes[index].Text = team.Name;
                teamsNode.Nodes[index].Tag = team;
            }
        }

        /// <summary>
        /// Locates a team within the list view, then updates its data with the supplied data
        /// </summary>
        /// <param name="team">The team that's been updated</param>
        private void UpdateTeamWithinTeamsListView(Team team)
        {
            int index = -1;
            for (int i = 0; i < TeamsListView.Items.Count && index == -1; ++i)
            {
                if (TeamsListView.Items[i].Tag is Team x)
                {
                    if (x.Name.Equals(team.Name, StringComparison.OrdinalIgnoreCase))
                        index = i;
                }
            }
            if (index != -1) // Don't bother if not found
            {
                TeamsListView.Items[index].Tag = team;
                TeamsListView.Items[index].SubItems[1].Text = team.HomeGround;
                TeamsListView.Items[index].SubItems[2].Text = team.Coach;
                TeamsListView.Items[index].SubItems[3].Text = team.YearFounded.ToString();
                TeamsListView.Items[index].SubItems[4].Text = team.Region;
            }
        }

        /// <summary>
        /// Ensures all three list views get updated with the same data
        /// </summary>
        /// <param name="oldName">The old team name</param>
        /// <param name="newName">The new team name</param>
        private void RenameTeamWithinGui(string oldName, string newName)
        {
            RenameTeamWithinTreeView(oldName, newName);
            RenameTeamWithinTeamsListView(oldName, newName);
            RenameTeamWithinSignedPlayersListView(oldName, newName);
        }

        /// <summary>
        /// Locates a team within the tree control, then updates the name of that team
        /// </summary>
        /// <param name="oldName">The old team name</param>
        /// <param name="newName">The new team name</param>
        private void RenameTeamWithinTreeView(string oldName, string newName)
        {
            var teamsNode = GetTeamsTreeNode();
            int index = -1;
            for (int i = 0; i < teamsNode.Nodes.Count && index == -1; ++i)
            {
                if (teamsNode.Nodes[i].Text.Equals(oldName, StringComparison.OrdinalIgnoreCase))
                    index = i;
            }
            if (index != -1) // Other bother if it was found
            {
                teamsNode.Nodes[index].Name = newName;
                teamsNode.Nodes[index].Text = newName;
                teamsNode.Nodes[index].Tag = _controller.FindTeam(newName);
            }

            // The team name will be present in a second part of the tree control
            var signedPlayersNode = GetSignedPlayersTreeNode();
            for (int i = 0; i < signedPlayersNode.Nodes.Count; ++i)
            {
                if (signedPlayersNode.Nodes[i].Tag is SignedPlayer x)
                {
                    string oldDisplayName = $"{x.PlayerName} ({x.PlayerId}, {oldName})";
                    if (signedPlayersNode.Nodes[i].Text.Equals(oldDisplayName, StringComparison.OrdinalIgnoreCase))
                    {
                        signedPlayersNode.Nodes[i].Name = x.DisplayName;
                        signedPlayersNode.Nodes[i].Text = x.DisplayName;
                    }
                }
            }
        }

        /// <summary>
        /// Locates a team within the teams list view, then updates the name of that team
        /// </summary>
        /// <param name="oldName">The old team name</param>
        /// <param name="newName">The new team name</param>
        private void RenameTeamWithinTeamsListView(string oldName, string newName)
        {
            int index = -1;
            for (int i = 0; i < TeamsListView.Items.Count && index == -1; ++i)
            {
                if (TeamsListView.Items[i].Text.Equals(oldName, StringComparison.OrdinalIgnoreCase))
                    index = i;
            }
            if (index != -1)
            {
                TeamsListView.Items[index].Tag = _controller.FindTeam(newName);
                TeamsListView.Items[index].SubItems[0].Text = newName;
            }
        }

        /// <summary>
        /// Locates a team within the signed players list view, then updates the name of that team
        /// </summary>
        /// <param name="oldName">The old team name</param>
        /// <param name="newName">The new team name</param>
        private void RenameTeamWithinSignedPlayersListView(string oldName, string newName)
        {
            for (int i = 0; i < SignedPlayersListView.Items.Count; ++i)
            {
                if (SignedPlayersListView.Items[i].SubItems[2].Text.Equals(oldName, StringComparison.OrdinalIgnoreCase))
                    SignedPlayersListView.Items[i].SubItems[2].Text = newName;
            }
        }

        /// <summary>
        /// Ensures the tree control and list views get the same data deleted
        /// </summary>
        /// <param name="teamName">The name of the team</param>
        private void DeleteTeamFromGui(string teamName)
        {
            DeleteTeamFromTreeView(teamName);
            DeleteTeamFromTeamsListView(teamName);
            DeleteTeamFromSignedPlayersListView(teamName);
        }

        /// <summary>
        /// Locates the team within the tree control, then deletes it
        /// </summary>
        /// <param name="teamName">The name of the team</param>
        private void DeleteTeamFromTreeView(string teamName)
        {
            var teamsNode = GetTeamsTreeNode();
            bool deleted = false;
            for (int j = 0; j < teamsNode.Nodes.Count && !deleted; ++j)
            {
                if (teamsNode.Nodes[j].Tag is Team x)
                {
                    if (x.Name.Equals(teamName, StringComparison.OrdinalIgnoreCase))
                    {
                        teamsNode.Nodes.RemoveAt(j);
                        deleted = true; // It will only appear once in this part of tree
                    }
                }
            }

            // The team name will be present in a second part of the tree control
            var signedPlayersNode = GetSignedPlayersTreeNode();
            int i = 0;
            while (i < signedPlayersNode.Nodes.Count)
            {
                if (signedPlayersNode.Nodes[i].Tag is SignedPlayer x)
                {
                    if (x.TeamName.Equals(teamName, StringComparison.OrdinalIgnoreCase))
                        signedPlayersNode.Nodes.RemoveAt(i);
                    else
                        ++i;
                }
                else
                    ++i;
            }
        }

        /// <summary>
        /// Locate the team within the teams list view, then delete it
        /// </summary>
        /// <param name="teamName">The name of the team</param>
        private void DeleteTeamFromTeamsListView(string teamName)
        {
            bool deleted = false;
            for (int i = 0; i < TeamsListView.Items.Count && !deleted; ++i)
            {
                if (TeamsListView.Items[i].Tag is Team x)
                {
                    if (x.Name.Equals(teamName, StringComparison.OrdinalIgnoreCase))
                    {
                        TeamsListView.Items.RemoveAt(i);
                        deleted = true; // It will only be present once
                    }
                }
            }
        }

        /// <summary>
        /// Locate the team within the signed players list view, then delete it
        /// </summary>
        /// <param name="teamName">The name of the team</param>
        private void DeleteTeamFromSignedPlayersListView(string teamName)
        {
            int i = 0;
            while (i < SignedPlayersListView.Items.Count)
            {
                if (SignedPlayersListView.Items[i].Tag is SignedPlayer x)
                {
                    if (x.TeamName.Equals(teamName, StringComparison.OrdinalIgnoreCase))
                        SignedPlayersListView.Items.RemoveAt(i);
                    else
                        ++i;
                }
                else
                    ++i;
            }
        }

        /// <summary>
        /// Copies a list of players into the players list view
        /// </summary>
        /// <param name="players">The list of players</param>
        private void CopyPlayersIntoGui(List<Player> players)
        {
            foreach (var player in players)
                CopyPlayerIntoGui(player);
            Utility.AutoSizeListViewColumns(PlayersListView);
        }

        /// <summary>
        /// Copies a single player into the players list view
        /// </summary>
        /// <param name="player">The player</param>
        private void CopyPlayerIntoGui(Player player)
        {
            var node = GetPlayersTreeNode().Nodes.Add(player.DisplayName, player.DisplayName, 2, 2);
            node.Tag = player;

            var item = PlayersListView.Items.Add(player.Id.ToString(), 2);
            item.ImageIndex = 2;
            item.Tag = player;
            item.SubItems.Add(player.FirstName);
            item.SubItems.Add(player.LastName);
            item.SubItems.Add($"{player.Height} cm");
            item.SubItems.Add($"{player.Weight} kg");
            item.SubItems.Add(player.DateOfBirth.ToShortDateString());
            item.SubItems.Add(player.PlaceOfBirth);
        }

        /// <summary>
        /// Ensures both the tree control and listviews get updated with the same data
        /// </summary>
        /// <param name="player">The player that's been updated</param>
        private void UpdatePlayerWithinGui(Player player)
        {
            UpdatePlayerWithinTreeView(player);
            UpdatePlayerWithinPlayersListView(player);
            UpdatePlayerWithinSignedPlayersListView(player);
        }

        /// <summary>
        /// Locates a player within the tree control, then updates its data with the supplied data
        /// </summary>
        /// <param name="player">The player that's been updated</param>
        private void UpdatePlayerWithinTreeView(Player player)
        {
            var playersNode = GetPlayersTreeNode();
            int index = -1;
            for (int i = 0; i < playersNode.Nodes.Count && index == -1; ++i)
            {
                if (playersNode.Nodes[i].Tag is Player x)
                {
                    if (x.Id == player.Id)
                        index = i;
                }
            }
            if (index != -1) // Don't bother if not found
            {
                playersNode.Nodes[index].Name = player.DisplayName;
                playersNode.Nodes[index].Text = player.DisplayName;
                playersNode.Nodes[index].Tag = player;
            }
        }

        /// <summary>
        /// Locates a player within the list view, then updates its data with the supplied data
        /// </summary>
        /// <param name="player">The player that's been updated</param>
        private void UpdatePlayerWithinPlayersListView(Player player)
        {
            int index = -1;
            for (int i = 0; i < PlayersListView.Items.Count && index == -1; ++i)
            {
                if (PlayersListView.Items[i].Tag is Player x)
                {
                    if (x.Id == player.Id)
                        index = i;
                }
            }
            if (index != -1) // Don't bother if not found
            {
                PlayersListView.Items[index].Tag = player;
                PlayersListView.Items[index].SubItems[1].Text = player.FirstName;
                PlayersListView.Items[index].SubItems[2].Text = player.LastName;
                PlayersListView.Items[index].SubItems[3].Text = $"{player.Height} cm";
                PlayersListView.Items[index].SubItems[4].Text = $"{player.Weight} kg";
                PlayersListView.Items[index].SubItems[5].Text = player.DateOfBirth.ToShortDateString();
                PlayersListView.Items[index].SubItems[6].Text = player.PlaceOfBirth;
            }
        }

        /// <summary>
        /// Locates a player within the list view, then updates its data with the supplied data
        /// </summary>
        /// <param name="player">The player that's been updated</param>
        private void UpdatePlayerWithinSignedPlayersListView(Player player)
        {
            int index = -1;
            for (int i = 0; i < SignedPlayersListView.Items.Count && index == -1; ++i)
            {
                if (SignedPlayersListView.Items[i].Tag is SignedPlayer x)
                {
                    if (x.PlayerId == player.Id)
                        index = i;
                }
            }
            if (index != -1) // Don't bother if not found
            {
                SignedPlayersListView.Items[index].Tag = _controller.FindSignedPlayer(player.Id);
                SignedPlayersListView.Items[index].SubItems[1].Text = player.DisplayName;
            }
        }

        /// <summary>
        /// Ensures all three list views get the same data deleted
        /// </summary>
        /// <param name="playerId">The ID of the player</param>
        private void DeletePlayerFromGui(int playerId)
        {
            DeletePlayerFromTreeView(playerId);
            DeletePlayerFromPlayersListView(playerId);
            DeletePlayerFromSignedPlayersListView(playerId);
        }

        /// <summary>
        /// Locates the player within the tree control, then deletes it
        /// </summary>
        /// <param name="playerId">The ID of the player</param>
        private void DeletePlayerFromTreeView(int playerId)
        {
            var playersNode = GetPlayersTreeNode();
            bool deleted = false;
            for (int i = 0; i < playersNode.Nodes.Count && !deleted; ++i)
            {
                if (playersNode.Nodes[i].Tag is Player x)
                {
                    if (x.Id == playerId)
                    {
                        playersNode.Nodes.RemoveAt(i);
                        deleted = true; // It will only appear once in this part of tree
                    }
                }
            }

            // The player ID will be present in a second part of the tree control
            var signedPlayersNode = GetSignedPlayersTreeNode();
            deleted = false;
            for (int i = 0; i < signedPlayersNode.Nodes.Count && !deleted; ++i)
            {
                if (signedPlayersNode.Nodes[i].Tag is SignedPlayer x)
                {
                    if (x.PlayerId == playerId)
                    {
                        signedPlayersNode.Nodes.RemoveAt(i);
                        deleted = true;
                    }
                }
            }
        }

        /// <summary>
        /// Locate the player within the players list view, then delete it
        /// </summary>
        /// <param name="playerId">The ID of the player</param>
        private void DeletePlayerFromPlayersListView(int playerId)
        {
            bool deleted = false;
            for (int i = 0; i < PlayersListView.Items.Count && !deleted; ++i)
            {
                if (PlayersListView.Items[i].Tag is Player x)
                {
                    if (x.Id == playerId)
                    {
                        PlayersListView.Items.RemoveAt(i);
                        deleted = true; // It will only be present once
                    }
                }
            }
        }

        /// <summary>
        /// Locate the player within the signed players list view, then delete it
        /// </summary>
        /// <param name="playerId">The ID of the player</param>
        private void DeletePlayerFromSignedPlayersListView(int playerId)
        {
            bool deleted = false;
            for (int i = 0; i < SignedPlayersListView.Items.Count && !deleted; ++i)
            {
                if (SignedPlayersListView.Items[i].Tag is SignedPlayer x)
                {
                    if (x.PlayerId == playerId)
                    {
                        SignedPlayersListView.Items.RemoveAt(i);
                        deleted = true; // It will only be present once
                    }
                }
            }
        }

        /// <summary>
        /// Copies a list of signed players into the signed players list view
        /// </summary>
        /// <param name="signedPlayers">The list of signed players</param>
        private void CopySignedPlayersIntoGui(List<SignedPlayer> signedPlayers)
        {
            foreach (var signedPlayer in signedPlayers)
                CopySignedPlayerIntoGui(signedPlayer);
            Utility.AutoSizeListViewColumns(SignedPlayersListView);
        }

        /// <summary>
        /// Copies a single signed player into the signed players list view
        /// </summary>
        /// <param name="signedPlayer">The signed players</param>
        private void CopySignedPlayerIntoGui(SignedPlayer signedPlayer)
        {
            var node = GetSignedPlayersTreeNode().Nodes.Add(signedPlayer.DisplayName, signedPlayer.DisplayName, 3, 3);
            node.Tag = signedPlayer;

            var item = SignedPlayersListView.Items.Add(signedPlayer.PlayerId.ToString(), 3);
            item.ImageIndex = 3;
            item.StateImageIndex = 3;
            item.Tag = signedPlayer;
            item.SubItems.Add(signedPlayer.PlayerName);
            item.SubItems.Add(signedPlayer.TeamName);
        }

        /// <summary>
        /// Ensures both the tree control and listview get updated with the same data
        /// </summary>
        /// <param name="playerId">The ID of the player</param>
        /// <param name="teamName">The name of the team</param>
        private void CopyIntoOrUpdateSignedPlayerWithinGui(int playerId, string teamName)
        {
            var signedPlayer = new SignedPlayer()
            {
                PlayerId = playerId,
                PlayerName = _controller.FindPlayer(playerId).DisplayName,
                TeamName = teamName
            };
            CopyIntoOrUpdateSignedPlayerWithinTreeView(signedPlayer);
            CopyIntoOrUpdateSignedPlayerWithinListView(signedPlayer);
        }

        /// <summary>
        /// Locates a signed player within the tree control, then updates its data with the supplied data
        /// </summary>
        /// <param name="signedPlayer">The signed player that's been updated</param>
        private void CopyIntoOrUpdateSignedPlayerWithinTreeView(SignedPlayer signedPlayer)
        {
            var signedPlayersNode = GetSignedPlayersTreeNode();
            int index = -1;
            for (int i = 0; i < signedPlayersNode.Nodes.Count && index == -1; ++i)
            {
                if (signedPlayersNode.Nodes[i].Tag is SignedPlayer x)
                {
                    if (x.PlayerId == signedPlayer.PlayerId)
                        index = i;
                }
            }
            if (index != -1) // It is already there, update it
            {
                signedPlayersNode.Nodes[index].Name = signedPlayer.DisplayName;
                signedPlayersNode.Nodes[index].Text = signedPlayer.DisplayName;
                signedPlayersNode.Nodes[index].Tag = signedPlayer;
            }
            else // It isn't there, add it
            {
                var node = GetSignedPlayersTreeNode().Nodes.Add(signedPlayer.DisplayName, signedPlayer.DisplayName, 3, 3);
                node.Tag = signedPlayer;
            }
        }

        /// <summary>
        /// Locates a signed player within the list view, then updates its data with the supplied data
        /// </summary>
        /// <param name="signedPlayer">The signed player that's been updated</param>
        private void CopyIntoOrUpdateSignedPlayerWithinListView(SignedPlayer signedPlayer)
        {
            int index = -1;
            for (int i = 0; i < SignedPlayersListView.Items.Count && index == -1; ++i)
            {
                if (SignedPlayersListView.Items[i].Tag is SignedPlayer x)
                {
                    if (x.PlayerId == signedPlayer.PlayerId)
                        index = i;
                }
            }
            if (index != -1) // It is already there, update it
            {
                SignedPlayersListView.Items[index].Tag = signedPlayer;
                SignedPlayersListView.Items[index].SubItems[1].Text = signedPlayer.PlayerName;
                SignedPlayersListView.Items[index].SubItems[2].Text = signedPlayer.TeamName;
            }
            else // It isn't there, add it
            {
                var item = SignedPlayersListView.Items.Add(signedPlayer.PlayerId.ToString(), 3);
                item.ImageIndex = 3;
                item.StateImageIndex = 3;
                item.Tag = signedPlayer;
                item.SubItems.Add(signedPlayer.PlayerName);
                item.SubItems.Add(signedPlayer.TeamName);
            }
        }

        /// <summary>
        /// Ensures the tree control and list view get the same data deleted
        /// </summary>
        /// <param name="playerId">The ID of the player</param>
        private void DeleteSignedPlayerFromGui(int playerId)
        {
            DeleteSignedPlayerFromTreeView(playerId);
            DeleteSignedPlayerFromSignedPlayersListView(playerId);
        }

        /// <summary>
        /// Locates the player within the tree control, then deletes it
        /// </summary>
        /// <param name="playerId">The ID of the player</param>
        private void DeleteSignedPlayerFromTreeView(int playerId)
        {
            var signedPlayersNode = GetSignedPlayersTreeNode();
            bool deleted = false;
            for (int i = 0; i < signedPlayersNode.Nodes.Count && !deleted; ++i)
            {
                if (signedPlayersNode.Nodes[i].Tag is SignedPlayer x)
                {
                    if (x.PlayerId == playerId)
                    {
                        signedPlayersNode.Nodes.RemoveAt(i);
                        deleted = true; // It will only appear once in this part of tree
                    }
                }
            }
        }

        /// <summary>
        /// Locate the player within the players list view, then delete it
        /// </summary>
        /// <param name="playerId">The ID of the player</param>
        private void DeleteSignedPlayerFromSignedPlayersListView(int playerId)
        {
            bool deleted = false;
            for (int i = 0; i < SignedPlayersListView.Items.Count && !deleted; ++i)
            {
                if (SignedPlayersListView.Items[i].Tag is SignedPlayer x)
                {
                    if (x.PlayerId == playerId)
                    {
                        SignedPlayersListView.Items.RemoveAt(i);
                        deleted = true; // It will only be present once
                    }
                }
            }
        }

        /// <summary>
        /// Enables or disables all menu items based on the state of the Rugby Union document and selected items
        /// within the GUI
        /// </summary>
        private void EnableMenuItems()
        {
            CloseRugbyUnion.Enabled = _controller.IsOpen;
            SaveRugbyUnion.Enabled = _controller.IsOpen && _controller.IsModified;
            SaveAsRugbyUnion.Enabled = _controller.IsOpen;
            RugbyUnionProperties.Enabled = _controller.IsOpen;

            bool somethingIsSelected = IsTeamSelected || IsPlayerSelected || IsSignedPlayerSelected;

            CutSelectedItemsToClipboard.Enabled = _controller.IsOpen && somethingIsSelected;
            CopySelectedItemsToClipboard.Enabled = _controller.IsOpen && somethingIsSelected;
            PasteFromClipboard.Enabled = _controller.IsOpen && Clipboard.ContainsText();
            DeleteSelectedItems.Enabled = _controller.IsOpen && somethingIsSelected;
            SelectAllItems.Enabled = _controller.IsOpen && IsInputContextAListView();
            UnselectAllItems.Enabled = _controller.IsOpen && IsInputContextAListView();
            FindItems.Enabled = _controller.IsOpen;
            ReplaceItems.Enabled = _controller.IsOpen;
            AdvancedFindAndReplace.Enabled = _controller.IsOpen;

            ViewRugbyUnion.Enabled = _controller.IsOpen;
            ViewTeams.Enabled = _controller.IsOpen;
            ViewPlayers.Enabled = _controller.IsOpen;
            ViewSignedPlayers.Enabled = _controller.IsOpen;
            ViewFindResults.Enabled = _controller.IsOpen;
            ViewCharts.Enabled = _controller.IsOpen;

            NewTeam.Enabled = _controller.IsOpen;
            EditTeam.Enabled = _controller.IsOpen && IsTeamSelected;
            SignPlayerToTeam.Enabled = _controller.IsOpen && IsTeamSelected;
            ImportTeam.Enabled = _controller.IsOpen;
            ExportTeam.Enabled = _controller.IsOpen && _controller.TeamsAvailable;

            NewPlayer.Enabled = _controller.IsOpen;
            EditPlayer.Enabled = _controller.IsOpen && IsPlayerSelected;
            SignPlayerWithTeam.Enabled = _controller.IsOpen && IsPlayerSelected;
            ImportPlayers.Enabled = _controller.IsOpen;
            ExportPlayers.Enabled = _controller.IsOpen && _controller.PlayersAvailable;
        }

        /// <summary>
        /// Enables or disables all toolbar buttons based on the state of the Rugby Union document and selected items
        /// within the GUI
        /// </summary>
        private void EnableToolbarButtons()
        {
            SaveRugbyUnionButton.Enabled = _controller.IsOpen && _controller.IsModified;
            RugbyUnionPropertiesButton.Enabled = _controller.IsOpen;

            NewTeamButton.Enabled = _controller.IsOpen;
            EditTeamButton.Enabled = _controller.IsOpen && IsTeamSelected;
            SignPlayerToTeamButton.Enabled = _controller.IsOpen && IsTeamSelected;

            NewPlayerButton.Enabled = _controller.IsOpen;
            EditPlayerButton.Enabled = _controller.IsOpen && IsPlayerSelected;
            SignPlayerWithTeamButton.Enabled = _controller.IsOpen && IsPlayerSelected;
        }

        /// <summary>
        /// Determines whether or not the input context indicates a list view
        /// </summary>
        /// <returns></returns>
        private bool IsInputContextAListView()
        {
            return _inputContext == InputContext.TeamsListView ||
                   _inputContext == InputContext.PlayersListView ||
                   _inputContext == InputContext.SignedPlayersListView;
        }

        /// <summary>
        /// Clears all data from all controls
        /// </summary>
        private void RemoveRugbyUnionDataFromGui()
        {
            GetTeamsTreeNode().Nodes.Clear();
            GetPlayersTreeNode().Nodes.Clear();
            GetSignedPlayersTreeNode().Nodes.Clear();
            TeamsListView.Items.Clear();
            PlayersListView.Items.Clear();
            SignedPlayersListView.Items.Clear();
            FindResultsListBox.Items.Clear();
        }

        /// <summary>
        /// Sets the text within the root tree control node
        /// </summary>
        private void UpdateTreeRootNode()
        {
            if (_controller.IsOpen)
                RugbyUnionTreeView.Nodes[0].Text = $"{TreeRootNodeText} ({_controller.RugbyUnionName})";
            else
                RugbyUnionTreeView.Nodes[0].Text = TreeRootNodeText;
        }

        /// <summary>
        /// Sets the text of the main window's title bar
        /// </summary>
        private void UpdateWindowTitle()
        {
            string documentName = null;
            if (!string.IsNullOrEmpty(_controller.PathName))
                documentName = _controller.PathName;
            else if (!string.IsNullOrEmpty(_controller.RugbyUnionName))
                documentName = _controller.RugbyUnionName;

            var message = ApplicationTitle;
            if (!string.IsNullOrEmpty(documentName)) // Include the Rugby Union document name, if it's open
            {
                message += $" - [{documentName}";
                if (_controller.IsModified) // Include an asterisk, if the document is modified
                    message += "*";
                message += "]";
            }
            Text = message;
        }

        /// <summary>
        /// The set of answers the user can respond with when queried
        /// </summary>
        private enum QueryResult
        {
            /// <summary>
            /// User answered yes to the query
            /// </summary>
            Yes,
            /// <summary>
            /// User answered no to the query
            /// </summary>
            No,
            /// <summary>
            /// User cancelled the query altogether
            /// </summary>
            Cancel
        }

        /// <summary>
        /// Shows the user a message box containing a query 
        /// </summary>
        /// <param name="message">The text of the query</param>
        /// <returns>Either yes, no, or cancel</returns>
        private QueryResult Query(string message)
        {
            var answer = MessageBox.Show(this, message, ApplicationTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
                return QueryResult.Yes;
            if (answer == DialogResult.No)
                return QueryResult.No;
            return QueryResult.Cancel;
        }

        /// <summary>
        /// Shows the user an error message
        /// </summary>
        /// <param name="message">The error message</param>
        private void Error(string message)
        {
            MessageBox.Show(this, message, ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Shows the user an informational message
        /// </summary>
        /// <param name="message">The informational message</param>
        private void Inform(string message)
        {
            MessageBox.Show(this, message, ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Asks the user to confirm or deny a question
        /// </summary>
        /// <param name="message">The question</param>
        /// <returns>Yes if the user confirmed, false otherwise</returns>
        private bool Confirm(string message)
        {
            return MessageBox.Show(this, message, ApplicationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        /// <summary>
        /// Copies whatever items within the GUI are selected, to the clipboard
        /// </summary>
        /// <returns>True if the items were copied, false otherwise</returns>
        private bool CopySelectedItemsToClipboardImpl()
        {
            switch (_inputContext) // Delegate based on the last context the user interacted with
            {
                case InputContext.RugbyUnionTreeView:
                    return CopySelectedTreeViewItemToClipboard();
                case InputContext.TeamsListView:
                    return CopySelectedTeamsListViewItemsToClipboard();
                case InputContext.PlayersListView:
                    return CopySelectedPlayersListViewItemsToClipboard();
                case InputContext.SignedPlayersListView:
                    return CopySelectedSignedPlayersListViewItemsToClipboard();
            }
            return false;
        }

        /// <summary>
        /// Copies the selected tree control item to the clipboard
        /// </summary>
        /// <returns>True if the item was copied, false otherwise</returns>
        private bool CopySelectedTreeViewItemToClipboard()
        {
            switch (ClassifyTreeViewSelection())
            {
                case TreeViewSelection.RugbyUnion:
                    Clipboard.SetText(RugbyUnionTreeView.SelectedNode.Text);
                    return true;
                case TreeViewSelection.Team:
                    if (RugbyUnionTreeView.SelectedNode.Tag is Team team)
                        Clipboard.SetText(team.ToFileString() + "\n");
                    return true;
                case TreeViewSelection.Player:
                    if (RugbyUnionTreeView.SelectedNode.Tag is Player player)
                        Clipboard.SetText(player.ToFileString() + "\n");
                    return true;
                case TreeViewSelection.SignedPlayer:
                    if (RugbyUnionTreeView.SelectedNode.Tag is SignedPlayer signedPlayer)
                        Clipboard.SetText(signedPlayer.ToFileString() + "\n");
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Copies the selected teams list view items to the clipboard
        /// </summary>
        /// <returns>True if the items were copied, false otherwise</returns>
        private bool CopySelectedTeamsListViewItemsToClipboard()
        {
            string ClipboardText = string.Empty;
            for (int i = 0; i < TeamsListView.SelectedItems.Count; ++i)
            {
                if (TeamsListView.SelectedItems[i].Tag is Team team)
                    ClipboardText += team.ToFileString() + "\n";
            }
            if (string.IsNullOrEmpty(ClipboardText))
                return false;
            Clipboard.SetText(ClipboardText);
            return true;
        }

        /// <summary>
        /// Copies the selected players list view items to the clipboard
        /// </summary>
        /// <returns>True if the items were copied, false otherwise</returns>
        private bool CopySelectedPlayersListViewItemsToClipboard()
        {
            string ClipboardText = string.Empty;
            for (int i = 0; i < PlayersListView.SelectedItems.Count; ++i)
            {
                if (PlayersListView.SelectedItems[i].Tag is Player player)
                    ClipboardText += player.ToFileString() + "\n";
            }
            if (string.IsNullOrEmpty(ClipboardText))
                return false;
            Clipboard.SetText(ClipboardText);
            return true;
        }

        /// <summary>
        /// Copies the selected signed players list view items to the clipboard
        /// </summary>
        /// <returns>True if the items were copied, false otherwise</returns>
        private bool CopySelectedSignedPlayersListViewItemsToClipboard()
        {
            string ClipboardText = string.Empty;
            for (int i = 0; i < SignedPlayersListView.SelectedItems.Count; ++i)
            {
                if (SignedPlayersListView.SelectedItems[i].Tag is SignedPlayer signedPlayer)
                    ClipboardText += signedPlayer.ToFileString() + "\n";
            }
            if (string.IsNullOrEmpty(ClipboardText))
                return false;
            Clipboard.SetText(ClipboardText);
            return true;
        }

        /// <summary>
        /// Pastes the clipboard contents to the tree control
        /// </summary>
        /// <returns>True if the contents were pasted, false otherwise</returns>
        private void PasteIntoTreeViewFromClipboard()
        {
            switch (ClassifyTreeViewSelection())
            {
                case TreeViewSelection.RugbyUnion:
                    {
                        var text = Clipboard.GetText();
                        if (!string.IsNullOrEmpty(text))
                            EditRugbyUnionProperties(text);
                        break;
                    }
                case TreeViewSelection.Team:
                case TreeViewSelection.TeamsContainer:
                    PasteTeamsIntoGuiFromClipboard();
                    break;
                case TreeViewSelection.Player:
                case TreeViewSelection.PlayersContainer:
                    PastePlayersIntoGuiFromClipboard();
                    break;
                case TreeViewSelection.SignedPlayer:
                case TreeViewSelection.SignedPlayersContainer:
                    PasteSignedPlayersIntoGuiFromClipboard();
                    break;
            }
        }

        /// <summary>
        /// Pastes the clipboard data into the application and tries to parse them as a Team
        /// </summary>
        private void PasteTeamsIntoGuiFromClipboard()
        {
            var text = Clipboard.GetText();
            if (Team.IsValidFileString(text))
                AddNewTeam(Team.FromFileString(text));
            else
                Inform("The data on the Clipboard do not form a valid single Team object");
        }

        /// <summary>
        /// Pastes the clipboard data into the application and tries to parse them as a Player
        /// </summary>
        private void PastePlayersIntoGuiFromClipboard()
        {
            var text = Clipboard.GetText();
            if (Player.IsValidFileString(text))
                AddNewPlayer(Player.FromFileString(text));
            else
                Inform("The data on the Clipboard do not form a valid single Player object");
        }

        /// <summary>
        /// Pastes the clipboard data into the application and tries to parse them as a Signed Player
        /// </summary>
        private void PasteSignedPlayersIntoGuiFromClipboard()
        {
            var text = Clipboard.GetText();
            if (SignedPlayer.IsValidFileString(text))
            {
                var signedPlayer = SignedPlayer.FromFileString(text);
                AddNewSignedPlayer(signedPlayer.TeamName, signedPlayer.PlayerId);
            }
            else
                Inform("The data on the Clipboard do not form a valid single Signed Player object");
        }

        /// <summary>
        /// Confirms with the user that they do indeed want to delete the selected tree view item, then performs the deletion
        /// </summary>
        private void DeleteSelectedTreeViewItem()
        {
            var itemType = string.Empty;
            try
            {
                switch (ClassifyTreeViewSelection())
                {
                    case TreeViewSelection.Team:
                        if (RugbyUnionTreeView.SelectedNode.Tag is Team team)
                        {
                            itemType = "team";
                            if (Confirm($"Are you sure you want to delete the team \"{team.Name}\"?"))
                                _controller.DeleteTeam(team.Name); // Delegate the work to the layer below
                        }
                        break;
                    case TreeViewSelection.Player:
                        if (RugbyUnionTreeView.SelectedNode.Tag is Player player)
                        {
                            itemType = "player";
                            if (Confirm($"Are you sure you want to delete the player \"{player.DisplayName}\"?"))
                                _controller.DeletePlayer(player.Id); // Delegate the work to the layer below
                        }
                        break;
                    case TreeViewSelection.SignedPlayer:
                        if (RugbyUnionTreeView.SelectedNode.Tag is SignedPlayer signedPlayer)
                        {
                            itemType = "signed player";
                            if (Confirm($"Are you sure you want to unsign the player \"{signedPlayer.PlayerName}\" from the team \"{signedPlayer.TeamName}\"?"))
                                _controller.UnsignPlayerFromTeam(signedPlayer.PlayerId, signedPlayer.TeamName); // Delegate the work to the layer below
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Error($"Unable to delete the selected {itemType}.\r\n\r\n{ex.Message}");
            }
        }

        /// <summary>
        /// Confirms with the user that they do indeed want to delete the selected teams list view items, then performs the deletion
        /// </summary>
        private void DeleteSelectedTeamsListViewItems()
        {
            var teams = GetSelectedTeamsFromListView();
            if (teams == null || teams.Count == 0)
                return;

            try
            {
                if (teams.Count == 1)
                {
                    if (Confirm($"Are you sure you want to delete the team \"{teams[0].Name}\"?"))
                        _controller.DeleteTeam(teams[0].Name); // Delegate the work to the layer below
                }
                else
                {
                    if (Confirm($"Are you sure you want to delete {teams.Count} teams?"))
                        _controller.DeleteTeams(teams.Select(x => x.Name).ToList()); // Delegate the work to the layer below
                }
            }
            catch (Exception ex)
            {
                var item = teams.Count == 1 ? "team" : "teams";
                Error($"Unable to delete the {item}.\r\n\r\n{ex.Message}");
            }
        }

        /// <summary>
        /// Confirms with the user that they do indeed want to delete the selected players list view items, then performs the deletion
        /// </summary>
        private void DeleteSelectedPlayersListViewItems()
        {
            var players = GetSelectedPlayersFromListView();
            if (players == null || players.Count == 0)
                return;

            try
            {
                if (players.Count == 1)
                {
                    if (Confirm($"Are you sure you want to delete the player \"{players[0].DisplayName}\"?"))
                        _controller.DeletePlayer(players[0].Id); // Delegate the work to the layer below
                }
                else
                {
                    if (Confirm($"Are you sure you want to delete {players.Count} players?"))
                        _controller.DeletePlayers(players.Select(x => x.Id).ToList()); // Delegate the work to the layer below
                }
            }
            catch (Exception ex)
            {
                var item = players.Count == 1 ? "player" : "players";
                Error($"Unable to delete the {item}.\r\n\r\n{ex.Message}");
            }
        }

        /// <summary>
        /// Confirms with the user that they do indeed want to delete the selected signed players list view items, then performs the deletion
        /// </summary>
        private void DeleteSelectedSignedPlayersListViewItems()
        {
            var signedPlayers = GetSelectedSignedPlayersFromListView();
            if (signedPlayers == null || signedPlayers.Count == 0)
                return;

            try
            {
                if (signedPlayers.Count == 1)
                {
                    if (Confirm($"Are you sure you want to delete the signedPlayer \"{signedPlayers[0].DisplayName}\"?"))
                        _controller.UnsignPlayerFromTeam(signedPlayers[0].PlayerId, signedPlayers[0].TeamName); // Delegate the work to the layer below
                }
                else
                {
                    if (Confirm($"Are you sure you want to delete {signedPlayers.Count} signedPlayers?"))
                        _controller.UnsignPlayersFromTeams(signedPlayers); // Delegate the work to the layer below
                }
            }
            catch (Exception ex)
            {
                var item = signedPlayers.Count == 1 ? "signed player" : "signed players";
                Error($"Unable to delete the {item}.\r\n\r\n{ex.Message}");
            }
        }

        /// <summary>
        /// Selects/unselects all list view items
        /// </summary>
        /// <param name="listView">The list view whose items will be selected/unselected</param>
        /// <param name="selected">True if the items should be selected, false if not</param>
        private void SelectAllListViewItems(ListView listView, bool selected = true)
        {
            for (int i = 0; i < listView.Items.Count; ++i)
                listView.Items[i].Selected = selected;
        }

        /// <summary>
        /// Presents the Rugby Union properties dialog to the user.
        /// </summary>
        /// <param name="rugbyUnionName">The Rugby Union name to initialise the dialog box with</param>
        private void EditRugbyUnionProperties(string rugbyUnionName)
        {
            try
            {
                var rugbyUnionDialog = new RugbyUnionDialog(rugbyUnionName) { RugbyUnionName = rugbyUnionName };
                if (rugbyUnionDialog.ShowDialog() == DialogResult.OK)
                    _controller.RenameRugbyUnion(rugbyUnionDialog.RugbyUnionName); // Delegate the work to the layer below
            }
            catch (Exception ex)
            {
                Error($"Unable to rename the Rugby Union.\r\n\r\n{ex.Message}");
            }
        }

        /// <summary>
        /// Presents the Team properties dialog to the user.
        /// </summary>
        /// <param name="team">The team to initialise the dialog box with</param>
        private void AddNewTeam(Team team)
        {
            try
            {
                var playerDialog = new TeamDialog(_controller)
                {
                    EditMode = TeamDialog.Mode.Add,
                    Team = team // Only not null if this is data pasted from the clipboard
                };
                if (playerDialog.ShowDialog() == DialogResult.OK)
                {
                    _controller.AddTeam(playerDialog.Team); // Delegate the work to the layer below

                    if (playerDialog.SignedPlayers != null) // If the user wants to sign players now, then do so
                    {
                        foreach (var signedPlayer in playerDialog.SignedPlayers)
                            _controller.SignPlayerToTeam(signedPlayer.PlayerId, signedPlayer.TeamName); // Delegate the work to the layer below
                    }
                }
            }
            catch (Exception ex)
            {
                Error($"Unable to add a new team.\r\n\r\n{ex.Message}");
            }
        }

        /// <summary>
        /// Presents the Team properties dialog to the user.
        /// </summary>
        /// <param name="team">The team to initialise the dialog box with</param>
        private void EditExistingTeam(Team team)
        {
            try
            {
                string oldTeamName = team != null ? string.Copy(team.Name) : null;
                var playerDialog = new TeamDialog(_controller)
                {
                    EditMode = TeamDialog.Mode.Edit,
                    Team = team
                };

                List<SignedPlayer> originalSignedPlayers = null;
                if (team != null && !string.IsNullOrEmpty(team.Name))
                    originalSignedPlayers = _controller.GetPlayersSignedToTeam(team.Name); // Collect the signed players, if applicable

                if (playerDialog.ShowDialog() == DialogResult.OK)
                {
                    if (oldTeamName != playerDialog.Team.Name)
                        _controller.RenameTeam(oldTeamName, playerDialog.Team.Name); // Delegate the work to the layer below
                    _controller.EditTeam(playerDialog.Team); // Delegate the work to the layer below

                    if (playerDialog.SignedPlayers == null) // The user either removed the signed users, or simply signed none
                    {
                        if (originalSignedPlayers != null)
                            _controller.UnsignPlayersFromTeams(originalSignedPlayers); // Delegate the work to the layer below
                    }
                    else
                    {
                        foreach (var signedPlayer in playerDialog.SignedPlayers)
                            _controller.SignPlayerToTeam(signedPlayer.PlayerId, signedPlayer.TeamName); // Delegate the work to the layer below

                        if (originalSignedPlayers != null)
                        {
                            // Work out the difference between the lists and unsign them
                            foreach (var signedPlayer in originalSignedPlayers)
                            {
                                if (playerDialog.SignedPlayers.FirstOrDefault(x => x.PlayerId == signedPlayer.PlayerId) == null)
                                    _controller.UnsignPlayerFromTeam(signedPlayer.PlayerId, signedPlayer.TeamName); // Delegate the work to the layer below
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Error($"Unable to edit the team.\r\n\r\n{ex.Message}");
            }
        }

        /// <summary>
        /// Presents the Player properties dialog to the user.
        /// </summary>
        /// <param name="player">The player to initialise the dialog box with</param>
        private void AddNewPlayer(Player player)
        {
            try
            {
                var playerDialog = new PlayerDialog(_controller)
                {
                    EditMode = PlayerDialog.Mode.Add,
                    Player = player // Only not null if this is data pasted from the clipboard
                };
                if (playerDialog.ShowDialog() == DialogResult.OK)
                {
                    var playerId = _controller.AddPlayer(playerDialog.Player); // Delegate the work to the layer below
                    if (!string.IsNullOrEmpty(playerDialog.TeamName))
                        _controller.SignPlayerToTeam(playerId, playerDialog.TeamName); // Delegate the work to the layer below
                }
            }
            catch (Exception ex)
            {
                Error($"Unable to add a new player.\r\n\r\n{ex.Message}");
            }
        }

        /// <summary>
        /// Presents the Player properties dialog to the user.
        /// </summary>
        /// <param name="player">The player to initialise the dialog box with</param>
        private void EditExistingPlayer(Player player)
        {
            try
            {
                var playerDialog = new PlayerDialog(_controller)
                {
                    EditMode = PlayerDialog.Mode.Edit,
                    Player = player
                };

                var oldTeamName = string.Empty;
                var signedPlayer = _controller.FindSignedPlayer(player.Id);
                if (signedPlayer != null)
                    oldTeamName = signedPlayer.TeamName; // Get the team name to show in the dialog, if applicable

                if (playerDialog.ShowDialog() == DialogResult.OK)
                {
                    playerDialog.Player.Id = player.Id;
                    _controller.EditPlayer(playerDialog.Player); // Delegate the work to the layer below

                    if (string.IsNullOrEmpty(oldTeamName))
                    {
                        if (!string.IsNullOrEmpty(playerDialog.TeamName))
                            _controller.SignPlayerToTeam(player.Id, playerDialog.TeamName); // Delegate the work to the layer below
                    }
                    else
                    {
                        // The player used to be signed to a team... are they still?
                        if (string.IsNullOrEmpty(playerDialog.TeamName) || oldTeamName != playerDialog.TeamName)
                            _controller.UnsignPlayerFromTeam(player.Id, oldTeamName); // Delegate the work to the layer below

                        if (!string.IsNullOrEmpty(playerDialog.TeamName))
                            _controller.SignPlayerToTeam(player.Id, playerDialog.TeamName); // Delegate the work to the layer below
                    }
                }
            }
            catch (Exception ex)
            {
                Error($"Unable to edit the player.\r\n\r\n{ex.Message}");
            }
        }

        /// <summary>
        /// Presents the signed player dialog to the user.
        /// </summary>
        /// <param name="team">The team to initialise the dialog with</param>
        /// <param name="playerId">The player ID to initialise the dialog with</param>
        private void AddNewSignedPlayer(string team, int? playerId)
        {
            try
            {
                var signedPlayerDialog = new SignedPlayerDialog(_controller)
                {
                    Team = team != null ? _controller.FindTeam(team) : null, // Only not null if this is data pasted from the clipboard
                    Player = playerId != null ? _controller.FindPlayer(playerId.Value) : null // Only not null if this is data pasted from the clipboard
                };
                if (signedPlayerDialog.ShowDialog() == DialogResult.OK)
                    _controller.SignPlayerToTeam(signedPlayerDialog.Player.Id, signedPlayerDialog.Team.Name); // Delegate the work to the layer below
            }
            catch (Exception ex)
            {
                Error($"Unable to sign the player to the team.\r\n\r\n{ex.Message}");
            }
        }

        /// <summary>
        /// Presents the signed player dialog to the user.
        /// </summary>
        /// <param name="team">The team to initialise the dialog with</param>
        /// <param name="playerId">The player ID to initialise the dialog with</param>
        private void EditSignedPlayer(string team, int? playerId)
        {
            try
            {
                string oldTeamName = string.Empty;
                if (!string.IsNullOrEmpty(team))
                    oldTeamName = string.Copy(team);

                int? oldPlayerId = playerId;
                var signedPlayerDialog = new SignedPlayerDialog(_controller)
                {
                    Team = team != null ? _controller.FindTeam(team) : null,
                    Player = playerId != null ? _controller.FindPlayer(playerId.Value) : null
                };

                if (signedPlayerDialog.ShowDialog() == DialogResult.OK)
                {
                    if (oldPlayerId != null && !string.IsNullOrEmpty(oldTeamName))
                    {
                        if (oldPlayerId.Value != signedPlayerDialog.Player.Id || oldTeamName != signedPlayerDialog.Team.Name)
                            _controller.UnsignPlayerFromTeam(oldPlayerId.Value, oldTeamName); // Delegate the work to the layer below
                    }
                    _controller.SignPlayerToTeam(signedPlayerDialog.Player.Id, signedPlayerDialog.Team.Name); // Delegate the work to the layer below
                }
            }
            catch (Exception ex)
            {
                Error($"Unable to edit the signed player.\r\n\r\n{ex.Message}");
            }
        }

        /// <summary>
        /// Determines the local directory from which this executable was launched
        /// </summary>
        private static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        /// <summary>
        /// Adds a filename to the recent files submenu
        /// </summary>
        /// <param name="fileName">The filename to add to the submenu</param>
        private void AddRecentFile(string fileName)
        {
            var recentFilesFullPath = Path.Combine(AssemblyDirectory, RecentFilesFileName); // Construct a full path

            // Retrieve all previously used full paths
            var fullPaths = new List<string>();
            if (File.Exists(recentFilesFullPath))
                fullPaths = File.ReadAllLines(recentFilesFullPath).Take(MaxRecentFiles).ToList();

            // Insert the new path at the front, then trim the list's length to the maximum
            fullPaths.Insert(0, fileName);
            fullPaths = fullPaths.Distinct().Take(MaxRecentFiles).ToList();

            try
            {
                File.WriteAllLines(recentFilesFullPath, fullPaths); // Rewrite the file
            }
            catch (Exception ex)
            {
                Error($"Unable to save the recent files list to the file \"{recentFilesFullPath}\"\n\nThe error reported is \"{ex.Message}\"");
            }

            RebuildRecentFilesMenu();
        }

        /// <summary>
        /// Removes a filename from the recent files submenu
        /// </summary>
        /// <param name="fileName">The filename to add to the submenu</param>
        private void RemoveRecentFile(string fileName)
        {
            var recentFilesFullPath = Path.Combine(AssemblyDirectory, RecentFilesFileName); // Construct a full path

            // Retrieve all previously used full paths
            var fullPaths = new List<string>();
            if (File.Exists(recentFilesFullPath))
                fullPaths = File.ReadAllLines(recentFilesFullPath).Take(MaxRecentFiles).ToList();

            fullPaths.RemoveAll(x => string.Equals(x, fileName, StringComparison.OrdinalIgnoreCase)); // Get rid of it

            try
            {
                File.WriteAllLines(recentFilesFullPath, fullPaths); // Rewrite the file
            }
            catch (Exception ex)
            {
                Error($"Unable to save the recent files list to the file \"{recentFilesFullPath}\"\n\nThe error reported is \"{ex.Message}\"");
            }

            RebuildRecentFilesMenu();
        }

        /// <summary>
        /// Rebuilds the recent files submenu from the data within the recent files file. If that file is absent, or empty, then
        /// a place holder menu item is used.
        /// </summary>
        private void RebuildRecentFilesMenu()
        {
            bool insertEmpty = true;
            if (File.Exists(Path.Combine(AssemblyDirectory, RecentFilesFileName)))
                insertEmpty = !RebuildRecentItemsMenuFromFile(RecentFiles, RecentFilesFileName);

            if (insertEmpty)
                InsertEmptyMenuItemPlaceHolder(RecentFiles);
        }

        /// <summary>
        /// Loads all lines of text from the file and creates a menu item from each
        /// </summary>
        /// <param name="menuItem">The menu item to add new sub menu items to</param>
        /// <param name="fileName">The file containing full paths of the recently used files</param>
        /// <returns>True if the sub menu was rebuild successfully, false otherwise</returns>
        private bool RebuildRecentItemsMenuFromFile(ToolStripMenuItem menuItem, string fileName)
        {
            bool success = false;
            try
            {
                menuItem.DropDownItems.Clear();
                var fullPaths = File.ReadAllLines(Path.Combine(AssemblyDirectory, fileName)).Take(MaxRecentFiles).ToList();
                fullPaths = fullPaths.Distinct().ToList();

                int count = 1;
                foreach (var fullPath in fullPaths)
                {
                    var shortCutKey = count == MaxRecentFiles ? "1&0" : ("&" + count.ToString());
                    var recentFileMenuItem = menuItem.DropDownItems.Add($"{shortCutKey} {ToEllipsisPath(fullPath)}");
                    recentFileMenuItem.Tag = fullPath;
                    recentFileMenuItem.Click += RecentFile_Click;
                    ++count;
                }

                success = fullPaths.Count > 0;
            }
            catch (Exception ex)
            {
                Error($"Unable to load the recent files list from the file \"{fileName}\"\n\nThe error reported is \"{ex.Message}\"");
            }
            return success;
        }

        /// <summary>
        /// The event handler that's called when the user clicks File -> Recent Files -> (some file). The application repsonds
        /// by trying to open the file.
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void RecentFile_Click(object sender, EventArgs e)
        {
            if (!(sender is ToolStripMenuItem menuItem))
                return;
            if (!(menuItem.Tag is string fullPath))
                return;

            bool removeMenuItem = false;
            try
            {
                if (!File.Exists(fullPath))
                {
                    Inform($"The file \"{fullPath}\" does not exist.\r\n\r\nRemoving the file from the menu.");
                    removeMenuItem = true;
                }
                else
                {
                    var attributes = File.GetAttributes(fullPath);
                    if ((attributes & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        Inform($"The file system item \"{fullPath}\" is not a file.\r\n\r\nRemoving the file from the menu.");
                        removeMenuItem = true;
                    }
                    else
                        OpenRugbyUnionImpl(fullPath);
                }
            }
            catch (Exception ex) // Any file I/O errors cause the menu item to be removed
            {
                Error($"Unable to load the file \"{fullPath}\". The error reported is \"{ex.Message}\".\r\n\r\nRemoving the file from the menu.");
                removeMenuItem = true;
            }

            if (removeMenuItem)
            {
                RemoveRecentFile(fullPath);
                RebuildRecentFilesMenu();
            }
        }

        /// <summary>
        /// Transforms a very long string to a shorter string. The approach is to treat the string as a pathname that uses
        /// either \ or / as path separators. The shorter string is achieved by collapsing text between slashes to an ellipsis.
        /// </summary>
        /// <param name="pathName">A string to make shorter</param>
        /// <param name="maxPathLength">The maximum length we're going to allow</param>
        /// <returns></returns>
        private string ToEllipsisPath(string pathName, int maxPathLength = MaxRecentFileStringLength)
        {
            if (string.IsNullOrEmpty(pathName) || pathName.Length <= maxPathLength)
                return pathName;

            string[] directories = pathName.Split('\\', '/');
            string left = string.Empty;
            string right = string.Empty;

            // Build two strings: left and right. Start from the left side of the large string and copy
            // subdirectories into the left string. At the same time, start from the right side of the
            // large string and copy subdirectories into the right string.
            // Keep going until the i and j indexes have met in the middle, or we've made the longest
            // string we're prepared to accept.

            for (int i = 0, j = directories.Length - 1; i < j && left.Length + right.Length < maxPathLength; ++i, --j)
            {
                left += directories[i] + "\\";
                if (string.IsNullOrEmpty(right))
                    right = "\\" + directories[j];
                else
                    right = "\\" + directories[j] + right;
            }

            return $"{left}...{right}"; // Build the final string
        }

        /// <summary>
        /// Insert a menu item as a child of the passed in menu item. It has the special text "(empty)" and
        /// is deliberately disabled so that it won't send a click event.
        /// </summary>
        /// <param name="menuItem"></param>
        private void InsertEmptyMenuItemPlaceHolder(ToolStripMenuItem menuItem)
        {
            menuItem.DropDownItems.Clear();
            var emptyMenuItem = menuItem.DropDownItems.Add("(empty)");
            emptyMenuItem.Enabled = false;
        }

        /// <summary>
        /// The event handler that's called after the tree control's selection has changed.
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void RugbyUnionTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            _inputContext = InputContext.RugbyUnionTreeView;
            // The menu items and toolbar buttons enable/disable based on this selection
            EnableMenuItems();
            EnableToolbarButtons();
        }

        /// <summary>
        /// The event handler that's called when the user clicks the tree control
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void RugbyUnionTreeView_MouseDown(object sender, MouseEventArgs e)
        {
            _inputContext = InputContext.RugbyUnionTreeView;
            // This enables the right mouse button to change the tree control's selection
            RugbyUnionTreeView.SelectedNode = RugbyUnionTreeView.HitTest(new Point(e.X, e.Y)).Node;
        }

        /// <summary>
        /// The event handler that's called when the user changes the teams list view selection
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void TeamsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            _inputContext = InputContext.TeamsListView;
            // The menu items and toolbar buttons enable/disable based on this selection
            EnableMenuItems();
            EnableToolbarButtons();
        }

        /// <summary>
        /// The event handler that's called when the user changes the players list view selection
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void PlayersListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            _inputContext = InputContext.PlayersListView;
            // The menu items and toolbar buttons enable/disable based on this selection
            EnableMenuItems();
            EnableToolbarButtons();
        }

        /// <summary>
        /// The event handler that's called when the user changes the signed players list view selection
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void SignedPlayersListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            _inputContext = InputContext.SignedPlayersListView;
            // The menu items and toolbar buttons enable/disable based on this selection
            EnableMenuItems();
            EnableToolbarButtons();
        }

        /// <summary>
        /// The event handler that's called when the current tab from the main tab control changes
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void MainTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MainTabControl.SelectedTab == TeamsTabPage)
                _inputContext = InputContext.TeamsListView;
            else if (MainTabControl.SelectedTab == PlayersTabPage)
                _inputContext = InputContext.PlayersListView;
            else if (MainTabControl.SelectedTab == SignedPlayersTabPage)
                _inputContext = InputContext.SignedPlayersListView;
            else if (MainTabControl.SelectedTab == ChartsTabPage)
            {
                ChartDropList_SelectedIndexChanged(null, null); // Rebuild the current chart
                _inputContext = InputContext.Nothing;
            }
            else
                _inputContext = InputContext.Nothing;
            // The menu items and toolbar buttons enable/disable based on the current tab
            EnableMenuItems();
            EnableToolbarButtons();
        }

        /// <summary>
        /// The event handler that's called when the user double clicks the teams list view
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void TeamsListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (TeamsListView.SelectedItems.Count > 0)
                EditExistingTeam(TeamsListView.SelectedItems[0].Tag as Team);
        }

        /// <summary>
        /// The event handler that's called when the user double clicks the players list view
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void PlayersListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (PlayersListView.SelectedItems.Count > 0)
                EditExistingPlayer(PlayersListView.SelectedItems[0].Tag as Player);
        }

        /// <summary>
        /// The event handler that's called when the user double clicks the signed players list view
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void SignedPlayersListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SignedPlayerProperties_Click(sender, e);
        }

        /// <summary>
        /// The event handler that's called when the user is opening the tree control's context menu. We will enable/disable
        /// the context menu's menu items.
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void RugbyUnionContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var treeViewSelection = ClassifyTreeViewSelection();
            bool itemSelected = treeViewSelection == TreeViewSelection.Team ||
                                treeViewSelection == TreeViewSelection.Player ||
                                treeViewSelection == TreeViewSelection.SignedPlayer;
            bool containerSelected = false;
            bool itemsAvailable = false;
            switch (treeViewSelection)
            {
                case TreeViewSelection.TeamsContainer:
                    containerSelected = true;
                    itemsAvailable = _controller.TeamsAvailable;
                    break;
                case TreeViewSelection.PlayersContainer:
                    containerSelected = true;
                    itemsAvailable = _controller.PlayersAvailable;
                    break;
                case TreeViewSelection.SignedPlayersContainer:
                    containerSelected = true;
                    itemsAvailable = _controller.SignedPlayersAvailable;
                    break;
            }

            NewItem.Enabled = containerSelected;
            DeleteItem.Enabled = itemSelected;
            DeleteAllItems.Enabled = containerSelected && itemsAvailable;
            SignPlayerToTeam2.Enabled = treeViewSelection == TreeViewSelection.Team;
            SignPlayerWithTeam2.Enabled = treeViewSelection == TreeViewSelection.Player;
            ItemProperties.Enabled = treeViewSelection == TreeViewSelection.RugbyUnion || itemSelected;
        }

        /// <summary>
        /// The event handler that's called when the user clicks the New menu item from the Rugby Union context menu
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void NewItem_Click(object sender, EventArgs e)
        {
            switch (ClassifyTreeViewSelection())
            {
                case TreeViewSelection.TeamsContainer:
                    AddNewTeam(null);
                    break;
                case TreeViewSelection.PlayersContainer:
                    AddNewPlayer(null);
                    break;
                case TreeViewSelection.SignedPlayersContainer:
                    AddNewSignedPlayer(null, null);
                    break;
            }
        }

        /// <summary>
        /// The event handler that's called when the user clicks the Delete menu item from the Rugby Union context menu
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void DeleteItem_Click(object sender, EventArgs e)
        {
            DeleteSelectedTreeViewItem();
        }

        /// <summary>
        /// The event handler that's called when the user clicks the Delete All menu item from the Rugby Union context menu
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void DeleteAllItems_Click(object sender, EventArgs e)
        {
            var itemType = string.Empty;
            try
            {
                switch (ClassifyTreeViewSelection())
                {
                    case TreeViewSelection.TeamsContainer:
                        itemType = "teams";
                        if (Confirm("Are you sure you want to delete all teams?"))
                            _controller.DeleteAllTeams(); // Delegate the work to the layer below
                        break;
                    case TreeViewSelection.PlayersContainer:
                        itemType = "players";
                        if (Confirm("Are you sure you want to delete all players?"))
                            _controller.DeleteAllPlayers(); // Delegate the work to the layer below
                        break;
                    case TreeViewSelection.SignedPlayersContainer:
                        itemType = "signed players";
                        if (Confirm("Are you sure you want to unsign all players from all teams?"))
                            _controller.UnsignAllPlayersFromAllTeams(); // Delegate the work to the layer below
                        break;
                }
            }
            catch (Exception ex)
            {
                Error($"Unable to delete all the {itemType}.\r\n\r\n{ex.Message}");
            }
        }

        /// <summary>
        /// The event handler that's called when the user clicks the Properties menu item from the Rugby Union context menu
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void ItemProperties_Click(object sender, EventArgs e)
        {
            switch (ClassifyTreeViewSelection())
            {
                case TreeViewSelection.RugbyUnion:
                    RugbyUnionProperties_Click(sender, e);
                    break;
                case TreeViewSelection.Team:
                    EditTeam_Click(sender, e);
                    break;
                case TreeViewSelection.Player:
                    EditPlayer_Click(sender, e);
                    break;
                case TreeViewSelection.SignedPlayer:
                    if (RugbyUnionTreeView.SelectedNode.Tag is SignedPlayer signedPlayer)
                        EditSignedPlayer(signedPlayer.TeamName, signedPlayer.PlayerId);
                    break;
            }
        }

        /// <summary>
        /// The event handler that's called when the user clicks the Delete menu item from the teams list view context menu
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void DeleteTeam_Click(object sender, EventArgs e)
        {
            DeleteSelectedTeamsListViewItems();
        }

        /// <summary>
        /// The event handler that's called when the user clicks the Delete All menu item from the teams list view context menu
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void DeleteAllTeams_Click(object sender, EventArgs e)
        {
            if (Confirm("Are you sure you want to delete all teams?"))
                _controller.DeleteAllTeams(); // Delegate the work to the layer below
        }

        /// <summary>
        /// The event handler that's called when the user clicks the Delete menu item from the players list view context menu
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void DeletePlayer_Click(object sender, EventArgs e)
        {
            DeleteSelectedPlayersListViewItems();
        }

        /// <summary>
        /// The event handler that's called when the user clicks the Delete All menu item from the players list view context menu
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void DeleteAllPlayers_Click(object sender, EventArgs e)
        {
            if (Confirm("Are you sure you want to delete all players?"))
                _controller.DeleteAllPlayers(); // Delegate the work to the layer below
        }

        /// <summary>
        /// The event handler that's called when the user clicks the New menu item from the signed players list view context menu
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void NewSignedPlayer_Click(object sender, EventArgs e)
        {
            AddNewSignedPlayer(null, null);
        }

        /// <summary>
        /// The event handler that's called when the user clicks the Delete menu item from the signed players list view context menu
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void DeleteSignedPlayer_Click(object sender, EventArgs e)
        {
            DeleteSelectedSignedPlayersListViewItems();
        }

        /// <summary>
        /// The event handler that's called when the user clicks the Delete All menu item from the signed players list view context menu
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void DeleteAllSignedPlayers_Click(object sender, EventArgs e)
        {
            if (Confirm("Are you sure you want to unsign all players from all teams?"))
                _controller.UnsignAllPlayersFromAllTeams(); // Delegate the work to the layer below
        }

        /// <summary>
        /// The event handler that's called when the user clicks the Properties menu item from the signed players list view context menu
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void SignedPlayerProperties_Click(object sender, EventArgs e)
        {
            if (SignedPlayersListView.SelectedItems.Count > 0)
            {
                if (SignedPlayersListView.SelectedItems[0].Tag is SignedPlayer signedPlayer)
                    EditSignedPlayer(signedPlayer.TeamName, signedPlayer.PlayerId);
            }
        }

        /// <summary>
        /// The event handler that's called when the user is opening the teams list view's context menu. We will enable/disable
        /// the context menu's menu items.
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void TeamsListViewContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DeleteTeamMenuItem.Enabled = TeamsListView.SelectedItems.Count > 0;
            DeleteAllTeamsMenuItem.Enabled = TeamsListView.Items.Count > 0;
            SignPlayerToTeamMenuItem.Enabled = TeamsListView.SelectedItems.Count > 0;
            TeamPropertiesMenuItem.Enabled = TeamsListView.SelectedItems.Count > 0;
        }

        /// <summary>
        /// The event handler that's called when the user is opening the players list view's context menu. We will enable/disable
        /// the context menu's menu items.
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void PlayersListViewContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DeletePlayerMenuItem.Enabled = PlayersListView.SelectedItems.Count > 0;
            DeleteAllPlayersMenuItem.Enabled = PlayersListView.Items.Count > 0;
            SignWithTeamMenuItem.Enabled = PlayersListView.SelectedItems.Count > 0;
            PlayerPropertiesMenuItem.Enabled = PlayersListView.SelectedItems.Count > 0;
        }

        /// <summary>
        /// The event handler that's called when the user is opening the signed players list view's context menu. We will enable/disable
        /// the context menu's menu items.
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void SignedPlayersListViewContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DeleteSignedPlayerMenuItem.Enabled = SignedPlayersListView.SelectedItems.Count > 0;
            DeleteAllSignedPlayersMenuItem.Enabled = SignedPlayersListView.Items.Count > 0;
            SignedPlayerPropertiesMenuItem.Enabled = SignedPlayersListView.SelectedItems.Count > 0;
        }

        /// <summary>
        /// The event handler that's called when the user clicks a list view's column header
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void ListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == _listViewColumnSorter.SortColumn)
            {
                if (_listViewColumnSorter.SortOrder == SortOrder.Ascending)
                    _listViewColumnSorter.SortOrder = SortOrder.Descending;
                else
                    _listViewColumnSorter.SortOrder = SortOrder.Ascending;
            }
            else
            {
                _listViewColumnSorter.SortColumn = e.Column;
                _listViewColumnSorter.SortOrder = SortOrder.Ascending;
            }

            ((ListView)sender).Sort();
        }

        /// <summary>
        /// The controller layer has completed a find and has produced a set of results
        /// </summary>
        /// <param name="findResults">The find results</param>
        public void OnFindResults(List<FindResult> findResults)
        {
            InsertFindResults(findResults);
        }

        /// <summary>
        /// The controller layer has completed a find and has produced a set of results
        /// </summary>
        /// <param name="advancedResults">The find results</param>
        public void OnFindResults(List<AdvancedResult> advancedResults)
        {
            InsertFindResults(advancedResults);
        }

        /// <summary>
        /// Inserts the find results into the find results window
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="findResults"></param>
        private void InsertFindResults<T>(List<T> findResults)
        {
            FindResultsListBox.Items.Clear();
            if (findResults == null || findResults.Count == 0)
                FindResultsListBox.Items.Add("No items matched the find criteria");
            else
            {
                FindResultsListBox.Items.Add($"{findResults.Count} items matched the find criteria");
                foreach (var findResult in findResults)
                    FindResultsListBox.Items.Add(findResult);
            }
        }

        /// <summary>
        /// The controller layer has completed a find and replace and has produced a set of results
        /// </summary>
        /// <param name="replaceResults">The find results</param>
        public void OnReplaceResults(List<ReplaceResult> replaceResults)
        {
            InsertReplaceResults(replaceResults);
        }

        /// <summary>
        /// The controller layer has completed a find and replace and has produced a set of results
        /// </summary>
        /// <param name="advancedResults">The find results</param>
        public void OnReplaceResults(List<AdvancedResult> advancedResults)
        {
            InsertReplaceResults(advancedResults);
        }

        /// <summary>
        /// Inserts the find and replaces results into the find results window
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="replaceResults"></param>
        public void InsertReplaceResults<T>(List<T> replaceResults)
        {
            FindResultsListBox.Items.Clear();
            if (replaceResults == null || replaceResults.Count == 0)
                FindResultsListBox.Items.Add("No items matched the find criteria");
            else
            {
                FindResultsListBox.Items.Add($"{replaceResults.Count} items matched the find criteria");
                foreach (var replaceResult in replaceResults)
                    FindResultsListBox.Items.Add(replaceResult);
            }
        }

        /// <summary>
        /// The event handler that's called when the user clicks within the find results window
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void FindResultsListBox_MouseDown(object sender, MouseEventArgs e)
        {
            // This enables the right mouse button to change the list box's selection
            FindResultsListBox.SelectedIndex = FindResultsListBox.IndexFromPoint(e.X, e.Y);
        }

        /// <summary>
        /// The event handler that's called when the user double clicks the find results window
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void FindResultsListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LocateFindResultItem_Click(sender, e);
        }

        /// <summary>
        /// The event handler that's called when the user is opening the find results window's context menu. We will enable/disable
        /// the context menu's menu items.
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void FindResultsContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            LocateFindResultItem.Enabled = FindResultsListBox.SelectedIndex > 0; // The first row is always a message, not a found item
            ClearFindResults.Enabled = FindResultsListBox.Items.Count > 0;
        }

        /// <summary>
        /// The event handler that's called when the user clicks the Locate menu item from the results window's context menu.
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void LocateFindResultItem_Click(object sender, EventArgs e)
        {
            if (FindResultsListBox.SelectedIndex < 1) // The first row is always a message, not a found item
                return;

            if (FindResultsListBox.Items[FindResultsListBox.SelectedIndex] is FindResult findResult)
                LocateFindResult(findResult);
            else if (FindResultsListBox.Items[FindResultsListBox.SelectedIndex] is ReplaceResult replaceResult)
                LocateFindResult(replaceResult);
            else if (FindResultsListBox.Items[FindResultsListBox.SelectedIndex] is AdvancedResult advancedResult)
                LocateAdvancedResult(advancedResult);
        }

        /// <summary>
        /// Reveals to the user whereabouts in the GUI the findresult's item is
        /// </summary>
        /// <param name="findResult">The find result item</param>
        private void LocateFindResult(FindResult findResult)
        {
            if (findResult.Item is Team team)
                LocateTeamWithinGui(team);
            else if (findResult.Item is Player player)
                LocatePlayerWithinGui(player);
            else if (findResult.Item is SignedPlayer signedPlayer)
                LocateSignedPlayerWithinGui(signedPlayer);
        }

        /// <summary>
        /// Reveals to the user whereabouts in the GUI the findresult's item is
        /// </summary>
        /// <param name="advancedResult">The advanced find result item</param>
        private void LocateAdvancedResult(AdvancedResult advancedResult)
        {
            if (advancedResult.Item is Team team)
                LocateTeamWithinGui(team);
            else if (advancedResult.Item is Player player)
                LocatePlayerWithinGui(player);
            else if (advancedResult.Item is SignedPlayer signedPlayer)
                LocateSignedPlayerWithinGui(signedPlayer);
        }

        /// <summary>
        /// The event handler that's called when the user clicks the Clear menu item from the results window's context menu.
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void ClearFindResults_Click(object sender, EventArgs e)
        {
            FindResultsListBox.Items.Clear();
        }

        /// <summary>
        /// Selects the passed in team within both the tree control and the teams list view
        /// </summary>
        /// <param name="team">The team to select</param>
        private void LocateTeamWithinGui(Team team)
        {
            if (team != null && !string.IsNullOrEmpty(team.Name))
            {
                LocateTeamWithinTreeView(team);
                LocateTeamWithinListView(team);
            }
        }

        /// <summary>
        /// Selects the passed in team within tree control
        /// </summary>
        /// <param name="team">The team to select</param>
        private void LocateTeamWithinTreeView(Team team)
        {
            bool found = false;
            var teamsTreeNode = GetTeamsTreeNode();
            for (int i = 0; i < teamsTreeNode.Nodes.Count && !found; ++i)
            {
                if (teamsTreeNode.Nodes[i].Tag is Team x)
                {
                    found = team.Name.Equals(x.Name, StringComparison.OrdinalIgnoreCase);
                    if (found)
                        RugbyUnionTreeView.SelectedNode = teamsTreeNode.Nodes[i];
                }
            }
        }

        /// <summary>
        /// Selects the passed in team within the teams list view
        /// </summary>
        /// <param name="team">The team to select</param>
        private void LocateTeamWithinListView(Team team)
        {
            int foundIndex = -1;
            for (int i = 0; i < TeamsListView.Items.Count && foundIndex == -1; ++i)
            {
                TeamsListView.Items[i].Selected =
                            TeamsListView.Items[i].Tag is Team x &&
                            team.Name.Equals(x.Name, StringComparison.OrdinalIgnoreCase);
                if (TeamsListView.Items[i].Selected)
                    foundIndex = i;
            }

            if (foundIndex != -1)
            {
                TeamsListView.TopItem = TeamsListView.Items[foundIndex];
                ViewTeams_Click(null, null);
            }
        }

        /// <summary>
        /// Selects the passed in player within both the tree control and the players list view
        /// </summary>
        /// <param name="player">The player to select</param>
        private void LocatePlayerWithinGui(Player player)
        {
            if (player != null)
            {
                LocatePlayerWithinTreeView(player);
                LocatePlayerWithinListView(player);
            }
        }

        /// <summary>
        /// Selects the passed in player within the tree control
        /// </summary>
        /// <param name="player">The player to select</param>
        private void LocatePlayerWithinTreeView(Player player)
        {
            bool found = false;
            var playersTreeNode = GetPlayersTreeNode();
            for (int i = 0; i < playersTreeNode.Nodes.Count && !found; ++i)
            {
                if (playersTreeNode.Nodes[i].Tag is Player x)
                {
                    found = player.Id == x.Id;
                    if (found)
                        RugbyUnionTreeView.SelectedNode = playersTreeNode.Nodes[i];
                }
            }
        }

        /// <summary>
        /// Selects the passed in player within the players list view
        /// </summary>
        /// <param name="player">The player to select</param>
        private void LocatePlayerWithinListView(Player player)
        {
            int foundIndex = -1;
            for (int i = 0; i < PlayersListView.Items.Count && foundIndex == -1; ++i)
            {
                PlayersListView.Items[i].Selected =
                            PlayersListView.Items[i].Tag is Player x &&
                            player.Id == x.Id;
                if (PlayersListView.Items[i].Selected)
                    foundIndex = i;
            }

            if (foundIndex != -1)
            {
                PlayersListView.TopItem = PlayersListView.Items[foundIndex];
                ViewPlayers_Click(null, null);
            }
        }

        /// <summary>
        /// Selects the passed in signed player within both the tree control and the signed players list view
        /// </summary>
        /// <param name="signedPlayer">The signed player to select</param>
        private void LocateSignedPlayerWithinGui(SignedPlayer signedPlayer)
        {
            if (signedPlayer != null)
            {
                LocateSignedPlayerWithinTreeView(signedPlayer);
                LocateSignedPlayerWithinListView(signedPlayer);
            }
        }

        /// <summary>
        /// Selects the passed in signed player within the tree control
        /// </summary>
        /// <param name="signedPlayer">The signed player to select</param>
        private void LocateSignedPlayerWithinTreeView(SignedPlayer signedPlayer)
        {
            bool found = false;
            var signedPlayersTreeNode = GetSignedPlayersTreeNode();
            for (int i = 0; i < signedPlayersTreeNode.Nodes.Count && !found; ++i)
            {
                if (signedPlayersTreeNode.Nodes[i].Tag is SignedPlayer x)
                {
                    found = signedPlayer.PlayerId == x.PlayerId;
                    if (found)
                        RugbyUnionTreeView.SelectedNode = signedPlayersTreeNode.Nodes[i];
                }
            }
        }

        /// <summary>
        /// Selects the passed in signed player within the signed players list view
        /// </summary>
        /// <param name="signedPlayer">The signed player to select</param>
        private void LocateSignedPlayerWithinListView(SignedPlayer signedPlayer)
        {
            int foundIndex = -1;
            for (int i = 0; i < SignedPlayersListView.Items.Count && foundIndex == -1; ++i)
            {
                SignedPlayersListView.Items[i].Selected =
                            SignedPlayersListView.Items[i].Tag is SignedPlayer x &&
                            signedPlayer.PlayerId == x.PlayerId;
                if (SignedPlayersListView.Items[i].Selected)
                    foundIndex = i;
            }

            if (foundIndex != -1)
            {
                SignedPlayersListView.TopItem = SignedPlayersListView.Items[foundIndex];
                ViewSignedPlayers_Click(null, null);
            }
        }

        /// <summary>
        /// The event handler that's called when the user clicks Team -> Import
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void ImportTeam_Click(object sender, EventArgs e)
        {
            string pathName = ChooseOpenFileName(); // Get a filename from the user
            if (string.IsNullOrEmpty(pathName))
                return;

            try
            {
                var teamFile = new TeamFile(pathName, new DefaultFileIo());
                if (teamFile.Teams == null || teamFile.Teams.Count == 0)
                {
                    Inform($"The file \"{pathName}\" import OK, but has no teams within it.");
                    return;
                }

                var teamNamesImported = new List<string>();
                var teamNamesInUse = new List<string>();
                foreach (var team in teamFile.Teams)
                {
                    if (_controller.FindTeam(team.Name) != null) // Do we already have it?
                        teamNamesInUse.Add(team.Name);
                    else
                    {
                        _controller.AddTeam(team);
                        teamNamesImported.Add(team.Name);
                    }
                }

                // Present the results
                var importSummaryDialog = new ImportSummaryDialog()
                {
                    PluralNoun = "teams",
                    SuccessfulItemNames = teamNamesImported,
                    SuccessfulText = "Imported OK",
                    UnsuccessfulItemNames = teamNamesInUse,
                    UnsuccessfulText = "Team name in use"
                };
                importSummaryDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                Error($"Unable to import the file \"{pathName}\"\r\n\r\n{ex.Message}");
            }
        }

        /// <summary>
        /// The event handler that's called when the user clicks Team -> Export
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void ExportTeam_Click(object sender, EventArgs e)
        {
            string pathName = ChooseSaveAsFileName("RugbyTeams.txt"); // Get a filename from the user
            if (string.IsNullOrEmpty(pathName))
                return;

            try
            {
                var teamFile = new TeamFile(new DefaultFileIo()) { Teams = _controller.Teams };
                teamFile.Save(pathName);

                // Present the results
                Inform($"{_controller.Teams.Count} teams were written to the file \"{pathName}\"");
            }
            catch (Exception ex)
            {
                Error($"Unable to export the file \"{pathName}\"\r\n\r\n{ex.Message}");
            }
        }

        /// <summary>
        /// The event handler that's called when the user clicks Player -> Import
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void ImportPlayers_Click(object sender, EventArgs e)
        {
            string pathName = ChooseOpenFileName(); // Get a filename from the usre
            if (string.IsNullOrEmpty(pathName))
                return;

            try
            {
                var playerFile = new PlayerFile(pathName, new DefaultFileIo());
                if (playerFile.Players == null || playerFile.Players.Count == 0)
                {
                    Inform($"The file \"{pathName}\" import OK, but has no players within it.");
                    return;
                }

                var playerNamesImported = new List<string>();
                var playerNamesInUse = new List<string>();
                foreach (var player in playerFile.Players)
                {
                    if (_controller.FindPlayer(player.FirstName, player.LastName) != null) // Do we already have it?
                        playerNamesInUse.Add(player.DisplayName);
                    else
                    {
                        _controller.AddPlayer(player); // Delegate the work to the layer below
                        playerNamesImported.Add(player.DisplayName);
                    }
                }

                // Present the results
                var importSummaryDialog = new ImportSummaryDialog()
                {
                    PluralNoun = "players",
                    SuccessfulItemNames = playerNamesImported,
                    SuccessfulText = "Imported OK",
                    UnsuccessfulItemNames = playerNamesInUse,
                    UnsuccessfulText = "Player name in use"
                };
                importSummaryDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                Error($"Unable to import the file \"{pathName}\"\r\n\r\n{ex.Message}");
            }
        }

        /// <summary>
        /// The event handler that's called when the user clicks Player -> Export
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void ExportPlayers_Click(object sender, EventArgs e)
        {
            string pathName = ChooseSaveAsFileName("RugbyPlayers.txt"); // Get a filename from the user
            if (string.IsNullOrEmpty(pathName))
                return;

            try
            {
                var playerFile = new PlayerFile(new DefaultFileIo()) { Players = _controller.Players };
                playerFile.Save(pathName);

                // Present the results
                Inform($"{_controller.Players.Count} players were written to the file \"{pathName}\"");
            }
            catch (Exception ex)
            {
                Error($"Unable to export the file \"{pathName}\"\r\n\r\n{ex.Message}");
            }
        }

        /// <summary>
        /// The event handler that's called when the user changes the current chart
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void ChartDropList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_controller == null || _controller.Players == null || _controller.Teams == null) // If nothing is open/loaded, then bail now
                return;

            switch (ChartDropList.SelectedIndex)
            {
                case 0:
                    RebuildPlayerHeightsAndWeightsChart();
                    break;
                case 1:
                    RebuildAgeGroupHistogram();
                    break;
                case 2:
                    RebuildHeightsByTeam();
                    break;
                case 3:
                    RebuildWeightsByTeam();
                    break;
                case 4:
                    RebuildPlaceOfBirthHistogram();
                    break;
                case 5:
                    RebuildYearFoundedByTeam();
                    break;
            }
        }

        /// <summary>
        /// Rebuilds the Player Heights And Weights chart.
        /// This chart plots two different series of data as lines on the same chart. The first
        /// series is Player heights, the second Player Weights. The heights and weights form the
        /// vertical axis, and the Player names form the horizontal axis.
        /// </summary>
        /// <seealso href="https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-studio-2010/dd456687(v=vs.100)">Chart Keywords</seealso>
        private void RebuildPlayerHeightsAndWeightsChart()
        {
            MainChart.Titles.Clear();
            var title = MainChart.Titles.Add("Player Heights and Weights");
            title.Font = new Font("Tahoma", 12.0f, FontStyle.Bold);

            MainChart.Legends.Clear();
            MainChart.Legends.Add(new Legend() { Name = "Legend", Title = "Legend" });

            MainChart.ChartAreas.Clear();
            MainChart.ChartAreas.Add("ChartArea");
            MainChart.ChartAreas[0].AxisX.Title = "Player";
            MainChart.ChartAreas[0].AxisX.Interval = 10;
            MainChart.ChartAreas[0].AxisY.Title = "Height and Weight";
            MainChart.ChartAreas[0].AxisY.Interval = 25;

            MainChart.Series.Clear();
            var playerHeightsSeries = MainChart.Series.Add("Height (cm)");
            playerHeightsSeries.ChartType = SeriesChartType.Line;
            playerHeightsSeries.BorderWidth = 2;
            playerHeightsSeries.ToolTip = "#VALX, #VAL";        // Chart keywords to make the chart's values appear in the tooltips
            foreach (var player in _controller.Players)
                playerHeightsSeries.Points.AddXY(player.DisplayName, player.Height);

            var playerWeightsSeries = MainChart.Series.Add("Weight (kg)");
            playerWeightsSeries.ChartType = SeriesChartType.Line;
            playerWeightsSeries.BorderWidth = 2;
            playerWeightsSeries.ToolTip = "#VALX, #VAL";        // Chart keywords to make the chart's values appear in the tooltips
            foreach (var player in _controller.Players)
                playerWeightsSeries.Points.AddXY(player.DisplayName, player.Weight);
        }

        /// <summary>
        /// Rebuilds the Age Group Histogram chart.
        /// This chart has a single series that is the age of the Players within the Rugby
        /// Union. The data are presented as a Histogram. The data undergo a Group By operation
        /// before being included into the chart, therefore the chart is showing how many
        /// Players of each age there are.
        /// </summary>
        /// <seealso href="https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-studio-2010/dd456687(v=vs.100)">Chart Keywords</seealso>
        private void RebuildAgeGroupHistogram()
        {
            MainChart.Titles.Clear();
            var title = MainChart.Titles.Add("Age Group Histogram");
            title.Font = new Font("Tahoma", 12.0f, FontStyle.Bold);

            MainChart.Legends.Clear();
            MainChart.Legends.Add(new Legend() { Name = "Legend", Title = "Legend" });

            MainChart.ChartAreas.Clear();
            MainChart.ChartAreas.Add("ChartArea");
            MainChart.ChartAreas[0].AxisX.Title = "Age";
            MainChart.ChartAreas[0].AxisX.Interval = 1;
            MainChart.ChartAreas[0].AxisY.Title = "Number of Players";
            MainChart.ChartAreas[0].AxisY.Interval = 5;

            MainChart.Series.Clear();
            var ageSeries = MainChart.Series.Add("Players");
            ageSeries.ChartType = SeriesChartType.Column;
            ageSeries.BorderWidth = 2;
            ageSeries.ToolTip = "#VAL players aged #VALX";        // Chart keywords to make the chart's values appear in the tooltips

            var now = DateTime.Now;
            var playersGroupedByAge = _controller.Players.GroupBy(x => RugbyModel.Utility.DateDiffAsYears(x.DateOfBirth, now)).ToList();
            foreach (var grouping in playersGroupedByAge)
                ageSeries.Points.AddXY(grouping.Key, grouping.ToList().Count);
        }

        /// <summary>
        /// Rebuilds the Heights By Team chart.
        /// This chart consists of one series for each team. All series are drawn within the same chart
        /// and as such, quite a busy chart is presented. The Player height forms the vertical axis, the
        /// Player names form the horizontal axis.
        /// </summary>
        /// <seealso href="https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-studio-2010/dd456687(v=vs.100)">Chart Keywords</seealso>
        private void RebuildHeightsByTeam()
        {
            MainChart.Titles.Clear();
            var title = MainChart.Titles.Add("Heights for each Team");
            title.Font = new Font("Tahoma", 12.0f, FontStyle.Bold);

            MainChart.Legends.Clear();
            MainChart.Legends.Add(new Legend() { Name = "Legend", Title = "Legend" });

            MainChart.ChartAreas.Clear();
            MainChart.ChartAreas.Add("ChartArea");
            MainChart.ChartAreas[0].AxisX.Title = "Player";
            MainChart.ChartAreas[0].AxisX.Interval = 1;
            MainChart.ChartAreas[0].AxisY.Title = "Height";
            MainChart.ChartAreas[0].AxisY.Interval = 25;
            MainChart.ChartAreas[0].AxisY.Minimum = 150;

            MainChart.Series.Clear();
            foreach (var team in _controller.Teams)
            {
                var series = MainChart.Series.Add(team.Name);
                series.ChartType = SeriesChartType.Spline;
                series.BorderWidth = 2;
                series.ToolTip = "#VALX, #VAL cm";        // Chart keywords to make the chart's values appear in the tooltips
                var signedPlayers = _controller.GetPlayersSignedToTeam(team.Name);
                if (signedPlayers != null)
                {
                    foreach (var signedPlayer in signedPlayers)
                    {
                        var player = _controller.FindPlayer(signedPlayer.PlayerId);
                        if (player != null)
                            series.Points.AddXY(signedPlayer.PlayerName, player.Height);
                    }
                }
            }
        }

        /// <summary>
        /// Rebuilds the Weights By Team chart.
        /// This chart consists of one series for each team. All series are drawn within the same chart
        /// and as such, quite a busy chart is presented. The Player weight forms the vertical axis, the
        /// Player names form the horizontal axis.
        /// </summary>
        /// <seealso href="https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-studio-2010/dd456687(v=vs.100)">Chart Keywords</seealso>
        private void RebuildWeightsByTeam()
        {
            MainChart.Titles.Clear();
            var title = MainChart.Titles.Add("Weights for each Team");
            title.Font = new Font("Tahoma", 12.0f, FontStyle.Bold);

            MainChart.Legends.Clear();
            MainChart.Legends.Add(new Legend() { Name = "Legend", Title = "Legend" });

            MainChart.ChartAreas.Clear();
            MainChart.ChartAreas.Add("ChartArea");
            MainChart.ChartAreas[0].AxisX.Title = "Players";
            MainChart.ChartAreas[0].AxisX.Interval = 1;
            MainChart.ChartAreas[0].AxisY.Title = "Weight";
            MainChart.ChartAreas[0].AxisY.Interval = 25;
            MainChart.ChartAreas[0].AxisY.Minimum = 50;

            MainChart.Series.Clear();
            foreach (var team in _controller.Teams)
            {
                var series = MainChart.Series.Add(team.Name);
                series.ChartType = SeriesChartType.Spline;
                series.BorderWidth = 2;
                series.ToolTip = "#VALX, #VAL kg";        // Chart keywords to make the chart's values appear in the tooltips
                var signedPlayers = _controller.GetPlayersSignedToTeam(team.Name);
                if (signedPlayers != null)
                {
                    foreach (var signedPlayer in signedPlayers)
                    {
                        var player = _controller.FindPlayer(signedPlayer.PlayerId);
                        if (player != null)
                            series.Points.AddXY(signedPlayer.PlayerName, player.Weight);
                    }
                }
            }
        }

        /// <summary>
        /// Rebuilds the Place Of Birth Histogram chart.
        /// This chart has a single series that is the Place of Birth of the Players within the Rugby
        /// Union. The data are presented as a Histogram. The data undergo a Group By operation before
        /// being included into the chart, therefore the chart is showing how many Players from each
        /// Place of Birth there are.
        /// A simple data cleaning step is performed for this chart; if the Place of Birth field contains
        /// a comma (,) character, then the rightmost comma separated value is used.
        /// </summary>
        /// <seealso href="https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-studio-2010/dd456687(v=vs.100)">Chart Keywords</seealso>
        private void RebuildPlaceOfBirthHistogram()
        {
            MainChart.Titles.Clear();
            var title = MainChart.Titles.Add("Place of Birth Histogram");
            title.Font = new Font("Tahoma", 12.0f, FontStyle.Bold);

            MainChart.Legends.Clear();
            MainChart.Legends.Add(new Legend() { Name = "Legend", Title = "Legend" });

            MainChart.ChartAreas.Clear();
            MainChart.ChartAreas.Add("ChartArea");
            MainChart.ChartAreas[0].AxisX.Title = "Place of Birth";
            MainChart.ChartAreas[0].AxisX.Interval = 1;
            MainChart.ChartAreas[0].AxisY.Title = "Number of Players";
            MainChart.ChartAreas[0].AxisY.Interval = 5;

            MainChart.Series.Clear();
            var pobSeries = MainChart.Series.Add("Players");
            pobSeries.ChartType = SeriesChartType.Column;
            pobSeries.BorderWidth = 2;
            pobSeries.ToolTip = "#VAL players were born in #VALX";        // Chart keywords to make the chart's values appear in the tooltips

            var playersGroupedByPlaceOfBirth = _controller.Players.GroupBy(x =>
            {
                var splits = x.PlaceOfBirth.Trim().Split(',');
                if (splits != null && splits.Length > 0)
                    return splits[splits.Length - 1].Trim();
                return x.PlaceOfBirth;
            }).ToList();
            foreach (var grouping in playersGroupedByPlaceOfBirth)
                pobSeries.Points.AddXY(grouping.Key, grouping.ToList().Count);
        }

        /// <summary>
        /// Rebuilds the Year Founded by Team chart.
        /// This chart has a single series that is the Year Founded for each Team within the Rugby
        /// Union. The data are presented as a Pie Graph. The data undergo a Group By operation before
        /// being included into the chart, therefore the chart is showing how many Teams were founded
        /// in a given year.
        /// </summary>
        /// <seealso href="https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-studio-2010/dd456687(v=vs.100)">Chart Keywords</seealso>
        private void RebuildYearFoundedByTeam()
        {
            MainChart.Titles.Clear();
            var title = MainChart.Titles.Add("Year Founded grouped by Team");
            title.Font = new Font("Tahoma", 12.0f, FontStyle.Bold);

            MainChart.Legends.Clear();
            MainChart.Legends.Add(new Legend() { Name = "Legend", Title = "Legend" });

            MainChart.ChartAreas.Clear();
            MainChart.ChartAreas.Add("ChartArea");
            MainChart.ChartAreas[0].AxisX.Title = "Number of Teams";
            MainChart.ChartAreas[0].AxisX.Interval = 1;
            MainChart.ChartAreas[0].AxisY.Title = "Year Founded";
            MainChart.ChartAreas[0].AxisY.Interval = 5;

            MainChart.Series.Clear();
            var yearFoundedSeries = MainChart.Series.Add("Year Founded");
            yearFoundedSeries.ChartType = SeriesChartType.Pie;
            yearFoundedSeries.BorderWidth = 2;
            yearFoundedSeries.ToolTip = "#VAL teams were founded in #VALX";        // Chart keywords to make the chart's values appear in the tooltips

            var teamsGroupedByPlaceOfBirth = _controller.Teams.GroupBy(x => x.YearFounded).ToList();
            foreach (var grouping in teamsGroupedByPlaceOfBirth)
                yearFoundedSeries.Points.AddXY(grouping.Key, grouping.ToList().Count);
        }
    }
}
