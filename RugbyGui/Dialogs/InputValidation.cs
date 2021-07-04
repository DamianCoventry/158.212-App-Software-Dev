using System;
using System.Windows.Forms;

namespace RugbyGui.Dialogs
{
    /// <summary>
    /// A small collection of input validation logic.
    /// </summary>
    /// <remarks>
    /// <para>Author: Damian Coventry</para>
    /// <para>Date: Feb-May 2021</para>
    /// </remarks>
    public static class InputValidation
    {
        /// <summary>
        /// Determines whether or not a Text field has been satisfactorily filled in by the user.
        /// </summary>
        /// <param name="form">The form that owns the control</param>
        /// <param name="control">The control to inspect</param>
        /// <param name="fieldName">The name of the field to show the user</param>
        /// <returns>True if the Text field has been satisfactorily filled in, false otherwise</returns>
        public static bool IsTextFieldValid(Form form, Control control, string fieldName)
        {
            if (string.IsNullOrEmpty(control.Text) || control.Text.Trim().Length == 0)
            {
                MessageBox.Show(form, $"The {fieldName} field is required", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                control.Focus();
                return false;
            }
            if (control.Text.Contains(";"))
            {
                MessageBox.Show(form, $"The {fieldName} must not contain the ';' character", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                control.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Determines whether or not a NumericUpDown field has been satisfactorily filled in by the user.
        /// </summary>
        /// <param name="form">The form that owns the control</param>
        /// <param name="control">The control to inspect</param>
        /// <param name="fieldName">The name of the field to show the user</param>
        /// <returns>True if the NumericUpDown field has been satisfactorily filled in, false otherwise</returns>
        public static bool IsNumericUpDownFieldValid(Form form, Control control, string fieldName)
        {
            if (string.IsNullOrEmpty(control.Text) || control.Text.Trim().Length == 0)
            {
                MessageBox.Show(form, $"The {fieldName} field is required", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                control.Focus();
                return false;
            }
            if (!decimal.TryParse(control.Text, out _))
            {
                MessageBox.Show(form, $"The {fieldName} field contains an invalid number", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                control.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Determines whether or not a Control has been satisfactorily filled in by the user.
        /// </summary>
        /// <param name="form">The form that owns the control</param>
        /// <param name="control">The control to inspect</param>
        /// <param name="predicate">The caller defined check to perform on the data</param>
        /// <param name="failureMessage">The message to show the user when the check returns false</param>
        /// <returns>True if the Control has been satisfactorily filled in, false otherwise</returns>
        public static bool IsFieldValid(Form form, Control control, Predicate<string> predicate, string failureMessage)
        {
            if (!predicate(control.Text) || control.Text.Trim().Length == 0)
            {
                MessageBox.Show(form, failureMessage, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                control.Focus();
                return false;
            }
            return true;
        }
    }
}
