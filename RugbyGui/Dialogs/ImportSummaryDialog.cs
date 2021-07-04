using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RugbyGui.Dialogs
{
    /// <summary>
    /// The event handlers for a dialog that presents the results of an import/export operation
    /// </summary>
    /// <remarks>
    /// <para>Author: Damian Coventry</para>
    /// <para>Date: Feb-May 2021</para>
    /// </remarks>
    public partial class ImportSummaryDialog : Form
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ImportSummaryDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Which plural noun to show to the user
        /// </summary>
        public string PluralNoun { get; set; }
        /// <summary>
        /// Text representing a successful result
        /// </summary>
        public string SuccessfulText { get; set; }
        /// <summary>
        /// Text representing an unsuccessful result
        /// </summary>
        public string UnsuccessfulText { get; set; }
        /// <summary>
        /// List of successfully imported/exported items
        /// </summary>
        public List<string> SuccessfulItemNames { get; set; }
        /// <summary>
        /// List of unsuccessfully imported/exported items
        /// </summary>
        public List<string> UnsuccessfulItemNames { get; set; }

        /// <summary>
        /// The event handler that's called when the dialog loads
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void ImportSummary_Load(object sender, EventArgs e)
        {
            if (UnsuccessfulItemNames != null && !string.IsNullOrEmpty(UnsuccessfulText)) // If data were supplied then initialise the controls with them
            {
                FailedToImportLabel.Text = $"{UnsuccessfulItemNames.Count} {PluralNoun} failed to import";

                foreach (var itemName in UnsuccessfulItemNames)
                {
                    var listViewItem = SummaryListView.Items.Add(itemName, 1);
                    listViewItem.SubItems.Add(UnsuccessfulText);
                }
            }

            if (SuccessfulItemNames != null && !string.IsNullOrEmpty(SuccessfulText)) // If data were supplied then initialise the controls with them
            {
                ImportedOkLabel.Text = $"{SuccessfulItemNames.Count} {PluralNoun} imported OK";

                foreach (var itemName in SuccessfulItemNames)
                {
                    var listViewItem = SummaryListView.Items.Add(itemName, 0);
                    listViewItem.SubItems.Add(SuccessfulText);
                }
            }

            Utility.AutoSizeListViewColumns(SummaryListView);
        }

        /// <summary>
        /// The event handler that's called when the user clicks the Close button
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void CloseButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
