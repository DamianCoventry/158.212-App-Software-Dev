
namespace RugbyGui.Dialogs
{
    partial class ChooseSingleTeamDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseSingleTeamDialog));
            this.TeamsListView = new System.Windows.Forms.ListView();
            this.TeamNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HomeGroundColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CoachColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.YearFoundedColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RegionColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TeamsImageList = new System.Windows.Forms.ImageList(this.components);
            this.MyCancelButton = new System.Windows.Forms.Button();
            this.OkButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TeamsListView
            // 
            this.TeamsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TeamsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TeamNameColumnHeader,
            this.HomeGroundColumnHeader,
            this.CoachColumnHeader,
            this.YearFoundedColumnHeader,
            this.RegionColumnHeader});
            this.TeamsListView.FullRowSelect = true;
            this.TeamsListView.HideSelection = false;
            this.TeamsListView.Location = new System.Drawing.Point(12, 12);
            this.TeamsListView.MultiSelect = false;
            this.TeamsListView.Name = "TeamsListView";
            this.TeamsListView.Size = new System.Drawing.Size(776, 373);
            this.TeamsListView.SmallImageList = this.TeamsImageList;
            this.TeamsListView.TabIndex = 0;
            this.TeamsListView.UseCompatibleStateImageBehavior = false;
            this.TeamsListView.View = System.Windows.Forms.View.Details;
            this.TeamsListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.TeamsListView_ColumnClick);
            this.TeamsListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TeamsListView_MouseDoubleClick);
            // 
            // TeamNameColumnHeader
            // 
            this.TeamNameColumnHeader.Text = "Team Name";
            this.TeamNameColumnHeader.Width = 100;
            // 
            // HomeGroundColumnHeader
            // 
            this.HomeGroundColumnHeader.Text = "Home Ground";
            this.HomeGroundColumnHeader.Width = 100;
            // 
            // CoachColumnHeader
            // 
            this.CoachColumnHeader.Text = "Coach";
            this.CoachColumnHeader.Width = 100;
            // 
            // YearFoundedColumnHeader
            // 
            this.YearFoundedColumnHeader.Text = "Year Founded";
            this.YearFoundedColumnHeader.Width = 100;
            // 
            // RegionColumnHeader
            // 
            this.RegionColumnHeader.Text = "Region";
            this.RegionColumnHeader.Width = 100;
            // 
            // TeamsImageList
            // 
            this.TeamsImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("TeamsImageList.ImageStream")));
            this.TeamsImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.TeamsImageList.Images.SetKeyName(0, "Team16x16.png");
            // 
            // MyCancelButton
            // 
            this.MyCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.MyCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.MyCancelButton.Location = new System.Drawing.Point(713, 393);
            this.MyCancelButton.Name = "MyCancelButton";
            this.MyCancelButton.Size = new System.Drawing.Size(75, 23);
            this.MyCancelButton.TabIndex = 2;
            this.MyCancelButton.Text = "&Cancel";
            this.MyCancelButton.UseVisualStyleBackColor = true;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.Location = new System.Drawing.Point(632, 393);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "&OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // ChooseSingleTeamDialog
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.MyCancelButton;
            this.ClientSize = new System.Drawing.Size(800, 428);
            this.Controls.Add(this.TeamsListView);
            this.Controls.Add(this.MyCancelButton);
            this.Controls.Add(this.OkButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "ChooseSingleTeamDialog";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Choose Team";
            this.Load += new System.EventHandler(this.ChooseSingleTeamDialog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView TeamsListView;
        private System.Windows.Forms.ColumnHeader TeamNameColumnHeader;
        private System.Windows.Forms.ColumnHeader HomeGroundColumnHeader;
        private System.Windows.Forms.ColumnHeader CoachColumnHeader;
        private System.Windows.Forms.ColumnHeader YearFoundedColumnHeader;
        private System.Windows.Forms.ColumnHeader RegionColumnHeader;
        private System.Windows.Forms.ImageList TeamsImageList;
        private System.Windows.Forms.Button MyCancelButton;
        private System.Windows.Forms.Button OkButton;
    }
}