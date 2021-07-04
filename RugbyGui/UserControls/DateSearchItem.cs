using System;
using System.Drawing;
using System.Windows.Forms;

namespace RugbyGui.UserControls
{
    /// <summary>
    /// A DateSearchItem allows the user to identify a field for a object whose type is a Date. The user can
    /// indicate they don't want to search this field by clearing the tick box. The user can specify an operation
    /// to perform on this field by choosing one from a context menu. Finally, the user can supply a parameter for
    /// the operation by typing into the edit box.
    /// </summary>
    /// <remarks>
    /// <para>Author: Damian Coventry</para>
    /// <para>Date: Feb-May 2021</para>
    /// </remarks>
    public partial class DateSearchItem : UserControl
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public DateSearchItem()
        {
            InitializeComponent();
            BeginDate = DateTime.Now.AddYears(-RugbyModel.Player.MaxAge);
            EndDate = DateTime.Now.AddYears(-RugbyModel.Player.MinAge);
        }

        /// <summary>
        /// Determines whether or not the user wants to use this search item
        /// </summary>
        public bool IsSearchItemChecked
        {
            get { return SearchItemEnabled.Checked; }
            set { SearchItemEnabled.Checked = value; }
        }

        /// <summary>
        /// The name of field within the object this search item identifies
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// The operation to perform upon the field
        /// </summary>
        public string Operation
        {
            get { return OperationLinkLabel.Text; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    OperationLinkLabel.Text = value;
            }
        }

        /// <summary>
        /// The begin value the user has supplied
        /// </summary>
        public DateTime BeginDate
        {
            get { return BeginDatePicker.Value; }
            set
            {
                if (value >= BeginDatePicker.MinDate && value <= BeginDatePicker.MaxDate)
                    BeginDatePicker.Value = value;
            }
        }

        /// <summary>
        /// The end value the user has supplied
        /// </summary>
        public DateTime EndDate
        {
            get { return EndDatePicker.Value; }
            set
            {
                if (value >= EndDatePicker.MinDate && value <= EndDatePicker.MaxDate)
                    EndDatePicker.Value = value;
            }
        }

        /// <summary>
        /// Produces a string representation of internal state suitable for displaying to the user
        /// </summary>
        /// <returns>A display string</returns>
        public string ToDisplayString()
        {
            if (Operation == "between")
                return $"{FieldName} is {Operation} \"{BeginDate.ToShortDateString()}\" and \"{EndDate.ToShortDateString()}\"";
            return $"{FieldName} is {Operation} \"{BeginDate.ToShortDateString()}\"";
        }

        /// <summary>
        /// Creates a FindField object from internal state
        /// </summary>
        /// <returns>A new FindField object</returns>
        public RugbyView.FindField ToFindField()
        {
            return new RugbyView.FindField()
            {
                Name = FieldName.Replace(" ", ""),
                Type = typeof(DateTime),
                Operation = Operation,
                BeginValue = BeginDate,
                EndValue = EndDate
            };
        }

        /// <summary>
        /// A type object objects can use to create a suitable event handler for handling the user enabling this search item
        /// </summary>
        /// <param name="sender">Will be this object</param>
        /// <param name="e">Arguments for the event</param>
        public delegate void searchItemEnabledEvent(object sender, EventArgs e);
        /// <summary>
        /// A type object objects can use to create a suitable event handler for handling the user changing the internal state of this object
        /// </summary>
        /// <param name="sender">Will be this object</param>
        /// <param name="e">Arguments for the event</param>
        public delegate void searchItemChangedEvent(object sender, EventArgs e);

        /// <summary>
        /// An event, that when fired, represents the user enabling this search item
        /// </summary>
        public event searchItemEnabledEvent SearchItemEnabledEvent;
        /// <summary>
        /// An event, that when fired, represents the user changing the internal state of this object
        /// </summary>
        public event searchItemChangedEvent SearchItemChangedEvent;

        /// <summary>
        /// The event handler that's called when the control loads
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void DateSearchItem_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FieldName))
                ItemNameLabel.Text = "FieldName is";
            else
                ItemNameLabel.Text = $"{FieldName} is";

            AndLabel.Visible = Operation == "between";
            EndDatePicker.Visible = Operation == "between";

            SearchItemEnabled_CheckedChanged(sender, e);
        }

        /// <summary>
        /// The event handler that's called when the user changes the state of the tick box
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void SearchItemEnabled_CheckedChanged(object sender, EventArgs e)
        {
            ItemNameLabel.Enabled = SearchItemEnabled.Checked;
            OperationLinkLabel.Enabled = SearchItemEnabled.Checked;
            BeginDatePicker.Enabled = SearchItemEnabled.Checked;
            AndLabel.Enabled = SearchItemEnabled.Checked;
            EndDatePicker.Enabled = SearchItemEnabled.Checked;

            IsSearchItemChecked = SearchItemEnabled.Checked;
            SearchItemEnabledEvent?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// The event handler that's called when the user clicks the operation link label
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void OperationLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OperationContextMenu.Show(OperationLinkLabel, new Point(0, OperationLinkLabel.Height));
        }

        /// <summary>
        /// The event handler that's called when the user choose a menu item from the context menu
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void OperationMenuItem_Click(object sender, EventArgs e)
        {
            OperationLinkLabel.Text = ((ToolStripMenuItem)sender).Text;
            Operation = OperationLinkLabel.Text;
            AndLabel.Visible = Operation == "between";
            EndDatePicker.Visible = Operation == "between";

            SearchItemChangedEvent?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// The event handler that's called when the user changes the text within the begin date picker control
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void BeginDatePicker_ValueChanged(object sender, EventArgs e)
        {
            BeginDate = BeginDatePicker.Value;

            SearchItemChangedEvent?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// The event handler that's called when the user changes the text within the end date picker control
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void EndDatePicker_ValueChanged(object sender, EventArgs e)
        {
            EndDate = EndDatePicker.Value;

            SearchItemChangedEvent?.Invoke(this, new EventArgs());
        }
    }
}
