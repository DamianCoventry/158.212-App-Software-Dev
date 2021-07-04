using RugbyView;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RugbyGui.Dialogs
{
    /// <summary>
    /// The event handlers for a dialog that collects input from the user that defines options for a find/replace operation.
    /// </summary>
    /// <remarks>
    /// <para>Author: Damian Coventry</para>
    /// <para>Date: Feb-May 2021</para>
    /// </remarks>
    public partial class AdvancedFindAndReplaceDialog : Form
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public AdvancedFindAndReplaceDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Options for the find/replace operation
        /// </summary>
        public AdvancedFindReplaceOptions FindReplaceOptions { get; set; }

        /// <summary>
        /// The event handler that's called when the dialog loads
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void AdvancedFindAndReplaceDialog_Load(object sender, EventArgs e)
        {
            SetupMinMaxRanges();
            SetupEventHandlers();
            SetupReplacementFields();
            SetupInitialValues();
            RebuildOutputText();
        }

        /// <summary>
        /// Delegates the setup work to whichever dataset has been chosen by the user
        /// </summary>
        private void SetupInitialValues()
        {
            if (FindReplaceOptions == null || FindReplaceOptions.FindFields == null)
                return;
            switch (FindReplaceOptions.FindWhat)
            {
                case AdvancedFindReplaceOptions.What.Teams:
                    SetupTeamInitialValues();
                    break;
                case AdvancedFindReplaceOptions.What.Players:
                    SetupPlayerInitialValues();
                    break;
                case AdvancedFindReplaceOptions.What.SignedPlayers:
                    SetupSignedPlayerInitialValues();
                    break;
            }
        }

        /// <summary>
        /// Sets up the search items for the team object fields
        /// </summary>
        private void SetupTeamInitialValues()
        {
            MainTabControl.SelectedTab = TeamsTabPage;

            TeamNameSearchItem.IsSearchItemChecked = IsPresent(TeamNameSearchItem.FieldName);
            TeamNameSearchItem.Operation = GetFieldOperation(TeamNameSearchItem.FieldName);
            TeamNameSearchItem.StringValue = GetFieldBeginValue<string>(TeamNameSearchItem.FieldName);

            HomeGroundSearchItem.IsSearchItemChecked = IsPresent(HomeGroundSearchItem.FieldName);
            HomeGroundSearchItem.Operation = GetFieldOperation(HomeGroundSearchItem.FieldName);
            HomeGroundSearchItem.StringValue = GetFieldBeginValue<string>(HomeGroundSearchItem.FieldName);

            CoachSearchItem.IsSearchItemChecked = IsPresent(CoachSearchItem.FieldName);
            CoachSearchItem.Operation = GetFieldOperation(CoachSearchItem.FieldName);
            CoachSearchItem.StringValue = GetFieldBeginValue<string>(CoachSearchItem.FieldName);

            RegionSearchItem.IsSearchItemChecked = IsPresent(RegionSearchItem.FieldName);
            RegionSearchItem.Operation = GetFieldOperation(RegionSearchItem.FieldName);
            RegionSearchItem.StringValue = GetFieldBeginValue<string>(RegionSearchItem.FieldName);

            YearFoundedSearchItem.IsSearchItemChecked = IsPresent(YearFoundedSearchItem.FieldName);
            YearFoundedSearchItem.Operation = GetFieldOperation(YearFoundedSearchItem.FieldName);
            YearFoundedSearchItem.BeginValue = GetFieldBeginValue<int>(YearFoundedSearchItem.FieldName);
            YearFoundedSearchItem.EndValue = GetFieldEndValue<int>(YearFoundedSearchItem.FieldName);

            if (FindReplaceOptions.ReplaceField != null)
            {
                TeamReplaceSearchItem.FieldName = FindReplaceOptions.ReplaceField.Name;
                TeamReplaceSearchItem.ReplacementValue = FindReplaceOptions.ReplaceField.Value;
                TeamReplaceSearchItem.IsSearchItemChecked = IsPresent(TeamReplaceSearchItem.FieldName);
            }
        }

        /// <summary>
        /// Sets up the search items for the player object fields
        /// </summary>
        private void SetupPlayerInitialValues()
        {
            MainTabControl.SelectedTab = PlayersTabPage;

            PlayerIdSearchItem.IsSearchItemChecked = IsPresent(PlayerIdSearchItem.FieldName);
            PlayerIdSearchItem.Operation = GetFieldOperation(PlayerIdSearchItem.FieldName);
            PlayerIdSearchItem.BeginValue = GetFieldBeginValue<int>(PlayerIdSearchItem.FieldName);
            PlayerIdSearchItem.EndValue = GetFieldEndValue<int>(PlayerIdSearchItem.FieldName);

            FirstNameSearchItem.IsSearchItemChecked = IsPresent(FirstNameSearchItem.FieldName);
            FirstNameSearchItem.Operation = GetFieldOperation(FirstNameSearchItem.FieldName);
            FirstNameSearchItem.StringValue = GetFieldBeginValue<string>(FirstNameSearchItem.FieldName);

            LastNameSearchItem.IsSearchItemChecked = IsPresent(LastNameSearchItem.FieldName);
            LastNameSearchItem.Operation = GetFieldOperation(LastNameSearchItem.FieldName);
            LastNameSearchItem.StringValue = GetFieldBeginValue<string>(LastNameSearchItem.FieldName);

            HeightSearchItem.IsSearchItemChecked = IsPresent(HeightSearchItem.FieldName);
            HeightSearchItem.Operation = GetFieldOperation(HeightSearchItem.FieldName);
            HeightSearchItem.BeginValue = GetFieldBeginValue<int>(HeightSearchItem.FieldName);
            HeightSearchItem.EndValue = GetFieldEndValue<int>(HeightSearchItem.FieldName);

            WeightSearchItem.IsSearchItemChecked = IsPresent(WeightSearchItem.FieldName);
            WeightSearchItem.Operation = GetFieldOperation(WeightSearchItem.FieldName);
            WeightSearchItem.BeginValue = GetFieldBeginValue<int>(WeightSearchItem.FieldName);
            WeightSearchItem.EndValue = GetFieldEndValue<int>(WeightSearchItem.FieldName);

            DateOfBirthSearchItem.IsSearchItemChecked = IsPresent(DateOfBirthSearchItem.FieldName);
            DateOfBirthSearchItem.Operation = GetFieldOperation(DateOfBirthSearchItem.FieldName);
            DateOfBirthSearchItem.BeginDate = GetFieldBeginValue<DateTime>(DateOfBirthSearchItem.FieldName);
            DateOfBirthSearchItem.EndDate = GetFieldEndValue<DateTime>(DateOfBirthSearchItem.FieldName);

            PlaceOfBirthSearchItem.IsSearchItemChecked = IsPresent(PlaceOfBirthSearchItem.FieldName);
            PlaceOfBirthSearchItem.Operation = GetFieldOperation(PlaceOfBirthSearchItem.FieldName);
            PlaceOfBirthSearchItem.StringValue = GetFieldBeginValue<string>(PlaceOfBirthSearchItem.FieldName);

            if (FindReplaceOptions.ReplaceField != null)
            {
                PlayerReplaceSearchItem.FieldName = FindReplaceOptions.ReplaceField.Name;
                PlayerReplaceSearchItem.ReplacementValue = FindReplaceOptions.ReplaceField.Value;
                PlayerReplaceSearchItem.IsSearchItemChecked = IsPresent(PlayerReplaceSearchItem.FieldName);
            }
        }

        /// <summary>
        /// Sets up the search items for the signed player object fields
        /// </summary>
        private void SetupSignedPlayerInitialValues()
        {
            MainTabControl.SelectedTab = SignedPlayersTabPage;

            SignedPlayerIdSearchItem.IsSearchItemChecked = IsPresent(SignedPlayerIdSearchItem.FieldName);
            SignedPlayerIdSearchItem.Operation = GetFieldOperation(SignedPlayerIdSearchItem.FieldName);
            SignedPlayerIdSearchItem.BeginValue = GetFieldBeginValue<int>(SignedPlayerIdSearchItem.FieldName);
            SignedPlayerIdSearchItem.EndValue = GetFieldEndValue<int>(SignedPlayerIdSearchItem.FieldName);

            SignedPlayerNameSearchItem.IsSearchItemChecked = IsPresent(SignedPlayerNameSearchItem.FieldName);
            SignedPlayerNameSearchItem.Operation = GetFieldOperation(SignedPlayerNameSearchItem.FieldName);
            SignedPlayerNameSearchItem.StringValue = GetFieldBeginValue<string>(SignedPlayerNameSearchItem.FieldName);

            SignedPlayerTeamSearchItem.IsSearchItemChecked = IsPresent(SignedPlayerTeamSearchItem.FieldName);
            SignedPlayerTeamSearchItem.Operation = GetFieldOperation(SignedPlayerTeamSearchItem.FieldName);
            SignedPlayerTeamSearchItem.StringValue = GetFieldBeginValue<string>(SignedPlayerTeamSearchItem.FieldName);

            if (FindReplaceOptions.ReplaceField != null)
            {
                SignedPlayerReplaceSearchItem.FieldName = FindReplaceOptions.ReplaceField.Name;
                SignedPlayerReplaceSearchItem.ReplacementValue = FindReplaceOptions.ReplaceField.Value;
                SignedPlayerReplaceSearchItem.IsSearchItemChecked = IsPresent(SignedPlayerReplaceSearchItem.FieldName);
            }
        }

        /// <summary>
        /// Determines whether or not the passed in field name has been included by the user
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns>True if the field name has been included by the user, false otherwise</returns>
        private bool IsPresent(string fieldName)
        {
            if (FindReplaceOptions == null || FindReplaceOptions.FindFields == null)
                return false;
            string noSpaces = string.Copy(fieldName).Replace(" ", "");
            return FindReplaceOptions.FindFields.FirstOrDefault(x => x.Name == noSpaces) != null;
        }

        /// <summary>
        /// Gets the operation value associated with the field
        /// </summary>
        /// <param name="fieldName">The name of the field whose value is to be retrieved</param>
        /// <returns>The operation value for the field, or null if not found</returns>
        private string GetFieldOperation(string fieldName)
        {
            return GetField<string>(fieldName, x => x.Operation);
        }

        /// <summary>
        /// Gets the begin value associated with the field
        /// </summary>
        /// <param name="fieldName">The name of the field whose value is to be retrieved</param>
        /// <returns>The begin value for the field, or null if not found</returns>
        private T GetFieldBeginValue<T>(string fieldName)
        {
            return GetField<T>(fieldName, x => x.BeginValue);
        }

        /// <summary>
        /// Gets the end value associated with the field
        /// </summary>
        /// <param name="fieldName">The name of the field whose value is to be retrieved</param>
        /// <returns>The end value for the field, or null if not found</returns>
        private T GetFieldEndValue<T>(string fieldName)
        {
            return GetField<T>(fieldName, x => x.EndValue);
        }

        /// <summary>
        /// Gets the value for a field
        /// </summary>
        /// <typeparam name="T">The type of value returned</typeparam>
        /// <param name="fieldName">The name of the field whose value is to be retrieved</param>
        /// <param name="selectFunction">The function that identifies which fields to use</param>
        /// <returns>The value for the field, or the default for the type if not found</returns>
        private T GetField<T>(string fieldName, Func<FindField, object> selectFunction)
        {
            if (FindReplaceOptions == null || FindReplaceOptions.FindFields == null)
                return default(T);
            string noSpaces = string.Copy(fieldName).Replace(" ", "");
            var list = FindReplaceOptions.FindFields.Where(x => x.Name.Equals(noSpaces, StringComparison.OrdinalIgnoreCase)).Select(selectFunction).ToList();
            if (list == null || list.Count == 0)
                return default(T);
            return (T)list[0];
        }

        /// <summary>
        /// Sets the min/max values for the range controls
        /// </summary>
        private void SetupMinMaxRanges()
        {
            YearFoundedSearchItem.MinRange = RugbyModel.Team.MinYearFounded;
            YearFoundedSearchItem.MaxRange = RugbyModel.Team.MaxYearFounded;

            PlayerIdSearchItem.MinRange = 1;
            PlayerIdSearchItem.MaxRange = int.MaxValue;
            HeightSearchItem.MinRange = RugbyModel.Player.MinHeight;
            HeightSearchItem.MaxRange = RugbyModel.Player.MaxHeight;
            WeightSearchItem.MinRange = RugbyModel.Player.MinWeight;
            WeightSearchItem.MaxRange = RugbyModel.Player.MaxWeight;

            SignedPlayerIdSearchItem.MinRange = 1;
            SignedPlayerIdSearchItem.MaxRange = int.MaxValue;
        }

        /// <summary>
        /// Sets this dialog's custom event handler on each search item control
        /// </summary>
        private void SetupEventHandlers()
        {
            TeamNameSearchItem.SearchItemEnabledEvent += SearchItem_EnabledOrChangedEvent;
            TeamNameSearchItem.SearchItemChangedEvent += SearchItem_EnabledOrChangedEvent;
            HomeGroundSearchItem.SearchItemEnabledEvent += SearchItem_EnabledOrChangedEvent;
            HomeGroundSearchItem.SearchItemChangedEvent += SearchItem_EnabledOrChangedEvent;
            CoachSearchItem.SearchItemEnabledEvent += SearchItem_EnabledOrChangedEvent;
            CoachSearchItem.SearchItemChangedEvent += SearchItem_EnabledOrChangedEvent;
            RegionSearchItem.SearchItemEnabledEvent += SearchItem_EnabledOrChangedEvent;
            RegionSearchItem.SearchItemChangedEvent += SearchItem_EnabledOrChangedEvent;
            YearFoundedSearchItem.SearchItemEnabledEvent += SearchItem_EnabledOrChangedEvent;
            YearFoundedSearchItem.SearchItemChangedEvent += SearchItem_EnabledOrChangedEvent;
            TeamReplaceSearchItem.SearchItemEnabledEvent += SearchItem_EnabledOrChangedEvent;
            TeamReplaceSearchItem.SearchItemChangedEvent += SearchItem_EnabledOrChangedEvent;

            PlayerIdSearchItem.SearchItemEnabledEvent += SearchItem_EnabledOrChangedEvent;
            PlayerIdSearchItem.SearchItemChangedEvent += SearchItem_EnabledOrChangedEvent;
            FirstNameSearchItem.SearchItemEnabledEvent += SearchItem_EnabledOrChangedEvent;
            FirstNameSearchItem.SearchItemChangedEvent += SearchItem_EnabledOrChangedEvent;
            LastNameSearchItem.SearchItemEnabledEvent += SearchItem_EnabledOrChangedEvent;
            LastNameSearchItem.SearchItemChangedEvent += SearchItem_EnabledOrChangedEvent;
            HeightSearchItem.SearchItemEnabledEvent += SearchItem_EnabledOrChangedEvent;
            HeightSearchItem.SearchItemChangedEvent += SearchItem_EnabledOrChangedEvent;
            WeightSearchItem.SearchItemEnabledEvent += SearchItem_EnabledOrChangedEvent;
            WeightSearchItem.SearchItemChangedEvent += SearchItem_EnabledOrChangedEvent;
            DateOfBirthSearchItem.SearchItemEnabledEvent += SearchItem_EnabledOrChangedEvent;
            DateOfBirthSearchItem.SearchItemChangedEvent += SearchItem_EnabledOrChangedEvent;
            PlaceOfBirthSearchItem.SearchItemEnabledEvent += SearchItem_EnabledOrChangedEvent;
            PlaceOfBirthSearchItem.SearchItemChangedEvent += SearchItem_EnabledOrChangedEvent;
            PlayerReplaceSearchItem.SearchItemEnabledEvent += SearchItem_EnabledOrChangedEvent;
            PlayerReplaceSearchItem.SearchItemChangedEvent += SearchItem_EnabledOrChangedEvent;

            SignedPlayerIdSearchItem.SearchItemEnabledEvent += SearchItem_EnabledOrChangedEvent;
            SignedPlayerIdSearchItem.SearchItemChangedEvent += SearchItem_EnabledOrChangedEvent;
            SignedPlayerNameSearchItem.SearchItemEnabledEvent += SearchItem_EnabledOrChangedEvent;
            SignedPlayerNameSearchItem.SearchItemChangedEvent += SearchItem_EnabledOrChangedEvent;
            SignedPlayerTeamSearchItem.SearchItemEnabledEvent += SearchItem_EnabledOrChangedEvent;
            SignedPlayerTeamSearchItem.SearchItemChangedEvent += SearchItem_EnabledOrChangedEvent;
            SignedPlayerReplaceSearchItem.SearchItemEnabledEvent += SearchItem_EnabledOrChangedEvent;
            SignedPlayerReplaceSearchItem.SearchItemChangedEvent += SearchItem_EnabledOrChangedEvent;
        }

        /// <summary>
        /// Inserts the fields of each dataset into the search items.
        /// </summary>
        private void SetupReplacementFields()
        {
            TeamReplaceSearchItem.Enabled = false;
            TeamReplaceSearchItem.Fields = new List<UserControls.ReplaceSearchItem.Field>()
            {
                new UserControls.ReplaceSearchItem.Field() { Name = nameof(RugbyModel.Team.Name), Type = typeof(string) },
                new UserControls.ReplaceSearchItem.Field() { Name = nameof(RugbyModel.Team.HomeGround), Type = typeof(string) },
                new UserControls.ReplaceSearchItem.Field() { Name = nameof(RugbyModel.Team.Coach), Type = typeof(string) },
                new UserControls.ReplaceSearchItem.Field() { Name = nameof(RugbyModel.Team.Region), Type = typeof(string) },
                new UserControls.ReplaceSearchItem.Field() { Name = nameof(RugbyModel.Team.YearFounded), Type = typeof(int) }
            };

            PlayerReplaceSearchItem.Enabled = false;
            PlayerReplaceSearchItem.Fields = new List<UserControls.ReplaceSearchItem.Field>()
            {
                new UserControls.ReplaceSearchItem.Field() { Name = nameof(RugbyModel.Player.Id), Type = typeof(int) },
                new UserControls.ReplaceSearchItem.Field() { Name = nameof(RugbyModel.Player.FirstName), Type = typeof(string) },
                new UserControls.ReplaceSearchItem.Field() { Name = nameof(RugbyModel.Player.LastName), Type = typeof(string) },
                new UserControls.ReplaceSearchItem.Field() { Name = nameof(RugbyModel.Player.Height), Type = typeof(int) },
                new UserControls.ReplaceSearchItem.Field() { Name = nameof(RugbyModel.Player.Weight), Type = typeof(int) },
                new UserControls.ReplaceSearchItem.Field() { Name = nameof(RugbyModel.Player.DateOfBirth), Type = typeof(DateTime) },
                new UserControls.ReplaceSearchItem.Field() { Name = nameof(RugbyModel.Player.PlaceOfBirth), Type = typeof(string) }
            };

            SignedPlayerReplaceSearchItem.Enabled = false;
            SignedPlayerReplaceSearchItem.Fields = new List<UserControls.ReplaceSearchItem.Field>()
            {
                new UserControls.ReplaceSearchItem.Field() { Name = nameof(RugbyModel.SignedPlayer.PlayerId), Type = typeof(int) },
                new UserControls.ReplaceSearchItem.Field() { Name = nameof(RugbyModel.SignedPlayer.PlayerName), Type = typeof(string) },
                new UserControls.ReplaceSearchItem.Field() { Name = nameof(RugbyModel.SignedPlayer.TeamName), Type = typeof(string) }
            };
        }

        /// <summary>
        /// The event handler that's called when the user changes the current tab
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void MainTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            RebuildOutputText();
        }

        /// <summary>
        /// The event handler that's called when the user interacts with a search item control
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void SearchItem_EnabledOrChangedEvent(object sender, EventArgs e)
        {
            RebuildOutputText();
        }

        /// <summary>
        /// The event handler that's called when the user clicks the find/replace button. The application responds by
        /// delegating to the input validation for the application page.
        /// </summary>
        /// <param name="sender">The object calling this handler</param>
        /// <param name="e">Arguments for this event</param>
        private void FindReplaceButton_Click(object sender, EventArgs e)
        {
            if (MainTabControl.SelectedTab == TeamsTabPage)
                ValidateTeamTabAndCloseDialog();
            else if (MainTabControl.SelectedTab == PlayersTabPage)
                ValidatePlayerTabAndCloseDialog();
            else if (MainTabControl.SelectedTab == SignedPlayersTabPage)
                ValidateSignedPlayerTabAndCloseDialog();
        }

        /// <summary>
        /// Executes the input validation for the team tab
        /// </summary>
        private void ValidateTeamTabAndCloseDialog()
        {
            if (!IsStringSearchItemValid("team name", TeamNameSearchItem))
                return;
            if (!IsStringSearchItemValid("home ground", HomeGroundSearchItem))
                return;
            if (!IsStringSearchItemValid("coach", CoachSearchItem))
                return;
            if (!IsStringSearchItemValid("region", RegionSearchItem))
                return;
            if (!IsReplaceSearchItemValid(TeamReplaceSearchItem))
                return;
            BuildTeamsFindOptions();
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Executes the input validation for the team tab
        /// </summary>
        private void ValidatePlayerTabAndCloseDialog()
        {
            if (!IsStringSearchItemValid("first name", FirstNameSearchItem))
                return;
            if (!IsStringSearchItemValid("last name", LastNameSearchItem))
                return;
            if (!IsStringSearchItemValid("place of birth", PlaceOfBirthSearchItem))
                return;
            if (!IsReplaceSearchItemValid(PlayerReplaceSearchItem))
                return;
            BuildPlayersFindOptions();
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Executes the input validation for the signed player tab
        /// </summary>
        private void ValidateSignedPlayerTabAndCloseDialog()
        {
            if (!IsStringSearchItemValid("player name", SignedPlayerNameSearchItem))
                return;
            if (!IsStringSearchItemValid("team name", SignedPlayerTeamSearchItem))
                return;
            if (!IsReplaceSearchItemValid(SignedPlayerReplaceSearchItem))
                return;
            BuildSignedPlayersFindOptions();
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Bundles the user's input into a class that represents which choices the user made while
        /// interacting with the teams tab. The code that invoked this dialog can use the information
        /// to execute a find/replace operation.
        /// </summary>
        private void BuildTeamsFindOptions()
        {
            FindReplaceOptions = new AdvancedFindReplaceOptions
            {
                FindFields = new List<FindField>()
            };
            FindReplaceOptions.FindWhat = AdvancedFindReplaceOptions.What.Teams;

            if (TeamNameSearchItem.IsSearchItemChecked)
                FindReplaceOptions.FindFields.Add(TeamNameSearchItem.ToFindField());
            if (HomeGroundSearchItem.IsSearchItemChecked)
                FindReplaceOptions.FindFields.Add(HomeGroundSearchItem.ToFindField());
            if (CoachSearchItem.IsSearchItemChecked)
                FindReplaceOptions.FindFields.Add(CoachSearchItem.ToFindField());
            if (RegionSearchItem.IsSearchItemChecked)
                FindReplaceOptions.FindFields.Add(RegionSearchItem.ToFindField());
            if (YearFoundedSearchItem.IsSearchItemChecked)
                FindReplaceOptions.FindFields.Add(YearFoundedSearchItem.ToFindField());
            if (TeamReplaceSearchItem.IsSearchItemChecked)
                FindReplaceOptions.ReplaceField = TeamReplaceSearchItem.ToReplaceField();
        }

        /// <summary>
        /// Bundles the user's input into a class that represents which choices the user made while
        /// interacting with the players tab. The code that invoked this dialog can use the information
        /// to execute a find/replace operation.
        /// </summary>
        private void BuildPlayersFindOptions()
        {
            FindReplaceOptions = new AdvancedFindReplaceOptions
            {
                FindFields = new List<FindField>()
            };
            FindReplaceOptions.FindWhat = AdvancedFindReplaceOptions.What.Players;

            if (PlayerIdSearchItem.IsSearchItemChecked)
                FindReplaceOptions.FindFields.Add(PlayerIdSearchItem.ToFindField());
            if (FirstNameSearchItem.IsSearchItemChecked)
                FindReplaceOptions.FindFields.Add(FirstNameSearchItem.ToFindField());
            if (LastNameSearchItem.IsSearchItemChecked)
                FindReplaceOptions.FindFields.Add(LastNameSearchItem.ToFindField());
            if (HeightSearchItem.IsSearchItemChecked)
                FindReplaceOptions.FindFields.Add(HeightSearchItem.ToFindField());
            if (WeightSearchItem.IsSearchItemChecked)
                FindReplaceOptions.FindFields.Add(WeightSearchItem.ToFindField());
            if (DateOfBirthSearchItem.IsSearchItemChecked)
                FindReplaceOptions.FindFields.Add(DateOfBirthSearchItem.ToFindField());
            if (PlaceOfBirthSearchItem.IsSearchItemChecked)
                FindReplaceOptions.FindFields.Add(PlaceOfBirthSearchItem.ToFindField());
            if (PlayerReplaceSearchItem.IsSearchItemChecked)
                FindReplaceOptions.ReplaceField = PlayerReplaceSearchItem.ToReplaceField();
        }

        /// <summary>
        /// Bundles the user's input into a class that represents which choices the user made while
        /// interacting with the signed players tab. The code that invoked this dialog can use the
        /// information to execute a find/replace operation.
        /// </summary>
        private void BuildSignedPlayersFindOptions()
        {
            FindReplaceOptions = new AdvancedFindReplaceOptions
            {
                FindFields = new List<FindField>()
            };
            FindReplaceOptions.FindWhat = AdvancedFindReplaceOptions.What.SignedPlayers;

            if (SignedPlayerIdSearchItem.IsSearchItemChecked)
                FindReplaceOptions.FindFields.Add(SignedPlayerIdSearchItem.ToFindField());
            if (SignedPlayerNameSearchItem.IsSearchItemChecked)
                FindReplaceOptions.FindFields.Add(SignedPlayerNameSearchItem.ToFindField());
            if (SignedPlayerTeamSearchItem.IsSearchItemChecked)
                FindReplaceOptions.FindFields.Add(SignedPlayerTeamSearchItem.ToFindField());
            if (SignedPlayerReplaceSearchItem.IsSearchItemChecked)
                FindReplaceOptions.ReplaceField = SignedPlayerReplaceSearchItem.ToReplaceField();
        }

        /// <summary>
        /// Determines whether or not the passed in search item + field are valid.
        /// </summary>
        /// <param name="fieldName">The name of the field to validate</param>
        /// <param name="searchItem">The search item to validate</param>
        /// <returns>True if the search item + field are valid, false otherwise</returns>
        private bool IsStringSearchItemValid(string fieldName, UserControls.StringSearchItem searchItem)
        {
            if (!searchItem.IsSearchItemChecked)
                return true;
            return ValidateStringField(fieldName, searchItem.StringValue, searchItem);
        }

        /// <summary>
        /// Determines whether or not the passed in replace search item is valid.
        /// </summary>
        /// <param name="searchItem">The replace search item to validate</param>
        /// <returns>True if the replace search item is valid, false otherwise</returns>
        private bool IsReplaceSearchItemValid(UserControls.ReplaceSearchItem searchItem)
        {
            if (!searchItem.IsSearchItemChecked)
                return true;
            if (string.IsNullOrEmpty(searchItem.FieldName) || searchItem.FieldName == UserControls.ReplaceSearchItem.NotYetChosen)
            {
                MessageBox.Show(this, "The replace field is required", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                searchItem.Focus();
                return false;
            }
            return ValidateStringField("replacement value", searchItem.ReplacementValue as string, searchItem);
        }

        /// <summary>
        /// Determines whether or not the field + value + control have been satisfactorily filled in by the user.
        /// </summary>
        /// <param name="fieldName">The name of the field to validate</param>
        /// <param name="fieldValue">The value of the field to validate</param>
        /// <param name="control">The control to validate</param>
        /// <returns></returns>
        private bool ValidateStringField(string fieldName, string fieldValue, Control control)
        {
            if (string.IsNullOrEmpty(fieldValue))
            {
                MessageBox.Show(this, $"The {fieldName} field is required", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                control.Focus();
                return false;
            }
            if (fieldValue.Contains(";"))
            {
                MessageBox.Show(this, $"The {fieldName} field contains the character ';'. This character is forbidden.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                control.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Delegates the rebuilding of the output text to the current tab
        /// </summary>
        private void RebuildOutputText()
        {
            if (MainTabControl.SelectedTab == TeamsTabPage)
                RebuildTeamsOutputText();
            else if (MainTabControl.SelectedTab == PlayersTabPage)
                RebuildPlayersOutputText();
            else if (MainTabControl.SelectedTab == SignedPlayersTabPage)
                RebuildSignedPlayersOutputText();
        }

        /// <summary>
        /// Rebuilds the output text based on the checked search items for the teams tab
        /// </summary>
        private void RebuildTeamsOutputText()
        {
            string text = string.Empty;
            string verb = "WHERE";
            if (TeamNameSearchItem.IsSearchItemChecked)
            {
                text += $"{verb} {TeamNameSearchItem.ToDisplayString()}\r\n";
                verb = "AND";
            }
            if (HomeGroundSearchItem.IsSearchItemChecked)
            {
                text += $"{verb} {HomeGroundSearchItem.ToDisplayString()}\r\n";
                verb = "AND";
            }
            if (CoachSearchItem.IsSearchItemChecked)
            {
                text += $"{verb} {CoachSearchItem.ToDisplayString()}\r\n";
                verb = "AND";
            }
            if (RegionSearchItem.IsSearchItemChecked)
            {
                text += $"{verb} {RegionSearchItem.ToDisplayString()}\r\n";
                verb = "AND";
            }
            if (YearFoundedSearchItem.IsSearchItemChecked)
                text += $"{verb} {YearFoundedSearchItem.ToDisplayString()}\r\n";

            TeamReplaceSearchItem.Enabled = !string.IsNullOrEmpty(text);
            if (TeamReplaceSearchItem.IsSearchItemChecked)
                text += $"REPLACE {TeamReplaceSearchItem.ToDisplayString()}\r\n";

            if (string.IsNullOrEmpty(text))
                OutputTextBox.Text = string.Empty;
            else
                OutputTextBox.Text = "FIND all teams\r\n" + text;

            if (TeamReplaceSearchItem.Enabled && TeamReplaceSearchItem.IsSearchItemChecked)
                FindReplaceButton.Text = "&Replace";
            else
                FindReplaceButton.Text = "Find";
        }

        /// <summary>
        /// Rebuilds the output text based on the checked search items for the players tab
        /// </summary>
        private void RebuildPlayersOutputText()
        {
            string text = string.Empty;
            string verb = "WHERE";
            if (PlayerIdSearchItem.IsSearchItemChecked)
            {
                text += $"{verb} {PlayerIdSearchItem.ToDisplayString()}\r\n";
                verb = "AND";
            }
            if (FirstNameSearchItem.IsSearchItemChecked)
            {
                text += $"{verb} {FirstNameSearchItem.ToDisplayString()}\r\n";
                verb = "AND";
            }
            if (LastNameSearchItem.IsSearchItemChecked)
            {
                text += $"{verb} {LastNameSearchItem.ToDisplayString()}\r\n";
                verb = "AND";
            }
            if (HeightSearchItem.IsSearchItemChecked)
            {
                text += $"{verb} {HeightSearchItem.ToDisplayString()}\r\n";
                verb = "AND";
            }
            if (WeightSearchItem.IsSearchItemChecked)
            {
                text += $"{verb} {WeightSearchItem.ToDisplayString()}\r\n";
                verb = "AND";
            }
            if (DateOfBirthSearchItem.IsSearchItemChecked)
            {
                text += $"{verb} {DateOfBirthSearchItem.ToDisplayString()}\r\n";
                verb = "AND";
            }
            if (PlaceOfBirthSearchItem.IsSearchItemChecked)
                text += $"{verb} {PlaceOfBirthSearchItem.ToDisplayString()}\r\n";

            PlayerReplaceSearchItem.Enabled = !string.IsNullOrEmpty(text);
            if (PlayerReplaceSearchItem.IsSearchItemChecked)
                text += $"REPLACE {PlayerReplaceSearchItem.ToDisplayString()}\r\n";

            if (string.IsNullOrEmpty(text))
                OutputTextBox.Text = string.Empty;
            else
                OutputTextBox.Text = "FIND all players\r\n" + text;

            if (PlayerReplaceSearchItem.Enabled && PlayerReplaceSearchItem.IsSearchItemChecked)
                FindReplaceButton.Text = "&Replace";
            else
                FindReplaceButton.Text = "Find";
        }

        /// <summary>
        /// Rebuilds the output text based on the checked search items for the signed players tab
        /// </summary>
        private void RebuildSignedPlayersOutputText()
        {
            string text = string.Empty;
            string verb = "WHERE";
            if (SignedPlayerIdSearchItem.IsSearchItemChecked)
            {
                text += $"{verb} {SignedPlayerIdSearchItem.ToDisplayString()}\r\n";
                verb = "AND";
            }
            if (SignedPlayerNameSearchItem.IsSearchItemChecked)
            {
                text += $"{verb} {SignedPlayerNameSearchItem.ToDisplayString()}\r\n";
                verb = "AND";
            }
            if (SignedPlayerTeamSearchItem.IsSearchItemChecked)
                text += $"{verb} {SignedPlayerTeamSearchItem.ToDisplayString()}\r\n";

            SignedPlayerReplaceSearchItem.Enabled = !string.IsNullOrEmpty(text);
            if (SignedPlayerReplaceSearchItem.IsSearchItemChecked)
                text += $"REPLACE {SignedPlayerReplaceSearchItem.ToDisplayString()}\r\n";

            if (string.IsNullOrEmpty(text))
                OutputTextBox.Text = string.Empty;
            else
                OutputTextBox.Text = "FIND all signed players\r\n" + text;

            if (SignedPlayerReplaceSearchItem.Enabled && SignedPlayerReplaceSearchItem.IsSearchItemChecked)
                FindReplaceButton.Text = "&Replace";
            else
                FindReplaceButton.Text = "&Find";
        }
    }
}
