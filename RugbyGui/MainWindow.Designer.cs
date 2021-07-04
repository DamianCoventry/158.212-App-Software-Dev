
namespace RugbyGui
{
    partial class MainWindow
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Teams", 1, 1);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Players", 2, 2);
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Signed Players", 3, 3);
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Rugby Union", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.MyMainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.NewRugbyUnion = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenRugbyUnion = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseRugbyUnion = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.SaveRugbyUnion = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsRugbyUnion = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.RugbyUnionProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.RecentFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.ApplicationExit = new System.Windows.Forms.ToolStripMenuItem();
            this.EditMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.CutSelectedItemsToClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.CopySelectedItemsToClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.PasteFromClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteSelectedItems = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.SelectAllItems = new System.Windows.Forms.ToolStripMenuItem();
            this.UnselectAllItems = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.FindAndReplace = new System.Windows.Forms.ToolStripMenuItem();
            this.FindItems = new System.Windows.Forms.ToolStripMenuItem();
            this.ReplaceItems = new System.Windows.Forms.ToolStripMenuItem();
            this.AdvancedFindAndReplace = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewRugbyUnion = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewTeams = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewPlayers = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewSignedPlayers = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewFindResults = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewCharts = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewStatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.TeamMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.NewTeam = new System.Windows.Forms.ToolStripMenuItem();
            this.EditTeam = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.SignPlayerToTeam = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem17 = new System.Windows.Forms.ToolStripSeparator();
            this.ImportTeam = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportTeam = new System.Windows.Forms.ToolStripMenuItem();
            this.PlayerMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.NewPlayer = new System.Windows.Forms.ToolStripMenuItem();
            this.EditPlayer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.SignPlayerWithTeam = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem18 = new System.Windows.Forms.ToolStripSeparator();
            this.ImportPlayers = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportPlayers = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenUserManual = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
            this.ViewAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.MainToolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.MainStatusBar = new System.Windows.Forms.StatusStrip();
            this.StatusBarMessageLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.TreeViewHeader = new System.Windows.Forms.Panel();
            this.TreeViewCloseButton = new System.Windows.Forms.Button();
            this.TreeViewHeaderLabel = new System.Windows.Forms.Label();
            this.RugbyUnionTreeView = new System.Windows.Forms.TreeView();
            this.RugbyUnionContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NewItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteAllItems = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripSeparator();
            this.SignPlayerToTeam2 = new System.Windows.Forms.ToolStripMenuItem();
            this.SignPlayerWithTeam2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripSeparator();
            this.ItemProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.RugbyUnionImageList = new System.Windows.Forms.ImageList(this.components);
            this.FindResultsSplitContainer = new System.Windows.Forms.SplitContainer();
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.TeamsTabPage = new System.Windows.Forms.TabPage();
            this.TeamsListView = new System.Windows.Forms.ListView();
            this.TeamName1ColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HomeGroundColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CoachColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.YearFoundedColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RegionColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TeamsListViewContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NewTeamMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteTeamMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteAllTeamsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripSeparator();
            this.SignPlayerToTeamMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripSeparator();
            this.TeamPropertiesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PlayersTabPage = new System.Windows.Forms.TabPage();
            this.PlayersListView = new System.Windows.Forms.ListView();
            this.IdColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FirstNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LastNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HeightColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.WeightColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DateOfBirthColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PlaceOfBirthColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PlayersListViewContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NewPlayerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeletePlayerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteAllPlayersMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripSeparator();
            this.SignWithTeamMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem15 = new System.Windows.Forms.ToolStripSeparator();
            this.PlayerPropertiesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SignedPlayersTabPage = new System.Windows.Forms.TabPage();
            this.SignedPlayersListView = new System.Windows.Forms.ListView();
            this.PlayerIdColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PlayerNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TeamName2ColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SignedPlayersListViewContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NewSignedPlayerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteSignedPlayerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteAllSignedPlayersMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem16 = new System.Windows.Forms.ToolStripSeparator();
            this.SignedPlayerPropertiesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChartsTabPage = new System.Windows.Forms.TabPage();
            this.MainChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.HeaderPanel = new System.Windows.Forms.Panel();
            this.ChartsLabel = new System.Windows.Forms.Label();
            this.ChartDropList = new System.Windows.Forms.ComboBox();
            this.FindResultsListBox = new System.Windows.Forms.ListBox();
            this.FindResultsContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.LocateFindResultItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearFindResults = new System.Windows.Forms.ToolStripMenuItem();
            this.FindResultsHeader = new System.Windows.Forms.Panel();
            this.FindResultsCloseButton = new System.Windows.Forms.Button();
            this.FindResultsHeaderLabel = new System.Windows.Forms.Label();
            this.EmptyWorkspacePanel = new System.Windows.Forms.Panel();
            this.RugbyUnionToolbar = new System.Windows.Forms.ToolStrip();
            this.NewRugbyUnionButton = new System.Windows.Forms.ToolStripButton();
            this.OpenRugbyUnionButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SaveRugbyUnionButton = new System.Windows.Forms.ToolStripButton();
            this.RugbyUnionPropertiesButton = new System.Windows.Forms.ToolStripButton();
            this.PlayerToolbar = new System.Windows.Forms.ToolStrip();
            this.NewPlayerButton = new System.Windows.Forms.ToolStripButton();
            this.EditPlayerButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.SignPlayerWithTeamButton = new System.Windows.Forms.ToolStripButton();
            this.TeamToolbar = new System.Windows.Forms.ToolStrip();
            this.NewTeamButton = new System.Windows.Forms.ToolStripButton();
            this.EditTeamButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.SignPlayerToTeamButton = new System.Windows.Forms.ToolStripButton();
            this.SeriesColourPicker = new System.Windows.Forms.ColorDialog();
            this.MyMainMenuStrip.SuspendLayout();
            this.MainToolStripContainer.BottomToolStripPanel.SuspendLayout();
            this.MainToolStripContainer.ContentPanel.SuspendLayout();
            this.MainToolStripContainer.TopToolStripPanel.SuspendLayout();
            this.MainToolStripContainer.SuspendLayout();
            this.MainStatusBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).BeginInit();
            this.MainSplitContainer.Panel1.SuspendLayout();
            this.MainSplitContainer.Panel2.SuspendLayout();
            this.MainSplitContainer.SuspendLayout();
            this.TreeViewHeader.SuspendLayout();
            this.RugbyUnionContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FindResultsSplitContainer)).BeginInit();
            this.FindResultsSplitContainer.Panel1.SuspendLayout();
            this.FindResultsSplitContainer.Panel2.SuspendLayout();
            this.FindResultsSplitContainer.SuspendLayout();
            this.MainTabControl.SuspendLayout();
            this.TeamsTabPage.SuspendLayout();
            this.TeamsListViewContextMenu.SuspendLayout();
            this.PlayersTabPage.SuspendLayout();
            this.PlayersListViewContextMenu.SuspendLayout();
            this.SignedPlayersTabPage.SuspendLayout();
            this.SignedPlayersListViewContextMenu.SuspendLayout();
            this.ChartsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainChart)).BeginInit();
            this.HeaderPanel.SuspendLayout();
            this.FindResultsContextMenu.SuspendLayout();
            this.FindResultsHeader.SuspendLayout();
            this.RugbyUnionToolbar.SuspendLayout();
            this.PlayerToolbar.SuspendLayout();
            this.TeamToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // MyMainMenuStrip
            // 
            this.MyMainMenuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.MyMainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu,
            this.EditMenu,
            this.ViewMenu,
            this.TeamMenu,
            this.PlayerMenu,
            this.HelpMenu});
            this.MyMainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MyMainMenuStrip.Name = "MyMainMenuStrip";
            this.MyMainMenuStrip.Size = new System.Drawing.Size(1387, 24);
            this.MyMainMenuStrip.TabIndex = 0;
            this.MyMainMenuStrip.Text = "MainMenuStrip";
            // 
            // FileMenu
            // 
            this.FileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewRugbyUnion,
            this.OpenRugbyUnion,
            this.CloseRugbyUnion,
            this.toolStripMenuItem1,
            this.SaveRugbyUnion,
            this.SaveAsRugbyUnion,
            this.toolStripMenuItem2,
            this.RugbyUnionProperties,
            this.toolStripMenuItem3,
            this.RecentFiles,
            this.toolStripMenuItem4,
            this.ApplicationExit});
            this.FileMenu.Name = "FileMenu";
            this.FileMenu.Size = new System.Drawing.Size(37, 20);
            this.FileMenu.Text = "&File";
            // 
            // NewRugbyUnion
            // 
            this.NewRugbyUnion.Image = global::RugbyGui.Properties.Resources.New16x16;
            this.NewRugbyUnion.Name = "NewRugbyUnion";
            this.NewRugbyUnion.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.NewRugbyUnion.Size = new System.Drawing.Size(195, 22);
            this.NewRugbyUnion.Text = "&New...";
            this.NewRugbyUnion.Click += new System.EventHandler(this.NewRugbyUnion_Click);
            // 
            // OpenRugbyUnion
            // 
            this.OpenRugbyUnion.Image = global::RugbyGui.Properties.Resources.Open16x16;
            this.OpenRugbyUnion.Name = "OpenRugbyUnion";
            this.OpenRugbyUnion.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.OpenRugbyUnion.Size = new System.Drawing.Size(195, 22);
            this.OpenRugbyUnion.Text = "&Open...";
            this.OpenRugbyUnion.Click += new System.EventHandler(this.OpenRugbyUnion_Click);
            // 
            // CloseRugbyUnion
            // 
            this.CloseRugbyUnion.Name = "CloseRugbyUnion";
            this.CloseRugbyUnion.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
            this.CloseRugbyUnion.Size = new System.Drawing.Size(195, 22);
            this.CloseRugbyUnion.Text = "&Close";
            this.CloseRugbyUnion.Click += new System.EventHandler(this.CloseRugbyUnion_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(192, 6);
            // 
            // SaveRugbyUnion
            // 
            this.SaveRugbyUnion.Image = global::RugbyGui.Properties.Resources.Save16x16;
            this.SaveRugbyUnion.Name = "SaveRugbyUnion";
            this.SaveRugbyUnion.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SaveRugbyUnion.Size = new System.Drawing.Size(195, 22);
            this.SaveRugbyUnion.Text = "&Save";
            this.SaveRugbyUnion.Click += new System.EventHandler(this.SaveRugbyUnion_Click);
            // 
            // SaveAsRugbyUnion
            // 
            this.SaveAsRugbyUnion.Image = global::RugbyGui.Properties.Resources.SaveAll16x16;
            this.SaveAsRugbyUnion.Name = "SaveAsRugbyUnion";
            this.SaveAsRugbyUnion.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.SaveAsRugbyUnion.Size = new System.Drawing.Size(195, 22);
            this.SaveAsRugbyUnion.Text = "Save &As...";
            this.SaveAsRugbyUnion.Click += new System.EventHandler(this.SaveAsRugbyUnion_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(192, 6);
            // 
            // RugbyUnionProperties
            // 
            this.RugbyUnionProperties.Image = global::RugbyGui.Properties.Resources.Properties16x16;
            this.RugbyUnionProperties.Name = "RugbyUnionProperties";
            this.RugbyUnionProperties.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.RugbyUnionProperties.Size = new System.Drawing.Size(195, 22);
            this.RugbyUnionProperties.Text = "&Project Properties...";
            this.RugbyUnionProperties.Click += new System.EventHandler(this.RugbyUnionProperties_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(192, 6);
            // 
            // RecentFiles
            // 
            this.RecentFiles.Name = "RecentFiles";
            this.RecentFiles.Size = new System.Drawing.Size(195, 22);
            this.RecentFiles.Text = "&Recent Files";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(192, 6);
            // 
            // ApplicationExit
            // 
            this.ApplicationExit.Name = "ApplicationExit";
            this.ApplicationExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.ApplicationExit.Size = new System.Drawing.Size(195, 22);
            this.ApplicationExit.Text = "E&xit";
            this.ApplicationExit.Click += new System.EventHandler(this.ApplicationExit_Click);
            // 
            // EditMenu
            // 
            this.EditMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CutSelectedItemsToClipboard,
            this.CopySelectedItemsToClipboard,
            this.PasteFromClipboard,
            this.DeleteSelectedItems,
            this.toolStripMenuItem5,
            this.SelectAllItems,
            this.UnselectAllItems,
            this.toolStripMenuItem6,
            this.FindAndReplace});
            this.EditMenu.Name = "EditMenu";
            this.EditMenu.Size = new System.Drawing.Size(39, 20);
            this.EditMenu.Text = "&Edit";
            // 
            // CutSelectedItemsToClipboard
            // 
            this.CutSelectedItemsToClipboard.Image = global::RugbyGui.Properties.Resources.Cut16x16;
            this.CutSelectedItemsToClipboard.Name = "CutSelectedItemsToClipboard";
            this.CutSelectedItemsToClipboard.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.CutSelectedItemsToClipboard.Size = new System.Drawing.Size(178, 22);
            this.CutSelectedItemsToClipboard.Text = "Cu&t";
            this.CutSelectedItemsToClipboard.Click += new System.EventHandler(this.CutSelectedItemsToClipboard_Click);
            // 
            // CopySelectedItemsToClipboard
            // 
            this.CopySelectedItemsToClipboard.Image = global::RugbyGui.Properties.Resources.Copy16x16;
            this.CopySelectedItemsToClipboard.Name = "CopySelectedItemsToClipboard";
            this.CopySelectedItemsToClipboard.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.CopySelectedItemsToClipboard.Size = new System.Drawing.Size(178, 22);
            this.CopySelectedItemsToClipboard.Text = "&Copy";
            this.CopySelectedItemsToClipboard.Click += new System.EventHandler(this.CopySelectedItemsToClipboard_Click);
            // 
            // PasteFromClipboard
            // 
            this.PasteFromClipboard.Image = global::RugbyGui.Properties.Resources.Paste16x16;
            this.PasteFromClipboard.Name = "PasteFromClipboard";
            this.PasteFromClipboard.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.PasteFromClipboard.Size = new System.Drawing.Size(178, 22);
            this.PasteFromClipboard.Text = "&Paste";
            this.PasteFromClipboard.Click += new System.EventHandler(this.PasteFromClipboard_Click);
            // 
            // DeleteSelectedItems
            // 
            this.DeleteSelectedItems.Image = global::RugbyGui.Properties.Resources.Delete16x16;
            this.DeleteSelectedItems.Name = "DeleteSelectedItems";
            this.DeleteSelectedItems.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.DeleteSelectedItems.Size = new System.Drawing.Size(178, 22);
            this.DeleteSelectedItems.Text = "&Delete";
            this.DeleteSelectedItems.Click += new System.EventHandler(this.DeleteSelectedItems_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(175, 6);
            // 
            // SelectAllItems
            // 
            this.SelectAllItems.Image = global::RugbyGui.Properties.Resources.SelectAll16x16;
            this.SelectAllItems.Name = "SelectAllItems";
            this.SelectAllItems.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.SelectAllItems.Size = new System.Drawing.Size(178, 22);
            this.SelectAllItems.Text = "&Select All";
            this.SelectAllItems.Click += new System.EventHandler(this.SelectAllItems_Click);
            // 
            // UnselectAllItems
            // 
            this.UnselectAllItems.Name = "UnselectAllItems";
            this.UnselectAllItems.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.UnselectAllItems.Size = new System.Drawing.Size(178, 22);
            this.UnselectAllItems.Text = "&Unselect All";
            this.UnselectAllItems.Click += new System.EventHandler(this.UnselectAllItems_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(175, 6);
            // 
            // FindAndReplace
            // 
            this.FindAndReplace.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FindItems,
            this.ReplaceItems,
            this.AdvancedFindAndReplace});
            this.FindAndReplace.Name = "FindAndReplace";
            this.FindAndReplace.Size = new System.Drawing.Size(178, 22);
            this.FindAndReplace.Text = "&Find and Replace";
            // 
            // FindItems
            // 
            this.FindItems.Image = global::RugbyGui.Properties.Resources.Search16x16;
            this.FindItems.Name = "FindItems";
            this.FindItems.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.FindItems.Size = new System.Drawing.Size(208, 22);
            this.FindItems.Text = "&Find...";
            this.FindItems.Click += new System.EventHandler(this.FindItems_Click);
            // 
            // ReplaceItems
            // 
            this.ReplaceItems.Image = global::RugbyGui.Properties.Resources.Search16x16;
            this.ReplaceItems.Name = "ReplaceItems";
            this.ReplaceItems.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.ReplaceItems.Size = new System.Drawing.Size(208, 22);
            this.ReplaceItems.Text = "&Replace...";
            this.ReplaceItems.Click += new System.EventHandler(this.ReplaceItems_Click);
            // 
            // AdvancedFindAndReplace
            // 
            this.AdvancedFindAndReplace.Name = "AdvancedFindAndReplace";
            this.AdvancedFindAndReplace.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.F)));
            this.AdvancedFindAndReplace.Size = new System.Drawing.Size(208, 22);
            this.AdvancedFindAndReplace.Text = "&Advanced...";
            this.AdvancedFindAndReplace.Click += new System.EventHandler(this.AdvancedFindAndReplace_Click);
            // 
            // ViewMenu
            // 
            this.ViewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewRugbyUnion,
            this.ViewTeams,
            this.ViewPlayers,
            this.ViewSignedPlayers,
            this.ViewFindResults,
            this.ViewCharts,
            this.ViewStatusBar});
            this.ViewMenu.Name = "ViewMenu";
            this.ViewMenu.Size = new System.Drawing.Size(44, 20);
            this.ViewMenu.Text = "&View";
            // 
            // ViewRugbyUnion
            // 
            this.ViewRugbyUnion.Image = global::RugbyGui.Properties.Resources.RugbyBall16x16;
            this.ViewRugbyUnion.Name = "ViewRugbyUnion";
            this.ViewRugbyUnion.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.ViewRugbyUnion.Size = new System.Drawing.Size(169, 22);
            this.ViewRugbyUnion.Text = "&Rugby Union";
            this.ViewRugbyUnion.Click += new System.EventHandler(this.ViewRugbyUnion_Click);
            // 
            // ViewTeams
            // 
            this.ViewTeams.Image = global::RugbyGui.Properties.Resources.Team16x16;
            this.ViewTeams.Name = "ViewTeams";
            this.ViewTeams.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.ViewTeams.Size = new System.Drawing.Size(169, 22);
            this.ViewTeams.Text = "&Teams";
            this.ViewTeams.Click += new System.EventHandler(this.ViewTeams_Click);
            // 
            // ViewPlayers
            // 
            this.ViewPlayers.Image = global::RugbyGui.Properties.Resources.Player16x16;
            this.ViewPlayers.Name = "ViewPlayers";
            this.ViewPlayers.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.ViewPlayers.Size = new System.Drawing.Size(169, 22);
            this.ViewPlayers.Text = "&Players";
            this.ViewPlayers.Click += new System.EventHandler(this.ViewPlayers_Click);
            // 
            // ViewSignedPlayers
            // 
            this.ViewSignedPlayers.Image = global::RugbyGui.Properties.Resources.Signed16x16;
            this.ViewSignedPlayers.Name = "ViewSignedPlayers";
            this.ViewSignedPlayers.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.ViewSignedPlayers.Size = new System.Drawing.Size(169, 22);
            this.ViewSignedPlayers.Text = "&Signed Players";
            this.ViewSignedPlayers.Click += new System.EventHandler(this.ViewSignedPlayers_Click);
            // 
            // ViewFindResults
            // 
            this.ViewFindResults.Image = global::RugbyGui.Properties.Resources.Search16x16;
            this.ViewFindResults.Name = "ViewFindResults";
            this.ViewFindResults.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.ViewFindResults.Size = new System.Drawing.Size(169, 22);
            this.ViewFindResults.Text = "&Find Results";
            this.ViewFindResults.Click += new System.EventHandler(this.ViewFindResults_Click);
            // 
            // ViewCharts
            // 
            this.ViewCharts.Image = global::RugbyGui.Properties.Resources.Chart16x16;
            this.ViewCharts.Name = "ViewCharts";
            this.ViewCharts.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.ViewCharts.Size = new System.Drawing.Size(169, 22);
            this.ViewCharts.Text = "&Charts";
            this.ViewCharts.Click += new System.EventHandler(this.ViewCharts_Click);
            // 
            // ViewStatusBar
            // 
            this.ViewStatusBar.Checked = true;
            this.ViewStatusBar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ViewStatusBar.Name = "ViewStatusBar";
            this.ViewStatusBar.Size = new System.Drawing.Size(169, 22);
            this.ViewStatusBar.Text = "Status &Bar";
            this.ViewStatusBar.Click += new System.EventHandler(this.ViewStatusBar_Click);
            // 
            // TeamMenu
            // 
            this.TeamMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewTeam,
            this.EditTeam,
            this.toolStripMenuItem7,
            this.SignPlayerToTeam,
            this.toolStripMenuItem17,
            this.ImportTeam,
            this.ExportTeam});
            this.TeamMenu.Name = "TeamMenu";
            this.TeamMenu.Size = new System.Drawing.Size(47, 20);
            this.TeamMenu.Text = "&Team";
            // 
            // NewTeam
            // 
            this.NewTeam.Image = global::RugbyGui.Properties.Resources.Team16x16;
            this.NewTeam.Name = "NewTeam";
            this.NewTeam.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.T)));
            this.NewTeam.Size = new System.Drawing.Size(254, 22);
            this.NewTeam.Text = "&New...";
            this.NewTeam.Click += new System.EventHandler(this.NewTeam_Click);
            // 
            // EditTeam
            // 
            this.EditTeam.Image = global::RugbyGui.Properties.Resources.Edit16x16;
            this.EditTeam.Name = "EditTeam";
            this.EditTeam.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.T)));
            this.EditTeam.Size = new System.Drawing.Size(254, 22);
            this.EditTeam.Text = "&Edit...";
            this.EditTeam.Click += new System.EventHandler(this.EditTeam_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(251, 6);
            // 
            // SignPlayerToTeam
            // 
            this.SignPlayerToTeam.Image = global::RugbyGui.Properties.Resources.Signed16x16;
            this.SignPlayerToTeam.Name = "SignPlayerToTeam";
            this.SignPlayerToTeam.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.T)));
            this.SignPlayerToTeam.Size = new System.Drawing.Size(254, 22);
            this.SignPlayerToTeam.Text = "&Sign Player to Team...";
            this.SignPlayerToTeam.Click += new System.EventHandler(this.SignPlayerToTeam_Click);
            // 
            // toolStripMenuItem17
            // 
            this.toolStripMenuItem17.Name = "toolStripMenuItem17";
            this.toolStripMenuItem17.Size = new System.Drawing.Size(251, 6);
            // 
            // ImportTeam
            // 
            this.ImportTeam.Image = global::RugbyGui.Properties.Resources.Open16x16;
            this.ImportTeam.Name = "ImportTeam";
            this.ImportTeam.Size = new System.Drawing.Size(254, 22);
            this.ImportTeam.Text = "&Import...";
            this.ImportTeam.Click += new System.EventHandler(this.ImportTeam_Click);
            // 
            // ExportTeam
            // 
            this.ExportTeam.Image = global::RugbyGui.Properties.Resources.Save16x16;
            this.ExportTeam.Name = "ExportTeam";
            this.ExportTeam.Size = new System.Drawing.Size(254, 22);
            this.ExportTeam.Text = "E&xport...";
            this.ExportTeam.Click += new System.EventHandler(this.ExportTeam_Click);
            // 
            // PlayerMenu
            // 
            this.PlayerMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewPlayer,
            this.EditPlayer,
            this.toolStripMenuItem8,
            this.SignPlayerWithTeam,
            this.toolStripMenuItem18,
            this.ImportPlayers,
            this.ExportPlayers});
            this.PlayerMenu.Name = "PlayerMenu";
            this.PlayerMenu.Size = new System.Drawing.Size(51, 20);
            this.PlayerMenu.Text = "&Player";
            // 
            // NewPlayer
            // 
            this.NewPlayer.Image = global::RugbyGui.Properties.Resources.Player16x16;
            this.NewPlayer.Name = "NewPlayer";
            this.NewPlayer.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.P)));
            this.NewPlayer.Size = new System.Drawing.Size(232, 22);
            this.NewPlayer.Text = "&New...";
            this.NewPlayer.Click += new System.EventHandler(this.NewPlayer_Click);
            // 
            // EditPlayer
            // 
            this.EditPlayer.Image = global::RugbyGui.Properties.Resources.Edit16x16;
            this.EditPlayer.Name = "EditPlayer";
            this.EditPlayer.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.P)));
            this.EditPlayer.Size = new System.Drawing.Size(232, 22);
            this.EditPlayer.Text = "&Edit...";
            this.EditPlayer.Click += new System.EventHandler(this.EditPlayer_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(229, 6);
            // 
            // SignPlayerWithTeam
            // 
            this.SignPlayerWithTeam.Image = global::RugbyGui.Properties.Resources.Signed16x16;
            this.SignPlayerWithTeam.Name = "SignPlayerWithTeam";
            this.SignPlayerWithTeam.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.P)));
            this.SignPlayerWithTeam.Size = new System.Drawing.Size(232, 22);
            this.SignPlayerWithTeam.Text = "&Sign with Team...";
            this.SignPlayerWithTeam.Click += new System.EventHandler(this.SignPlayerWithTeam_Click);
            // 
            // toolStripMenuItem18
            // 
            this.toolStripMenuItem18.Name = "toolStripMenuItem18";
            this.toolStripMenuItem18.Size = new System.Drawing.Size(229, 6);
            // 
            // ImportPlayers
            // 
            this.ImportPlayers.Image = global::RugbyGui.Properties.Resources.Open16x16;
            this.ImportPlayers.Name = "ImportPlayers";
            this.ImportPlayers.Size = new System.Drawing.Size(232, 22);
            this.ImportPlayers.Text = "&Import...";
            this.ImportPlayers.Click += new System.EventHandler(this.ImportPlayers_Click);
            // 
            // ExportPlayers
            // 
            this.ExportPlayers.Image = global::RugbyGui.Properties.Resources.Save16x16;
            this.ExportPlayers.Name = "ExportPlayers";
            this.ExportPlayers.Size = new System.Drawing.Size(232, 22);
            this.ExportPlayers.Text = "E&xport...";
            this.ExportPlayers.Click += new System.EventHandler(this.ExportPlayers_Click);
            // 
            // HelpMenu
            // 
            this.HelpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenUserManual,
            this.toolStripMenuItem9,
            this.ViewAbout});
            this.HelpMenu.Name = "HelpMenu";
            this.HelpMenu.Size = new System.Drawing.Size(44, 20);
            this.HelpMenu.Text = "&Help";
            // 
            // OpenUserManual
            // 
            this.OpenUserManual.Image = global::RugbyGui.Properties.Resources.UserManual16x16;
            this.OpenUserManual.Name = "OpenUserManual";
            this.OpenUserManual.Size = new System.Drawing.Size(149, 22);
            this.OpenUserManual.Text = "&User Manual...";
            this.OpenUserManual.Click += new System.EventHandler(this.OpenUserManual_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(146, 6);
            // 
            // ViewAbout
            // 
            this.ViewAbout.Image = global::RugbyGui.Properties.Resources.RugbyBall16x16;
            this.ViewAbout.Name = "ViewAbout";
            this.ViewAbout.Size = new System.Drawing.Size(149, 22);
            this.ViewAbout.Text = "&About...";
            this.ViewAbout.Click += new System.EventHandler(this.ViewAbout_Click);
            // 
            // MainToolStripContainer
            // 
            // 
            // MainToolStripContainer.BottomToolStripPanel
            // 
            this.MainToolStripContainer.BottomToolStripPanel.Controls.Add(this.MainStatusBar);
            // 
            // MainToolStripContainer.ContentPanel
            // 
            this.MainToolStripContainer.ContentPanel.Controls.Add(this.MainSplitContainer);
            this.MainToolStripContainer.ContentPanel.Controls.Add(this.EmptyWorkspacePanel);
            this.MainToolStripContainer.ContentPanel.Size = new System.Drawing.Size(1387, 808);
            this.MainToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainToolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.MainToolStripContainer.Name = "MainToolStripContainer";
            this.MainToolStripContainer.Size = new System.Drawing.Size(1387, 879);
            this.MainToolStripContainer.TabIndex = 1;
            this.MainToolStripContainer.Text = "toolStripContainer1";
            // 
            // MainToolStripContainer.TopToolStripPanel
            // 
            this.MainToolStripContainer.TopToolStripPanel.Controls.Add(this.MyMainMenuStrip);
            this.MainToolStripContainer.TopToolStripPanel.Controls.Add(this.RugbyUnionToolbar);
            this.MainToolStripContainer.TopToolStripPanel.Controls.Add(this.TeamToolbar);
            this.MainToolStripContainer.TopToolStripPanel.Controls.Add(this.PlayerToolbar);
            // 
            // MainStatusBar
            // 
            this.MainStatusBar.Dock = System.Windows.Forms.DockStyle.None;
            this.MainStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusBarMessageLabel});
            this.MainStatusBar.Location = new System.Drawing.Point(0, 0);
            this.MainStatusBar.Name = "MainStatusBar";
            this.MainStatusBar.Size = new System.Drawing.Size(1387, 22);
            this.MainStatusBar.TabIndex = 2;
            this.MainStatusBar.Text = "statusStrip1";
            // 
            // StatusBarMessageLabel
            // 
            this.StatusBarMessageLabel.Name = "StatusBarMessageLabel";
            this.StatusBarMessageLabel.Size = new System.Drawing.Size(42, 17);
            this.StatusBarMessageLabel.Text = "Ready.";
            // 
            // MainSplitContainer
            // 
            this.MainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.MainSplitContainer.Name = "MainSplitContainer";
            // 
            // MainSplitContainer.Panel1
            // 
            this.MainSplitContainer.Panel1.Controls.Add(this.TreeViewHeader);
            this.MainSplitContainer.Panel1.Controls.Add(this.RugbyUnionTreeView);
            // 
            // MainSplitContainer.Panel2
            // 
            this.MainSplitContainer.Panel2.Controls.Add(this.FindResultsSplitContainer);
            this.MainSplitContainer.Size = new System.Drawing.Size(1387, 808);
            this.MainSplitContainer.SplitterDistance = 268;
            this.MainSplitContainer.TabIndex = 2;
            // 
            // TreeViewHeader
            // 
            this.TreeViewHeader.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.TreeViewHeader.Controls.Add(this.TreeViewCloseButton);
            this.TreeViewHeader.Controls.Add(this.TreeViewHeaderLabel);
            this.TreeViewHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.TreeViewHeader.Location = new System.Drawing.Point(0, 0);
            this.TreeViewHeader.Name = "TreeViewHeader";
            this.TreeViewHeader.Size = new System.Drawing.Size(268, 27);
            this.TreeViewHeader.TabIndex = 1;
            // 
            // TreeViewCloseButton
            // 
            this.TreeViewCloseButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.TreeViewCloseButton.FlatAppearance.BorderSize = 0;
            this.TreeViewCloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TreeViewCloseButton.Location = new System.Drawing.Point(243, 0);
            this.TreeViewCloseButton.Name = "TreeViewCloseButton";
            this.TreeViewCloseButton.Size = new System.Drawing.Size(25, 27);
            this.TreeViewCloseButton.TabIndex = 1;
            this.TreeViewCloseButton.Text = "✖";
            this.TreeViewCloseButton.UseVisualStyleBackColor = true;
            this.TreeViewCloseButton.Click += new System.EventHandler(this.TreeViewCloseButton_Click);
            // 
            // TreeViewHeaderLabel
            // 
            this.TreeViewHeaderLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.TreeViewHeaderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TreeViewHeaderLabel.Location = new System.Drawing.Point(0, 0);
            this.TreeViewHeaderLabel.Name = "TreeViewHeaderLabel";
            this.TreeViewHeaderLabel.Size = new System.Drawing.Size(163, 27);
            this.TreeViewHeaderLabel.TabIndex = 0;
            this.TreeViewHeaderLabel.Text = "Rugby Union";
            this.TreeViewHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RugbyUnionTreeView
            // 
            this.RugbyUnionTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RugbyUnionTreeView.ContextMenuStrip = this.RugbyUnionContextMenu;
            this.RugbyUnionTreeView.HideSelection = false;
            this.RugbyUnionTreeView.ImageIndex = 0;
            this.RugbyUnionTreeView.ImageList = this.RugbyUnionImageList;
            this.RugbyUnionTreeView.Location = new System.Drawing.Point(0, 27);
            this.RugbyUnionTreeView.Name = "RugbyUnionTreeView";
            treeNode1.ImageIndex = 1;
            treeNode1.Name = "TeamsNode";
            treeNode1.SelectedImageIndex = 1;
            treeNode1.Text = "Teams";
            treeNode2.ImageIndex = 2;
            treeNode2.Name = "PlayersNode";
            treeNode2.SelectedImageIndex = 2;
            treeNode2.Text = "Players";
            treeNode3.ImageIndex = 3;
            treeNode3.Name = "SignedPlayersNode";
            treeNode3.SelectedImageIndex = 3;
            treeNode3.Text = "Signed Players";
            treeNode4.Name = "RugbyUnionNode";
            treeNode4.Text = "Rugby Union";
            this.RugbyUnionTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4});
            this.RugbyUnionTreeView.SelectedImageIndex = 0;
            this.RugbyUnionTreeView.ShowLines = false;
            this.RugbyUnionTreeView.Size = new System.Drawing.Size(268, 781);
            this.RugbyUnionTreeView.TabIndex = 0;
            this.RugbyUnionTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.RugbyUnionTreeView_AfterSelect);
            this.RugbyUnionTreeView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ItemProperties_Click);
            this.RugbyUnionTreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RugbyUnionTreeView_MouseDown);
            // 
            // RugbyUnionContextMenu
            // 
            this.RugbyUnionContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewItem,
            this.DeleteItem,
            this.DeleteAllItems,
            this.toolStripMenuItem10,
            this.SignPlayerToTeam2,
            this.SignPlayerWithTeam2,
            this.toolStripMenuItem11,
            this.ItemProperties});
            this.RugbyUnionContextMenu.Name = "RugbyUnionContextMenu";
            this.RugbyUnionContextMenu.Size = new System.Drawing.Size(164, 148);
            this.RugbyUnionContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.RugbyUnionContextMenu_Opening);
            // 
            // NewItem
            // 
            this.NewItem.Image = global::RugbyGui.Properties.Resources.New16x16;
            this.NewItem.Name = "NewItem";
            this.NewItem.Size = new System.Drawing.Size(163, 22);
            this.NewItem.Text = "&New...";
            this.NewItem.Click += new System.EventHandler(this.NewItem_Click);
            // 
            // DeleteItem
            // 
            this.DeleteItem.Image = global::RugbyGui.Properties.Resources.Delete16x16;
            this.DeleteItem.Name = "DeleteItem";
            this.DeleteItem.Size = new System.Drawing.Size(163, 22);
            this.DeleteItem.Text = "&Delete";
            this.DeleteItem.Click += new System.EventHandler(this.DeleteItem_Click);
            // 
            // DeleteAllItems
            // 
            this.DeleteAllItems.Name = "DeleteAllItems";
            this.DeleteAllItems.Size = new System.Drawing.Size(163, 22);
            this.DeleteAllItems.Text = "&Delete All";
            this.DeleteAllItems.Click += new System.EventHandler(this.DeleteAllItems_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(160, 6);
            // 
            // SignPlayerToTeam2
            // 
            this.SignPlayerToTeam2.Image = global::RugbyGui.Properties.Resources.Signed16x16;
            this.SignPlayerToTeam2.Name = "SignPlayerToTeam2";
            this.SignPlayerToTeam2.Size = new System.Drawing.Size(163, 22);
            this.SignPlayerToTeam2.Text = "&Sign Player...";
            this.SignPlayerToTeam2.Click += new System.EventHandler(this.SignPlayerToTeam_Click);
            // 
            // SignPlayerWithTeam2
            // 
            this.SignPlayerWithTeam2.Image = global::RugbyGui.Properties.Resources.Signed16x16;
            this.SignPlayerWithTeam2.Name = "SignPlayerWithTeam2";
            this.SignPlayerWithTeam2.Size = new System.Drawing.Size(163, 22);
            this.SignPlayerWithTeam2.Text = "Sign with &Team...";
            this.SignPlayerWithTeam2.Click += new System.EventHandler(this.SignPlayerWithTeam_Click);
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(160, 6);
            // 
            // ItemProperties
            // 
            this.ItemProperties.Image = global::RugbyGui.Properties.Resources.Properties16x16;
            this.ItemProperties.Name = "ItemProperties";
            this.ItemProperties.Size = new System.Drawing.Size(163, 22);
            this.ItemProperties.Text = "&Properties...";
            this.ItemProperties.Click += new System.EventHandler(this.ItemProperties_Click);
            // 
            // RugbyUnionImageList
            // 
            this.RugbyUnionImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("RugbyUnionImageList.ImageStream")));
            this.RugbyUnionImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.RugbyUnionImageList.Images.SetKeyName(0, "RugbyBall16x16.png");
            this.RugbyUnionImageList.Images.SetKeyName(1, "Team16x16.png");
            this.RugbyUnionImageList.Images.SetKeyName(2, "Player16x16.png");
            this.RugbyUnionImageList.Images.SetKeyName(3, "Signed16x16.png");
            this.RugbyUnionImageList.Images.SetKeyName(4, "Chart16x16.png");
            // 
            // FindResultsSplitContainer
            // 
            this.FindResultsSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FindResultsSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.FindResultsSplitContainer.Name = "FindResultsSplitContainer";
            this.FindResultsSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // FindResultsSplitContainer.Panel1
            // 
            this.FindResultsSplitContainer.Panel1.Controls.Add(this.MainTabControl);
            // 
            // FindResultsSplitContainer.Panel2
            // 
            this.FindResultsSplitContainer.Panel2.Controls.Add(this.FindResultsListBox);
            this.FindResultsSplitContainer.Panel2.Controls.Add(this.FindResultsHeader);
            this.FindResultsSplitContainer.Size = new System.Drawing.Size(1115, 808);
            this.FindResultsSplitContainer.SplitterDistance = 569;
            this.FindResultsSplitContainer.TabIndex = 0;
            // 
            // MainTabControl
            // 
            this.MainTabControl.Controls.Add(this.TeamsTabPage);
            this.MainTabControl.Controls.Add(this.PlayersTabPage);
            this.MainTabControl.Controls.Add(this.SignedPlayersTabPage);
            this.MainTabControl.Controls.Add(this.ChartsTabPage);
            this.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTabControl.ImageList = this.RugbyUnionImageList;
            this.MainTabControl.Location = new System.Drawing.Point(0, 0);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(1115, 569);
            this.MainTabControl.TabIndex = 0;
            this.MainTabControl.SelectedIndexChanged += new System.EventHandler(this.MainTabControl_SelectedIndexChanged);
            // 
            // TeamsTabPage
            // 
            this.TeamsTabPage.Controls.Add(this.TeamsListView);
            this.TeamsTabPage.ImageIndex = 1;
            this.TeamsTabPage.Location = new System.Drawing.Point(4, 23);
            this.TeamsTabPage.Name = "TeamsTabPage";
            this.TeamsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.TeamsTabPage.Size = new System.Drawing.Size(1107, 542);
            this.TeamsTabPage.TabIndex = 0;
            this.TeamsTabPage.Text = "Teams";
            this.TeamsTabPage.UseVisualStyleBackColor = true;
            // 
            // TeamsListView
            // 
            this.TeamsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TeamName1ColumnHeader,
            this.HomeGroundColumnHeader,
            this.CoachColumnHeader,
            this.YearFoundedColumnHeader,
            this.RegionColumnHeader});
            this.TeamsListView.ContextMenuStrip = this.TeamsListViewContextMenu;
            this.TeamsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TeamsListView.FullRowSelect = true;
            this.TeamsListView.HideSelection = false;
            this.TeamsListView.Location = new System.Drawing.Point(3, 3);
            this.TeamsListView.Name = "TeamsListView";
            this.TeamsListView.Size = new System.Drawing.Size(1101, 536);
            this.TeamsListView.SmallImageList = this.RugbyUnionImageList;
            this.TeamsListView.TabIndex = 0;
            this.TeamsListView.UseCompatibleStateImageBehavior = false;
            this.TeamsListView.View = System.Windows.Forms.View.Details;
            this.TeamsListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListView_ColumnClick);
            this.TeamsListView.SelectedIndexChanged += new System.EventHandler(this.TeamsListView_SelectedIndexChanged);
            this.TeamsListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TeamsListView_MouseDoubleClick);
            // 
            // TeamName1ColumnHeader
            // 
            this.TeamName1ColumnHeader.Text = "Team Name";
            this.TeamName1ColumnHeader.Width = 100;
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
            // TeamsListViewContextMenu
            // 
            this.TeamsListViewContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewTeamMenuItem,
            this.DeleteTeamMenuItem,
            this.DeleteAllTeamsMenuItem,
            this.toolStripMenuItem12,
            this.SignPlayerToTeamMenuItem,
            this.toolStripMenuItem13,
            this.TeamPropertiesMenuItem});
            this.TeamsListViewContextMenu.Name = "TeamsListViewContextMenu";
            this.TeamsListViewContextMenu.Size = new System.Drawing.Size(187, 126);
            this.TeamsListViewContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.TeamsListViewContextMenu_Opening);
            // 
            // NewTeamMenuItem
            // 
            this.NewTeamMenuItem.Image = global::RugbyGui.Properties.Resources.Team16x16;
            this.NewTeamMenuItem.Name = "NewTeamMenuItem";
            this.NewTeamMenuItem.Size = new System.Drawing.Size(186, 22);
            this.NewTeamMenuItem.Text = "&New...";
            this.NewTeamMenuItem.Click += new System.EventHandler(this.NewTeam_Click);
            // 
            // DeleteTeamMenuItem
            // 
            this.DeleteTeamMenuItem.Image = global::RugbyGui.Properties.Resources.Delete16x16;
            this.DeleteTeamMenuItem.Name = "DeleteTeamMenuItem";
            this.DeleteTeamMenuItem.Size = new System.Drawing.Size(186, 22);
            this.DeleteTeamMenuItem.Text = "&Delete";
            this.DeleteTeamMenuItem.Click += new System.EventHandler(this.DeleteTeam_Click);
            // 
            // DeleteAllTeamsMenuItem
            // 
            this.DeleteAllTeamsMenuItem.Name = "DeleteAllTeamsMenuItem";
            this.DeleteAllTeamsMenuItem.Size = new System.Drawing.Size(186, 22);
            this.DeleteAllTeamsMenuItem.Text = "Delete &All";
            this.DeleteAllTeamsMenuItem.Click += new System.EventHandler(this.DeleteAllTeams_Click);
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(183, 6);
            // 
            // SignPlayerToTeamMenuItem
            // 
            this.SignPlayerToTeamMenuItem.Image = global::RugbyGui.Properties.Resources.Signed16x16;
            this.SignPlayerToTeamMenuItem.Name = "SignPlayerToTeamMenuItem";
            this.SignPlayerToTeamMenuItem.Size = new System.Drawing.Size(186, 22);
            this.SignPlayerToTeamMenuItem.Text = "&Sign Player to Team...";
            this.SignPlayerToTeamMenuItem.Click += new System.EventHandler(this.SignPlayerToTeam_Click);
            // 
            // toolStripMenuItem13
            // 
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            this.toolStripMenuItem13.Size = new System.Drawing.Size(183, 6);
            // 
            // TeamPropertiesMenuItem
            // 
            this.TeamPropertiesMenuItem.Image = global::RugbyGui.Properties.Resources.Properties16x16;
            this.TeamPropertiesMenuItem.Name = "TeamPropertiesMenuItem";
            this.TeamPropertiesMenuItem.Size = new System.Drawing.Size(186, 22);
            this.TeamPropertiesMenuItem.Text = "&Properties..";
            this.TeamPropertiesMenuItem.Click += new System.EventHandler(this.EditTeam_Click);
            // 
            // PlayersTabPage
            // 
            this.PlayersTabPage.Controls.Add(this.PlayersListView);
            this.PlayersTabPage.ImageIndex = 2;
            this.PlayersTabPage.Location = new System.Drawing.Point(4, 23);
            this.PlayersTabPage.Name = "PlayersTabPage";
            this.PlayersTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.PlayersTabPage.Size = new System.Drawing.Size(1107, 524);
            this.PlayersTabPage.TabIndex = 1;
            this.PlayersTabPage.Text = "Players";
            this.PlayersTabPage.UseVisualStyleBackColor = true;
            // 
            // PlayersListView
            // 
            this.PlayersListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IdColumnHeader,
            this.FirstNameColumnHeader,
            this.LastNameColumnHeader,
            this.HeightColumnHeader,
            this.WeightColumnHeader,
            this.DateOfBirthColumnHeader,
            this.PlaceOfBirthColumnHeader});
            this.PlayersListView.ContextMenuStrip = this.PlayersListViewContextMenu;
            this.PlayersListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PlayersListView.FullRowSelect = true;
            this.PlayersListView.HideSelection = false;
            this.PlayersListView.Location = new System.Drawing.Point(3, 3);
            this.PlayersListView.Name = "PlayersListView";
            this.PlayersListView.Size = new System.Drawing.Size(1101, 518);
            this.PlayersListView.SmallImageList = this.RugbyUnionImageList;
            this.PlayersListView.TabIndex = 0;
            this.PlayersListView.UseCompatibleStateImageBehavior = false;
            this.PlayersListView.View = System.Windows.Forms.View.Details;
            this.PlayersListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListView_ColumnClick);
            this.PlayersListView.SelectedIndexChanged += new System.EventHandler(this.PlayersListView_SelectedIndexChanged);
            this.PlayersListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.PlayersListView_MouseDoubleClick);
            // 
            // IdColumnHeader
            // 
            this.IdColumnHeader.Text = "ID";
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
            // 
            // WeightColumnHeader
            // 
            this.WeightColumnHeader.Text = "Weight";
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
            // PlayersListViewContextMenu
            // 
            this.PlayersListViewContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewPlayerMenuItem,
            this.DeletePlayerMenuItem,
            this.DeleteAllPlayersMenuItem,
            this.toolStripMenuItem14,
            this.SignWithTeamMenuItem,
            this.toolStripMenuItem15,
            this.PlayerPropertiesMenuItem});
            this.PlayersListViewContextMenu.Name = "PlayersListViewContextMenu";
            this.PlayersListViewContextMenu.Size = new System.Drawing.Size(164, 126);
            this.PlayersListViewContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.PlayersListViewContextMenu_Opening);
            // 
            // NewPlayerMenuItem
            // 
            this.NewPlayerMenuItem.Image = global::RugbyGui.Properties.Resources.Player16x16;
            this.NewPlayerMenuItem.Name = "NewPlayerMenuItem";
            this.NewPlayerMenuItem.Size = new System.Drawing.Size(163, 22);
            this.NewPlayerMenuItem.Text = "&New...";
            this.NewPlayerMenuItem.Click += new System.EventHandler(this.NewPlayer_Click);
            // 
            // DeletePlayerMenuItem
            // 
            this.DeletePlayerMenuItem.Image = global::RugbyGui.Properties.Resources.Delete16x16;
            this.DeletePlayerMenuItem.Name = "DeletePlayerMenuItem";
            this.DeletePlayerMenuItem.Size = new System.Drawing.Size(163, 22);
            this.DeletePlayerMenuItem.Text = "&Delete";
            this.DeletePlayerMenuItem.Click += new System.EventHandler(this.DeletePlayer_Click);
            // 
            // DeleteAllPlayersMenuItem
            // 
            this.DeleteAllPlayersMenuItem.Name = "DeleteAllPlayersMenuItem";
            this.DeleteAllPlayersMenuItem.Size = new System.Drawing.Size(163, 22);
            this.DeleteAllPlayersMenuItem.Text = "Delete &All";
            this.DeleteAllPlayersMenuItem.Click += new System.EventHandler(this.DeleteAllPlayers_Click);
            // 
            // toolStripMenuItem14
            // 
            this.toolStripMenuItem14.Name = "toolStripMenuItem14";
            this.toolStripMenuItem14.Size = new System.Drawing.Size(160, 6);
            // 
            // SignWithTeamMenuItem
            // 
            this.SignWithTeamMenuItem.Image = global::RugbyGui.Properties.Resources.Signed16x16;
            this.SignWithTeamMenuItem.Name = "SignWithTeamMenuItem";
            this.SignWithTeamMenuItem.Size = new System.Drawing.Size(163, 22);
            this.SignWithTeamMenuItem.Text = "&Sign with Team...";
            this.SignWithTeamMenuItem.Click += new System.EventHandler(this.SignPlayerWithTeam_Click);
            // 
            // toolStripMenuItem15
            // 
            this.toolStripMenuItem15.Name = "toolStripMenuItem15";
            this.toolStripMenuItem15.Size = new System.Drawing.Size(160, 6);
            // 
            // PlayerPropertiesMenuItem
            // 
            this.PlayerPropertiesMenuItem.Image = global::RugbyGui.Properties.Resources.Properties16x16;
            this.PlayerPropertiesMenuItem.Name = "PlayerPropertiesMenuItem";
            this.PlayerPropertiesMenuItem.Size = new System.Drawing.Size(163, 22);
            this.PlayerPropertiesMenuItem.Text = "&Properties...";
            this.PlayerPropertiesMenuItem.Click += new System.EventHandler(this.EditPlayer_Click);
            // 
            // SignedPlayersTabPage
            // 
            this.SignedPlayersTabPage.Controls.Add(this.SignedPlayersListView);
            this.SignedPlayersTabPage.ImageIndex = 3;
            this.SignedPlayersTabPage.Location = new System.Drawing.Point(4, 23);
            this.SignedPlayersTabPage.Name = "SignedPlayersTabPage";
            this.SignedPlayersTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.SignedPlayersTabPage.Size = new System.Drawing.Size(1107, 524);
            this.SignedPlayersTabPage.TabIndex = 2;
            this.SignedPlayersTabPage.Text = "Signed Players";
            this.SignedPlayersTabPage.UseVisualStyleBackColor = true;
            // 
            // SignedPlayersListView
            // 
            this.SignedPlayersListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PlayerIdColumnHeader,
            this.PlayerNameColumnHeader,
            this.TeamName2ColumnHeader});
            this.SignedPlayersListView.ContextMenuStrip = this.SignedPlayersListViewContextMenu;
            this.SignedPlayersListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SignedPlayersListView.FullRowSelect = true;
            this.SignedPlayersListView.HideSelection = false;
            this.SignedPlayersListView.Location = new System.Drawing.Point(3, 3);
            this.SignedPlayersListView.Name = "SignedPlayersListView";
            this.SignedPlayersListView.Size = new System.Drawing.Size(1101, 518);
            this.SignedPlayersListView.SmallImageList = this.RugbyUnionImageList;
            this.SignedPlayersListView.TabIndex = 0;
            this.SignedPlayersListView.UseCompatibleStateImageBehavior = false;
            this.SignedPlayersListView.View = System.Windows.Forms.View.Details;
            this.SignedPlayersListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListView_ColumnClick);
            this.SignedPlayersListView.SelectedIndexChanged += new System.EventHandler(this.SignedPlayersListView_SelectedIndexChanged);
            this.SignedPlayersListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.SignedPlayersListView_MouseDoubleClick);
            // 
            // PlayerIdColumnHeader
            // 
            this.PlayerIdColumnHeader.Text = "Player ID";
            this.PlayerIdColumnHeader.Width = 100;
            // 
            // PlayerNameColumnHeader
            // 
            this.PlayerNameColumnHeader.Text = "Player Name";
            this.PlayerNameColumnHeader.Width = 100;
            // 
            // TeamName2ColumnHeader
            // 
            this.TeamName2ColumnHeader.Text = "Team Name";
            this.TeamName2ColumnHeader.Width = 100;
            // 
            // SignedPlayersListViewContextMenu
            // 
            this.SignedPlayersListViewContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewSignedPlayerMenuItem,
            this.DeleteSignedPlayerMenuItem,
            this.DeleteAllSignedPlayersMenuItem,
            this.toolStripMenuItem16,
            this.SignedPlayerPropertiesMenuItem});
            this.SignedPlayersListViewContextMenu.Name = "SignedPlayersListViewContextMenu";
            this.SignedPlayersListViewContextMenu.Size = new System.Drawing.Size(137, 98);
            this.SignedPlayersListViewContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.SignedPlayersListViewContextMenu_Opening);
            // 
            // NewSignedPlayerMenuItem
            // 
            this.NewSignedPlayerMenuItem.Image = global::RugbyGui.Properties.Resources.Signed16x16;
            this.NewSignedPlayerMenuItem.Name = "NewSignedPlayerMenuItem";
            this.NewSignedPlayerMenuItem.Size = new System.Drawing.Size(136, 22);
            this.NewSignedPlayerMenuItem.Text = "&New...";
            this.NewSignedPlayerMenuItem.Click += new System.EventHandler(this.NewSignedPlayer_Click);
            // 
            // DeleteSignedPlayerMenuItem
            // 
            this.DeleteSignedPlayerMenuItem.Image = global::RugbyGui.Properties.Resources.Delete16x16;
            this.DeleteSignedPlayerMenuItem.Name = "DeleteSignedPlayerMenuItem";
            this.DeleteSignedPlayerMenuItem.Size = new System.Drawing.Size(136, 22);
            this.DeleteSignedPlayerMenuItem.Text = "&Delete";
            this.DeleteSignedPlayerMenuItem.Click += new System.EventHandler(this.DeleteSignedPlayer_Click);
            // 
            // DeleteAllSignedPlayersMenuItem
            // 
            this.DeleteAllSignedPlayersMenuItem.Name = "DeleteAllSignedPlayersMenuItem";
            this.DeleteAllSignedPlayersMenuItem.Size = new System.Drawing.Size(136, 22);
            this.DeleteAllSignedPlayersMenuItem.Text = "Delete &All";
            this.DeleteAllSignedPlayersMenuItem.Click += new System.EventHandler(this.DeleteAllSignedPlayers_Click);
            // 
            // toolStripMenuItem16
            // 
            this.toolStripMenuItem16.Name = "toolStripMenuItem16";
            this.toolStripMenuItem16.Size = new System.Drawing.Size(133, 6);
            // 
            // SignedPlayerPropertiesMenuItem
            // 
            this.SignedPlayerPropertiesMenuItem.Image = global::RugbyGui.Properties.Resources.Properties16x16;
            this.SignedPlayerPropertiesMenuItem.Name = "SignedPlayerPropertiesMenuItem";
            this.SignedPlayerPropertiesMenuItem.Size = new System.Drawing.Size(136, 22);
            this.SignedPlayerPropertiesMenuItem.Text = "&Properties...";
            this.SignedPlayerPropertiesMenuItem.Click += new System.EventHandler(this.SignedPlayerProperties_Click);
            // 
            // ChartsTabPage
            // 
            this.ChartsTabPage.Controls.Add(this.MainChart);
            this.ChartsTabPage.Controls.Add(this.HeaderPanel);
            this.ChartsTabPage.ImageIndex = 4;
            this.ChartsTabPage.Location = new System.Drawing.Point(4, 23);
            this.ChartsTabPage.Name = "ChartsTabPage";
            this.ChartsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ChartsTabPage.Size = new System.Drawing.Size(1107, 524);
            this.ChartsTabPage.TabIndex = 3;
            this.ChartsTabPage.Text = "Charts";
            this.ChartsTabPage.UseVisualStyleBackColor = true;
            // 
            // MainChart
            // 
            chartArea1.Name = "ChartArea1";
            this.MainChart.ChartAreas.Add(chartArea1);
            this.MainChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend";
            legend1.Title = "Legend";
            this.MainChart.Legends.Add(legend1);
            this.MainChart.Location = new System.Drawing.Point(3, 43);
            this.MainChart.Name = "MainChart";
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.IsValueShownAsLabel = true;
            series1.Legend = "Legend";
            series1.Name = "Series1";
            series2.BorderWidth = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.IsValueShownAsLabel = true;
            series2.Legend = "Legend";
            series2.Name = "Series2";
            this.MainChart.Series.Add(series1);
            this.MainChart.Series.Add(series2);
            this.MainChart.Size = new System.Drawing.Size(1101, 478);
            this.MainChart.TabIndex = 0;
            this.MainChart.Text = "Rugby Chart";
            // 
            // HeaderPanel
            // 
            this.HeaderPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.HeaderPanel.Controls.Add(this.ChartsLabel);
            this.HeaderPanel.Controls.Add(this.ChartDropList);
            this.HeaderPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.HeaderPanel.Location = new System.Drawing.Point(3, 3);
            this.HeaderPanel.Name = "HeaderPanel";
            this.HeaderPanel.Size = new System.Drawing.Size(1101, 40);
            this.HeaderPanel.TabIndex = 1;
            // 
            // ChartsLabel
            // 
            this.ChartsLabel.AutoSize = true;
            this.ChartsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChartsLabel.Location = new System.Drawing.Point(16, 8);
            this.ChartsLabel.Name = "ChartsLabel";
            this.ChartsLabel.Size = new System.Drawing.Size(56, 20);
            this.ChartsLabel.TabIndex = 0;
            this.ChartsLabel.Text = "Charts";
            // 
            // ChartDropList
            // 
            this.ChartDropList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ChartDropList.FormattingEnabled = true;
            this.ChartDropList.Items.AddRange(new object[] {
            "Player heights and weights",
            "Age group histogram",
            "Heights for each team",
            "Weights for each team",
            "Place of birth histogram",
            "Year Founded grouped by team"});
            this.ChartDropList.Location = new System.Drawing.Point(79, 10);
            this.ChartDropList.Name = "ChartDropList";
            this.ChartDropList.Size = new System.Drawing.Size(285, 21);
            this.ChartDropList.TabIndex = 1;
            this.ChartDropList.SelectedIndexChanged += new System.EventHandler(this.ChartDropList_SelectedIndexChanged);
            // 
            // FindResultsListBox
            // 
            this.FindResultsListBox.ContextMenuStrip = this.FindResultsContextMenu;
            this.FindResultsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FindResultsListBox.FormattingEnabled = true;
            this.FindResultsListBox.IntegralHeight = false;
            this.FindResultsListBox.Location = new System.Drawing.Point(0, 27);
            this.FindResultsListBox.Name = "FindResultsListBox";
            this.FindResultsListBox.Size = new System.Drawing.Size(1115, 208);
            this.FindResultsListBox.TabIndex = 1;
            this.FindResultsListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.FindResultsListBox_MouseDoubleClick);
            this.FindResultsListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FindResultsListBox_MouseDown);
            // 
            // FindResultsContextMenu
            // 
            this.FindResultsContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LocateFindResultItem,
            this.ClearFindResults});
            this.FindResultsContextMenu.Name = "FindResultsContextMenu";
            this.FindResultsContextMenu.Size = new System.Drawing.Size(142, 48);
            this.FindResultsContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.FindResultsContextMenu_Opening);
            // 
            // LocateFindResultItem
            // 
            this.LocateFindResultItem.Image = global::RugbyGui.Properties.Resources.Search16x16;
            this.LocateFindResultItem.Name = "LocateFindResultItem";
            this.LocateFindResultItem.Size = new System.Drawing.Size(141, 22);
            this.LocateFindResultItem.Text = "&Locate Item";
            this.LocateFindResultItem.Click += new System.EventHandler(this.LocateFindResultItem_Click);
            // 
            // ClearFindResults
            // 
            this.ClearFindResults.Image = global::RugbyGui.Properties.Resources.Delete16x16;
            this.ClearFindResults.Name = "ClearFindResults";
            this.ClearFindResults.Size = new System.Drawing.Size(141, 22);
            this.ClearFindResults.Text = "&Clear Results";
            this.ClearFindResults.Click += new System.EventHandler(this.ClearFindResults_Click);
            // 
            // FindResultsHeader
            // 
            this.FindResultsHeader.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.FindResultsHeader.Controls.Add(this.FindResultsCloseButton);
            this.FindResultsHeader.Controls.Add(this.FindResultsHeaderLabel);
            this.FindResultsHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.FindResultsHeader.Location = new System.Drawing.Point(0, 0);
            this.FindResultsHeader.Name = "FindResultsHeader";
            this.FindResultsHeader.Size = new System.Drawing.Size(1115, 27);
            this.FindResultsHeader.TabIndex = 0;
            // 
            // FindResultsCloseButton
            // 
            this.FindResultsCloseButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.FindResultsCloseButton.FlatAppearance.BorderSize = 0;
            this.FindResultsCloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FindResultsCloseButton.Location = new System.Drawing.Point(1090, 0);
            this.FindResultsCloseButton.Name = "FindResultsCloseButton";
            this.FindResultsCloseButton.Size = new System.Drawing.Size(25, 27);
            this.FindResultsCloseButton.TabIndex = 3;
            this.FindResultsCloseButton.Text = "✖";
            this.FindResultsCloseButton.UseVisualStyleBackColor = true;
            this.FindResultsCloseButton.Click += new System.EventHandler(this.FindResultsCloseButton_Click);
            // 
            // FindResultsHeaderLabel
            // 
            this.FindResultsHeaderLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.FindResultsHeaderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FindResultsHeaderLabel.Location = new System.Drawing.Point(0, 0);
            this.FindResultsHeaderLabel.Name = "FindResultsHeaderLabel";
            this.FindResultsHeaderLabel.Size = new System.Drawing.Size(773, 27);
            this.FindResultsHeaderLabel.TabIndex = 2;
            this.FindResultsHeaderLabel.Text = "Find Results";
            this.FindResultsHeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // EmptyWorkspacePanel
            // 
            this.EmptyWorkspacePanel.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.EmptyWorkspacePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.EmptyWorkspacePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EmptyWorkspacePanel.Location = new System.Drawing.Point(0, 0);
            this.EmptyWorkspacePanel.Name = "EmptyWorkspacePanel";
            this.EmptyWorkspacePanel.Size = new System.Drawing.Size(1387, 808);
            this.EmptyWorkspacePanel.TabIndex = 1;
            this.EmptyWorkspacePanel.Visible = false;
            // 
            // RugbyUnionToolbar
            // 
            this.RugbyUnionToolbar.Dock = System.Windows.Forms.DockStyle.None;
            this.RugbyUnionToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewRugbyUnionButton,
            this.OpenRugbyUnionButton,
            this.toolStripSeparator1,
            this.SaveRugbyUnionButton,
            this.RugbyUnionPropertiesButton});
            this.RugbyUnionToolbar.Location = new System.Drawing.Point(3, 24);
            this.RugbyUnionToolbar.Name = "RugbyUnionToolbar";
            this.RugbyUnionToolbar.Size = new System.Drawing.Size(110, 25);
            this.RugbyUnionToolbar.TabIndex = 1;
            // 
            // NewRugbyUnionButton
            // 
            this.NewRugbyUnionButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NewRugbyUnionButton.Image = global::RugbyGui.Properties.Resources.New16x16;
            this.NewRugbyUnionButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewRugbyUnionButton.Name = "NewRugbyUnionButton";
            this.NewRugbyUnionButton.Size = new System.Drawing.Size(23, 22);
            this.NewRugbyUnionButton.Text = "New Rugby Union";
            this.NewRugbyUnionButton.Click += new System.EventHandler(this.NewRugbyUnion_Click);
            // 
            // OpenRugbyUnionButton
            // 
            this.OpenRugbyUnionButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OpenRugbyUnionButton.Image = global::RugbyGui.Properties.Resources.Open16x16;
            this.OpenRugbyUnionButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenRugbyUnionButton.Name = "OpenRugbyUnionButton";
            this.OpenRugbyUnionButton.Size = new System.Drawing.Size(23, 22);
            this.OpenRugbyUnionButton.Text = "Open Rugby Union";
            this.OpenRugbyUnionButton.Click += new System.EventHandler(this.OpenRugbyUnion_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // SaveRugbyUnionButton
            // 
            this.SaveRugbyUnionButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveRugbyUnionButton.Image = global::RugbyGui.Properties.Resources.Save16x16;
            this.SaveRugbyUnionButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveRugbyUnionButton.Name = "SaveRugbyUnionButton";
            this.SaveRugbyUnionButton.Size = new System.Drawing.Size(23, 22);
            this.SaveRugbyUnionButton.Text = "Save Rugby Union";
            this.SaveRugbyUnionButton.Click += new System.EventHandler(this.SaveRugbyUnion_Click);
            // 
            // RugbyUnionPropertiesButton
            // 
            this.RugbyUnionPropertiesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RugbyUnionPropertiesButton.Image = global::RugbyGui.Properties.Resources.Properties16x16;
            this.RugbyUnionPropertiesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RugbyUnionPropertiesButton.Name = "RugbyUnionPropertiesButton";
            this.RugbyUnionPropertiesButton.Size = new System.Drawing.Size(23, 22);
            this.RugbyUnionPropertiesButton.Text = "Rugby Union Properties";
            this.RugbyUnionPropertiesButton.Click += new System.EventHandler(this.RugbyUnionProperties_Click);
            // 
            // PlayerToolbar
            // 
            this.PlayerToolbar.Dock = System.Windows.Forms.DockStyle.None;
            this.PlayerToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewPlayerButton,
            this.EditPlayerButton,
            this.toolStripSeparator3,
            this.SignPlayerWithTeamButton});
            this.PlayerToolbar.Location = new System.Drawing.Point(201, 24);
            this.PlayerToolbar.Name = "PlayerToolbar";
            this.PlayerToolbar.Size = new System.Drawing.Size(118, 25);
            this.PlayerToolbar.TabIndex = 3;
            // 
            // NewPlayerButton
            // 
            this.NewPlayerButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NewPlayerButton.Image = global::RugbyGui.Properties.Resources.Player16x16;
            this.NewPlayerButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewPlayerButton.Name = "NewPlayerButton";
            this.NewPlayerButton.Size = new System.Drawing.Size(23, 22);
            this.NewPlayerButton.Text = "New Player";
            this.NewPlayerButton.Click += new System.EventHandler(this.NewPlayer_Click);
            // 
            // EditPlayerButton
            // 
            this.EditPlayerButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EditPlayerButton.Image = global::RugbyGui.Properties.Resources.Edit16x16;
            this.EditPlayerButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EditPlayerButton.Name = "EditPlayerButton";
            this.EditPlayerButton.Size = new System.Drawing.Size(23, 22);
            this.EditPlayerButton.Text = "Edit Player";
            this.EditPlayerButton.Click += new System.EventHandler(this.EditPlayer_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // SignPlayerWithTeamButton
            // 
            this.SignPlayerWithTeamButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SignPlayerWithTeamButton.Image = global::RugbyGui.Properties.Resources.Signed16x16;
            this.SignPlayerWithTeamButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SignPlayerWithTeamButton.Name = "SignPlayerWithTeamButton";
            this.SignPlayerWithTeamButton.Size = new System.Drawing.Size(23, 22);
            this.SignPlayerWithTeamButton.Text = "Sign with Team";
            this.SignPlayerWithTeamButton.Click += new System.EventHandler(this.SignPlayerWithTeam_Click);
            // 
            // TeamToolbar
            // 
            this.TeamToolbar.Dock = System.Windows.Forms.DockStyle.None;
            this.TeamToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewTeamButton,
            this.EditTeamButton,
            this.toolStripSeparator2,
            this.SignPlayerToTeamButton});
            this.TeamToolbar.Location = new System.Drawing.Point(113, 24);
            this.TeamToolbar.Name = "TeamToolbar";
            this.TeamToolbar.Size = new System.Drawing.Size(87, 25);
            this.TeamToolbar.TabIndex = 2;
            // 
            // NewTeamButton
            // 
            this.NewTeamButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NewTeamButton.Image = global::RugbyGui.Properties.Resources.Team16x16;
            this.NewTeamButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewTeamButton.Name = "NewTeamButton";
            this.NewTeamButton.Size = new System.Drawing.Size(23, 22);
            this.NewTeamButton.Text = "New Team";
            this.NewTeamButton.Click += new System.EventHandler(this.NewTeam_Click);
            // 
            // EditTeamButton
            // 
            this.EditTeamButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EditTeamButton.Image = global::RugbyGui.Properties.Resources.Edit16x16;
            this.EditTeamButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EditTeamButton.Name = "EditTeamButton";
            this.EditTeamButton.Size = new System.Drawing.Size(23, 22);
            this.EditTeamButton.Text = "Edit Team";
            this.EditTeamButton.Click += new System.EventHandler(this.EditTeam_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // SignPlayerToTeamButton
            // 
            this.SignPlayerToTeamButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SignPlayerToTeamButton.Image = global::RugbyGui.Properties.Resources.Signed16x16;
            this.SignPlayerToTeamButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SignPlayerToTeamButton.Name = "SignPlayerToTeamButton";
            this.SignPlayerToTeamButton.Size = new System.Drawing.Size(23, 22);
            this.SignPlayerToTeamButton.Text = "Sign Player to Team";
            this.SignPlayerToTeamButton.Click += new System.EventHandler(this.SignPlayerToTeam_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1387, 879);
            this.Controls.Add(this.MainToolStripContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MyMainMenuStrip;
            this.Name = "MainWindow";
            this.Text = "Rugby Union Signing Application";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.MyMainMenuStrip.ResumeLayout(false);
            this.MyMainMenuStrip.PerformLayout();
            this.MainToolStripContainer.BottomToolStripPanel.ResumeLayout(false);
            this.MainToolStripContainer.BottomToolStripPanel.PerformLayout();
            this.MainToolStripContainer.ContentPanel.ResumeLayout(false);
            this.MainToolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.MainToolStripContainer.TopToolStripPanel.PerformLayout();
            this.MainToolStripContainer.ResumeLayout(false);
            this.MainToolStripContainer.PerformLayout();
            this.MainStatusBar.ResumeLayout(false);
            this.MainStatusBar.PerformLayout();
            this.MainSplitContainer.Panel1.ResumeLayout(false);
            this.MainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).EndInit();
            this.MainSplitContainer.ResumeLayout(false);
            this.TreeViewHeader.ResumeLayout(false);
            this.RugbyUnionContextMenu.ResumeLayout(false);
            this.FindResultsSplitContainer.Panel1.ResumeLayout(false);
            this.FindResultsSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FindResultsSplitContainer)).EndInit();
            this.FindResultsSplitContainer.ResumeLayout(false);
            this.MainTabControl.ResumeLayout(false);
            this.TeamsTabPage.ResumeLayout(false);
            this.TeamsListViewContextMenu.ResumeLayout(false);
            this.PlayersTabPage.ResumeLayout(false);
            this.PlayersListViewContextMenu.ResumeLayout(false);
            this.SignedPlayersTabPage.ResumeLayout(false);
            this.SignedPlayersListViewContextMenu.ResumeLayout(false);
            this.ChartsTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainChart)).EndInit();
            this.HeaderPanel.ResumeLayout(false);
            this.HeaderPanel.PerformLayout();
            this.FindResultsContextMenu.ResumeLayout(false);
            this.FindResultsHeader.ResumeLayout(false);
            this.RugbyUnionToolbar.ResumeLayout(false);
            this.RugbyUnionToolbar.PerformLayout();
            this.PlayerToolbar.ResumeLayout(false);
            this.PlayerToolbar.PerformLayout();
            this.TeamToolbar.ResumeLayout(false);
            this.TeamToolbar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip MyMainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem FileMenu;
        private System.Windows.Forms.ToolStripMenuItem NewRugbyUnion;
        private System.Windows.Forms.ToolStripMenuItem OpenRugbyUnion;
        private System.Windows.Forms.ToolStripMenuItem CloseRugbyUnion;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem SaveRugbyUnion;
        private System.Windows.Forms.ToolStripMenuItem SaveAsRugbyUnion;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem RugbyUnionProperties;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem RecentFiles;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem ApplicationExit;
        private System.Windows.Forms.ToolStripMenuItem EditMenu;
        private System.Windows.Forms.ToolStripMenuItem ViewMenu;
        private System.Windows.Forms.ToolStripMenuItem TeamMenu;
        private System.Windows.Forms.ToolStripMenuItem PlayerMenu;
        private System.Windows.Forms.ToolStripMenuItem HelpMenu;
        private System.Windows.Forms.ToolStripContainer MainToolStripContainer;
        private System.Windows.Forms.StatusStrip MainStatusBar;
        private System.Windows.Forms.ToolStripMenuItem CutSelectedItemsToClipboard;
        private System.Windows.Forms.ToolStripMenuItem CopySelectedItemsToClipboard;
        private System.Windows.Forms.ToolStripMenuItem PasteFromClipboard;
        private System.Windows.Forms.ToolStripMenuItem DeleteSelectedItems;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem SelectAllItems;
        private System.Windows.Forms.ToolStripMenuItem UnselectAllItems;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem ViewRugbyUnion;
        private System.Windows.Forms.ToolStripMenuItem ViewTeams;
        private System.Windows.Forms.ToolStripMenuItem ViewPlayers;
        private System.Windows.Forms.ToolStripMenuItem ViewSignedPlayers;
        private System.Windows.Forms.ToolStripMenuItem ViewFindResults;
        private System.Windows.Forms.ToolStripMenuItem ViewStatusBar;
        private System.Windows.Forms.ToolStripMenuItem NewTeam;
        private System.Windows.Forms.ToolStripMenuItem EditTeam;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem SignPlayerToTeam;
        private System.Windows.Forms.ToolStripMenuItem NewPlayer;
        private System.Windows.Forms.ToolStripMenuItem EditPlayer;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem SignPlayerWithTeam;
        private System.Windows.Forms.ToolStripMenuItem OpenUserManual;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem ViewAbout;
        private System.Windows.Forms.SplitContainer MainSplitContainer;
        private System.Windows.Forms.SplitContainer FindResultsSplitContainer;
        private System.Windows.Forms.Panel TreeViewHeader;
        private System.Windows.Forms.Button TreeViewCloseButton;
        private System.Windows.Forms.Label TreeViewHeaderLabel;
        private System.Windows.Forms.TreeView RugbyUnionTreeView;
        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.TabPage TeamsTabPage;
        private System.Windows.Forms.TabPage PlayersTabPage;
        private System.Windows.Forms.TabPage SignedPlayersTabPage;
        private System.Windows.Forms.ListView TeamsListView;
        private System.Windows.Forms.ColumnHeader TeamName1ColumnHeader;
        private System.Windows.Forms.ColumnHeader HomeGroundColumnHeader;
        private System.Windows.Forms.ColumnHeader CoachColumnHeader;
        private System.Windows.Forms.ColumnHeader YearFoundedColumnHeader;
        private System.Windows.Forms.ColumnHeader RegionColumnHeader;
        private System.Windows.Forms.ListView PlayersListView;
        private System.Windows.Forms.ColumnHeader IdColumnHeader;
        private System.Windows.Forms.ColumnHeader FirstNameColumnHeader;
        private System.Windows.Forms.ColumnHeader LastNameColumnHeader;
        private System.Windows.Forms.ColumnHeader HeightColumnHeader;
        private System.Windows.Forms.ColumnHeader WeightColumnHeader;
        private System.Windows.Forms.ColumnHeader DateOfBirthColumnHeader;
        private System.Windows.Forms.ColumnHeader PlaceOfBirthColumnHeader;
        private System.Windows.Forms.ListView SignedPlayersListView;
        private System.Windows.Forms.ColumnHeader PlayerIdColumnHeader;
        private System.Windows.Forms.ColumnHeader PlayerNameColumnHeader;
        private System.Windows.Forms.ColumnHeader TeamName2ColumnHeader;
        private System.Windows.Forms.ListBox FindResultsListBox;
        private System.Windows.Forms.Panel FindResultsHeader;
        private System.Windows.Forms.Button FindResultsCloseButton;
        private System.Windows.Forms.Label FindResultsHeaderLabel;
        private System.Windows.Forms.ImageList RugbyUnionImageList;
        private System.Windows.Forms.Panel EmptyWorkspacePanel;
        private System.Windows.Forms.ToolStripStatusLabel StatusBarMessageLabel;
        private System.Windows.Forms.ToolStrip RugbyUnionToolbar;
        private System.Windows.Forms.ToolStripButton NewRugbyUnionButton;
        private System.Windows.Forms.ToolStripButton OpenRugbyUnionButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton SaveRugbyUnionButton;
        private System.Windows.Forms.ToolStripButton RugbyUnionPropertiesButton;
        private System.Windows.Forms.ToolStrip TeamToolbar;
        private System.Windows.Forms.ToolStripButton NewTeamButton;
        private System.Windows.Forms.ToolStripButton EditTeamButton;
        private System.Windows.Forms.ToolStripButton SignPlayerToTeamButton;
        private System.Windows.Forms.ToolStrip PlayerToolbar;
        private System.Windows.Forms.ToolStripButton NewPlayerButton;
        private System.Windows.Forms.ToolStripButton EditPlayerButton;
        private System.Windows.Forms.ToolStripButton SignPlayerWithTeamButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ContextMenuStrip RugbyUnionContextMenu;
        private System.Windows.Forms.ToolStripMenuItem NewItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteAllItems;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem SignPlayerToTeam2;
        private System.Windows.Forms.ToolStripMenuItem SignPlayerWithTeam2;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem11;
        private System.Windows.Forms.ToolStripMenuItem ItemProperties;
        private System.Windows.Forms.ContextMenuStrip TeamsListViewContextMenu;
        private System.Windows.Forms.ToolStripMenuItem NewTeamMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteTeamMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteAllTeamsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem12;
        private System.Windows.Forms.ToolStripMenuItem SignPlayerToTeamMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem13;
        private System.Windows.Forms.ToolStripMenuItem TeamPropertiesMenuItem;
        private System.Windows.Forms.ContextMenuStrip PlayersListViewContextMenu;
        private System.Windows.Forms.ContextMenuStrip SignedPlayersListViewContextMenu;
        private System.Windows.Forms.ToolStripMenuItem NewPlayerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeletePlayerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteAllPlayersMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem14;
        private System.Windows.Forms.ToolStripMenuItem SignWithTeamMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem15;
        private System.Windows.Forms.ToolStripMenuItem PlayerPropertiesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewSignedPlayerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteSignedPlayerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteAllSignedPlayersMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem16;
        private System.Windows.Forms.ToolStripMenuItem SignedPlayerPropertiesMenuItem;
        private System.Windows.Forms.ContextMenuStrip FindResultsContextMenu;
        private System.Windows.Forms.ToolStripMenuItem LocateFindResultItem;
        private System.Windows.Forms.ToolStripMenuItem ClearFindResults;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem17;
        private System.Windows.Forms.ToolStripMenuItem ImportTeam;
        private System.Windows.Forms.ToolStripMenuItem ExportTeam;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem18;
        private System.Windows.Forms.ToolStripMenuItem ImportPlayers;
        private System.Windows.Forms.ToolStripMenuItem ExportPlayers;
        private System.Windows.Forms.ToolStripMenuItem ViewCharts;
        private System.Windows.Forms.TabPage ChartsTabPage;
        private System.Windows.Forms.DataVisualization.Charting.Chart MainChart;
        private System.Windows.Forms.Panel HeaderPanel;
        private System.Windows.Forms.Label ChartsLabel;
        private System.Windows.Forms.ComboBox ChartDropList;
        private System.Windows.Forms.ColorDialog SeriesColourPicker;
        private System.Windows.Forms.ToolStripMenuItem FindAndReplace;
        private System.Windows.Forms.ToolStripMenuItem FindItems;
        private System.Windows.Forms.ToolStripMenuItem ReplaceItems;
        private System.Windows.Forms.ToolStripMenuItem AdvancedFindAndReplace;
    }
}

