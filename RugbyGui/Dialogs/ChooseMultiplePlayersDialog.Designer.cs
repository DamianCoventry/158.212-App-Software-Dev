
namespace RugbyGui.Dialogs
{
    partial class ChooseMultiplePlayersDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseMultiplePlayersDialog));
            this.ChosenPlayersLabel = new System.Windows.Forms.Label();
            this.ChosenPlayersListView = new System.Windows.Forms.ListView();
            this.ChosenIdColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ChosenNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ChosenCurrentTeamColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PlayerImageList = new System.Windows.Forms.ImageList(this.components);
            this.MoveAllLeftButton = new System.Windows.Forms.Button();
            this.MoveLeftButton = new System.Windows.Forms.Button();
            this.MoveRightButton = new System.Windows.Forms.Button();
            this.MoveAllRightButton = new System.Windows.Forms.Button();
            this.AvailablePlayersLabel = new System.Windows.Forms.Label();
            this.AvailablePlayersListView = new System.Windows.Forms.ListView();
            this.IdColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FirstNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LastNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HeightColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.WeightColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DateOfBirthColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PlaceOfBirthColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SignedToTeamColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.OkButton = new System.Windows.Forms.Button();
            this.MyCancelButton = new System.Windows.Forms.Button();
            this.ButtonsToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // ChosenPlayersLabel
            // 
            this.ChosenPlayersLabel.AutoSize = true;
            this.ChosenPlayersLabel.Location = new System.Drawing.Point(9, 9);
            this.ChosenPlayersLabel.Name = "ChosenPlayersLabel";
            this.ChosenPlayersLabel.Size = new System.Drawing.Size(83, 13);
            this.ChosenPlayersLabel.TabIndex = 0;
            this.ChosenPlayersLabel.Text = "C&hosen Players:";
            // 
            // ChosenPlayersListView
            // 
            this.ChosenPlayersListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ChosenPlayersListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ChosenIdColumnHeader,
            this.ChosenNameColumnHeader,
            this.ChosenCurrentTeamColumnHeader});
            this.ChosenPlayersListView.FullRowSelect = true;
            this.ChosenPlayersListView.HideSelection = false;
            this.ChosenPlayersListView.Location = new System.Drawing.Point(12, 25);
            this.ChosenPlayersListView.Name = "ChosenPlayersListView";
            this.ChosenPlayersListView.Size = new System.Drawing.Size(285, 384);
            this.ChosenPlayersListView.SmallImageList = this.PlayerImageList;
            this.ChosenPlayersListView.TabIndex = 1;
            this.ChosenPlayersListView.UseCompatibleStateImageBehavior = false;
            this.ChosenPlayersListView.View = System.Windows.Forms.View.Details;
            this.ChosenPlayersListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListView_ColumnClick);
            this.ChosenPlayersListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ChosenPlayersListView_MouseDoubleClick);
            // 
            // ChosenIdColumnHeader
            // 
            this.ChosenIdColumnHeader.Text = "ID";
            this.ChosenIdColumnHeader.Width = 30;
            // 
            // ChosenNameColumnHeader
            // 
            this.ChosenNameColumnHeader.Text = "Name";
            this.ChosenNameColumnHeader.Width = 100;
            // 
            // ChosenCurrentTeamColumnHeader
            // 
            this.ChosenCurrentTeamColumnHeader.Text = "Signed to Team";
            this.ChosenCurrentTeamColumnHeader.Width = 100;
            // 
            // PlayerImageList
            // 
            this.PlayerImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("PlayerImageList.ImageStream")));
            this.PlayerImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.PlayerImageList.Images.SetKeyName(0, "Player16x16.png");
            // 
            // MoveAllLeftButton
            // 
            this.MoveAllLeftButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MoveAllLeftButton.ForeColor = System.Drawing.Color.Blue;
            this.MoveAllLeftButton.Location = new System.Drawing.Point(303, 113);
            this.MoveAllLeftButton.Name = "MoveAllLeftButton";
            this.MoveAllLeftButton.Size = new System.Drawing.Size(46, 32);
            this.MoveAllLeftButton.TabIndex = 2;
            this.MoveAllLeftButton.Text = "🡄🡄";
            this.ButtonsToolTip.SetToolTip(this.MoveAllLeftButton, "Move All Left");
            this.MoveAllLeftButton.UseVisualStyleBackColor = true;
            this.MoveAllLeftButton.Click += new System.EventHandler(this.MoveAllLeftButton_Click);
            // 
            // MoveLeftButton
            // 
            this.MoveLeftButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MoveLeftButton.ForeColor = System.Drawing.Color.Blue;
            this.MoveLeftButton.Location = new System.Drawing.Point(303, 151);
            this.MoveLeftButton.Name = "MoveLeftButton";
            this.MoveLeftButton.Size = new System.Drawing.Size(46, 32);
            this.MoveLeftButton.TabIndex = 3;
            this.MoveLeftButton.Text = "🡄";
            this.ButtonsToolTip.SetToolTip(this.MoveLeftButton, "Move Selected Left");
            this.MoveLeftButton.UseVisualStyleBackColor = true;
            this.MoveLeftButton.Click += new System.EventHandler(this.MoveLeftButton_Click);
            // 
            // MoveRightButton
            // 
            this.MoveRightButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MoveRightButton.ForeColor = System.Drawing.Color.Blue;
            this.MoveRightButton.Location = new System.Drawing.Point(303, 189);
            this.MoveRightButton.Name = "MoveRightButton";
            this.MoveRightButton.Size = new System.Drawing.Size(46, 32);
            this.MoveRightButton.TabIndex = 4;
            this.MoveRightButton.Text = "🡆";
            this.ButtonsToolTip.SetToolTip(this.MoveRightButton, "Move Selected Right");
            this.MoveRightButton.UseVisualStyleBackColor = true;
            this.MoveRightButton.Click += new System.EventHandler(this.MoveRightButton_Click);
            // 
            // MoveAllRightButton
            // 
            this.MoveAllRightButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MoveAllRightButton.ForeColor = System.Drawing.Color.Blue;
            this.MoveAllRightButton.Location = new System.Drawing.Point(303, 227);
            this.MoveAllRightButton.Name = "MoveAllRightButton";
            this.MoveAllRightButton.Size = new System.Drawing.Size(46, 32);
            this.MoveAllRightButton.TabIndex = 5;
            this.MoveAllRightButton.Text = "🡆🡆";
            this.ButtonsToolTip.SetToolTip(this.MoveAllRightButton, "Move All Right");
            this.MoveAllRightButton.UseVisualStyleBackColor = true;
            this.MoveAllRightButton.Click += new System.EventHandler(this.MoveAllRightButton_Click);
            // 
            // AvailablePlayersLabel
            // 
            this.AvailablePlayersLabel.AutoSize = true;
            this.AvailablePlayersLabel.Location = new System.Drawing.Point(352, 9);
            this.AvailablePlayersLabel.Name = "AvailablePlayersLabel";
            this.AvailablePlayersLabel.Size = new System.Drawing.Size(90, 13);
            this.AvailablePlayersLabel.TabIndex = 6;
            this.AvailablePlayersLabel.Text = "&Available Players:";
            // 
            // AvailablePlayersListView
            // 
            this.AvailablePlayersListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AvailablePlayersListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IdColumnHeader,
            this.FirstNameColumnHeader,
            this.LastNameColumnHeader,
            this.HeightColumnHeader,
            this.WeightColumnHeader,
            this.DateOfBirthColumnHeader,
            this.PlaceOfBirthColumnHeader,
            this.SignedToTeamColumnHeader});
            this.AvailablePlayersListView.FullRowSelect = true;
            this.AvailablePlayersListView.HideSelection = false;
            this.AvailablePlayersListView.Location = new System.Drawing.Point(355, 25);
            this.AvailablePlayersListView.Name = "AvailablePlayersListView";
            this.AvailablePlayersListView.Size = new System.Drawing.Size(766, 384);
            this.AvailablePlayersListView.SmallImageList = this.PlayerImageList;
            this.AvailablePlayersListView.TabIndex = 7;
            this.AvailablePlayersListView.UseCompatibleStateImageBehavior = false;
            this.AvailablePlayersListView.View = System.Windows.Forms.View.Details;
            this.AvailablePlayersListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListView_ColumnClick);
            this.AvailablePlayersListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.AvailablePlayersListView_MouseDoubleClick);
            // 
            // IdColumnHeader
            // 
            this.IdColumnHeader.Text = "ID";
            this.IdColumnHeader.Width = 35;
            // 
            // FirstNameColumnHeader
            // 
            this.FirstNameColumnHeader.Text = "First Name";
            this.FirstNameColumnHeader.Width = 100;
            // 
            // LastNameColumnHeader
            // 
            this.LastNameColumnHeader.Text = "Last Name";
            this.LastNameColumnHeader.Width = 100;
            // 
            // HeightColumnHeader
            // 
            this.HeightColumnHeader.Text = "Height";
            this.HeightColumnHeader.Width = 100;
            // 
            // WeightColumnHeader
            // 
            this.WeightColumnHeader.Text = "Weight";
            this.WeightColumnHeader.Width = 100;
            // 
            // DateOfBirthColumnHeader
            // 
            this.DateOfBirthColumnHeader.Text = "Date of Birth";
            this.DateOfBirthColumnHeader.Width = 100;
            // 
            // PlaceOfBirthColumnHeader
            // 
            this.PlaceOfBirthColumnHeader.Text = "Place of Birth";
            this.PlaceOfBirthColumnHeader.Width = 100;
            // 
            // SignedToTeamColumnHeader
            // 
            this.SignedToTeamColumnHeader.Text = "Signed to Team";
            this.SignedToTeamColumnHeader.Width = 100;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.Location = new System.Drawing.Point(965, 415);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 8;
            this.OkButton.Text = "&OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // MyCancelButton
            // 
            this.MyCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.MyCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.MyCancelButton.Location = new System.Drawing.Point(1046, 415);
            this.MyCancelButton.Name = "MyCancelButton";
            this.MyCancelButton.Size = new System.Drawing.Size(75, 23);
            this.MyCancelButton.TabIndex = 9;
            this.MyCancelButton.Text = "&Cancel";
            this.MyCancelButton.UseVisualStyleBackColor = true;
            // 
            // ChooseMultiplePlayersDialog
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.MyCancelButton;
            this.ClientSize = new System.Drawing.Size(1132, 448);
            this.Controls.Add(this.MyCancelButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.AvailablePlayersListView);
            this.Controls.Add(this.AvailablePlayersLabel);
            this.Controls.Add(this.MoveAllRightButton);
            this.Controls.Add(this.MoveRightButton);
            this.Controls.Add(this.MoveLeftButton);
            this.Controls.Add(this.MoveAllLeftButton);
            this.Controls.Add(this.ChosenPlayersListView);
            this.Controls.Add(this.ChosenPlayersLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 400);
            this.Name = "ChooseMultiplePlayersDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Choose Multiple Players";
            this.Load += new System.EventHandler(this.ChooseMultiplePlayersDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ChosenPlayersLabel;
        private System.Windows.Forms.ListView ChosenPlayersListView;
        private System.Windows.Forms.Button MoveAllLeftButton;
        private System.Windows.Forms.Button MoveLeftButton;
        private System.Windows.Forms.Button MoveRightButton;
        private System.Windows.Forms.Button MoveAllRightButton;
        private System.Windows.Forms.Label AvailablePlayersLabel;
        private System.Windows.Forms.ListView AvailablePlayersListView;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button MyCancelButton;
        private System.Windows.Forms.ToolTip ButtonsToolTip;
        private System.Windows.Forms.ColumnHeader ChosenIdColumnHeader;
        private System.Windows.Forms.ColumnHeader ChosenNameColumnHeader;
        private System.Windows.Forms.ColumnHeader ChosenCurrentTeamColumnHeader;
        private System.Windows.Forms.ColumnHeader IdColumnHeader;
        private System.Windows.Forms.ColumnHeader FirstNameColumnHeader;
        private System.Windows.Forms.ColumnHeader LastNameColumnHeader;
        private System.Windows.Forms.ColumnHeader HeightColumnHeader;
        private System.Windows.Forms.ColumnHeader WeightColumnHeader;
        private System.Windows.Forms.ColumnHeader DateOfBirthColumnHeader;
        private System.Windows.Forms.ColumnHeader PlaceOfBirthColumnHeader;
        private System.Windows.Forms.ImageList PlayerImageList;
        private System.Windows.Forms.ColumnHeader SignedToTeamColumnHeader;
    }
}