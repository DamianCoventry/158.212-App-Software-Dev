
namespace RugbyGui.UserControls
{
    partial class ReplaceSearchItem
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.MainFlowLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.SearchItemEnabled = new System.Windows.Forms.CheckBox();
            this.ReplaceLabel = new System.Windows.Forms.Label();
            this.FieldNameLinkLabel = new System.Windows.Forms.LinkLabel();
            this.WithLabel = new System.Windows.Forms.Label();
            this.ReplaceValueTextBox = new System.Windows.Forms.TextBox();
            this.ReplaceValueUpDown = new System.Windows.Forms.NumericUpDown();
            this.ReplaceValueDatePicker = new System.Windows.Forms.DateTimePicker();
            this.FieldNameContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MainFlowLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReplaceValueUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // MainFlowLayout
            // 
            this.MainFlowLayout.AutoSize = true;
            this.MainFlowLayout.Controls.Add(this.SearchItemEnabled);
            this.MainFlowLayout.Controls.Add(this.ReplaceLabel);
            this.MainFlowLayout.Controls.Add(this.FieldNameLinkLabel);
            this.MainFlowLayout.Controls.Add(this.WithLabel);
            this.MainFlowLayout.Controls.Add(this.ReplaceValueTextBox);
            this.MainFlowLayout.Controls.Add(this.ReplaceValueUpDown);
            this.MainFlowLayout.Controls.Add(this.ReplaceValueDatePicker);
            this.MainFlowLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainFlowLayout.Location = new System.Drawing.Point(0, 0);
            this.MainFlowLayout.Name = "MainFlowLayout";
            this.MainFlowLayout.Size = new System.Drawing.Size(491, 26);
            this.MainFlowLayout.TabIndex = 0;
            // 
            // SearchItemEnabled
            // 
            this.SearchItemEnabled.AutoSize = true;
            this.SearchItemEnabled.Location = new System.Drawing.Point(3, 6);
            this.SearchItemEnabled.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.SearchItemEnabled.Name = "SearchItemEnabled";
            this.SearchItemEnabled.Size = new System.Drawing.Size(15, 14);
            this.SearchItemEnabled.TabIndex = 0;
            this.SearchItemEnabled.UseVisualStyleBackColor = true;
            this.SearchItemEnabled.CheckedChanged += new System.EventHandler(this.SearchItemEnabled_CheckedChanged);
            // 
            // ReplaceLabel
            // 
            this.ReplaceLabel.AutoSize = true;
            this.ReplaceLabel.Location = new System.Drawing.Point(21, 6);
            this.ReplaceLabel.Margin = new System.Windows.Forms.Padding(0, 6, 0, 3);
            this.ReplaceLabel.Name = "ReplaceLabel";
            this.ReplaceLabel.Size = new System.Drawing.Size(47, 13);
            this.ReplaceLabel.TabIndex = 1;
            this.ReplaceLabel.Text = "Replace";
            // 
            // FieldNameLinkLabel
            // 
            this.FieldNameLinkLabel.AutoSize = true;
            this.FieldNameLinkLabel.Location = new System.Drawing.Point(68, 6);
            this.FieldNameLinkLabel.Margin = new System.Windows.Forms.Padding(0, 6, 0, 3);
            this.FieldNameLinkLabel.Name = "FieldNameLinkLabel";
            this.FieldNameLinkLabel.Size = new System.Drawing.Size(54, 13);
            this.FieldNameLinkLabel.TabIndex = 2;
            this.FieldNameLinkLabel.TabStop = true;
            this.FieldNameLinkLabel.Text = "FirstName";
            this.FieldNameLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.FieldNameLinkLabel_LinkClicked);
            // 
            // WithLabel
            // 
            this.WithLabel.AutoSize = true;
            this.WithLabel.Location = new System.Drawing.Point(122, 6);
            this.WithLabel.Margin = new System.Windows.Forms.Padding(0, 6, 0, 3);
            this.WithLabel.Name = "WithLabel";
            this.WithLabel.Size = new System.Drawing.Size(26, 13);
            this.WithLabel.TabIndex = 3;
            this.WithLabel.Text = "with";
            // 
            // ReplaceValueTextBox
            // 
            this.ReplaceValueTextBox.Location = new System.Drawing.Point(151, 3);
            this.ReplaceValueTextBox.Name = "ReplaceValueTextBox";
            this.ReplaceValueTextBox.Size = new System.Drawing.Size(160, 20);
            this.ReplaceValueTextBox.TabIndex = 4;
            this.ReplaceValueTextBox.TextChanged += new System.EventHandler(this.ReplaceValueTextBox_TextChanged);
            // 
            // ReplaceValueUpDown
            // 
            this.ReplaceValueUpDown.Location = new System.Drawing.Point(317, 3);
            this.ReplaceValueUpDown.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.ReplaceValueUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ReplaceValueUpDown.Name = "ReplaceValueUpDown";
            this.ReplaceValueUpDown.Size = new System.Drawing.Size(75, 20);
            this.ReplaceValueUpDown.TabIndex = 5;
            this.ReplaceValueUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ReplaceValueUpDown.Visible = false;
            this.ReplaceValueUpDown.ValueChanged += new System.EventHandler(this.ReplaceValueUpDown_ValueChanged);
            // 
            // ReplaceValueDatePicker
            // 
            this.ReplaceValueDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.ReplaceValueDatePicker.Location = new System.Drawing.Point(398, 3);
            this.ReplaceValueDatePicker.Name = "ReplaceValueDatePicker";
            this.ReplaceValueDatePicker.Size = new System.Drawing.Size(90, 20);
            this.ReplaceValueDatePicker.TabIndex = 6;
            this.ReplaceValueDatePicker.Visible = false;
            this.ReplaceValueDatePicker.ValueChanged += new System.EventHandler(this.ReplaceValueDatePicker_ValueChanged);
            // 
            // FieldNameContextMenu
            // 
            this.FieldNameContextMenu.Name = "FieldContextMenu";
            this.FieldNameContextMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // ReplaceSearchItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.MainFlowLayout);
            this.Name = "ReplaceSearchItem";
            this.Size = new System.Drawing.Size(491, 26);
            this.Load += new System.EventHandler(this.ReplaceSearchItem_Load);
            this.MainFlowLayout.ResumeLayout(false);
            this.MainFlowLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReplaceValueUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel MainFlowLayout;
        private System.Windows.Forms.CheckBox SearchItemEnabled;
        private System.Windows.Forms.Label ReplaceLabel;
        private System.Windows.Forms.LinkLabel FieldNameLinkLabel;
        private System.Windows.Forms.Label WithLabel;
        private System.Windows.Forms.TextBox ReplaceValueTextBox;
        private System.Windows.Forms.ContextMenuStrip FieldNameContextMenu;
        private System.Windows.Forms.NumericUpDown ReplaceValueUpDown;
        private System.Windows.Forms.DateTimePicker ReplaceValueDatePicker;
    }
}
