using System;
using System.Windows.Forms;

namespace RugbyGui.Dialogs
{
    /// <summary>
    /// The event handlers for a dialog that collects input from the user that defines a Player.
    /// </summary>
    /// <remarks>
    /// <para>Author: Damian Coventry</para>
    /// <para>Date: Feb-May 2021</para>
    /// </remarks>
    public partial class PlayerDialog : Form
    {
        private const string NotSignedToATeam = "(not signed to a team)";
        private readonly RugbyController.IRugbyController _controller;
        private string _playerNameAtDialogLoad;

        /// <summary>
        /// The constructor. A reference to a RugbyController must be supplied
        /// </summary>
        /// <param name="controller">A reference to a RugbyController</param>
        public PlayerDialog(RugbyController.IRugbyController controller)
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
        /// The player used to initialise the dialog box, and also the updated player data at dialog close
        /// </summary>
        public RugbyModel.Player Player { get; set; }
        /// <summary>
        /// The name of the team the user choose within the GUI
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// The event handler that's called when the dialog loads
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void PlayerDialog_Load(object sender, EventArgs e)
        {
            Text = EditMode == Mode.Add ? "Add Player" : "Edit Player"; // Set the dialog's title bar text accordingly

            bool playerIsSignedToTeam = false;
            if (Player != null) // If data were supplied then initialise the controls with them
            {
                _playerNameAtDialogLoad = $"{Player.FirstName} {Player.LastName}";
                FirstNameTextBox.Text = Player.FirstName;
                LastNameTextBox.Text = Player.LastName;
                HeightUpDown.Value = Player.Height;
                WeightUpDown.Value = Player.Weight;
                DateOfBirthDatePicker.Value = Player.DateOfBirth;
                PlaceOfBirthTextBox.Text = Player.PlaceOfBirth;

                if (EditMode == Mode.Edit) // If we're editing an existing item, it might have associated data
                {
                    var signedPlayer = _controller.FindSignedPlayer(Player.Id);
                    if (signedPlayer != null)
                    {
                        playerIsSignedToTeam = true;
                        TeamNameLabel.Text = signedPlayer.TeamName; // Let the user know the team name
                        ChooseLinkLabel.Text = "Change...";
                    }
                }
            }
            else
                DateOfBirthDatePicker.Value = DateTime.Now.AddYears(-(RugbyModel.Player.MinAge + 7)); // A reasonable default

            UnsignLinkLabel.Enabled = playerIsSignedToTeam;
            if (!playerIsSignedToTeam)
            {
                TeamNameLabel.Text = NotSignedToATeam; // Let the user know this player is not signed to a team
                ChooseLinkLabel.Text = "Choose...";
            }
        }

        /// <summary>
        /// The event handler that's called when the user clicks the team name link label
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void TeamNameLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var chooseSingleTeamDialog = new ChooseSingleTeamDialog(_controller)
            {
                TeamName = TeamNameLabel.Text == NotSignedToATeam ? null : TeamNameLabel.Text
            };
            if (chooseSingleTeamDialog.ShowDialog() == DialogResult.OK)
            {
                TeamNameLabel.Text = chooseSingleTeamDialog.Team.Name; // Let the user know the team name
                ChooseLinkLabel.Text = "Change...";
                UnsignLinkLabel.Enabled = true;
            }
        }

        /// <summary>
        /// The event handler that's called when the user clicks the unsign link label
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void UnsignLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TeamNameLabel.Text = NotSignedToATeam;
            ChooseLinkLabel.Text = "Choose...";
            UnsignLinkLabel.Enabled = false;
        }

        /// <summary>
        /// The event handler that's called when the user clicks the OK button
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void OkButton_Click(object sender, EventArgs e)
        {
            if (!InputValidation.IsTextFieldValid(this, FirstNameTextBox, "first name"))
                return;
            if (!InputValidation.IsTextFieldValid(this, LastNameTextBox, "last name"))
                return;
            if (!InputValidation.IsTextFieldValid(this, HeightUpDown, "height"))
                return;
            if (!InputValidation.IsTextFieldValid(this, WeightUpDown, "weight"))
                return;
            if (!InputValidation.IsTextFieldValid(this, DateOfBirthDatePicker, "date of birth"))
                return;
            if (!InputValidation.IsTextFieldValid(this, PlaceOfBirthTextBox, "place of birth"))
                return;
            if (!InputValidation.IsFieldValid(this, FirstNameTextBox, (controlText) =>
                    {
                        if (_controller.FindPlayer(FirstNameTextBox.Text.Trim(), LastNameTextBox.Text.Trim()) == null) // If it doesn't exist it's OK to create it.
                            return true;
                        if (EditMode == Mode.Add) // Disallow duplicate player names to be added
                            return false;
                        // If the user is editing an existing player and hasn't renamed it then that's an OK use case.
                        return _playerNameAtDialogLoad.Equals($"{FirstNameTextBox.Text.Trim()} {LastNameTextBox.Text.Trim()}", StringComparison.OrdinalIgnoreCase); // Don't allow renaming to an existing player.
                    },
                    $"The name \"{FirstNameTextBox.Text} {LastNameTextBox.Text}\" is already in use by another player."))
            {
                return;
            }

            var age = RugbyModel.Utility.DateDiffAsYears(DateOfBirthDatePicker.Value, DateTime.Now);
            if (age < RugbyModel.Player.MinAge)
            {
                MessageBox.Show(this, $"The player must be at least {RugbyModel.Player.MinAge} years old.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (age > RugbyModel.Player.MaxAge)
            {
                MessageBox.Show(this, $"The player is too old. The maximum allowable age is {RugbyModel.Player.MaxAge} years old.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Copy it all in to a new instance for the caller to use.
            Player = new RugbyModel.Player
            {
                FirstName = FirstNameTextBox.Text.Trim(),
                LastName = LastNameTextBox.Text.Trim(),
                Height = (int)HeightUpDown.Value,
                Weight = (int)WeightUpDown.Value,
                DateOfBirth = DateOfBirthDatePicker.Value,
                PlaceOfBirth = PlaceOfBirthTextBox.Text.Trim()
            };

            TeamName = TeamNameLabel.Text == NotSignedToATeam ? null : string.Copy(TeamNameLabel.Text);

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
