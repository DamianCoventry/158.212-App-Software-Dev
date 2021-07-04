using System;
using System.Windows.Forms;

namespace RugbyGui.Dialogs
{
    /// <summary>
    /// The event handlers for a dialog that collects input from the user that defines options for a find operation.
    /// </summary>
    /// <remarks>
    /// <para>Author: Damian Coventry</para>
    /// <para>Date: Feb-May 2021</para>
    /// </remarks>
    public partial class FindDialog : Form
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public FindDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The options to initialise the dialog with, and which options the user chose at dialog close
        /// </summary>
        public RugbyView.FindReplaceOptions FindOptions { get; set; }

        /// <summary>
        /// The event handler that's called when the dialog loads
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void FindDialog_Load(object sender, EventArgs e)
        {
            if (FindOptions != null) // If data were supplied then initialise the controls with them
            {
                FindWhatTextBox.Text = FindOptions.FindWhat;
                MatchCaseCheckBox.Checked = FindOptions.MatchCase;
                MatchWholeWordCheckBox.Checked = FindOptions.MatchWholeWord;
                UseRegularExpressionCheckBox.Checked = FindOptions.UseRegularExpression;
                FindTeamsCheckBox.Checked = FindOptions.FindTeams;
                FindPlayersCheckBox.Checked = FindOptions.FindPlayers;
                FindSignedPlayersCheckBox.Checked = FindOptions.FindSignedPlayers;
            }
        }

        /// <summary>
        /// The event handler that's called when the user clicks the OK button
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void OkButton_Click(object sender, EventArgs e)
        {
            if (!InputValidation.IsTextFieldValid(this, FindWhatTextBox, "Find What"))
                return;
            if (!FindTeamsCheckBox.Checked && !FindPlayersCheckBox.Checked && !FindSignedPlayersCheckBox.Checked)
            {
                MessageBox.Show(this, "Please select either 'Find teams', 'Find players', or 'Find signed players'", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FindTeamsCheckBox.Focus();
                return;
            }

            FindOptions.FindWhat = FindWhatTextBox.Text;
            FindOptions.MatchCase = MatchCaseCheckBox.Checked;
            FindOptions.MatchWholeWord = MatchWholeWordCheckBox.Checked;
            FindOptions.UseRegularExpression = UseRegularExpressionCheckBox.Checked;
            FindOptions.FindTeams = FindTeamsCheckBox.Checked;
            FindOptions.FindPlayers = FindPlayersCheckBox.Checked;
            FindOptions.FindSignedPlayers = FindSignedPlayersCheckBox.Checked;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
