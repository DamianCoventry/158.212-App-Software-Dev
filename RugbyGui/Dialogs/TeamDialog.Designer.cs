
namespace RugbyGui.Dialogs
{
    partial class TeamDialog
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TeamDialog));
            this.TeamImage = new System.Windows.Forms.PictureBox();
            this.TeamLabel = new System.Windows.Forms.Label();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.NameLabel = new System.Windows.Forms.Label();
            this.HomeGroundTextBox = new System.Windows.Forms.TextBox();
            this.HomeGroundLabel = new System.Windows.Forms.Label();
            this.RegionTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CoachTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.PlayersLabel = new System.Windows.Forms.Label();
            this.PlayersImage = new System.Windows.Forms.PictureBox();
            this.YearFoundedNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.PlayersFlowLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.PlayerCountLinkLabel = new System.Windows.Forms.LinkLabel();
            this.ChooseLinkLabel = new System.Windows.Forms.LinkLabel();
            this.OkButton = new System.Windows.Forms.Button();
            this.MyCancelButton = new System.Windows.Forms.Button();
            this.TeamContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.TeamImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlayersImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YearFoundedNumericUpDown)).BeginInit();
            this.PlayersFlowLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // TeamImage
            // 
            this.TeamImage.Image = global::RugbyGui.Properties.Resources.Team32x32;
            this.TeamImage.Location = new System.Drawing.Point(12, 12);
            this.TeamImage.Name = "TeamImage";
            this.TeamImage.Size = new System.Drawing.Size(32, 32);
            this.TeamImage.TabIndex = 0;
            this.TeamImage.TabStop = false;
            // 
            // TeamLabel
            // 
            this.TeamLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TeamLabel.Location = new System.Drawing.Point(50, 12);
            this.TeamLabel.Name = "TeamLabel";
            this.TeamLabel.Size = new System.Drawing.Size(231, 32);
            this.TeamLabel.TabIndex = 0;
            this.TeamLabel.Text = "Team";
            this.TeamLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NameTextBox
            // 
            this.NameTextBox.Location = new System.Drawing.Point(53, 71);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(228, 20);
            this.NameTextBox.TabIndex = 2;
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(50, 55);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(38, 13);
            this.NameLabel.TabIndex = 1;
            this.NameLabel.Text = "&Name:";
            // 
            // HomeGroundTextBox
            // 
            this.HomeGroundTextBox.Location = new System.Drawing.Point(53, 116);
            this.HomeGroundTextBox.Name = "HomeGroundTextBox";
            this.HomeGroundTextBox.Size = new System.Drawing.Size(228, 20);
            this.HomeGroundTextBox.TabIndex = 4;
            // 
            // HomeGroundLabel
            // 
            this.HomeGroundLabel.AutoSize = true;
            this.HomeGroundLabel.Location = new System.Drawing.Point(50, 100);
            this.HomeGroundLabel.Name = "HomeGroundLabel";
            this.HomeGroundLabel.Size = new System.Drawing.Size(76, 13);
            this.HomeGroundLabel.TabIndex = 3;
            this.HomeGroundLabel.Text = "&Home Ground:";
            // 
            // RegionTextBox
            // 
            this.RegionTextBox.Location = new System.Drawing.Point(53, 206);
            this.RegionTextBox.Name = "RegionTextBox";
            this.RegionTextBox.Size = new System.Drawing.Size(228, 20);
            this.RegionTextBox.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "&Region:";
            // 
            // CoachTextBox
            // 
            this.CoachTextBox.Location = new System.Drawing.Point(53, 161);
            this.CoachTextBox.Name = "CoachTextBox";
            this.CoachTextBox.Size = new System.Drawing.Size(228, 20);
            this.CoachTextBox.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Co&ach:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 235);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "&Year Founded:";
            // 
            // PlayersLabel
            // 
            this.PlayersLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayersLabel.Location = new System.Drawing.Point(50, 287);
            this.PlayersLabel.Name = "PlayersLabel";
            this.PlayersLabel.Size = new System.Drawing.Size(231, 32);
            this.PlayersLabel.TabIndex = 11;
            this.PlayersLabel.Text = "Players";
            this.PlayersLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PlayersImage
            // 
            this.PlayersImage.Image = global::RugbyGui.Properties.Resources.Player32x32;
            this.PlayersImage.Location = new System.Drawing.Point(12, 287);
            this.PlayersImage.Name = "PlayersImage";
            this.PlayersImage.Size = new System.Drawing.Size(32, 32);
            this.PlayersImage.TabIndex = 14;
            this.PlayersImage.TabStop = false;
            // 
            // YearFoundedNumericUpDown
            // 
            this.YearFoundedNumericUpDown.Location = new System.Drawing.Point(53, 251);
            this.YearFoundedNumericUpDown.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.YearFoundedNumericUpDown.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.YearFoundedNumericUpDown.Name = "YearFoundedNumericUpDown";
            this.YearFoundedNumericUpDown.Size = new System.Drawing.Size(228, 20);
            this.YearFoundedNumericUpDown.TabIndex = 10;
            this.YearFoundedNumericUpDown.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // PlayersFlowLayout
            // 
            this.PlayersFlowLayout.AutoSize = true;
            this.PlayersFlowLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PlayersFlowLayout.Controls.Add(this.PlayerCountLinkLabel);
            this.PlayersFlowLayout.Controls.Add(this.ChooseLinkLabel);
            this.PlayersFlowLayout.Location = new System.Drawing.Point(53, 322);
            this.PlayersFlowLayout.Name = "PlayersFlowLayout";
            this.PlayersFlowLayout.Size = new System.Drawing.Size(135, 16);
            this.PlayersFlowLayout.TabIndex = 17;
            // 
            // PlayerCountLinkLabel
            // 
            this.PlayerCountLinkLabel.AutoSize = true;
            this.PlayerCountLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerCountLinkLabel.Location = new System.Drawing.Point(3, 0);
            this.PlayerCountLinkLabel.Name = "PlayerCountLinkLabel";
            this.PlayerCountLinkLabel.Size = new System.Drawing.Size(71, 16);
            this.PlayerCountLinkLabel.TabIndex = 0;
            this.PlayerCountLinkLabel.TabStop = true;
            this.PlayerCountLinkLabel.Text = "13 Players";
            this.PlayerCountLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.PlayerCountLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.PlayerCountLinkLabel_LinkClicked);
            // 
            // ChooseLinkLabel
            // 
            this.ChooseLinkLabel.AutoSize = true;
            this.ChooseLinkLabel.Location = new System.Drawing.Point(80, 0);
            this.ChooseLinkLabel.Name = "ChooseLinkLabel";
            this.ChooseLinkLabel.Size = new System.Drawing.Size(52, 13);
            this.ChooseLinkLabel.TabIndex = 1;
            this.ChooseLinkLabel.TabStop = true;
            this.ChooseLinkLabel.Text = "Choose...";
            this.ChooseLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ChooseLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ChooseLinkLabel_LinkClicked);
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(168, 358);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 12;
            this.OkButton.Text = "&OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // MyCancelButton
            // 
            this.MyCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.MyCancelButton.Location = new System.Drawing.Point(249, 358);
            this.MyCancelButton.Name = "MyCancelButton";
            this.MyCancelButton.Size = new System.Drawing.Size(75, 23);
            this.MyCancelButton.TabIndex = 13;
            this.MyCancelButton.Text = "&Cancel";
            this.MyCancelButton.UseVisualStyleBackColor = true;
            // 
            // TeamContextMenuStrip
            // 
            this.TeamContextMenuStrip.Name = "TeamContextMenuStrip";
            this.TeamContextMenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // TeamDialog
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.MyCancelButton;
            this.ClientSize = new System.Drawing.Size(336, 393);
            this.Controls.Add(this.MyCancelButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.PlayersFlowLayout);
            this.Controls.Add(this.YearFoundedNumericUpDown);
            this.Controls.Add(this.PlayersLabel);
            this.Controls.Add(this.PlayersImage);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.RegionTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CoachTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.HomeGroundTextBox);
            this.Controls.Add(this.HomeGroundLabel);
            this.Controls.Add(this.NameTextBox);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.TeamLabel);
            this.Controls.Add(this.TeamImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TeamDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Team";
            this.Load += new System.EventHandler(this.TeamDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TeamImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlayersImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YearFoundedNumericUpDown)).EndInit();
            this.PlayersFlowLayout.ResumeLayout(false);
            this.PlayersFlowLayout.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox TeamImage;
        private System.Windows.Forms.Label TeamLabel;
        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.TextBox HomeGroundTextBox;
        private System.Windows.Forms.Label HomeGroundLabel;
        private System.Windows.Forms.TextBox RegionTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox CoachTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label PlayersLabel;
        private System.Windows.Forms.PictureBox PlayersImage;
        private System.Windows.Forms.NumericUpDown YearFoundedNumericUpDown;
        private System.Windows.Forms.FlowLayoutPanel PlayersFlowLayout;
        private System.Windows.Forms.LinkLabel PlayerCountLinkLabel;
        private System.Windows.Forms.LinkLabel ChooseLinkLabel;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button MyCancelButton;
        private System.Windows.Forms.ContextMenuStrip TeamContextMenuStrip;
    }
}