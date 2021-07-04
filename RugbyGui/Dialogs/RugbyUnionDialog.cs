using System;
using System.Windows.Forms;

namespace RugbyGui.Dialogs
{
    /// <summary>
    /// The event handlers for a dialog that collects input from the user that defines a name for a Rugby Union.
    /// </summary>
    /// <remarks>
    /// <para>Author: Damian Coventry</para>
    /// <para>Date: Feb-May 2021</para>
    /// </remarks>
    public partial class RugbyUnionDialog : Form
    {
        /// <summary>
        /// The constructor. A Rugby Union name must be supplied
        /// </summary>
        /// <param name="rugbyUnionName">A Rugby Union name</param>
        public RugbyUnionDialog(string rugbyUnionName)
        {
            InitializeComponent();
            RugbyUnionName = rugbyUnionName;
        }

        /// <summary>
        /// The name used to initialise the dialog box, and the name entered by the user
        /// </summary>
        public string RugbyUnionName { get; internal set; }

        /// <summary>
        /// The event handler that's called when the dialog loads
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void RugbyUnionDialog_Load(object sender, EventArgs e)
        {
            NameTextBox.Text = RugbyUnionName; // If a name was supplied then initialise the text box with it
        }

        /// <summary>
        /// The event handler that's called when the user clicks the OK button
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void OkButton_Click(object sender, EventArgs e)
        {
            if (!InputValidation.IsTextFieldValid(this, NameTextBox, "name"))
                return;
            RugbyUnionName = NameTextBox.Text;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
