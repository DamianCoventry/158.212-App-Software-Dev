using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RugbyGui.UserControls
{
    /// <summary>
    /// A ReplaceSearchItem allows the user to identify a field for a object whose type is a String. The user can
    /// indicate they don't want to search this field by clearing the tick box. By using this search item type the
    /// user is saying the want the find operation to become a find and replace operation. Finally, the user can
    /// supply a value for the replace operation to use by typing into the edit box.
    /// </summary>
    /// <remarks>
    /// <para>Author: Damian Coventry</para>
    /// <para>Date: Feb-May 2021</para>
    /// </remarks>
    public partial class ReplaceSearchItem : UserControl
    {
        private List<Field> _fields;

        /// <summary>
        /// The text "(not yet chosen)"
        /// </summary>
        public const string NotYetChosen = "(not yet chosen)";

        /// <summary>
        /// Default constructor
        /// </summary>
        public ReplaceSearchItem()
        {
            InitializeComponent();
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
        /// A simple abstraction representing the name and type of a field whose value
        /// the user can choose to replace. Didn't want to use Tuple because the members
        /// Item1 and Item2 don't convey intent.
        /// </summary>
        public class Field
        {
            /// <summary>
            /// The name of the field
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// The type of data the field contains
            /// </summary>
            public Type Type { get; set; }
        }

        /// <summary>
        /// The list of fields the caller wants to be displayed to the user
        /// </summary>
        public List<Field> Fields
        {
            set
            {
                _fields = value;
                if (_fields != null)
                {
                    foreach (var field in _fields)
                    {
                        var item = FieldNameContextMenu.Items.Add(field.Name);
                        item.Click += Item_Click;
                    }
                }
            }
        }

        /// <summary>
        /// The value to replace any found data with
        /// </summary>
        public object ReplacementValue
        {
            get
            {
                if (ReplaceValueUpDown.Visible)
                    return ReplaceValueUpDown.Value;
                if (ReplaceValueDatePicker.Visible)
                    return ReplaceValueDatePicker.Value;
                return ReplaceValueTextBox.Text;
            }
            set
            {
                if (value != null)
                {
                    ReplaceValueUpDown.Visible = value.GetType() == typeof(int);
                    ReplaceValueDatePicker.Visible = value.GetType() == typeof(DateTime);
                    ReplaceValueTextBox.Visible = value.GetType() == typeof(string);

                    if (ReplaceValueUpDown.Visible)
                        ReplaceValueUpDown.Value = (int)value;
                    else if (ReplaceValueDatePicker.Visible)
                        ReplaceValueDatePicker.Value = (DateTime)value;
                    else if (ReplaceValueTextBox.Visible)
                        ReplaceValueTextBox.Text = value as string;
                }
            }
        }

        /// <summary>
        /// The type of data that the user is asking to be replaced
        /// </summary>
        public Type ReplacementType { get; set; }

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
        /// Produces a string representation of internal state suitable for displaying to the user
        /// </summary>
        /// <returns>A display string</returns>
        public string ToDisplayString()
        {
            return $"\"{FieldName}\" with \"{ReplacementValue}\"";
        }

        /// <summary>
        /// Creates a ReplaceField object from internal state
        /// </summary>
        /// <returns>A new FindField object</returns>
        public RugbyView.ReplaceField ToReplaceField()
        {
            return new RugbyView.ReplaceField()
            {
                Name = FieldName.Replace(" ", ""),
                Type = ReplacementType,
                Value = ReplacementValue
            };
        }

        /// <summary>
        /// The event handler that's called when the control loads
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void ReplaceSearchItem_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FieldName))
                FieldNameLinkLabel.Text = NotYetChosen;
            else
                FieldNameLinkLabel.Text = FieldName;

            SearchItemEnabled_CheckedChanged(sender, e);
        }

        /// <summary>
        /// The event handler that's called when the user changes the state of the tick box
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void SearchItemEnabled_CheckedChanged(object sender, EventArgs e)
        {
            ReplaceLabel.Enabled = SearchItemEnabled.Checked;
            FieldNameLinkLabel.Enabled = SearchItemEnabled.Checked;
            WithLabel.Enabled = SearchItemEnabled.Checked;
            ReplaceValueTextBox.Enabled = SearchItemEnabled.Checked;

            IsSearchItemChecked = SearchItemEnabled.Checked;
            SearchItemEnabledEvent?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// The event handler that's called when the user clicks the field name link label
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void FieldNameLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FieldNameContextMenu.Show(FieldNameLinkLabel, new Point(0, FieldNameLinkLabel.Height));
        }

        private void Item_Click(object sender, EventArgs e)
        {
            FieldNameLinkLabel.Text = ((ToolStripMenuItem)sender).Text;
            FieldName = FieldNameLinkLabel.Text;

            var field = _fields.FirstOrDefault(x => x.Name == FieldName);
            if (field != null)
            {
                ReplacementType = field.Type;
                ReplaceValueTextBox.Visible = field.Type == typeof(string);
                ReplaceValueUpDown.Visible = field.Type == typeof(int);
                ReplaceValueDatePicker.Visible = field.Type == typeof(DateTime);

                if (ReplaceValueTextBox.Visible)
                    ReplacementValue = ReplaceValueTextBox.Text;
                else if (ReplaceValueUpDown.Visible)
                    ReplacementValue = ReplaceValueUpDown.Value.ToString();
                else if (ReplaceValueDatePicker.Visible)
                    ReplacementValue = ReplaceValueDatePicker.Value.ToShortDateString();
            }

            SearchItemChangedEvent?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// The event handler that's called when the user changes the text within the parameter text box
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void ReplaceValueTextBox_TextChanged(object sender, EventArgs e)
        {
            ReplacementValue = ReplaceValueTextBox.Text;

            SearchItemChangedEvent?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// The event handler that's called when the user changes the value within the numeric up/down control
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void ReplaceValueUpDown_ValueChanged(object sender, EventArgs e)
        {
            ReplacementValue = ReplaceValueUpDown.Value.ToString();

            SearchItemChangedEvent?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// The event handler that's called when the user changes the text within the date picker control
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void ReplaceValueDatePicker_ValueChanged(object sender, EventArgs e)
        {
            ReplacementValue = ReplaceValueDatePicker.Value.ToShortDateString();

            SearchItemChangedEvent?.Invoke(this, new EventArgs());
        }
    }
}
