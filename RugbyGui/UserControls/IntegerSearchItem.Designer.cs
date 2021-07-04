
namespace RugbyGui.UserControls
{
    partial class IntegerSearchItem
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
            this.BeginUpDown = new System.Windows.Forms.NumericUpDown();
            this.AndLabel = new System.Windows.Forms.Label();
            this.EndUpDown = new System.Windows.Forms.NumericUpDown();
            this.OperationContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.EqualToMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FewerThanMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GreaterThanMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BetweenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainFlowLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BeginUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EndUpDown)).BeginInit();
            this.OperationContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainFlowLayout
            // 
            this.MainFlowLayout.AutoSize = true;
            this.MainFlowLayout.Controls.Add(this.SearchItemEnabled);
            this.MainFlowLayout.Controls.Add(this.ItemNameLabel);
            this.MainFlowLayout.Controls.Add(this.OperationLinkLabel);
            this.MainFlowLayout.Controls.Add(this.BeginUpDown);
            this.MainFlowLayout.Controls.Add(this.AndLabel);
            this.MainFlowLayout.Controls.Add(this.EndUpDown);
            this.MainFlowLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainFlowLayout.Location = new System.Drawing.Point(0, 0);
            this.MainFlowLayout.Name = "MainFlowLayout";
            this.MainFlowLayout.Size = new System.Drawing.Size(339, 26);
            this.MainFlowLayout.TabIndex = 0;
            // 
            // SearchItemEnabled
            // 
            this.SearchItemEnabled.AutoSize = true;
            this.SearchItemEnabled.Location = new System.Drawing.Point(3, 6);
            this.SearchItemEnabled.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.SearchItemEnabled.Name = "SearchItemEnabled";
            this.SearchItemEnabled.Size = new System.Drawing.Size(15, 14);
            this.SearchItemEnabled.TabIndex = 8;
            this.SearchItemEnabled.UseVisualStyleBackColor = true;
            this.SearchItemEnabled.CheckedChanged += new System.EventHandler(this.SearchItemEnabled_CheckedChanged);
            // 
            // ItemNameLabel
            // 
            this.ItemNameLabel.AutoSize = true;
            this.ItemNameLabel.Location = new System.Drawing.Point(21, 6);
            this.ItemNameLabel.Margin = new System.Windows.Forms.Padding(0, 6, 0, 3);
            this.ItemNameLabel.Name = "ItemNameLabel";
            this.ItemNameLabel.Size = new System.Drawing.Size(74, 13);
            this.ItemNameLabel.TabIndex = 3;
            this.ItemNameLabel.Text = "Year Founded";
            // 
            // OperationLinkLabel
            // 
            this.OperationLinkLabel.AutoSize = true;
            this.OperationLinkLabel.Location = new System.Drawing.Point(95, 6);
            this.OperationLinkLabel.Margin = new System.Windows.Forms.Padding(0, 6, 0, 3);
            this.OperationLinkLabel.Name = "OperationLinkLabel";
            this.OperationLinkLabel.Size = new System.Drawing.Size(57, 13);
            this.OperationLinkLabel.TabIndex = 4;
            this.OperationLinkLabel.TabStop = true;
            this.OperationLinkLabel.Text = "fewer than";
            this.OperationLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OperationLinkLabel_LinkClicked);
            // 
            // BeginUpDown
            // 
            this.BeginUpDown.Location = new System.Drawing.Point(155, 3);
            this.BeginUpDown.Name = "BeginUpDown";
            this.BeginUpDown.Size = new System.Drawing.Size(75, 20);
            this.BeginUpDown.TabIndex = 5;
            this.BeginUpDown.ValueChanged += new System.EventHandler(this.BeginUpDown_ValueChanged);
            // 
            // AndLabel
            // 
            this.AndLabel.AutoSize = true;
            this.AndLabel.Location = new System.Drawing.Point(233, 6);
            this.AndLabel.Margin = new System.Windows.Forms.Padding(0, 6, 0, 3);
            this.AndLabel.Name = "AndLabel";
            this.AndLabel.Size = new System.Drawing.Size(25, 13);
            this.AndLabel.TabIndex = 6;
            this.AndLabel.Text = "and";
            // 
            // EndUpDown
            // 
            this.EndUpDown.Location = new System.Drawing.Point(261, 3);
            this.EndUpDown.Name = "EndUpDown";
            this.EndUpDown.Size = new System.Drawing.Size(75, 20);
            this.EndUpDown.TabIndex = 7;
            this.EndUpDown.ValueChanged += new System.EventHandler(this.EndUpDown_ValueChanged);
            // 
            // OperationContextMenu
            // 
            this.OperationContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EqualToMenuItem,
            this.FewerThanMenuItem,
            this.GreaterThanMenuItem,
            this.BetweenMenuItem});
            this.OperationContextMenu.Name = "OperationContextMenu";
            this.OperationContextMenu.Size = new System.Drawing.Size(139, 92);
            // 
            // EqualToMenuItem
            // 
            this.EqualToMenuItem.Name = "EqualToMenuItem";
            this.EqualToMenuItem.Size = new System.Drawing.Size(138, 22);
            this.EqualToMenuItem.Text = "equal to";
            this.EqualToMenuItem.Click += new System.EventHandler(this.OperationMenuItem_Click);
            // 
            // FewerThanMenuItem
            // 
            this.FewerThanMenuItem.Name = "FewerThanMenuItem";
            this.FewerThanMenuItem.Size = new System.Drawing.Size(138, 22);
            this.FewerThanMenuItem.Text = "fewer than";
            this.FewerThanMenuItem.Click += new System.EventHandler(this.OperationMenuItem_Click);
            // 
            // GreaterThanMenuItem
            // 
            this.GreaterThanMenuItem.Name = "GreaterThanMenuItem";
            this.GreaterThanMenuItem.Size = new System.Drawing.Size(138, 22);
            this.GreaterThanMenuItem.Text = "greater than";
            this.GreaterThanMenuItem.Click += new System.EventHandler(this.OperationMenuItem_Click);
            // 
            // BetweenMenuItem
            // 
            this.BetweenMenuItem.Name = "BetweenMenuItem";
            this.BetweenMenuItem.Size = new System.Drawing.Size(138, 22);
            this.BetweenMenuItem.Text = "between";
            this.BetweenMenuItem.Click += new System.EventHandler(this.OperationMenuItem_Click);
            // 
            // IntegerSearchItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.MainFlowLayout);
            this.Name = "IntegerSearchItem";
            this.Size = new System.Drawing.Size(339, 26);
            this.Load += new System.EventHandler(this.IntegerSearchItem_Load);
            this.MainFlowLayout.ResumeLayout(false);
            this.MainFlowLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BeginUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EndUpDown)).EndInit();
            this.OperationContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel MainFlowLayout;
        private System.Windows.Forms.Label ItemNameLabel;
        private System.Windows.Forms.LinkLabel OperationLinkLabel;
        private System.Windows.Forms.NumericUpDown BeginUpDown;
        private System.Windows.Forms.ContextMenuStrip OperationContextMenu;
        private System.Windows.Forms.Label AndLabel;
        private System.Windows.Forms.NumericUpDown EndUpDown;
        private System.Windows.Forms.ToolStripMenuItem EqualToMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FewerThanMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GreaterThanMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BetweenMenuItem;
        private System.Windows.Forms.CheckBox SearchItemEnabled;
    }
}
