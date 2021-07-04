using System;
using System.Windows.Forms;

namespace RugbyGui.Dialogs
{
    /// <summary>
    /// The event handlers for a dialog that collects input from the user that defines a team the user has chosen.
    /// </summary>
    /// <remarks>
    /// <para>Author: Damian Coventry</para>
    /// <para>Date: Feb-May 2021</para>
    /// </remarks>
    public partial class ChooseSingleTeamDialog : Form
    {
        private readonly RugbyController.IRugbyController _controller;
        private readonly ListViewColumnSorter _listViewColumnSorter = new ListViewColumnSorter();

        /// <summary>
        /// The constructor. A reference to a RugbyController must be supplied
        /// </summary>
        /// <param name="controller">A reference to a RugbyController</param>
        public ChooseSingleTeamDialog(RugbyController.IRugbyController controller)
        {
            InitializeComponent();
            _controller = controller;
        }

        /// <summary>
        /// The name of the team to initialise the dialog with
        /// </summary>
        public string TeamName { get; set; }
        /// <summary>
        /// The team to initialise the dialog with, and also the team chosen by the user
        /// </summary>
        public RugbyModel.Team Team { get; set; }

        /// <summary>
        /// The event handler that's called when the dialog loads
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void ChooseSingleTeamDialog_Load(object sender, EventArgs e)
        {
            if (_controller.Teams == null) // If data were supplied then initialise the controls with them
                return;

            foreach (var team in _controller.Teams)
            {
                var item = TeamsListView.Items.Add(team.Name, 0);
                item.ImageIndex = 0;
                item.Tag = team;
                item.SubItems.Add(team.HomeGround);
                item.SubItems.Add(team.Coach);
                item.SubItems.Add(team.YearFounded.ToString());
                item.SubItems.Add(team.Region);
                if (Team != null)
                    item.Selected = team.Name.Equals(Team.Name, StringComparison.OrdinalIgnoreCase);
                else if (!string.IsNullOrEmpty(TeamName))
                    item.Selected = team.Name.Equals(TeamName, StringComparison.OrdinalIgnoreCase);
            }

            TeamsListView.ListViewItemSorter = _listViewColumnSorter;
            Utility.AutoSizeListViewColumns(TeamsListView);
        }

        /// <summary>
        /// The event handler that's called when the user double clicks the listview
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void TeamsListView_MouseDoubleClick(object sender, MouseEventArgs e)
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
            if (TeamsListView.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "Please select a team", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Team = TeamsListView.SelectedItems[0].Tag as RugbyModel.Team;

            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// The event handler that's called when the user clicks a column header
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void TeamsListView_ColumnClick(object sender, ColumnClickEventArgs e)
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
