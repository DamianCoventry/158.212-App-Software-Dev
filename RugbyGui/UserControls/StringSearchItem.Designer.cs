
namespace RugbyGui.UserControls
{
    partial class StringSearchItem
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
            this.StringTextBox = new System.Windows.Forms.TextBox();
            this.OperationContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.IsEqualToMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContainsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StartsWithMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EndsWithMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.MainFlowLayout.Controls.Add(this.StringTextBox);
            this.MainFlowLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainFlowLayout.Location = new System.Drawing.Point(0, 0);
            this.MainFlowLayout.Name = "MainFlowLayout";
            this.MainFlowLayout.Size = new System.Drawing.Size(305, 26);
            this.MainFlowLayout.TabIndex = 0;
            // 
            // SearchItemEnabled
            // 
            this.SearchItemEnabled.AutoSize = true;
            this.SearchItemEnabled.Location = new System.Drawing.Point(3, 6);
            this.SearchItemEnabled.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.SearchItemEnabled.Name = "SearchItemEnabled";
            this.SearchItemEnabled.Size = new System.Drawing.Size(15, 14);
            this.SearchItemEnabled.TabIndex = 6;
            this.SearchItemEnabled.UseVisualStyleBackColor = true;
            this.SearchItemEnabled.CheckedChanged += new System.EventHandler(this.SearchItemEnabled_CheckedChanged);
            // 
            // ItemNameLabel
            // 
            this.ItemNameLabel.AutoSize = true;
            this.ItemNameLabel.Location = new System.Drawing.Point(21, 6);
            this.ItemNameLabel.Margin = new System.Windows.Forms.Padding(0, 6, 0, 3);
            this.ItemNameLabel.Name = "ItemNameLabel";
            this.ItemNameLabel.Size = new System.Drawing.Size(63, 13);
            this.ItemNameLabel.TabIndex = 3;
            this.ItemNameLabel.Text = "Team name";
            // 
            // OperationLinkLabel
            // 
            this.OperationLinkLabel.AutoSize = true;
            this.OperationLinkLabel.Location = new System.Drawing.Point(84, 6);
            this.OperationLinkLabel.Margin = new System.Windows.Forms.Padding(0, 6, 0, 3);
            this.OperationLinkLabel.Name = "OperationLinkLabel";
            this.OperationLinkLabel.Size = new System.Drawing.Size(55, 13);
            this.OperationLinkLabel.TabIndex = 4;
            this.OperationLinkLabel.TabStop = true;
            this.OperationLinkLabel.Text = "is equal to";
            this.OperationLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OperationLinkLabel_LinkClicked);
            // 
            // StringTextBox
            // 
            this.StringTextBox.Location = new System.Drawing.Point(142, 3);
            this.StringTextBox.Name = "StringTextBox";
            this.StringTextBox.Size = new System.Drawing.Size(160, 20);
            this.StringTextBox.TabIndex = 5;
            this.StringTextBox.TextChanged += new System.EventHandler(this.StringTextBox_TextChanged);
            // 
            // OperationContextMenu
            // 
            this.OperationContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.IsEqualToMenuItem,
            this.ContainsMenuItem,
            this.StartsWithMenuItem,
            this.EndsWithMenuItem});
            this.OperationContextMenu.Name = "OperationContextMenu";
            this.OperationContextMenu.Size = new System.Drawing.Size(129, 92);
            // 
            // IsEqualToMenuItem
            // 
            this.IsEqualToMenuItem.Name = "IsEqualToMenuItem";
            this.IsEqualToMenuItem.Size = new System.Drawing.Size(128, 22);
            this.IsEqualToMenuItem.Text = "is equal to";
            this.IsEqualToMenuItem.Click += new System.EventHandler(this.OperationMenuItem_Click);
            // 
            // ContainsMenuItem
            // 
            this.ContainsMenuItem.Name = "ContainsMenuItem";
            this.ContainsMenuItem.Size = new System.Drawing.Size(128, 22);
            this.ContainsMenuItem.Text = "contains";
            this.ContainsMenuItem.Click += new System.EventHandler(this.OperationMenuItem_Click);
            // 
            // StartsWithMenuItem
            // 
            this.StartsWithMenuItem.Name = "StartsWithMenuItem";
            this.StartsWithMenuItem.Size = new System.Drawing.Size(128, 22);
            this.StartsWithMenuItem.Text = "starts with";
            this.StartsWithMenuItem.Click += new System.EventHandler(this.OperationMenuItem_Click);
            // 
            // EndsWithMenuItem
            // 
            this.EndsWithMenuItem.Name = "EndsWithMenuItem";
            this.EndsWithMenuItem.Size = new System.Drawing.Size(128, 22);
            this.EndsWithMenuItem.Text = "ends with";
            this.EndsWithMenuItem.Click += new System.EventHandler(this.OperationMenuItem_Click);
            // 
            // StringSearchItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.MainFlowLayout);
            this.Name = "StringSearchItem";
            this.Size = new System.Drawing.Size(305, 26);
            this.Load += new System.EventHandler(this.StringSearchItem_Load);
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
        private System.Windows.Forms.TextBox StringTextBox;
        private System.Windows.Forms.ContextMenuStrip OperationContextMenu;
        private System.Windows.Forms.ToolStripMenuItem IsEqualToMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ContainsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StartsWithMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EndsWithMenuItem;
        private System.Windows.Forms.CheckBox SearchItemEnabled;
    }
}
