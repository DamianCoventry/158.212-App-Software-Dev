
namespace RugbyGui.Dialogs
{
    partial class ChooseSinglePlayerDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseSinglePlayerDialog));
            this.PlayersListView = new System.Windows.Forms.ListView();
            this.IdColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FirstNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LastNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HeightColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.WeightColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DateOfBirthColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PlaceOfBirthColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PlayersImageList = new System.Windows.Forms.ImageList(this.components);
            this.OkButton = new System.Windows.Forms.Button();
            this.MyCancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PlayersListView
            // 
            this.PlayersListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlayersListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IdColumnHeader,
            this.FirstNameColumnHeader,
            this.LastNameColumnHeader,
            this.HeightColumnHeader,
            this.WeightColumnHeader,
            this.DateOfBirthColumnHeader,
            this.PlaceOfBirthColumnHeader});
            this.PlayersListView.FullRowSelect = true;
            this.PlayersListView.HideSelection = false;
            this.PlayersListView.Location = new System.Drawing.Point(12, 12);
            this.PlayersListView.MultiSelect = false;
            this.PlayersListView.Name = "PlayersListView";
            this.PlayersListView.Size = new System.Drawing.Size(776, 373);
            this.PlayersListView.SmallImageList = this.PlayersImageList;
            this.PlayersListView.TabIndex = 0;
            this.PlayersListView.UseCompatibleStateImageBehavior = false;
            this.PlayersListView.View = System.Windows.Forms.View.Details;
            this.PlayersListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.PlayersListView_ColumnClick);
            this.PlayersListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.PlayersListView_MouseDoubleClick);
            // 
            // IdColumnHeader
            // 
            this.IdColumnHeader.Text = "ID";
            this.IdColumnHeader.Width = 100;
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
            // PlayersImageList
            // 
            this.PlayersImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("PlayersImageList.ImageStream")));
            this.PlayersImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.PlayersImageList.Images.SetKeyName(0, "Player16x16.png");
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
            // ChooseSinglePlayerDialog
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.MyCancelButton;
            this.ClientSize = new System.Drawing.Size(800, 428);
            this.Controls.Add(this.MyCancelButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.PlayersListView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "ChooseSinglePlayerDialog";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Choose Player";
            this.Load += new System.EventHandler(this.ChooseSinglePlayerDialog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView PlayersListView;
        private System.Windows.Forms.ColumnHeader IdColumnHeader;
        private System.Windows.Forms.ColumnHeader FirstNameColumnHeader;
        private System.Windows.Forms.ColumnHeader LastNameColumnHeader;
        private System.Windows.Forms.ColumnHeader HeightColumnHeader;
        private System.Windows.Forms.ColumnHeader WeightColumnHeader;
        private System.Windows.Forms.ColumnHeader DateOfBirthColumnHeader;
        private System.Windows.Forms.ColumnHeader PlaceOfBirthColumnHeader;
        private System.Windows.Forms.ImageList PlayersImageList;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button MyCancelButton;
    }
}