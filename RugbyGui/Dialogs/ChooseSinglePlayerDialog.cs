using System;
using System.Windows.Forms;

namespace RugbyGui.Dialogs
{
    /// <summary>
    /// The event handlers for a dialog that collects input from the user that defines a player the user has chosen.
    /// </summary>
    /// <remarks>
    /// <para>Author: Damian Coventry</para>
    /// <para>Date: Feb-May 2021</para>
    /// </remarks>
    public partial class ChooseSinglePlayerDialog : Form
    {
        private readonly RugbyController.IRugbyController _controller;
        private readonly ListViewColumnSorter _listViewColumnSorter = new ListViewColumnSorter();

        /// <summary>
        /// The constructor. A reference to a RugbyController must be supplied
        /// </summary>
        /// <param name="controller">A reference to a RugbyController</param>
        public ChooseSinglePlayerDialog(RugbyController.IRugbyController controller)
        {
            InitializeComponent();
            _controller = controller;
        }

        /// <summary>
        /// The data to initialise the dialog, and also the user's choice at dialog close
        /// </summary>
        public RugbyModel.Player Player { get; set; }

        /// <summary>
        /// The event handler that's called when the dialog loads
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void ChooseSinglePlayerDialog_Load(object sender, EventArgs e)
        {
            if (_controller.Players == null) // If data were supplied then initialise the controls with them
                return;

            foreach (var player in _controller.Players)
            {
                var item = PlayersListView.Items.Add(player.Id.ToString(), 0);
                item.ImageIndex = 0;
                item.Tag = player;
                item.SubItems.Add(player.FirstName);
                item.SubItems.Add(player.LastName);
                item.SubItems.Add($"{player.Height} cm");
                item.SubItems.Add($"{player.Weight} kg");
                item.SubItems.Add(player.DateOfBirth.ToShortDateString());
                item.SubItems.Add(player.PlaceOfBirth);
                item.Selected = Player != null && Player.Id == player.Id;
            }

            PlayersListView.ListViewItemSorter = _listViewColumnSorter;
            Utility.AutoSizeListViewColumns(PlayersListView);
        }

        /// <summary>
        /// The event handler that's called when the user double clicks the listview
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void PlayersListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OkButton_Click(sender, e);
        }

        /// <summary>
        /// The event handler that's called when the user clicks the OK button
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void OkButton_Click(object sender, EventArgs e)
        {
            if (PlayersListView.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "Please select a player", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Player = PlayersListView.SelectedItems[0].Tag as RugbyModel.Player;

            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// The event handler that's called when the user clicks a column header
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void PlayersListView_ColumnClick(object sender, ColumnClickEventArgs e)
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
    }
}
