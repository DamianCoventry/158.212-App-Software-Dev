using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RugbyGui.Dialogs
{
    /// <summary>
    /// The event handlers for a dialog that collects input from the user that defines a list of chosen players.
    /// </summary>
    /// <remarks>
    /// <para>Author: Damian Coventry</para>
    /// <para>Date: Feb-May 2021</para>
    /// </remarks>
    public partial class ChooseMultiplePlayersDialog : Form
    {
        private readonly RugbyController.IRugbyController _controller;
        private readonly ListViewColumnSorter _listViewColumnSorter = new ListViewColumnSorter();

        /// <summary>
        /// The constructor. A reference to a RugbyController must be supplied
        /// </summary>
        /// <param name="controller">A reference to a RugbyController</param>
        public ChooseMultiplePlayersDialog(RugbyController.IRugbyController controller)
        {
            InitializeComponent();
            _controller = controller;
        }

        /// <summary>
        /// The data to initialise the dialog, and also the user's choices at dialog close
        /// </summary>
        public List<RugbyModel.Player> Players { get; set; }

        /// <summary>
        /// The event handler that's called when the dialog loads
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void ChooseMultiplePlayersDialog_Load(object sender, EventArgs e)
        {
            if (_controller.Players == null) // If data were supplied then initialise the controls with them
                return;

            // Split the data into a list of selected and not selected players
            List<RugbyModel.Player> notSelectedPlayers;
            if (Players != null)
                notSelectedPlayers = _controller.Players.Where(x => !Players.Contains(x)).ToList();
            else
                notSelectedPlayers = _controller.Players;

            foreach (var player in notSelectedPlayers) // Show the user the available players
                InsertAvailablePlayer(player);

            if (Players != null)
            {
                foreach (var player in Players) // Show the user the selected players
                    InsertChosenPlayer(player);
            }

            ChosenPlayersListView.ListViewItemSorter = _listViewColumnSorter;
            AvailablePlayersListView.ListViewItemSorter = _listViewColumnSorter;
            Utility.AutoSizeListViewColumns(ChosenPlayersListView);
            Utility.AutoSizeListViewColumns(AvailablePlayersListView);
        }

        /// <summary>
        /// Inserts one player into the listview for chosen players
        /// </summary>
        /// <param name="player">The player to insert</param>
        private void InsertChosenPlayer(RugbyModel.Player player)
        {
            var item = ChosenPlayersListView.Items.Add(player.Id.ToString(), 0);
            item.ImageIndex = 0;
            item.Tag = player;
            item.SubItems.Add(player.DisplayName);
            var signedPlayer = _controller.FindSignedPlayer(player.Id);
            if (signedPlayer != null)
                item.SubItems.Add(signedPlayer.TeamName);
        }

        /// <summary>
        /// Inserts one player into the listview for available players
        /// </summary>
        /// <param name="player">The player to insert</param>
        private void InsertAvailablePlayer(RugbyModel.Player player)
        {
            var item = AvailablePlayersListView.Items.Add(player.Id.ToString(), 0);
            item.ImageIndex = 0;
            item.Tag = player;
            item.SubItems.Add(player.FirstName);
            item.SubItems.Add(player.LastName);
            item.SubItems.Add($"{player.Height} cm");
            item.SubItems.Add($"{player.Weight} kg");
            item.SubItems.Add(player.DateOfBirth.ToShortDateString());
            item.SubItems.Add(player.PlaceOfBirth);
            var signedPlayer = _controller.FindSignedPlayer(player.Id);
            if (signedPlayer != null)
                item.SubItems.Add(signedPlayer.TeamName);
        }

        /// <summary>
        /// The event handler that's called when the user clicks a column header
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
        /// The event handler that's called when the user clicks the move all left button
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void MoveAllLeftButton_Click(object sender, EventArgs e)
        {
            Utility.SuspendDrawing(ChosenPlayersListView);
            Utility.SuspendDrawing(AvailablePlayersListView);

            // Move all available players to the listview for chosen players

            for (int i = 0; i < AvailablePlayersListView.Items.Count; ++i)
            {
                if (AvailablePlayersListView.Items[i].Tag is RugbyModel.Player player)
                    InsertChosenPlayer(player);
            }
            AvailablePlayersListView.Items.Clear();

            Utility.AutoSizeListViewColumns(ChosenPlayersListView);
            Utility.AutoSizeListViewColumns(AvailablePlayersListView);
            Utility.ResumeDrawing(ChosenPlayersListView);
            Utility.ResumeDrawing(AvailablePlayersListView);
        }

        /// <summary>
        /// The event handler that's called when the user clicks move left button
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void MoveLeftButton_Click(object sender, EventArgs e)
        {
            Utility.SuspendDrawing(ChosenPlayersListView);
            Utility.SuspendDrawing(AvailablePlayersListView);

            // Move the selected available players to the listview for chosen players

            int i = 0;
            while (i < AvailablePlayersListView.SelectedItems.Count)
            {
                if (AvailablePlayersListView.SelectedItems[i].Tag is RugbyModel.Player player)
                {
                    InsertChosenPlayer(player);
                    AvailablePlayersListView.Items.RemoveAt(AvailablePlayersListView.SelectedItems[i].Index);
                }
                else
                    ++i;
            }

            Utility.AutoSizeListViewColumns(ChosenPlayersListView);
            Utility.AutoSizeListViewColumns(AvailablePlayersListView);
            Utility.ResumeDrawing(ChosenPlayersListView);
            Utility.ResumeDrawing(AvailablePlayersListView);
        }

        /// <summary>
        /// The event handler that's called when the user clicks the move right button
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void MoveRightButton_Click(object sender, EventArgs e)
        {
            Utility.SuspendDrawing(ChosenPlayersListView);
            Utility.SuspendDrawing(AvailablePlayersListView);

            // Move the selected chosen players to the listview for available players

            int i = 0;
            while (i < ChosenPlayersListView.SelectedItems.Count)
            {
                if (ChosenPlayersListView.SelectedItems[i].Tag is RugbyModel.Player player)
                {
                    InsertAvailablePlayer(player);
                    ChosenPlayersListView.Items.RemoveAt(ChosenPlayersListView.SelectedItems[i].Index);
                }
                else
                    ++i;
            }

            Utility.AutoSizeListViewColumns(ChosenPlayersListView);
            Utility.AutoSizeListViewColumns(AvailablePlayersListView);
            Utility.ResumeDrawing(ChosenPlayersListView);
            Utility.ResumeDrawing(AvailablePlayersListView);
        }

        /// <summary>
        /// The event handler that's called when the user clicks the move all right button
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void MoveAllRightButton_Click(object sender, EventArgs e)
        {
            Utility.SuspendDrawing(ChosenPlayersListView);
            Utility.SuspendDrawing(AvailablePlayersListView);

            // Move all chosen players to the listview for available players

            for (int i = 0; i < ChosenPlayersListView.Items.Count; ++i)
            {
                if (ChosenPlayersListView.Items[i].Tag is RugbyModel.Player player)
                    InsertAvailablePlayer(player);
            }
            ChosenPlayersListView.Items.Clear();

            Utility.AutoSizeListViewColumns(ChosenPlayersListView);
            Utility.AutoSizeListViewColumns(AvailablePlayersListView);
            Utility.ResumeDrawing(ChosenPlayersListView);
            Utility.ResumeDrawing(AvailablePlayersListView);
        }

        /// <summary>
        /// The event handler that's called when the user clicks the OK button
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void OkButton_Click(object sender, EventArgs e)
        {
            if (ChosenPlayersListView.Items.Count == 0)
            {
                if (MessageBox.Show(this, "No players are selected. Continue anyway?", "Validation Error", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
            }

            Players = new List<RugbyModel.Player>();
            for (int i = 0; i < ChosenPlayersListView.Items.Count; ++i)
                Players.Add(ChosenPlayersListView.Items[i].Tag as RugbyModel.Player);

            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// The event handler that's called when the user double clicks the listview for chosen players
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void ChosenPlayersListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MoveRightButton_Click(sender, e);
        }

        /// <summary>
        /// The event handler that's called when the user double clicks the listview for available players
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void AvailablePlayersListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MoveLeftButton_Click(sender, e);
        }
    }
}
