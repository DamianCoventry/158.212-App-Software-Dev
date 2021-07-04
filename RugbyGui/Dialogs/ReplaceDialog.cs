using System;
using System.Windows.Forms;

namespace RugbyGui.Dialogs
{
    /// <summary>
    /// The event handlers for a dialog that collects input from the user that defines options for a find and replace operation.
    /// </summary>
    /// <remarks>
    /// <para>Author: Damian Coventry</para>
    /// <para>Date: Feb-May 2021</para>
    /// </remarks>
    public partial class ReplaceDialog : Form
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ReplaceDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The options to initialise the dialog with, and which options the user chose at dialog close
        /// </summary>
        public RugbyView.ReplaceOptions ReplaceOptions { get; set; }

        /// <summary>
        /// The event handler that's called when the dialog loads
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void ReplaceDialog_Load(object sender, EventArgs e)
        {
            if (ReplaceOptions != null) // If data were supplied then initialise the controls with them
            {
                FindWhatTextBox.Text = ReplaceOptions.FindWhat;
                ReplaceWithTextBox.Text = ReplaceOptions.ReplaceWith;
                MatchCaseCheckBox.Checked = ReplaceOptions.MatchCase;
                MatchWholeWordCheckBox.Checked = ReplaceOptions.MatchWholeWord;
                UseRegularExpressionCheckBox.Checked = ReplaceOptions.UseRegularExpression;
                FindTeamsCheckBox.Checked = ReplaceOptions.FindTeams;
                FindPlayersCheckBox.Checked = ReplaceOptions.FindPlayers;
                FindSignedPlayersCheckBox.Checked = ReplaceOptions.FindSignedPlayers;
            }
        }

        /// <summary>
        /// The event handler that's called when the user clicks the OK button
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void OkButton_Click(object sender, EventArgs e)
        {
            if (!InputValidation.IsTextFieldValid(this, FindWhatTextBox, "find text"))
                return;
            if (!InputValidation.IsTextFieldValid(this, ReplaceWithTextBox, "replace text"))
                return;
            if (!FindTeamsCheckBox.Checked && !FindPlayersCheckBox.Checked && !FindSignedPlayersCheckBox.Checked)
            {
                MessageBox.Show(this, "Please select either 'Find teams', 'Find players', or 'Find signed players'", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                FindTeamsCheckBox.Focus();
                return;
            }

            ReplaceOptions.FindWhat = FindWhatTextBox.Text;
            ReplaceOptions.ReplaceWith = ReplaceWithTextBox.Text;
            ReplaceOptions.MatchCase = MatchCaseCheckBox.Checked;
            ReplaceOptions.MatchWholeWord = MatchWholeWordCheckBox.Checked;
            ReplaceOptions.UseRegularExpression = UseRegularExpressionCheckBox.Checked;
            ReplaceOptions.FindTeams = FindTeamsCheckBox.Checked;
            ReplaceOptions.FindPlayers = FindPlayersCheckBox.Checked;
            ReplaceOptions.FindSignedPlayers = FindSignedPlayersCheckBox.Checked;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
