
namespace RugbyGui.UserControls
{
    partial class DateSearchItem
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
            this.ItemNameLabel = new System.Windows.Forms.Label();
            this.OperationLinkLabel = new System.Windows.Forms.LinkLabel();
            this.BeginDatePicker = new System.Windows.Forms.DateTimePicker();
            this.AndLabel = new System.Windows.Forms.Label();
            this.EndDatePicker = new System.Windows.Forms.DateTimePicker();
            this.OperationContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.EqualToMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BeforeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AfterMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BetweenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainFlowLayout.SuspendLayout();
            this.OperationContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainFlowLayout
            // 
            this.MainFlowLayout.AutoSize = true;
            this.MainFlowLayout.Controls.Add(this.SearchItemEnabled);
            this.MainFlowLayout.Controls.Add(this.ItemNameLabel);
            this.MainFlowLayout.Controls.Add(this.OperationLinkLabel);
            this.MainFlowLayout.Controls.Add(this.BeginDatePicker);
            this.MainFlowLayout.Controls.Add(this.AndLabel);
            this.MainFlowLayout.Controls.Add(this.EndDatePicker);
            this.MainFlowLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainFlowLayout.Location = new System.Drawing.Point(0, 0);
            this.MainFlowLayout.Name = "MainFlowLayout";
            this.MainFlowLayout.Size = new System.Drawing.Size(366, 26);
            this.MainFlowLayout.TabIndex = 0;
            // 
            // SearchItemEnabled
            // 
            this.SearchItemEnabled.AutoSize = true;
            this.SearchItemEnabled.Location = new System.Drawing.Point(3, 6);
            this.SearchItemEnabled.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.SearchItemEnabled.Name = "SearchItemEnabled";
            this.SearchItemEnabled.Size = new System.Drawing.Size(15, 14);
            this.SearchItemEnabled.TabIndex = 5;
            this.SearchItemEnabled.UseVisualStyleBackColor = true;
            this.SearchItemEnabled.CheckedChanged += new System.EventHandler(this.SearchItemEnabled_CheckedChanged);
            // 
            // ItemNameLabel
            // 
            this.ItemNameLabel.AutoSize = true;
            this.ItemNameLabel.Location = new System.Drawing.Point(21, 6);
            this.ItemNameLabel.Margin = new System.Windows.Forms.Padding(0, 6, 0, 3);
            this.ItemNameLabel.Name = "ItemNameLabel";
            this.ItemNameLabel.Size = new System.Drawing.Size(80, 13);
            this.ItemNameLabel.TabIndex = 0;
            this.ItemNameLabel.Text = "{DateOfBirth} is";
            // 
            // OperationLinkLabel
            // 
            this.OperationLinkLabel.AutoSize = true;
            this.OperationLinkLabel.Location = new System.Drawing.Point(101, 6);
            this.OperationLinkLabel.Margin = new System.Windows.Forms.Padding(0, 6, 0, 3);
            this.OperationLinkLabel.Name = "OperationLinkLabel";
            this.OperationLinkLabel.Size = new System.Drawing.Size(37, 13);
            this.OperationLinkLabel.TabIndex = 1;
            this.OperationLinkLabel.TabStop = true;
            this.OperationLinkLabel.Text = "before";
            this.OperationLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OperationLinkLabel_LinkClicked);
            // 
            // BeginDatePicker
            // 
            this.BeginDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.BeginDatePicker.Location = new System.Drawing.Point(141, 3);
            this.BeginDatePicker.Name = "BeginDatePicker";
            this.BeginDatePicker.Size = new System.Drawing.Size(90, 20);
            this.BeginDatePicker.TabIndex = 2;
            this.BeginDatePicker.ValueChanged += new System.EventHandler(this.BeginDatePicker_ValueChanged);
            // 
            // AndLabel
            // 
            this.AndLabel.AutoSize = true;
            this.AndLabel.Location = new System.Drawing.Point(234, 6);
            this.AndLabel.Margin = new System.Windows.Forms.Padding(0, 6, 0, 3);
            this.AndLabel.Name = "AndLabel";
            this.AndLabel.Size = new System.Drawing.Size(25, 13);
            this.AndLabel.TabIndex = 3;
            this.AndLabel.Text = "and";
            // 
            // EndDatePicker
            // 
            this.EndDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.EndDatePicker.Location = new System.Drawing.Point(262, 3);
            this.EndDatePicker.Name = "EndDatePicker";
            this.EndDatePicker.Size = new System.Drawing.Size(90, 20);
            this.EndDatePicker.TabIndex = 4;
            this.EndDatePicker.ValueChanged += new System.EventHandler(this.EndDatePicker_ValueChanged);
            // 
            // OperationContextMenu
            // 
            this.OperationContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EqualToMenuItem,
            this.BeforeMenuItem,
            this.AfterMenuItem,
            this.BetweenMenuItem});
            this.OperationContextMenu.Name = "OperationContextMenu";
            this.OperationContextMenu.Size = new System.Drawing.Size(120, 92);
            // 
            // EqualToMenuItem
            // 
            this.EqualToMenuItem.Name = "EqualToMenuItem";
            this.EqualToMenuItem.Size = new System.Drawing.Size(119, 22);
            this.EqualToMenuItem.Text = "equal to";
            this.EqualToMenuItem.Click += new System.EventHandler(this.OperationMenuItem_Click);
            // 
            // BeforeMenuItem
            // 
            this.BeforeMenuItem.Name = "BeforeMenuItem";
            this.BeforeMenuItem.Size = new System.Drawing.Size(119, 22);
            this.BeforeMenuItem.Text = "before";
            this.BeforeMenuItem.Click += new System.EventHandler(this.OperationMenuItem_Click);
            // 
            // AfterMenuItem
            // 
            this.AfterMenuItem.Name = "AfterMenuItem";
            this.AfterMenuItem.Size = new System.Drawing.Size(119, 22);
            this.AfterMenuItem.Text = "after";
            this.AfterMenuItem.Click += new System.EventHandler(this.OperationMenuItem_Click);
            // 
            // BetweenMenuItem
            // 
            this.BetweenMenuItem.Name = "BetweenMenuItem";
            this.BetweenMenuItem.Size = new System.Drawing.Size(119, 22);
            this.BetweenMenuItem.Text = "between";
            this.BetweenMenuItem.Click += new System.EventHandler(this.OperationMenuItem_Click);
            // 
            // DateSearchItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.MainFlowLayout);
            this.Name = "DateSearchItem";
            this.Size = new System.Drawing.Size(366, 26);
            this.Load += new System.EventHandler(this.DateSearchItem_Load);
            this.MainFlowLayout.ResumeLayout(false);
            this.MainFlowLayout.PerformLayout();
            this.OperationContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel MainFlowLayout;
        private System.Windows.Forms.Label ItemNameLabel;
        private System.Windows.Forms.LinkLabel OperationLinkLabel;
        private System.Windows.Forms.DateTimePicker BeginDatePicker;
        private System.Windows.Forms.Label AndLabel;
        private System.Windows.Forms.DateTimePicker EndDatePicker;
        private System.Windows.Forms.ContextMenuStrip OperationContextMenu;
        private System.Windows.Forms.ToolStripMenuItem EqualToMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BeforeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AfterMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BetweenMenuItem;
        private System.Windows.Forms.CheckBox SearchItemEnabled;
    }
}
