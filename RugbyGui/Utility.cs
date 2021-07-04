using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RugbyGui
{
    /// <summary>
    /// A small collection of utility methods related to making a nicer GUI.
    /// </summary>
    /// <remarks>
    /// <para>Author: Damian Coventry</para>
    /// <para>Date: Feb-May 2021</para>
    /// </remarks>
    public static class Utility
    {
        /// <summary>
        /// Sets the width of the listview's columns to that of the listview's content
        /// </summary>
        /// <param name="listView">The listview whose column widths to change</param>
        public static void AutoSizeListViewColumns(ListView listView)
        {
            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        /// <summary>
        /// Function pointer to an internal Windows function that sends window messages
        /// </summary>
        /// <param name="hWnd">The handle of a window</param>
        /// <param name="wMsg">The code of the message to send</param>
        /// <param name="wParam">The first parameter for the message</param>
        /// <param name="lParam">The second parameter for the message</param>
        /// <returns>A message specific value</returns>
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Internal Windows message code that controls whether or not a window should redraw
        /// </summary>
        private const int WM_SETREDRAW = 0x000B;

        /// <summary>
        /// Disables redrawing for the passed in control
        /// </summary>
        /// <param name="control"></param>
        public static void SuspendDrawing(Control control)
        {
            SendMessage(control.Handle, WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);
        }

        /// <summary>
        /// Enables redrawing for the passed in control
        /// </summary>
        /// <param name="control"></param>
        public static void ResumeDrawing(Control control)
        {
            SendMessage(control.Handle, WM_SETREDRAW, new IntPtr(1), IntPtr.Zero);
            control.Invalidate(true);
        }
    }
}
