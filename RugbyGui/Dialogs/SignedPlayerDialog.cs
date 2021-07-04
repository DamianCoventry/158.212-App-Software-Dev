using System;
using System.Windows.Forms;

namespace RugbyGui.Dialogs
{
    /// <summary>
    /// The event handlers for a dialog that collects input from the user that defines a Signed Player.
    /// </summary>
    /// <remarks>
    /// <para>Author: Damian Coventry</para>
    /// <para>Date: Feb-May 2021</para>
    /// </remarks>
    public partial class SignedPlayerDialog : Form
    {
        private readonly RugbyController.IRugbyController _controller;

        /// <summary>
        /// The constructor. A reference to a RugbyController must be supplied
        /// </summary>
        /// <param name="controller">A reference to a RugbyController</param>
        public SignedPlayerDialog(RugbyController.IRugbyController controller)
        {
            InitializeComponent();
            _controller = controller;
        }

        /// <summary>
        /// The team used to initialise the dialog box, and also the updated team data at dialog close
        /// </summary>
        public RugbyModel.Team Team { get; set; }
        /// <summary>
        /// The player used to initialise the dialog box, and also the updated player data at dialog close
        /// </summary>
        public RugbyModel.Player Player { get; set; }

        /// <summary>
        /// The event handler that's called when the dialog loads
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void SignedPlayerDialog_Load(object sender, EventArgs e)
        {
            if (Player != null)
                CopyPlayerToGui(Player); // If data were supplied then initialise the controls with them
            else
            {
                PlayerNameLabel.Text = "(no player chosen)";
                PlayerAgeValue.Text = string.Empty;
                PlayerHeightValue.Text = string.Empty;
                PlayerWeightValue.Text = string.Empty;
                ChoosePlayerLinkLabel.Text = "Choose a player...";
            }

            if (Team != null)
                CopyTeamToGui(Team); // If data were supplied then initialise the controls with them
            else
            {
                TeamLabel.Text = "(no team chosen)";
                HomeGroundValue.Text = string.Empty;
                CoachValue.Text = string.Empty;
                NumPlayersValue.Text = string.Empty;
                ChooseTeamLinkLabel.Text = "Choose a team...";
            }

            OkButton.Enabled = Player != null && Team != null;
        }

        /// <summary>
        /// Copies the state of the passed in player to the controls within the GUI
        /// </summary>
        /// <param name="player">The player object</param>
        private void CopyPlayerToGui(RugbyModel.Player player)
        {
            PlayerNameLabel.Text = player.DisplayName;
            PlayerAgeValue.Text = RugbyModel.Utility.DateDiffAsYears(player.DateOfBirth, DateTime.Now).ToString();
            PlayerHeightValue.Text = $"{player.Height} cm";
            PlayerWeightValue.Text = $"{player.Weight} kg";
            ChoosePlayerLinkLabel.Text = "Choose a different player...";
        }

        /// <summary>
        /// Copies the state of the passed in team to the controls within the GUI
        /// </summary>
        /// <param name="team">The team object</param>
        private void CopyTeamToGui(RugbyModel.Team team)
        {
            TeamLabel.Text = team.Name;
            HomeGroundValue.Text = team.HomeGround;
            CoachValue.Text = team.Coach;
            var players = _controller.GetPlayersSignedToTeam(team.Name);
            if (players != null)
                NumPlayersValue.Text = players.Count.ToString();
            else
                NumPlayersValue.Text = "0";
            ChooseTeamLinkLabel.Text = "Choose a different team...";
        }

        /// <summary>
        /// The event handler that's called when the user clicks the choose player link label. The application's response
        /// is to show the Choose Single Player dialog box.
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void ChoosePlayerLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var chooseSinglePlayerDialog = new ChooseSinglePlayerDialog(_controller)
            {
                Player = Player
            };
            if (chooseSinglePlayerDialog.ShowDialog() == DialogResult.OK)
            {
                Player = chooseSinglePlayerDialog.Player;
                CopyPlayerToGui(Player);
                OkButton.Enabled = Player != null && Team != null;
            }
        }

        /// <summary>
        /// The event handler that's called when the user clicks the choose team link label. The application's response
        /// is to show the Choose Single Team dialog box.
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void ChooseTeamLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var chooseSingleTeamDialog = new ChooseSingleTeamDialog(_controller)
            {
                Team = Team
            };
            if (chooseSingleTeamDialog.ShowDialog() == DialogResult.OK)
            {
                Team = chooseSingleTeamDialog.Team;
                CopyTeamToGui(Team);
                OkButton.Enabled = Player != null && Team != null;
            }
        }

        /// <summary>
        /// The event handler that's called when the user clicks the OK button
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void OkButton_Click(object sender, EventArgs e)
        {
            if (Player != null && Team != null)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
