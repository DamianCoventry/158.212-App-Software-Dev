using System;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RugbyGui.Dialogs
{
    /// <summary>
    /// The event handlers for a dialog that collects input from the user that defines a Team.
    /// </summary>
    /// <remarks>
    /// <para>Author: Damian Coventry</para>
    /// <para>Date: Feb-May 2021</para>
    /// </remarks>
    public partial class TeamDialog : Form
    {
        private readonly RugbyController.IRugbyController _controller;
        private string _teamNameAtDialogLoad;

        /// <summary>
        /// The constructor. A reference to a RugbyController must be supplied
        /// </summary>
        /// <param name="controller">A reference to a RugbyController</param>
        public TeamDialog(RugbyController.IRugbyController controller)
        {
            InitializeComponent();
            _controller = controller;
        }

        /// <summary>
        /// Defines the mode of operation for the dialog
        /// </summary>
        public enum Mode
        {
            /// <summary>
            /// The dialog is adding a new Team
            /// </summary>
            Add,
            /// <summary>
            /// The dialog is editing an existing Team
            /// </summary>
            Edit
        }
        /// <summary>
        /// The mode the dialog is currently using
        /// </summary>
        public Mode EditMode { get; set; } = Mode.Edit;
        /// <summary>
        /// The team used to initialise the dialog box, and also the updated team data at dialog close
        /// </summary>
        public RugbyModel.Team Team { get; set; }
        /// <summary>
        /// The signed players used to initialise the dialog box, and also the updated team data at dialog close
        /// </summary>
        public List<RugbyModel.SignedPlayer> SignedPlayers { get; private set; }

        /// <summary>
        /// The event handler that's called when the dialog loads
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void TeamDialog_Load(object sender, EventArgs e)
        {
            Text = EditMode == Mode.Add ? "Add Team" : "Edit Team"; // Set the dialog's title bar text accordingly

            bool playersAreSigned = false;
            SignedPlayers = null;

            if (Team != null) // If data were supplied then initialise the controls with them
            {
                _teamNameAtDialogLoad = Team.Name.Trim();
                NameTextBox.Text = Team.Name;
                HomeGroundTextBox.Text = Team.HomeGround;
                CoachTextBox.Text = Team.Coach;
                RegionTextBox.Text = Team.Region;
                YearFoundedNumericUpDown.Value = Team.YearFounded;

                if (EditMode == Mode.Edit) // If we're editing an existing item, it might have associated data
                {
                    SignedPlayers = _controller.GetPlayersSignedToTeam(Team.Name);
                    if (SignedPlayers != null && SignedPlayers.Count > 0)
                    {
                        playersAreSigned = true;
                        PlayerCountLinkLabel.Text = $"{SignedPlayers.Count} players";
                        RebuildContextMenu(SignedPlayers); // Insert each player into the context menu
                    }
                }
            }

            PlayerCountLinkLabel.Enabled = playersAreSigned;
            if (!playersAreSigned)
                PlayerCountLinkLabel.Text = "(no players signed to team)";
        }

        /// <summary>
        /// Inserts each player into the context menu
        /// </summary>
        /// <param name="playersSignedToTeam">The players to insert into the context menu</param>
        private void RebuildContextMenu(List<RugbyModel.SignedPlayer> playersSignedToTeam)
        {
            TeamContextMenuStrip.Items.Clear();
            foreach (var signedPlayer in playersSignedToTeam)
            {
                var player = _controller.FindPlayer(signedPlayer.PlayerId);
                if (player != null)
                    TeamContextMenuStrip.Items.Add(player.DisplayName, Properties.Resources.Player16x16);
            }
        }

        /// <summary>
        /// Inserts each player into the context menu
        /// </summary>
        /// <param name="players">The players to insert into the context menu</param>
        private void RebuildContextMenu(List<RugbyModel.Player> players)
        {
            TeamContextMenuStrip.Items.Clear();
            foreach (var player in players)
                TeamContextMenuStrip.Items.Add(player.DisplayName, Properties.Resources.Player16x16);
        }

        /// <summary>
        /// The event handler that's called when the user clicks the player count link label
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void PlayerCountLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Show the user list of players
            TeamContextMenuStrip.Show(PlayerCountLinkLabel, new Point(0, PlayerCountLinkLabel.Height));
        }

        /// <summary>
        /// The event handler that's called when the user clicks the choose link label. The application's response
        /// is to show the Choose Multiple Players dialog box.
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void ChooseLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var chooseMultiplePlayersDialog = new ChooseMultiplePlayersDialog(_controller);

            if (Team != null) // If we have a team then copy its state into the dialog box
            {
                var players = _controller.GetPlayersSignedToTeam(Team.Name);
                if (players != null)
                {
                    chooseMultiplePlayersDialog.Players = new List<RugbyModel.Player>();
                    foreach (var signedPlayer in players)
                        chooseMultiplePlayersDialog.Players.Add(_controller.FindPlayer(signedPlayer.PlayerId));
                }
            }

            if (chooseMultiplePlayersDialog.ShowDialog() == DialogResult.OK)
            {
                // The user might have opted to remove all players from the Team, so make sure we're handling that case.

                if (chooseMultiplePlayersDialog.Players == null || chooseMultiplePlayersDialog.Players.Count == 0)
                {
                    PlayerCountLinkLabel.Text = "(no players signed to team)";
                    PlayerCountLinkLabel.Enabled = false;
                }
                else
                {
                    PlayerCountLinkLabel.Text = $"{chooseMultiplePlayersDialog.Players.Count} players";
                    PlayerCountLinkLabel.Enabled = true;

                    RebuildContextMenu(chooseMultiplePlayersDialog.Players); // Insert each player into the context menu
                    SignedPlayers = ToSignedPlayers(chooseMultiplePlayersDialog.Players);
                }
            }
        }

        /// <summary>
        /// Creates a SignedPlayer instance out of each Player instance
        /// </summary>
        /// <param name="players">The player data to transform into another state</param>
        /// <returns>The list of transformed player data</returns>
        private List<RugbyModel.SignedPlayer> ToSignedPlayers(List<RugbyModel.Player> players)
        {
            return players.Select(x => new RugbyModel.SignedPlayer()
            {
                PlayerId = x.Id,
                PlayerName = x.DisplayName,
                TeamName = Team?.Name
            }).ToList();
        }

        /// <summary>
        /// The event handler that's called when the user clicks the OK button
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void OkButton_Click(object sender, EventArgs e)
        {
            if (!InputValidation.IsTextFieldValid(this, NameTextBox, "Name"))
                return;
            if (!InputValidation.IsTextFieldValid(this, HomeGroundTextBox, "Home Ground"))
                return;
            if (!InputValidation.IsTextFieldValid(this, CoachTextBox, "Coach"))
                return;
            if (!InputValidation.IsTextFieldValid(this, RegionTextBox, "Region"))
                return;
            if (!InputValidation.IsNumericUpDownFieldValid(this, YearFoundedNumericUpDown, "Year Founded"))
                return;

            if (!InputValidation.IsFieldValid(this, NameTextBox, (controlText) =>
                    {
                        if (_controller.FindTeam(controlText.Trim()) == null) // If it doesn't exist it's OK to create it.
                            return true;
                        if (EditMode == Mode.Add) // Disallow duplicate team names to be added
                            return false;
                        // If the user is editing an existing team and hasn't renamed it then that's an OK use case.
                        return _teamNameAtDialogLoad.Equals(controlText.Trim(), StringComparison.OrdinalIgnoreCase); // Don't allow renaming to an existing team.
                    },
                    $"The name \"{NameTextBox.Text}\" is already in use by another team."))
            {
                return;
            }

            // Copy it all in to a new instance for the caller to use.
            Team = new RugbyModel.Team
            {
                Name = NameTextBox.Text.Trim(),
                HomeGround = HomeGroundTextBox.Text.Trim(),
                Coach = CoachTextBox.Text.Trim(),
                Region = RegionTextBox.Text.Trim(),
                YearFounded = (int)YearFoundedNumericUpDown.Value
            };

            if (SignedPlayers != null)
            {
                foreach (var signedPlayer in SignedPlayers)
                    signedPlayer.TeamName = Team.Name;
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
