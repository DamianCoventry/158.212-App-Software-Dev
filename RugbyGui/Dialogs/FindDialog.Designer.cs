
namespace RugbyGui.Dialogs
{
    partial class FindDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindDialog));
            this.FindWhatLabel = new System.Windows.Forms.Label();
            this.FindWhatTextBox = new System.Windows.Forms.TextBox();
            this.MatchCaseCheckBox = new System.Windows.Forms.CheckBox();
            this.MatchWholeWordCheckBox = new System.Windows.Forms.CheckBox();
            this.UseRegularExpressionCheckBox = new System.Windows.Forms.CheckBox();
            this.FindTeamsCheckBox = new System.Windows.Forms.CheckBox();
            this.FindPlayersCheckBox = new System.Windows.Forms.CheckBox();
            this.FindSignedPlayersCheckBox = new System.Windows.Forms.CheckBox();
            this.OkButton = new System.Windows.Forms.Button();
            this.MyCancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FindWhatLabel
            // 
            this.FindWhatLabel.AutoSize = true;
            this.FindWhatLabel.Location = new System.Drawing.Point(10, 9);
            this.FindWhatLabel.Name = "FindWhatLabel";
            this.FindWhatLabel.Size = new System.Drawing.Size(59, 13);
            this.FindWhatLabel.TabIndex = 0;
            this.FindWhatLabel.Text = "&Find What:";
            // 
            // FindWhatTextBox
            // 
            this.FindWhatTextBox.Location = new System.Drawing.Point(13, 25);
            this.FindWhatTextBox.Name = "FindWhatTextBox";
            this.FindWhatTextBox.Size = new System.Drawing.Size(369, 20);
            this.FindWhatTextBox.TabIndex = 1;
            // 
            // MatchCaseCheckBox
            // 
            this.MatchCaseCheckBox.AutoSize = true;
            this.MatchCaseCheckBox.Location = new System.Drawing.Point(13, 51);
            this.MatchCaseCheckBox.Name = "MatchCaseCheckBox";
            this.MatchCaseCheckBox.Size = new System.Drawing.Size(82, 17);
            this.MatchCaseCheckBox.TabIndex = 2;
            this.MatchCaseCheckBox.Text = "Match c&ase";
            this.MatchCaseCheckBox.UseVisualStyleBackColor = true;
            // 
            // MatchWholeWordCheckBox
            // 
            this.MatchWholeWordCheckBox.AutoSize = true;
            this.MatchWholeWordCheckBox.Location = new System.Drawing.Point(13, 74);
            this.MatchWholeWordCheckBox.Name = "MatchWholeWordCheckBox";
            this.MatchWholeWordCheckBox.Size = new System.Drawing.Size(113, 17);
            this.MatchWholeWordCheckBox.TabIndex = 3;
            this.MatchWholeWordCheckBox.Text = "Match &whole word";
            this.MatchWholeWordCheckBox.UseVisualStyleBackColor = true;
            // 
            // UseRegularExpressionCheckBox
            // 
            this.UseRegularExpressionCheckBox.AutoSize = true;
            this.UseRegularExpressionCheckBox.Location = new System.Drawing.Point(13, 97);
            this.UseRegularExpressionCheckBox.Name = "UseRegularExpressionCheckBox";
            this.UseRegularExpressionCheckBox.Size = new System.Drawing.Size(139, 17);
            this.UseRegularExpressionCheckBox.TabIndex = 4;
            this.UseRegularExpressionCheckBox.Text = "Use &Regular Expression";
            this.UseRegularExpressionCheckBox.UseVisualStyleBackColor = true;
            // 
            // FindTeamsCheckBox
            // 
            this.FindTeamsCheckBox.AutoSize = true;
            this.FindTeamsCheckBox.Checked = true;
            this.FindTeamsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.FindTeamsCheckBox.Location = new System.Drawing.Point(206, 51);
            this.FindTeamsCheckBox.Name = "FindTeamsCheckBox";
            this.FindTeamsCheckBox.Size = new System.Drawing.Size(77, 17);
            this.FindTeamsCheckBox.TabIndex = 5;
            this.FindTeamsCheckBox.Text = "Find &teams";
            this.FindTeamsCheckBox.UseVisualStyleBackColor = true;
            // 
            // FindPlayersCheckBox
            // 
            this.FindPlayersCheckBox.AutoSize = true;
            this.FindPlayersCheckBox.Checked = true;
            this.FindPlayersCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.FindPlayersCheckBox.Location = new System.Drawing.Point(206, 74);
            this.FindPlayersCheckBox.Name = "FindPlayersCheckBox";
            this.FindPlayersCheckBox.Size = new System.Drawing.Size(82, 17);
            this.FindPlayersCheckBox.TabIndex = 6;
            this.FindPlayersCheckBox.Text = "Find &players";
            this.FindPlayersCheckBox.UseVisualStyleBackColor = true;
            // 
            // FindSignedPlayersCheckBox
            // 
            this.FindSignedPlayersCheckBox.AutoSize = true;
            this.FindSignedPlayersCheckBox.Checked = true;
            this.FindSignedPlayersCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.FindSignedPlayersCheckBox.Location = new System.Drawing.Point(206, 97);
            this.FindSignedPlayersCheckBox.Name = "FindSignedPlayersCheckBox";
            this.FindSignedPlayersCheckBox.Size = new System.Drawing.Size(116, 17);
            this.FindSignedPlayersCheckBox.TabIndex = 7;
            this.FindSignedPlayersCheckBox.Text = "Find &signed players";
            this.FindSignedPlayersCheckBox.UseVisualStyleBackColor = true;
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(226, 120);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 8;
            this.OkButton.Text = "&OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // MyCancelButton
            // 
            this.MyCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.MyCancelButton.Location = new System.Drawing.Point(307, 120);
            this.MyCancelButton.Name = "MyCancelButton";
            this.MyCancelButton.Size = new System.Drawing.Size(75, 23);
            this.MyCancelButton.TabIndex = 9;
            this.MyCancelButton.Text = "&Cancel";
            this.MyCancelButton.UseVisualStyleBackColor = true;
            // 
            // FindDialog
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.MyCancelButton;
            this.ClientSize = new System.Drawing.Size(395, 158);
            this.Controls.Add(this.MyCancelButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.FindSignedPlayersCheckBox);
            this.Controls.Add(this.FindPlayersCheckBox);
            this.Controls.Add(this.FindTeamsCheckBox);
            this.Controls.Add(this.UseRegularExpressionCheckBox);
            this.Controls.Add(this.MatchWholeWordCheckBox);
            this.Controls.Add(this.MatchCaseCheckBox);
            this.Controls.Add(this.FindWhatTextBox);
            this.Controls.Add(this.FindWhatLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FindDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Find";
            this.Load += new System.EventHandler(this.FindDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label FindWhatLabel;
        private System.Windows.Forms.TextBox FindWhatTextBox;
        private System.Windows.Forms.CheckBox MatchCaseCheckBox;
        private System.Windows.Forms.CheckBox MatchWholeWordCheckBox;
        private System.Windows.Forms.CheckBox UseRegularExpressionCheckBox;
        private System.Windows.Forms.CheckBox FindTeamsCheckBox;
        private System.Windows.Forms.CheckBox FindPlayersCheckBox;
        private System.Windows.Forms.CheckBox FindSignedPlayersCheckBox;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button MyCancelButton;
    }
}