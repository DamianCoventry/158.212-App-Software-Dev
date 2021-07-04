
namespace RugbyGui.Dialogs
{
    partial class AdvancedFindAndReplaceDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvancedFindAndReplaceDialog));
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.TeamsTabPage = new System.Windows.Forms.TabPage();
            this.PlayersTabPage = new System.Windows.Forms.TabPage();
            this.SignedPlayersTabPage = new System.Windows.Forms.TabPage();
            this.TabImageList = new System.Windows.Forms.ImageList(this.components);
            this.FindReplaceButton = new System.Windows.Forms.Button();
            this.MyCancelButton = new System.Windows.Forms.Button();
            this.OutputTextBox = new System.Windows.Forms.TextBox();
            this.TeamReplaceSearchItem = new RugbyGui.UserControls.ReplaceSearchItem();
            this.YearFoundedSearchItem = new RugbyGui.UserControls.IntegerSearchItem();
            this.RegionSearchItem = new RugbyGui.UserControls.StringSearchItem();
            this.CoachSearchItem = new RugbyGui.UserControls.StringSearchItem();
            this.HomeGroundSearchItem = new RugbyGui.UserControls.StringSearchItem();
            this.TeamNameSearchItem = new RugbyGui.UserControls.StringSearchItem();
            this.PlaceOfBirthSearchItem = new RugbyGui.UserControls.StringSearchItem();
            this.DateOfBirthSearchItem = new RugbyGui.UserControls.DateSearchItem();
            this.WeightSearchItem = new RugbyGui.UserControls.IntegerSearchItem();
            this.HeightSearchItem = new RugbyGui.UserControls.IntegerSearchItem();
            this.LastNameSearchItem = new RugbyGui.UserControls.StringSearchItem();
            this.FirstNameSearchItem = new RugbyGui.UserControls.StringSearchItem();
            this.PlayerIdSearchItem = new RugbyGui.UserControls.IntegerSearchItem();
            this.SignedPlayerTeamSearchItem = new RugbyGui.UserControls.StringSearchItem();
            this.SignedPlayerNameSearchItem = new RugbyGui.UserControls.StringSearchItem();
            this.SignedPlayerIdSearchItem = new RugbyGui.UserControls.IntegerSearchItem();
            this.PlayerReplaceSearchItem = new RugbyGui.UserControls.ReplaceSearchItem();
            this.SignedPlayerReplaceSearchItem = new RugbyGui.UserControls.ReplaceSearchItem();
            this.MainTabControl.SuspendLayout();
            this.TeamsTabPage.SuspendLayout();
            this.PlayersTabPage.SuspendLayout();
            this.SignedPlayersTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainTabControl
            // 
            this.MainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainTabControl.Controls.Add(this.TeamsTabPage);
            this.MainTabControl.Controls.Add(this.PlayersTabPage);
            this.MainTabControl.Controls.Add(this.SignedPlayersTabPage);
            this.MainTabControl.ImageList = this.TabImageList;
            this.MainTabControl.Location = new System.Drawing.Point(12, 12);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(458, 315);
            this.MainTabControl.TabIndex = 0;
            this.MainTabControl.SelectedIndexChanged += new System.EventHandler(this.MainTabControl_SelectedIndexChanged);
            // 
            // TeamsTabPage
            // 
            this.TeamsTabPage.Controls.Add(this.TeamReplaceSearchItem);
            this.TeamsTabPage.Controls.Add(this.YearFoundedSearchItem);
            this.TeamsTabPage.Controls.Add(this.RegionSearchItem);
            this.TeamsTabPage.Controls.Add(this.CoachSearchItem);
            this.TeamsTabPage.Controls.Add(this.HomeGroundSearchItem);
            this.TeamsTabPage.Controls.Add(this.TeamNameSearchItem);
            this.TeamsTabPage.ImageIndex = 0;
            this.TeamsTabPage.Location = new System.Drawing.Point(4, 23);
            this.TeamsTabPage.Name = "TeamsTabPage";
            this.TeamsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.TeamsTabPage.Size = new System.Drawing.Size(450, 288);
            this.TeamsTabPage.TabIndex = 0;
            this.TeamsTabPage.Text = "Teams";
            this.TeamsTabPage.UseVisualStyleBackColor = true;
            // 
            // PlayersTabPage
            // 
            this.PlayersTabPage.Controls.Add(this.PlayerReplaceSearchItem);
            this.PlayersTabPage.Controls.Add(this.PlaceOfBirthSearchItem);
            this.PlayersTabPage.Controls.Add(this.DateOfBirthSearchItem);
            this.PlayersTabPage.Controls.Add(this.WeightSearchItem);
            this.PlayersTabPage.Controls.Add(this.HeightSearchItem);
            this.PlayersTabPage.Controls.Add(this.LastNameSearchItem);
            this.PlayersTabPage.Controls.Add(this.FirstNameSearchItem);
            this.PlayersTabPage.Controls.Add(this.PlayerIdSearchItem);
            this.PlayersTabPage.ImageIndex = 1;
            this.PlayersTabPage.Location = new System.Drawing.Point(4, 23);
            this.PlayersTabPage.Name = "PlayersTabPage";
            this.PlayersTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.PlayersTabPage.Size = new System.Drawing.Size(450, 288);
            this.PlayersTabPage.TabIndex = 1;
            this.PlayersTabPage.Text = "Players";
            this.PlayersTabPage.UseVisualStyleBackColor = true;
            // 
            // SignedPlayersTabPage
            // 
            this.SignedPlayersTabPage.Controls.Add(this.SignedPlayerReplaceSearchItem);
            this.SignedPlayersTabPage.Controls.Add(this.SignedPlayerTeamSearchItem);
            this.SignedPlayersTabPage.Controls.Add(this.SignedPlayerNameSearchItem);
            this.SignedPlayersTabPage.Controls.Add(this.SignedPlayerIdSearchItem);
            this.SignedPlayersTabPage.ImageIndex = 2;
            this.SignedPlayersTabPage.Location = new System.Drawing.Point(4, 23);
            this.SignedPlayersTabPage.Name = "SignedPlayersTabPage";
            this.SignedPlayersTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.SignedPlayersTabPage.Size = new System.Drawing.Size(450, 288);
            this.SignedPlayersTabPage.TabIndex = 2;
            this.SignedPlayersTabPage.Text = "Signed Players";
            this.SignedPlayersTabPage.UseVisualStyleBackColor = true;
            // 
            // TabImageList
            // 
            this.TabImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("TabImageList.ImageStream")));
            this.TabImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.TabImageList.Images.SetKeyName(0, "Team16x16.png");
            this.TabImageList.Images.SetKeyName(1, "Player16x16.png");
            this.TabImageList.Images.SetKeyName(2, "Signed16x16.png");
            // 
            // FindReplaceButton
            // 
            this.FindReplaceButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.FindReplaceButton.Location = new System.Drawing.Point(314, 473);
            this.FindReplaceButton.Name = "FindReplaceButton";
            this.FindReplaceButton.Size = new System.Drawing.Size(75, 23);
            this.FindReplaceButton.TabIndex = 2;
            this.FindReplaceButton.Text = "&Find";
            this.FindReplaceButton.UseVisualStyleBackColor = true;
            this.FindReplaceButton.Click += new System.EventHandler(this.FindReplaceButton_Click);
            // 
            // MyCancelButton
            // 
            this.MyCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.MyCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.MyCancelButton.Location = new System.Drawing.Point(395, 473);
            this.MyCancelButton.Name = "MyCancelButton";
            this.MyCancelButton.Size = new System.Drawing.Size(75, 23);
            this.MyCancelButton.TabIndex = 3;
            this.MyCancelButton.Text = "&Cancel";
            this.MyCancelButton.UseVisualStyleBackColor = true;
            // 
            // OutputTextBox
            // 
            this.OutputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputTextBox.Location = new System.Drawing.Point(12, 333);
            this.OutputTextBox.Multiline = true;
            this.OutputTextBox.Name = "OutputTextBox";
            this.OutputTextBox.ReadOnly = true;
            this.OutputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.OutputTextBox.Size = new System.Drawing.Size(454, 134);
            this.OutputTextBox.TabIndex = 1;
            // 
            // TeamReplaceSearchItem
            // 
            this.TeamReplaceSearchItem.FieldName = null;
            //this.TeamReplaceSearchItem.Fields = null;
            this.TeamReplaceSearchItem.IsSearchItemChecked = false;
            this.TeamReplaceSearchItem.Location = new System.Drawing.Point(6, 166);
            this.TeamReplaceSearchItem.Name = "TeamReplaceSearchItem";
            this.TeamReplaceSearchItem.Size = new System.Drawing.Size(373, 26);
            this.TeamReplaceSearchItem.ReplacementValue = null;
            this.TeamReplaceSearchItem.TabIndex = 5;
            // 
            // YearFoundedSearchItem
            // 
            this.YearFoundedSearchItem.AutoSize = true;
            this.YearFoundedSearchItem.BeginValue = 1;
            this.YearFoundedSearchItem.EndValue = 1;
            this.YearFoundedSearchItem.FieldName = "Year Founded";
            this.YearFoundedSearchItem.IsSearchItemChecked = false;
            this.YearFoundedSearchItem.Location = new System.Drawing.Point(6, 134);
            this.YearFoundedSearchItem.MaxRange = 1980;
            this.YearFoundedSearchItem.MinRange = 1;
            this.YearFoundedSearchItem.Name = "YearFoundedSearchItem";
            this.YearFoundedSearchItem.Operation = "fewer than";
            this.YearFoundedSearchItem.Size = new System.Drawing.Size(243, 26);
            this.YearFoundedSearchItem.TabIndex = 4;
            // 
            // RegionSearchItem
            // 
            this.RegionSearchItem.AutoSize = true;
            this.RegionSearchItem.FieldName = "Region";
            this.RegionSearchItem.IsSearchItemChecked = false;
            this.RegionSearchItem.Location = new System.Drawing.Point(6, 102);
            this.RegionSearchItem.Name = "RegionSearchItem";
            this.RegionSearchItem.Operation = "contains";
            this.RegionSearchItem.Size = new System.Drawing.Size(284, 26);
            this.RegionSearchItem.StringValue = "";
            this.RegionSearchItem.TabIndex = 3;
            // 
            // CoachSearchItem
            // 
            this.CoachSearchItem.AutoSize = true;
            this.CoachSearchItem.FieldName = "Coach";
            this.CoachSearchItem.IsSearchItemChecked = false;
            this.CoachSearchItem.Location = new System.Drawing.Point(6, 70);
            this.CoachSearchItem.Name = "CoachSearchItem";
            this.CoachSearchItem.Operation = "contains";
            this.CoachSearchItem.Size = new System.Drawing.Size(284, 26);
            this.CoachSearchItem.StringValue = "";
            this.CoachSearchItem.TabIndex = 2;
            // 
            // HomeGroundSearchItem
            // 
            this.HomeGroundSearchItem.AutoSize = true;
            this.HomeGroundSearchItem.FieldName = "Home Ground";
            this.HomeGroundSearchItem.IsSearchItemChecked = false;
            this.HomeGroundSearchItem.Location = new System.Drawing.Point(6, 38);
            this.HomeGroundSearchItem.Name = "HomeGroundSearchItem";
            this.HomeGroundSearchItem.Operation = "contains";
            this.HomeGroundSearchItem.Size = new System.Drawing.Size(307, 26);
            this.HomeGroundSearchItem.StringValue = "";
            this.HomeGroundSearchItem.TabIndex = 1;
            // 
            // TeamNameSearchItem
            // 
            this.TeamNameSearchItem.AutoSize = true;
            this.TeamNameSearchItem.FieldName = "Name";
            this.TeamNameSearchItem.IsSearchItemChecked = false;
            this.TeamNameSearchItem.Location = new System.Drawing.Point(6, 6);
            this.TeamNameSearchItem.Name = "TeamNameSearchItem";
            this.TeamNameSearchItem.Operation = "contains";
            this.TeamNameSearchItem.Size = new System.Drawing.Size(284, 26);
            this.TeamNameSearchItem.StringValue = "";
            this.TeamNameSearchItem.TabIndex = 0;
            // 
            // PlaceOfBirthSearchItem
            // 
            this.PlaceOfBirthSearchItem.AutoSize = true;
            this.PlaceOfBirthSearchItem.FieldName = "Place of Birth";
            this.PlaceOfBirthSearchItem.IsSearchItemChecked = false;
            this.PlaceOfBirthSearchItem.Location = new System.Drawing.Point(6, 198);
            this.PlaceOfBirthSearchItem.Name = "PlaceOfBirthSearchItem";
            this.PlaceOfBirthSearchItem.Operation = "contains";
            this.PlaceOfBirthSearchItem.Size = new System.Drawing.Size(312, 26);
            this.PlaceOfBirthSearchItem.StringValue = null;
            this.PlaceOfBirthSearchItem.TabIndex = 6;
            // 
            // DateOfBirthSearchItem
            // 
            this.DateOfBirthSearchItem.AutoSize = true;
            this.DateOfBirthSearchItem.BeginDate = new System.DateTime(2011, 4, 6, 13, 34, 45, 300);
            this.DateOfBirthSearchItem.EndDate = new System.DateTime(2031, 4, 6, 13, 34, 45, 300);
            this.DateOfBirthSearchItem.FieldName = "Date of Birth";
            this.DateOfBirthSearchItem.IsSearchItemChecked = false;
            this.DateOfBirthSearchItem.Location = new System.Drawing.Point(6, 166);
            this.DateOfBirthSearchItem.Name = "DateOfBirthSearchItem";
            this.DateOfBirthSearchItem.Operation = "before";
            this.DateOfBirthSearchItem.Size = new System.Drawing.Size(355, 26);
            this.DateOfBirthSearchItem.TabIndex = 5;
            // 
            // WeightSearchItem
            // 
            this.WeightSearchItem.AutoSize = true;
            this.WeightSearchItem.BeginValue = 1;
            this.WeightSearchItem.EndValue = 1;
            this.WeightSearchItem.FieldName = "Weight";
            this.WeightSearchItem.IsSearchItemChecked = false;
            this.WeightSearchItem.Location = new System.Drawing.Point(6, 134);
            this.WeightSearchItem.MaxRange = 1000;
            this.WeightSearchItem.MinRange = 1;
            this.WeightSearchItem.Name = "WeightSearchItem";
            this.WeightSearchItem.Operation = "fewer than";
            this.WeightSearchItem.Size = new System.Drawing.Size(339, 26);
            this.WeightSearchItem.TabIndex = 4;
            // 
            // HeightSearchItem
            // 
            this.HeightSearchItem.AutoSize = true;
            this.HeightSearchItem.BeginValue = 1;
            this.HeightSearchItem.EndValue = 1;
            this.HeightSearchItem.FieldName = "Height";
            this.HeightSearchItem.IsSearchItemChecked = false;
            this.HeightSearchItem.Location = new System.Drawing.Point(6, 102);
            this.HeightSearchItem.MaxRange = 1000;
            this.HeightSearchItem.MinRange = 1;
            this.HeightSearchItem.Name = "HeightSearchItem";
            this.HeightSearchItem.Operation = "fewer than";
            this.HeightSearchItem.Size = new System.Drawing.Size(339, 26);
            this.HeightSearchItem.TabIndex = 3;
            // 
            // LastNameSearchItem
            // 
            this.LastNameSearchItem.AutoSize = true;
            this.LastNameSearchItem.FieldName = "Last Name";
            this.LastNameSearchItem.IsSearchItemChecked = false;
            this.LastNameSearchItem.Location = new System.Drawing.Point(6, 70);
            this.LastNameSearchItem.Name = "LastNameSearchItem";
            this.LastNameSearchItem.Operation = "contains";
            this.LastNameSearchItem.Size = new System.Drawing.Size(305, 26);
            this.LastNameSearchItem.StringValue = null;
            this.LastNameSearchItem.TabIndex = 2;
            // 
            // FirstNameSearchItem
            // 
            this.FirstNameSearchItem.AutoSize = true;
            this.FirstNameSearchItem.FieldName = "First Name";
            this.FirstNameSearchItem.IsSearchItemChecked = false;
            this.FirstNameSearchItem.Location = new System.Drawing.Point(6, 38);
            this.FirstNameSearchItem.Name = "FirstNameSearchItem";
            this.FirstNameSearchItem.Operation = "contains";
            this.FirstNameSearchItem.Size = new System.Drawing.Size(305, 26);
            this.FirstNameSearchItem.StringValue = null;
            this.FirstNameSearchItem.TabIndex = 1;
            // 
            // PlayerIdSearchItem
            // 
            this.PlayerIdSearchItem.AutoSize = true;
            this.PlayerIdSearchItem.BeginValue = 1;
            this.PlayerIdSearchItem.EndValue = 1;
            this.PlayerIdSearchItem.FieldName = "ID";
            this.PlayerIdSearchItem.IsSearchItemChecked = false;
            this.PlayerIdSearchItem.Location = new System.Drawing.Point(6, 6);
            this.PlayerIdSearchItem.MaxRange = 1000;
            this.PlayerIdSearchItem.MinRange = 1;
            this.PlayerIdSearchItem.Name = "PlayerIdSearchItem";
            this.PlayerIdSearchItem.Operation = "fewer than";
            this.PlayerIdSearchItem.Size = new System.Drawing.Size(339, 26);
            this.PlayerIdSearchItem.TabIndex = 0;
            // 
            // SignedPlayerTeamSearchItem
            // 
            this.SignedPlayerTeamSearchItem.AutoSize = true;
            this.SignedPlayerTeamSearchItem.FieldName = "Team Name";
            this.SignedPlayerTeamSearchItem.IsSearchItemChecked = false;
            this.SignedPlayerTeamSearchItem.Location = new System.Drawing.Point(6, 70);
            this.SignedPlayerTeamSearchItem.Name = "SignedPlayerTeamSearchItem";
            this.SignedPlayerTeamSearchItem.Operation = "contains";
            this.SignedPlayerTeamSearchItem.Size = new System.Drawing.Size(307, 26);
            this.SignedPlayerTeamSearchItem.StringValue = null;
            this.SignedPlayerTeamSearchItem.TabIndex = 2;
            // 
            // SignedPlayerNameSearchItem
            // 
            this.SignedPlayerNameSearchItem.AutoSize = true;
            this.SignedPlayerNameSearchItem.FieldName = "Player Name";
            this.SignedPlayerNameSearchItem.IsSearchItemChecked = false;
            this.SignedPlayerNameSearchItem.Location = new System.Drawing.Point(6, 38);
            this.SignedPlayerNameSearchItem.Name = "SignedPlayerNameSearchItem";
            this.SignedPlayerNameSearchItem.Operation = "contains";
            this.SignedPlayerNameSearchItem.Size = new System.Drawing.Size(309, 26);
            this.SignedPlayerNameSearchItem.StringValue = null;
            this.SignedPlayerNameSearchItem.TabIndex = 1;
            // 
            // SignedPlayerIdSearchItem
            // 
            this.SignedPlayerIdSearchItem.AutoSize = true;
            this.SignedPlayerIdSearchItem.BeginValue = 1;
            this.SignedPlayerIdSearchItem.EndValue = 1;
            this.SignedPlayerIdSearchItem.FieldName = "Player ID";
            this.SignedPlayerIdSearchItem.IsSearchItemChecked = false;
            this.SignedPlayerIdSearchItem.Location = new System.Drawing.Point(6, 6);
            this.SignedPlayerIdSearchItem.MaxRange = 1000;
            this.SignedPlayerIdSearchItem.MinRange = 1;
            this.SignedPlayerIdSearchItem.Name = "SignedPlayerIdSearchItem";
            this.SignedPlayerIdSearchItem.Operation = "fewer than";
            this.SignedPlayerIdSearchItem.Size = new System.Drawing.Size(339, 26);
            this.SignedPlayerIdSearchItem.TabIndex = 0;
            // 
            // PlayerReplaceSearchItem
            // 
            this.PlayerReplaceSearchItem.FieldName = null;
            //this.PlayerReplaceSearchItem.Fields = null;
            this.PlayerReplaceSearchItem.IsSearchItemChecked = false;
            this.PlayerReplaceSearchItem.Location = new System.Drawing.Point(6, 230);
            this.PlayerReplaceSearchItem.Name = "PlayerReplaceSearchItem";
            this.PlayerReplaceSearchItem.Size = new System.Drawing.Size(367, 28);
            this.PlayerReplaceSearchItem.ReplacementValue = null;
            this.PlayerReplaceSearchItem.TabIndex = 7;
            // 
            // SignedPlayerReplaceSearchItem
            // 
            this.SignedPlayerReplaceSearchItem.AutoSize = true;
            this.SignedPlayerReplaceSearchItem.FieldName = null;
            //this.SignedPlayerReplaceSearchItem.Fields = null;
            this.SignedPlayerReplaceSearchItem.IsSearchItemChecked = false;
            this.SignedPlayerReplaceSearchItem.Location = new System.Drawing.Point(6, 102);
            this.SignedPlayerReplaceSearchItem.Name = "SignedPlayerReplaceSearchItem";
            this.SignedPlayerReplaceSearchItem.Size = new System.Drawing.Size(326, 26);
            this.SignedPlayerReplaceSearchItem.ReplacementValue = null;
            this.SignedPlayerReplaceSearchItem.TabIndex = 3;
            // 
            // AdvancedFindAndReplaceDialog
            // 
            this.AcceptButton = this.FindReplaceButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.MyCancelButton;
            this.ClientSize = new System.Drawing.Size(484, 511);
            this.Controls.Add(this.OutputTextBox);
            this.Controls.Add(this.MyCancelButton);
            this.Controls.Add(this.FindReplaceButton);
            this.Controls.Add(this.MainTabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "AdvancedFindAndReplaceDialog";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Advanced Find and Replace";
            this.Load += new System.EventHandler(this.AdvancedFindAndReplaceDialog_Load);
            this.MainTabControl.ResumeLayout(false);
            this.TeamsTabPage.ResumeLayout(false);
            this.TeamsTabPage.PerformLayout();
            this.PlayersTabPage.ResumeLayout(false);
            this.PlayersTabPage.PerformLayout();
            this.SignedPlayersTabPage.ResumeLayout(false);
            this.SignedPlayersTabPage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.TabPage TeamsTabPage;
        private System.Windows.Forms.TabPage PlayersTabPage;
        private System.Windows.Forms.TabPage SignedPlayersTabPage;
        private System.Windows.Forms.ImageList TabImageList;
        private System.Windows.Forms.Button FindReplaceButton;
        private System.Windows.Forms.Button MyCancelButton;
        private UserControls.IntegerSearchItem YearFoundedSearchItem;
        private UserControls.StringSearchItem RegionSearchItem;
        private UserControls.StringSearchItem CoachSearchItem;
        private UserControls.StringSearchItem HomeGroundSearchItem;
        private UserControls.StringSearchItem TeamNameSearchItem;
        private UserControls.StringSearchItem PlaceOfBirthSearchItem;
        private UserControls.DateSearchItem DateOfBirthSearchItem;
        private UserControls.IntegerSearchItem WeightSearchItem;
        private UserControls.IntegerSearchItem HeightSearchItem;
        private UserControls.StringSearchItem LastNameSearchItem;
        private UserControls.StringSearchItem FirstNameSearchItem;
        private UserControls.IntegerSearchItem PlayerIdSearchItem;
        private UserControls.StringSearchItem SignedPlayerTeamSearchItem;
        private UserControls.StringSearchItem SignedPlayerNameSearchItem;
        private UserControls.IntegerSearchItem SignedPlayerIdSearchItem;
        private System.Windows.Forms.TextBox OutputTextBox;
        private UserControls.ReplaceSearchItem TeamReplaceSearchItem;
        private UserControls.ReplaceSearchItem PlayerReplaceSearchItem;
        private UserControls.ReplaceSearchItem SignedPlayerReplaceSearchItem;
    }
}