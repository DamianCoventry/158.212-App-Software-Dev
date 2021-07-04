
namespace RugbyGui.Dialogs
{
    partial class PlayerDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerDialog));
            this.TeamContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.OkButton = new System.Windows.Forms.Button();
            this.ChooseLinkLabel = new System.Windows.Forms.LinkLabel();
            this.UnsignLinkLabel = new System.Windows.Forms.LinkLabel();
            this.MyCancelButton = new System.Windows.Forms.Button();
            this.PlayersFlowLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.TeamNameLabel = new System.Windows.Forms.Label();
            this.HeightUpDown = new System.Windows.Forms.NumericUpDown();
            this.TeamLabel = new System.Windows.Forms.Label();
            this.PlayersImageasdf = new System.Windows.Forms.PictureBox();
            this.HeightLabel = new System.Windows.Forms.Label();
            this.LastNameTextBox = new System.Windows.Forms.TextBox();
            this.LastNameLabel = new System.Windows.Forms.Label();
            this.FirstNameTextBox = new System.Windows.Forms.TextBox();
            this.FirstNameLabel = new System.Windows.Forms.Label();
            this.PlayerLabel = new System.Windows.Forms.Label();
            this.PlayerImage = new System.Windows.Forms.PictureBox();
            this.WeightUpDown = new System.Windows.Forms.NumericUpDown();
            this.WeightLabel = new System.Windows.Forms.Label();
            this.DateOfBirthLabel = new System.Windows.Forms.Label();
            this.PlaceOfBirthTextBox = new System.Windows.Forms.TextBox();
            this.PlaceOfBirthLabel = new System.Windows.Forms.Label();
            this.DateOfBirthDatePicker = new System.Windows.Forms.DateTimePicker();
            this.TeamContextMenuStrip.SuspendLayout();
            this.PlayersFlowLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HeightUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlayersImageasdf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlayerImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WeightUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // TeamContextMenuStrip
            // 
            this.TeamContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2});
            this.TeamContextMenuStrip.Name = "TeamContextMenuStrip";
            this.TeamContextMenuStrip.Size = new System.Drawing.Size(80, 26);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(79, 22);
            this.toolStripMenuItem2.Text = "\\";
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(168, 409);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 14;
            this.OkButton.Text = "&OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // ChooseLinkLabel
            // 
            this.ChooseLinkLabel.AutoSize = true;
            this.ChooseLinkLabel.Location = new System.Drawing.Point(93, 0);
            this.ChooseLinkLabel.Name = "ChooseLinkLabel";
            this.ChooseLinkLabel.Size = new System.Drawing.Size(52, 13);
            this.ChooseLinkLabel.TabIndex = 0;
            this.ChooseLinkLabel.TabStop = true;
            this.ChooseLinkLabel.Text = "Choose...";
            this.ChooseLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ChooseLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.TeamNameLinkLabel_LinkClicked);
            // 
            // UnsignLinkLabel
            // 
            this.UnsignLinkLabel.AutoSize = true;
            this.UnsignLinkLabel.Location = new System.Drawing.Point(151, 0);
            this.UnsignLinkLabel.Name = "UnsignLinkLabel";
            this.UnsignLinkLabel.Size = new System.Drawing.Size(40, 13);
            this.UnsignLinkLabel.TabIndex = 1;
            this.UnsignLinkLabel.TabStop = true;
            this.UnsignLinkLabel.Text = "Unsign";
            this.UnsignLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.UnsignLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.UnsignLinkLabel_LinkClicked);
            // 
            // MyCancelButton
            // 
            this.MyCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.MyCancelButton.Location = new System.Drawing.Point(249, 409);
            this.MyCancelButton.Name = "MyCancelButton";
            this.MyCancelButton.Size = new System.Drawing.Size(75, 23);
            this.MyCancelButton.TabIndex = 15;
            this.MyCancelButton.Text = "&Cancel";
            this.MyCancelButton.UseVisualStyleBackColor = true;
            // 
            // PlayersFlowLayout
            // 
            this.PlayersFlowLayout.AutoSize = true;
            this.PlayersFlowLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PlayersFlowLayout.Controls.Add(this.TeamNameLabel);
            this.PlayersFlowLayout.Controls.Add(this.ChooseLinkLabel);
            this.PlayersFlowLayout.Controls.Add(this.UnsignLinkLabel);
            this.PlayersFlowLayout.Location = new System.Drawing.Point(53, 370);
            this.PlayersFlowLayout.Name = "PlayersFlowLayout";
            this.PlayersFlowLayout.Size = new System.Drawing.Size(194, 16);
            this.PlayersFlowLayout.TabIndex = 34;
            // 
            // TeamNameLabel
            // 
            this.TeamNameLabel.AutoSize = true;
            this.TeamNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TeamNameLabel.Location = new System.Drawing.Point(3, 0);
            this.TeamNameLabel.Name = "TeamNameLabel";
            this.TeamNameLabel.Size = new System.Drawing.Size(84, 16);
            this.TeamNameLabel.TabIndex = 2;
            this.TeamNameLabel.Text = "Team Name";
            this.TeamNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // HeightUpDown
            // 
            this.HeightUpDown.Location = new System.Drawing.Point(53, 161);
            this.HeightUpDown.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.HeightUpDown.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.HeightUpDown.Name = "HeightUpDown";
            this.HeightUpDown.Size = new System.Drawing.Size(228, 20);
            this.HeightUpDown.TabIndex = 6;
            this.HeightUpDown.Value = new decimal(new int[] {
            182,
            0,
            0,
            0});
            // 
            // TeamLabel
            // 
            this.TeamLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TeamLabel.Location = new System.Drawing.Point(50, 335);
            this.TeamLabel.Name = "TeamLabel";
            this.TeamLabel.Size = new System.Drawing.Size(231, 32);
            this.TeamLabel.TabIndex = 13;
            this.TeamLabel.Text = "Team";
            this.TeamLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PlayersImageasdf
            // 
            this.PlayersImageasdf.Image = global::RugbyGui.Properties.Resources.Team32x32;
            this.PlayersImageasdf.Location = new System.Drawing.Point(12, 335);
            this.PlayersImageasdf.Name = "PlayersImageasdf";
            this.PlayersImageasdf.Size = new System.Drawing.Size(32, 32);
            this.PlayersImageasdf.TabIndex = 33;
            this.PlayersImageasdf.TabStop = false;
            // 
            // HeightLabel
            // 
            this.HeightLabel.AutoSize = true;
            this.HeightLabel.Location = new System.Drawing.Point(50, 145);
            this.HeightLabel.Name = "HeightLabel";
            this.HeightLabel.Size = new System.Drawing.Size(64, 13);
            this.HeightLabel.TabIndex = 5;
            this.HeightLabel.Text = "&Height (cm):";
            // 
            // LastNameTextBox
            // 
            this.LastNameTextBox.Location = new System.Drawing.Point(53, 116);
            this.LastNameTextBox.Name = "LastNameTextBox";
            this.LastNameTextBox.Size = new System.Drawing.Size(228, 20);
            this.LastNameTextBox.TabIndex = 4;
            // 
            // LastNameLabel
            // 
            this.LastNameLabel.AutoSize = true;
            this.LastNameLabel.Location = new System.Drawing.Point(50, 100);
            this.LastNameLabel.Name = "LastNameLabel";
            this.LastNameLabel.Size = new System.Drawing.Size(61, 13);
            this.LastNameLabel.TabIndex = 3;
            this.LastNameLabel.Text = "&Last Name:";
            // 
            // FirstNameTextBox
            // 
            this.FirstNameTextBox.Location = new System.Drawing.Point(53, 71);
            this.FirstNameTextBox.Name = "FirstNameTextBox";
            this.FirstNameTextBox.Size = new System.Drawing.Size(228, 20);
            this.FirstNameTextBox.TabIndex = 2;
            // 
            // FirstNameLabel
            // 
            this.FirstNameLabel.AutoSize = true;
            this.FirstNameLabel.Location = new System.Drawing.Point(50, 55);
            this.FirstNameLabel.Name = "FirstNameLabel";
            this.FirstNameLabel.Size = new System.Drawing.Size(60, 13);
            this.FirstNameLabel.TabIndex = 1;
            this.FirstNameLabel.Text = "&First Name:";
            // 
            // PlayerLabel
            // 
            this.PlayerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerLabel.Location = new System.Drawing.Point(50, 12);
            this.PlayerLabel.Name = "PlayerLabel";
            this.PlayerLabel.Size = new System.Drawing.Size(231, 32);
            this.PlayerLabel.TabIndex = 0;
            this.PlayerLabel.Text = "Player";
            this.PlayerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PlayerImage
            // 
            this.PlayerImage.Image = global::RugbyGui.Properties.Resources.Player32x32;
            this.PlayerImage.Location = new System.Drawing.Point(12, 12);
            this.PlayerImage.Name = "PlayerImage";
            this.PlayerImage.Size = new System.Drawing.Size(32, 32);
            this.PlayerImage.TabIndex = 19;
            this.PlayerImage.TabStop = false;
            // 
            // WeightUpDown
            // 
            this.WeightUpDown.Location = new System.Drawing.Point(53, 206);
            this.WeightUpDown.Maximum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.WeightUpDown.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.WeightUpDown.Name = "WeightUpDown";
            this.WeightUpDown.Size = new System.Drawing.Size(228, 20);
            this.WeightUpDown.TabIndex = 8;
            this.WeightUpDown.Value = new decimal(new int[] {
            85,
            0,
            0,
            0});
            // 
            // WeightLabel
            // 
            this.WeightLabel.AutoSize = true;
            this.WeightLabel.Location = new System.Drawing.Point(50, 190);
            this.WeightLabel.Name = "WeightLabel";
            this.WeightLabel.Size = new System.Drawing.Size(65, 13);
            this.WeightLabel.TabIndex = 7;
            this.WeightLabel.Text = "&Weight (kg):";
            // 
            // DateOfBirthLabel
            // 
            this.DateOfBirthLabel.AutoSize = true;
            this.DateOfBirthLabel.Location = new System.Drawing.Point(50, 235);
            this.DateOfBirthLabel.Name = "DateOfBirthLabel";
            this.DateOfBirthLabel.Size = new System.Drawing.Size(69, 13);
            this.DateOfBirthLabel.TabIndex = 9;
            this.DateOfBirthLabel.Text = "&Date of Birth:";
            // 
            // PlaceOfBirthTextBox
            // 
            this.PlaceOfBirthTextBox.Location = new System.Drawing.Point(53, 296);
            this.PlaceOfBirthTextBox.Name = "PlaceOfBirthTextBox";
            this.PlaceOfBirthTextBox.Size = new System.Drawing.Size(228, 20);
            this.PlaceOfBirthTextBox.TabIndex = 12;
            // 
            // PlaceOfBirthLabel
            // 
            this.PlaceOfBirthLabel.AutoSize = true;
            this.PlaceOfBirthLabel.Location = new System.Drawing.Point(50, 280);
            this.PlaceOfBirthLabel.Name = "PlaceOfBirthLabel";
            this.PlaceOfBirthLabel.Size = new System.Drawing.Size(73, 13);
            this.PlaceOfBirthLabel.TabIndex = 11;
            this.PlaceOfBirthLabel.Text = "&Place of Birth:";
            // 
            // DateOfBirthDatePicker
            // 
            this.DateOfBirthDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DateOfBirthDatePicker.Location = new System.Drawing.Point(53, 251);
            this.DateOfBirthDatePicker.Name = "DateOfBirthDatePicker";
            this.DateOfBirthDatePicker.Size = new System.Drawing.Size(228, 20);
            this.DateOfBirthDatePicker.TabIndex = 10;
            // 
            // PlayerDialog
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.MyCancelButton;
            this.ClientSize = new System.Drawing.Size(336, 450);
            this.Controls.Add(this.DateOfBirthDatePicker);
            this.Controls.Add(this.PlaceOfBirthTextBox);
            this.Controls.Add(this.PlaceOfBirthLabel);
            this.Controls.Add(this.DateOfBirthLabel);
            this.Controls.Add(this.WeightUpDown);
            this.Controls.Add(this.WeightLabel);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.MyCancelButton);
            this.Controls.Add(this.PlayersFlowLayout);
            this.Controls.Add(this.HeightUpDown);
            this.Controls.Add(this.TeamLabel);
            this.Controls.Add(this.PlayersImageasdf);
            this.Controls.Add(this.HeightLabel);
            this.Controls.Add(this.LastNameTextBox);
            this.Controls.Add(this.LastNameLabel);
            this.Controls.Add(this.FirstNameTextBox);
            this.Controls.Add(this.FirstNameLabel);
            this.Controls.Add(this.PlayerLabel);
            this.Controls.Add(this.PlayerImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PlayerDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Player";
            this.Load += new System.EventHandler(this.PlayerDialog_Load);
            this.TeamContextMenuStrip.ResumeLayout(false);
            this.PlayersFlowLayout.ResumeLayout(false);
            this.PlayersFlowLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HeightUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlayersImageasdf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlayerImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WeightUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip TeamContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.LinkLabel ChooseLinkLabel;
        private System.Windows.Forms.LinkLabel UnsignLinkLabel;
        private System.Windows.Forms.Button MyCancelButton;
        private System.Windows.Forms.FlowLayoutPanel PlayersFlowLayout;
        private System.Windows.Forms.NumericUpDown HeightUpDown;
        private System.Windows.Forms.Label TeamLabel;
        private System.Windows.Forms.PictureBox PlayersImageasdf;
        private System.Windows.Forms.Label HeightLabel;
        private System.Windows.Forms.TextBox LastNameTextBox;
        private System.Windows.Forms.Label LastNameLabel;
        private System.Windows.Forms.TextBox FirstNameTextBox;
        private System.Windows.Forms.Label FirstNameLabel;
        private System.Windows.Forms.Label PlayerLabel;
        private System.Windows.Forms.PictureBox PlayerImage;
        private System.Windows.Forms.NumericUpDown WeightUpDown;
        private System.Windows.Forms.Label WeightLabel;
        private System.Windows.Forms.Label DateOfBirthLabel;
        private System.Windows.Forms.TextBox PlaceOfBirthTextBox;
        private System.Windows.Forms.Label PlaceOfBirthLabel;
        private System.Windows.Forms.DateTimePicker DateOfBirthDatePicker;
        private System.Windows.Forms.Label TeamNameLabel;
    }
}