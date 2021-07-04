
namespace RugbyGui.Dialogs
{
    partial class ImportSummaryDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportSummaryDialog));
            this.SummaryLabel = new System.Windows.Forms.Label();
            this.SummaryListView = new System.Windows.Forms.ListView();
            this.ItemNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ResultColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CloseButton = new System.Windows.Forms.Button();
            this.SummaryImageList = new System.Windows.Forms.ImageList(this.components);
            this.OkImage = new System.Windows.Forms.PictureBox();
            this.ImportedOkLabel = new System.Windows.Forms.Label();
            this.FailedToImportLabel = new System.Windows.Forms.Label();
            this.ErrorImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.OkImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorImage)).BeginInit();
            this.SuspendLayout();
            // 
            // SummaryLabel
            // 
            this.SummaryLabel.AutoSize = true;
            this.SummaryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SummaryLabel.Location = new System.Drawing.Point(12, 9);
            this.SummaryLabel.Name = "SummaryLabel";
            this.SummaryLabel.Size = new System.Drawing.Size(126, 20);
            this.SummaryLabel.TabIndex = 0;
            this.SummaryLabel.Text = "Import Summary";
            // 
            // SummaryListView
            // 
            this.SummaryListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SummaryListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ItemNameColumnHeader,
            this.ResultColumnHeader});
            this.SummaryListView.HideSelection = false;
            this.SummaryListView.Location = new System.Drawing.Point(16, 102);
            this.SummaryListView.Name = "SummaryListView";
            this.SummaryListView.Size = new System.Drawing.Size(326, 265);
            this.SummaryListView.SmallImageList = this.SummaryImageList;
            this.SummaryListView.TabIndex = 3;
            this.SummaryListView.UseCompatibleStateImageBehavior = false;
            this.SummaryListView.View = System.Windows.Forms.View.Details;
            // 
            // ItemNameColumnHeader
            // 
            this.ItemNameColumnHeader.Text = "Item Name";
            this.ItemNameColumnHeader.Width = 175;
            // 
            // ResultColumnHeader
            // 
            this.ResultColumnHeader.Text = "Import Result";
            this.ResultColumnHeader.Width = 125;
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.Location = new System.Drawing.Point(267, 373);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 4;
            this.CloseButton.Text = "&Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // SummaryImageList
            // 
            this.SummaryImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("SummaryImageList.ImageStream")));
            this.SummaryImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.SummaryImageList.Images.SetKeyName(0, "GreenTick16x16.png");
            this.SummaryImageList.Images.SetKeyName(1, "Delete16x16.png");
            // 
            // OkImage
            // 
            this.OkImage.Image = global::RugbyGui.Properties.Resources.GreenTick16x16;
            this.OkImage.Location = new System.Drawing.Point(37, 41);
            this.OkImage.Name = "OkImage";
            this.OkImage.Size = new System.Drawing.Size(16, 16);
            this.OkImage.TabIndex = 3;
            this.OkImage.TabStop = false;
            // 
            // ImportedOkLabel
            // 
            this.ImportedOkLabel.AutoSize = true;
            this.ImportedOkLabel.Location = new System.Drawing.Point(59, 44);
            this.ImportedOkLabel.Name = "ImportedOkLabel";
            this.ImportedOkLabel.Size = new System.Drawing.Size(111, 13);
            this.ImportedOkLabel.TabIndex = 1;
            this.ImportedOkLabel.Text = "15 teams imported OK";
            // 
            // FailedToImportLabel
            // 
            this.FailedToImportLabel.AutoSize = true;
            this.FailedToImportLabel.Location = new System.Drawing.Point(59, 75);
            this.FailedToImportLabel.Name = "FailedToImportLabel";
            this.FailedToImportLabel.Size = new System.Drawing.Size(115, 13);
            this.FailedToImportLabel.TabIndex = 2;
            this.FailedToImportLabel.Text = "7 teams failed to import";
            // 
            // ErrorImage
            // 
            this.ErrorImage.Image = global::RugbyGui.Properties.Resources.Delete16x16;
            this.ErrorImage.Location = new System.Drawing.Point(37, 72);
            this.ErrorImage.Name = "ErrorImage";
            this.ErrorImage.Size = new System.Drawing.Size(16, 16);
            this.ErrorImage.TabIndex = 5;
            this.ErrorImage.TabStop = false;
            // 
            // ImportSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 411);
            this.Controls.Add(this.FailedToImportLabel);
            this.Controls.Add(this.ErrorImage);
            this.Controls.Add(this.ImportedOkLabel);
            this.Controls.Add(this.OkImage);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.SummaryListView);
            this.Controls.Add(this.SummaryLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 400);
            this.Name = "ImportSummary";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import Summary";
            this.Load += new System.EventHandler(this.ImportSummary_Load);
            ((System.ComponentModel.ISupportInitialize)(this.OkImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label SummaryLabel;
        private System.Windows.Forms.ListView SummaryListView;
        private System.Windows.Forms.ColumnHeader ItemNameColumnHeader;
        private System.Windows.Forms.ColumnHeader ResultColumnHeader;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.ImageList SummaryImageList;
        private System.Windows.Forms.PictureBox OkImage;
        private System.Windows.Forms.Label ImportedOkLabel;
        private System.Windows.Forms.Label FailedToImportLabel;
        private System.Windows.Forms.PictureBox ErrorImage;
    }
}