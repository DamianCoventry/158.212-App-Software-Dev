
namespace RugbyGui.Dialogs
{
    partial class SignedPlayerDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SignedPlayerDialog));
            this.PlayerPanel = new System.Windows.Forms.Panel();
            this.ChoosePlayerLinkLabel = new System.Windows.Forms.LinkLabel();
            this.PlayerWeightValue = new System.Windows.Forms.Label();
            this.PlayerHeightValue = new System.Windows.Forms.Label();
            this.PlayerAgeValue = new System.Windows.Forms.Label();
            this.PlayerWeightLabel = new System.Windows.Forms.Label();
            this.PlayerHeightLabel = new System.Windows.Forms.Label();
            this.PlayerAgeLabel = new System.Windows.Forms.Label();
            this.PlayerNameLabel = new System.Windows.Forms.Label();
            this.PlayerImage = new System.Windows.Forms.PictureBox();
            this.OkButton = new System.Windows.Forms.Button();
            this.MyCancelButton = new System.Windows.Forms.Button();
            this.ArrowLabel = new System.Windows.Forms.Label();
            this.TeamPanel = new System.Windows.Forms.Panel();
            this.ChooseTeamLinkLabel = new System.Windows.Forms.LinkLabel();
            this.NumPlayersValue = new System.Windows.Forms.Label();
            this.CoachValue = new System.Windows.Forms.Label();
            this.HomeGroundValue = new System.Windows.Forms.Label();
            this.NumPlayersLabel = new System.Windows.Forms.Label();
            this.Coach = new System.Windows.Forms.Label();
            this.HomeGroundLabel = new System.Windows.Forms.Label();
            this.TeamLabel = new System.Windows.Forms.Label();
            this.TeamImage = new System.Windows.Forms.PictureBox();
            this.PlayerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PlayerImage)).BeginInit();
            this.TeamPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TeamImage)).BeginInit();
            this.SuspendLayout();
            // 
            // PlayerPanel
            // 
            this.PlayerPanel.Controls.Add(this.ChoosePlayerLinkLabel);
            this.PlayerPanel.Controls.Add(this.PlayerWeightValue);
            this.PlayerPanel.Controls.Add(this.PlayerHeightValue);
            this.PlayerPanel.Controls.Add(this.PlayerAgeValue);
            this.PlayerPanel.Controls.Add(this.PlayerWeightLabel);
            this.PlayerPanel.Controls.Add(this.PlayerHeightLabel);
            this.PlayerPanel.Controls.Add(this.PlayerAgeLabel);
            this.PlayerPanel.Controls.Add(this.PlayerNameLabel);
            this.PlayerPanel.Controls.Add(this.PlayerImage);
            this.PlayerPanel.Location = new System.Drawing.Point(12, 12);
            this.PlayerPanel.Name = "PlayerPanel";
            this.PlayerPanel.Size = new System.Drawing.Size(271, 189);
            this.PlayerPanel.TabIndex = 0;
            // 
            // ChoosePlayerLinkLabel
            // 
            this.ChoosePlayerLinkLabel.Location = new System.Drawing.Point(3, 154);
            this.ChoosePlayerLinkLabel.Name = "ChoosePlayerLinkLabel";
            this.ChoosePlayerLinkLabel.Size = new System.Drawing.Size(265, 23);
            this.ChoosePlayerLinkLabel.TabIndex = 7;
            this.ChoosePlayerLinkLabel.TabStop = true;
            this.ChoosePlayerLinkLabel.Text = "Choose a different player..";
            this.ChoosePlayerLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ChoosePlayerLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ChoosePlayerLinkLabel_LinkClicked);
            // 
            // PlayerWeightValue
            // 
            this.PlayerWeightValue.AutoEllipsis = true;
            this.PlayerWeightValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerWeightValue.Location = new System.Drawing.Point(109, 106);
            this.PlayerWeightValue.Name = "PlayerWeightValue";
            this.PlayerWeightValue.Size = new System.Drawing.Size(159, 23);
            this.PlayerWeightValue.TabIndex = 6;
            this.PlayerWeightValue.Text = "85 kg";
            this.PlayerWeightValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PlayerHeightValue
            // 
            this.PlayerHeightValue.AutoEllipsis = true;
            this.PlayerHeightValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerHeightValue.Location = new System.Drawing.Point(109, 83);
            this.PlayerHeightValue.Name = "PlayerHeightValue";
            this.PlayerHeightValue.Size = new System.Drawing.Size(159, 23);
            this.PlayerHeightValue.TabIndex = 4;
            this.PlayerHeightValue.Text = "182 cm";
            this.PlayerHeightValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PlayerAgeValue
            // 
            this.PlayerAgeValue.AutoEllipsis = true;
            this.PlayerAgeValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerAgeValue.Location = new System.Drawing.Point(109, 60);
            this.PlayerAgeValue.Name = "PlayerAgeValue";
            this.PlayerAgeValue.Size = new System.Drawing.Size(159, 23);
            this.PlayerAgeValue.TabIndex = 2;
            this.PlayerAgeValue.Text = "34";
            this.PlayerAgeValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PlayerWeightLabel
            // 
            this.PlayerWeightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerWeightLabel.Location = new System.Drawing.Point(3, 106);
            this.PlayerWeightLabel.Name = "PlayerWeightLabel";
            this.PlayerWeightLabel.Size = new System.Drawing.Size(100, 23);
            this.PlayerWeightLabel.TabIndex = 5;
            this.PlayerWeightLabel.Text = "Weight:";
            this.PlayerWeightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PlayerHeightLabel
            // 
            this.PlayerHeightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerHeightLabel.Location = new System.Drawing.Point(3, 83);
            this.PlayerHeightLabel.Name = "PlayerHeightLabel";
            this.PlayerHeightLabel.Size = new System.Drawing.Size(100, 23);
            this.PlayerHeightLabel.TabIndex = 3;
            this.PlayerHeightLabel.Text = "Height:";
            this.PlayerHeightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PlayerAgeLabel
            // 
            this.PlayerAgeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerAgeLabel.Location = new System.Drawing.Point(3, 60);
            this.PlayerAgeLabel.Name = "PlayerAgeLabel";
            this.PlayerAgeLabel.Size = new System.Drawing.Size(100, 23);
            this.PlayerAgeLabel.TabIndex = 1;
            this.PlayerAgeLabel.Text = "Age:";
            this.PlayerAgeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PlayerNameLabel
            // 
            this.PlayerNameLabel.AutoEllipsis = true;
            this.PlayerNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerNameLabel.Location = new System.Drawing.Point(68, 0);
            this.PlayerNameLabel.Name = "PlayerNameLabel";
            this.PlayerNameLabel.Size = new System.Drawing.Size(200, 60);
            this.PlayerNameLabel.TabIndex = 0;
            this.PlayerNameLabel.Text = "Whetukamokamo Douglas";
            this.PlayerNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PlayerImage
            // 
            this.PlayerImage.Image = global::RugbyGui.Properties.Resources.Player32x32;
            this.PlayerImage.Location = new System.Drawing.Point(30, 15);
            this.PlayerImage.Name = "PlayerImage";
            this.PlayerImage.Size = new System.Drawing.Size(32, 32);
            this.PlayerImage.TabIndex = 1;
            this.PlayerImage.TabStop = false;
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(204, 207);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(178, 23);
            this.OkButton.TabIndex = 3;
            this.OkButton.Text = "&Sign Player to Team";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // MyCancelButton
            // 
            this.MyCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.MyCancelButton.Location = new System.Drawing.Point(388, 207);
            this.MyCancelButton.Name = "MyCancelButton";
            this.MyCancelButton.Size = new System.Drawing.Size(75, 23);
            this.MyCancelButton.TabIndex = 4;
            this.MyCancelButton.Text = "&Cancel";
            this.MyCancelButton.UseVisualStyleBackColor = true;
            // 
            // ArrowLabel
            // 
            this.ArrowLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ArrowLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.ArrowLabel.Location = new System.Drawing.Point(289, 12);
            this.ArrowLabel.Name = "ArrowLabel";
            this.ArrowLabel.Size = new System.Drawing.Size(106, 189);
            this.ArrowLabel.TabIndex = 1;
            this.ArrowLabel.Text = "➟";
            this.ArrowLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TeamPanel
            // 
            this.TeamPanel.Controls.Add(this.ChooseTeamLinkLabel);
            this.TeamPanel.Controls.Add(this.NumPlayersValue);
            this.TeamPanel.Controls.Add(this.CoachValue);
            this.TeamPanel.Controls.Add(this.HomeGroundValue);
            this.TeamPanel.Controls.Add(this.NumPlayersLabel);
            this.TeamPanel.Controls.Add(this.Coach);
            this.TeamPanel.Controls.Add(this.HomeGroundLabel);
            this.TeamPanel.Controls.Add(this.TeamLabel);
            this.TeamPanel.Controls.Add(this.TeamImage);
            this.TeamPanel.Location = new System.Drawing.Point(401, 12);
            this.TeamPanel.Name = "TeamPanel";
            this.TeamPanel.Size = new System.Drawing.Size(271, 189);
            this.TeamPanel.TabIndex = 2;
            // 
            // ChooseTeamLinkLabel
            // 
            this.ChooseTeamLinkLabel.Location = new System.Drawing.Point(3, 154);
            this.ChooseTeamLinkLabel.Name = "ChooseTeamLinkLabel";
            this.ChooseTeamLinkLabel.Size = new System.Drawing.Size(265, 23);
            this.ChooseTeamLinkLabel.TabIndex = 7;
            this.ChooseTeamLinkLabel.TabStop = true;
            this.ChooseTeamLinkLabel.Text = "Choose a different team...";
            this.ChooseTeamLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ChooseTeamLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ChooseTeamLinkLabel_LinkClicked);
            // 
            // NumPlayersValue
            // 
            this.NumPlayersValue.AutoEllipsis = true;
            this.NumPlayersValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumPlayersValue.Location = new System.Drawing.Point(109, 106);
            this.NumPlayersValue.Name = "NumPlayersValue";
            this.NumPlayersValue.Size = new System.Drawing.Size(159, 23);
            this.NumPlayersValue.TabIndex = 6;
            this.NumPlayersValue.Text = "13";
            this.NumPlayersValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CoachValue
            // 
            this.CoachValue.AutoEllipsis = true;
            this.CoachValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CoachValue.Location = new System.Drawing.Point(109, 83);
            this.CoachValue.Name = "CoachValue";
            this.CoachValue.Size = new System.Drawing.Size(159, 23);
            this.CoachValue.TabIndex = 4;
            this.CoachValue.Text = "Tony Brown";
            this.CoachValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // HomeGroundValue
            // 
            this.HomeGroundValue.AutoEllipsis = true;
            this.HomeGroundValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HomeGroundValue.Location = new System.Drawing.Point(109, 60);
            this.HomeGroundValue.Name = "HomeGroundValue";
            this.HomeGroundValue.Size = new System.Drawing.Size(159, 23);
            this.HomeGroundValue.TabIndex = 2;
            this.HomeGroundValue.Text = "Forsyth Barr Stadium";
            this.HomeGroundValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NumPlayersLabel
            // 
            this.NumPlayersLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumPlayersLabel.Location = new System.Drawing.Point(3, 106);
            this.NumPlayersLabel.Name = "NumPlayersLabel";
            this.NumPlayersLabel.Size = new System.Drawing.Size(100, 23);
            this.NumPlayersLabel.TabIndex = 5;
            this.NumPlayersLabel.Text = "Num Players:";
            this.NumPlayersLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Coach
            // 
            this.Coach.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Coach.Location = new System.Drawing.Point(3, 83);
            this.Coach.Name = "Coach";
            this.Coach.Size = new System.Drawing.Size(100, 23);
            this.Coach.TabIndex = 3;
            this.Coach.Text = "Coach:";
            this.Coach.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // HomeGroundLabel
            // 
            this.HomeGroundLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HomeGroundLabel.Location = new System.Drawing.Point(3, 60);
            this.HomeGroundLabel.Name = "HomeGroundLabel";
            this.HomeGroundLabel.Size = new System.Drawing.Size(100, 23);
            this.HomeGroundLabel.TabIndex = 1;
            this.HomeGroundLabel.Text = "Home Ground:";
            this.HomeGroundLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TeamLabel
            // 
            this.TeamLabel.AutoEllipsis = true;
            this.TeamLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TeamLabel.Location = new System.Drawing.Point(68, 0);
            this.TeamLabel.Name = "TeamLabel";
            this.TeamLabel.Size = new System.Drawing.Size(200, 60);
            this.TeamLabel.TabIndex = 0;
            this.TeamLabel.Text = "Highlanders";
            this.TeamLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TeamImage
            // 
            this.TeamImage.Image = global::RugbyGui.Properties.Resources.Team32x32;
            this.TeamImage.Location = new System.Drawing.Point(30, 15);
            this.TeamImage.Name = "TeamImage";
            this.TeamImage.Size = new System.Drawing.Size(32, 32);
            this.TeamImage.TabIndex = 1;
            this.TeamImage.TabStop = false;
            // 
            // SignedPlayerDialog
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.MyCancelButton;
            this.ClientSize = new System.Drawing.Size(684, 240);
            this.Controls.Add(this.TeamPanel);
            this.Controls.Add(this.ArrowLabel);
            this.Controls.Add(this.MyCancelButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.PlayerPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SignedPlayerDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sign Player to Team";
            this.Load += new System.EventHandler(this.SignedPlayerDialog_Load);
            this.PlayerPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PlayerImage)).EndInit();
            this.TeamPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TeamImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PlayerPanel;
        private System.Windows.Forms.LinkLabel ChoosePlayerLinkLabel;
        private System.Windows.Forms.Label PlayerWeightValue;
        private System.Windows.Forms.Label PlayerHeightValue;
        private System.Windows.Forms.Label PlayerAgeValue;
        private System.Windows.Forms.Label PlayerWeightLabel;
        private System.Windows.Forms.Label PlayerHeightLabel;
        private System.Windows.Forms.Label PlayerAgeLabel;
        private System.Windows.Forms.Label PlayerNameLabel;
        private System.Windows.Forms.PictureBox PlayerImage;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button MyCancelButton;
        private System.Windows.Forms.Label ArrowLabel;
        private System.Windows.Forms.Panel TeamPanel;
        private System.Windows.Forms.LinkLabel ChooseTeamLinkLabel;
        private System.Windows.Forms.Label NumPlayersValue;
        private System.Windows.Forms.Label CoachValue;
        private System.Windows.Forms.Label HomeGroundValue;
        private System.Windows.Forms.Label NumPlayersLabel;
        private System.Windows.Forms.Label Coach;
        private System.Windows.Forms.Label HomeGroundLabel;
        private System.Windows.Forms.Label TeamLabel;
        private System.Windows.Forms.PictureBox TeamImage;
    }
}