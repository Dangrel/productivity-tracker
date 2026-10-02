using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace test1
{
    public partial class Form1 : Form
    {
        int verticalPosition = 0;
        int verticalChange = 25;
        decimal currentGoal = 0;
        decimal currentEPHGoal = 0;
        // Tracks if the user checked the boxes in the Set Goals menu
        bool isTotalGoalActive = false;
        bool isEPHGoalActive = false;
        // Master Settings Dictionary
        Dictionary<string, AppSetting> appSettings = new Dictionary<string, AppSetting>()
        {
            { "optSaveGoal", new AppSetting { DisplayText = "Save Goal between sessions", Value = true } },
            { "optSaveElements", new AppSetting { DisplayText = "Save Elements list", Value = true } },
            { "optSaveValues", new AppSetting { DisplayText = "Save Element Values", Value = true } },
            { "optSaveEntries", new AppSetting { DisplayText = "Save Element Entries", Value = true } },
            { "optDailyLogging", new AppSetting { DisplayText = "Enable Daily Text File Logging", Value = true } },
            { "SkipDeleteWarning", new AppSetting { DisplayText = "Skip Deletion Warnings", Value = false } },
            { "optDarkMode", new AppSetting { DisplayText = "Enable Dark Mode", Value = false } }
        };
        // Remembers the path to the user's chosen save folder
        string saveDirectory = "";
        List<ElementRow> trackedRows = new List<ElementRow>();
        // Stopwatch variables
        int totalStopwatchSeconds = 0;
        bool skipStopwatchWarning = false;


        public Form1()
        {
            InitializeComponent();
            // Apply the Light Theme immediately
            ApplyLightTheme();
            // Lock the minimum window size to whatever we set in the visual designer
            this.MinimumSize = this.Size;

            // The (s, args) catches the required button click data, and the => runs your custom method
            btnAddElement.Click += (s, args) => CreateRow("", "0", "0");
            //btnSetGoal.Click += BtnSetGoal_Click;
            btnSettings.Click += BtnSettings_Click;
            // --- FOCUS MANAGEMENT ---
            // If the user clicks the blank background of the main window, drop the textbox focus
            this.Click += (s, args) => this.ActiveControl = null;
            // --- RESPONSIVE UI TRIGGERS ---
            pnlElements.Resize += (s, args) =>
            {
                pnlRowHeaders.Width = pnlElements.Width; // Force header panel to match!
                ResizeResponsiveRows();
            };

            // Wire the Paint events to the panels
            pnlProgressBar.Paint += PnlProgressBar_Paint;
            pnlSpeedBar.Paint += PnlSpeedBar_Paint;

            // Redraw whenever the user types a new goal or checks a box
            EventHandler redrawGoals = (s, args) =>
            {
                txtGoal.Enabled = chkGoal.Checked;
                txtEPHGoal.Enabled = chkEPHGoal.Checked;
                pnlProgressBar.Invalidate();
                pnlSpeedBar.Invalidate();
                UpdateTotalValue();       // Recalculates total colors
                UpdateStopwatchDisplay(); // Recalculates EPH colors
            };

            chkGoal.CheckedChanged += redrawGoals;
            txtGoal.TextChanged += redrawGoals;
            chkEPHGoal.CheckedChanged += redrawGoals;
            txtEPHGoal.TextChanged += redrawGoals;

            // If the user clicks the blank background of the scrollable panel, drop the textbox focus
            pnlElements.Click += (s, args) => this.ActiveControl = null;

            // --- 1. DIRECTORY SETUP ---
            // Check if we already have a saved directory pointer
            if (File.Exists("config.txt"))
            {
                saveDirectory = File.ReadAllText("config.txt");
            }

            // If there is no pointer, or if the user deleted the folder they originally chose
            if (string.IsNullOrEmpty(saveDirectory) || !Directory.Exists(saveDirectory))
            {
                MessageBox.Show("Welcome! Please select a folder to store your tracker saves and logs.", "First Time Setup");

                // Open the Windows folder selection window
                using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
                {
                    folderDialog.Description = "Select a save folder for Productivity Tracker";

                    if (folderDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Save their choice
                        saveDirectory = folderDialog.SelectedPath;
                        File.WriteAllText("config.txt", saveDirectory);
                    }
                    else
                    {
                        // Fallback: If they click Cancel, default to their Documents folder
                        saveDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                        File.WriteAllText("config.txt", saveDirectory);
                        MessageBox.Show($"Save location defaulted to: {saveDirectory}");
                    }
                }
            }

            // --- UNIFIED LOADING SYSTEM ---
            string masterSavePath = Path.Combine(saveDirectory, "master_save.txt");

            if (File.Exists(masterSavePath))
            {
                string[] savedLines = File.ReadAllLines(masterSavePath);
                // ... keep the rest of your loading loop exactly the same

                foreach (string line in savedLines)
                {
                    string[] parts = line.Split('|');

                    // Route 1: The line is a Goal (Needs exactly 2 parts: GOAL and the Number)
                    //if (parts[0] == "GOAL" && parts.Length == 2)
                    //{
                    //    if (decimal.TryParse(parts[1], out decimal savedGoal) && savedGoal > 0)
                    //    {
                    //        currentGoal = savedGoal;
                    //        lblTotalGoal.Text = currentGoal.ToString();
                    //        lblTotalGoal.Visible = true;
                    //    }
                    //}
                    // Route 2: The line is an Element (Needs exactly 4 parts: ELEMENT, Name, Entries, Value)
                    if (parts[0] == "ELEMENT" && parts.Length == 4)
                    {
                        // parts[1] is the Name, parts[2] is Entries, parts[3] is Value
                        CreateRow(parts[1], parts[2], parts[3]);
                    }
                    // Route 3: The line is a Setting
                    // Route 3: The line is a Setting
                    // Route 3: The line is a Setting
                    else if (parts[0] == "SETTING" && parts.Length == 3)
                    {
                        // Check if the setting exists in our dictionary before trying to update it
                        if (appSettings.ContainsKey(parts[1]))
                        {
                            bool.TryParse(parts[2], out bool savedSettingValue);
                            appSettings[parts[1]].Value = savedSettingValue;
                        }
                    }
                    // Route 4: The Custom Title
                    else if (parts[0] == "TITLE" && parts.Length == 2)
                    {
                        txtAppTitle.Text = parts[1];
                    }
                    // Route 5: The EPH Goal
                    //else if (parts[0] == "EPHGOAL" && parts.Length == 2)
                    //{
                    //    if (decimal.TryParse(parts[1], out decimal savedEPHGoal))
                    //    {
                    //        currentEPHGoal = savedEPHGoal;
                    //    }
                    //}
                    //else if (parts[0] == "GOAL_ACTIVE" && parts.Length == 2)
                    //{
                    //    bool.TryParse(parts[1], out isTotalGoalActive);
                    //}
                    //else if (parts[0] == "EPHGOAL_ACTIVE" && parts.Length == 2)
                    //{
                    //    bool.TryParse(parts[1], out isEPHGoalActive);
                    //}
                }

                // Update the math and colors once everything is loaded
                UpdateTotalValue();
                SaveMasterData();
                //UpdateGoalPositions();
                AlignHeaders();
            }

            // --- DYNAMIC EPH TITLE ---
            txtAppTitle.TextChanged += (s, args) =>
            {
                // Replace the default word "Entries" with whatever the user typed
                string title = string.IsNullOrWhiteSpace(txtAppTitle.Text) ? "Entries" : txtAppTitle.Text;
                lblEPHText.Text = $"{title} per hour";
            };

            // Trigger it once on startup so it loads correctly
            lblEPHText.Text = $"{txtAppTitle.Text} per hour";

            // --- CLEAR ALL ENTRIES ---
            btnClearAll.Click += (s, args) =>
            {
                // Reset every single row's entry box to 0
                foreach (var row in trackedRows)
                {
                    row.EntriesBox.Text = "0";
                }

                UpdateTotalValue();
                SaveMasterData();

                // Log the master clear action
                string logEntry = $"Time: {DateTime.Now.ToString("hh:mm:ss tt")}, Name: ALL, Action: MASTER CLEAR, Current Value: {lblTotalValue.Text}";
                SaveLogEntry(logEntry);
                dgvLogs.Rows.Add("ALL", "MASTER CLEAR", lblTotalValue.Text, DateTime.Now.ToString("hh:mm:ss tt"));
            };

            // --- NEW SESSION (NUKE EVERYTHING) ---
            btnNewSession.Click += (s, args) =>
            {
                // 1. Visually remove every control from the screen
                foreach (var row in trackedRows)
                {
                    foreach (Control uiElement in row.GetAllControls())
                    {
                        pnlElements.Controls.Remove(uiElement);
                    }
                }

                // 2. Erase the backend memory
                trackedRows.Clear();
                dgvLogs.Rows.Clear();

                // 3. Reset the master variables
                verticalPosition = 0; // Use whatever your starting Y coordinate was!
                currentGoal = 0;
                currentEPHGoal = 0;
                //lblTotalGoal.Visible = false;

                // 4. Force a label update
                isTotalGoalActive = false;
                isEPHGoalActive = false;
                UpdateTotalValue();
                SaveMasterData();
                SaveLogEntry($"Time: {DateTime.Now.ToString("hh:mm:ss tt")}, Name: ALL, Action: NEW SESSION STARTED, Current Value: 0");

            };
            // --- OPEN SAVE DIRECTORY ---
            btnOpenFolder.Click += (s, args) =>
            {
                // System.Diagnostics.Process talks directly to the Windows operating system
                if (Directory.Exists(saveDirectory))
                {
                    System.Diagnostics.Process.Start("explorer.exe", saveDirectory);
                }
                else
                {
                    MessageBox.Show("Save folder not found!");
                }
            };
            // --- STOPWATCH CONTROLS ---

            // 1. Checkbox Enabler
            chkEnableStopwatch.Checked = true; // Trigger it once to lock the panel on startup
            chkEnableStopwatch.CheckedChanged += (s, args) =>
            {
                bool isEnabled = chkEnableStopwatch.Checked;
                btnStartPause.Enabled = isEnabled;
                btnReset.Enabled = isEnabled;
                txtHours.Enabled = isEnabled && !tmrStopwatch.Enabled;
                txtMinutes.Enabled = isEnabled && !tmrStopwatch.Enabled;
                txtSeconds.Enabled = isEnabled && !tmrStopwatch.Enabled;

                if (!isEnabled && tmrStopwatch.Enabled)
                {
                    // PAUSE IT
                    tmrStopwatch.Stop();
                    btnStartPause.Text = "Start";
                }
            };
            chkEnableStopwatch.Checked = false; // Trigger it once to lock the panel on startup

            // 2. Start / Pause Logic
            btnStartPause.Click += (s, args) =>
            {
                if (tmrStopwatch.Enabled)
                {
                    // PAUSE IT
                    tmrStopwatch.Stop();
                    btnStartPause.Text = "Start";
                    btnReset.Enabled = true; // Unlock reset

                    // Unlock text boxes for manual editing
                    txtHours.ReadOnly = false;
                    txtMinutes.ReadOnly = false;
                    txtSeconds.ReadOnly = false;
                }
                else
                {
                    // START IT
                    // Before starting, pull the manual edits from the textboxes just in case they changed them!
                    int.TryParse(txtHours.Text, out int h);
                    int.TryParse(txtMinutes.Text, out int m);
                    int.TryParse(txtSeconds.Text, out int sec);
                    totalStopwatchSeconds = (h * 3600) + (m * 60) + sec;

                    tmrStopwatch.Start();
                    btnStartPause.Text = "Pause";
                    btnReset.Enabled = false; // Lock reset

                    // Lock text boxes so they can't type while it's ticking
                    txtHours.ReadOnly = true;
                    txtMinutes.ReadOnly = true;
                    txtSeconds.ReadOnly = true;
                }
            };

            // 3. Reset Logic
            btnReset.Click += (s, args) =>
            {
                if (totalStopwatchSeconds > 0)
                {
                    if (!ConfirmStopwatchReset()) return;
                }

                totalStopwatchSeconds = 0;
                UpdateStopwatchDisplay();
            };

            // 4. The Timer Tick (Runs every 1 second)
            tmrStopwatch.Tick += (s, args) =>
            {
                totalStopwatchSeconds++;
                UpdateStopwatchDisplay();
            };

            // 5. Manual Editing Logic
            // If the stopwatch is paused and the user clicks away after typing a new time, update everything!
            EventHandler onTimeEdited = (s, args) =>
            {
                if (!tmrStopwatch.Enabled)
                {
                    int.TryParse(txtHours.Text, out int h);
                    int.TryParse(txtMinutes.Text, out int m);
                    int.TryParse(txtSeconds.Text, out int sec);
                    totalStopwatchSeconds = (h * 3600) + (m * 60) + sec;
                    UpdateStopwatchDisplay(); // Recalculates EPH and formats the boxes nicely
                }
            };

            txtHours.Leave += onTimeEdited;
            txtMinutes.Leave += onTimeEdited;
            txtSeconds.Leave += onTimeEdited;
        }

        // 17. UI STYLING
        private void ApplyLightTheme()
        {
            // 1. Define our color palette
            Color bgMain = ColorTranslator.FromHtml("#ADD8E6"); // Light Blue
            Color bgPanel = ColorTranslator.FromHtml("#f0ffff"); //Azure
            Color textMain = ColorTranslator.FromHtml("#2B2D42"); // Dark Navy
            Color actionBlue = ColorTranslator.FromHtml("#3A7CA5"); // Steel Blue
            Color warningRed = ColorTranslator.FromHtml("#E07A5F"); // Muted Terracotta

            // 2. Set the global font and background
            this.BackColor = bgMain;
            this.ForeColor = textMain;
            this.Font = new Font("Segoe UI", 10, FontStyle.Regular); // Modern standard font

            // 3. Style the Panels
            pnlElements.BackColor = bgPanel;
            pnlStopwatch.BackColor = bgPanel;
            pnlStats.BackColor = bgPanel;
            pnlRowHeaders.BackColor = actionBlue;

            // 4. Style all static buttons
            // We put EVERY blue button in this list, including Add Element and Set Goal!
            Button[] actionButtons = { btnSettings, btnOpenFolder, btnStartPause, btnAddElement };

            // We put EVERY red button in this list
            Button[] warningButtons = { btnNewSession, btnClearAll, btnReset };

            foreach (Button btn in actionButtons)
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.BackColor = actionBlue;
                btn.ForeColor = Color.White;
                btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                btn.UseVisualStyleBackColor = false; // This forces Windows to actually apply our White text color!

                btn.Tag = btn.BackColor; // Save the color to memory
                btn.EnabledChanged += FlatButton_EnabledChanged; // Wire up the grey-out logic
            }

            foreach (Button btn in warningButtons)
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.BackColor = warningRed;
                btn.ForeColor = Color.White;
                btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                btn.UseVisualStyleBackColor = false; // This forces Windows to actually apply our White text color!

                btn.Tag = btn.BackColor; // Save the color to memory
                btn.EnabledChanged += FlatButton_EnabledChanged; // Wire up the grey-out logic
            }

            // 5. Upgrade the DataGridView (The big table)
            dgvLogs.BackgroundColor = bgPanel;
            dgvLogs.BorderStyle = BorderStyle.None;
            dgvLogs.RowHeadersVisible = false; // Hide the empty left column
            dgvLogs.EnableHeadersVisualStyles = false; // Allows us to color the top header

            // Style the Header
            dgvLogs.ColumnHeadersDefaultCellStyle.BackColor = actionBlue;
            dgvLogs.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvLogs.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // Style the Rows
            dgvLogs.DefaultCellStyle.BackColor = bgPanel;
            dgvLogs.DefaultCellStyle.ForeColor = textMain;
            dgvLogs.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            dgvLogs.AlternatingRowsDefaultCellStyle.BackColor = bgMain; // Zebra striping!
            dgvLogs.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvLogs.GridColor = ColorTranslator.FromHtml("#D8E2EA"); // Very faint gray lines
        }

        // 18. DYNAMIC DISABLED BUTTON COLORS
        private void FlatButton_EnabledChanged(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Enabled)
            {
                // Restore the original color we saved in the Tag property
                if (btn.Tag is Color originalColor)
                {
                    btn.BackColor = originalColor;
                    btn.ForeColor = Color.White;
                }
            }
            else
            {
                // Apply a muted, greyed-out look
                btn.BackColor = ColorTranslator.FromHtml("#D8E2EA");
                btn.ForeColor = ColorTranslator.FromHtml("#A0AAB2");
            }
        }

        // 8. REUSABLE ROW CREATOR
        private void CreateRow(string startingName, string startingEntries, string startingValue)
        {
            // Notice the X coordinates (the first number in the Point) are shifted right!
            // 1. Create the UI elements using modern flat styles
            Button btnClear = new Button { Text = "C", Location = new Point(20, verticalPosition), Width = 30, FlatStyle = FlatStyle.Flat, BackColor = ColorTranslator.FromHtml("#E07A5F"), ForeColor = Color.White };
            btnClear.FlatAppearance.BorderSize = 0;

            Button btnMinus = new Button { Text = "-", Location = new Point(60, verticalPosition), Width = 30, FlatStyle = FlatStyle.Flat, BackColor = ColorTranslator.FromHtml("#3A7CA5"), ForeColor = Color.White };
            btnMinus.FlatAppearance.BorderSize = 0;

            TextBox txtName = new TextBox { Text = startingName, Location = new Point(100, verticalPosition), Width = 150, BorderStyle = BorderStyle.FixedSingle };

            Button btnPlus = new Button { Text = "+", Location = new Point(260, verticalPosition), Width = 30, FlatStyle = FlatStyle.Flat, BackColor = ColorTranslator.FromHtml("#3A7CA5"), ForeColor = Color.White };
            btnPlus.FlatAppearance.BorderSize = 0;

            TextBox txtEntries = new TextBox { Text = startingEntries, Location = new Point(300, verticalPosition), Width = 50, BorderStyle = BorderStyle.FixedSingle };

            TextBox txtValue = new TextBox { Text = startingValue, Location = new Point(360, verticalPosition), Width = 50, BorderStyle = BorderStyle.FixedSingle };

            Button btnDelete = new Button { Text = "X", Location = new Point(420, verticalPosition), Width = 30, FlatStyle = FlatStyle.Flat, BackColor = ColorTranslator.FromHtml("#E07A5F"), ForeColor = Color.White };
            btnDelete.FlatAppearance.BorderSize = 0;

            ElementRow newRow = new ElementRow
            {
                ClearButton = btnClear,
                MinusButton = btnMinus,
                NameBox = txtName,
                PlusButton = btnPlus,
                EntriesBox = txtEntries,
                ValueBox = txtValue,
                DeleteButton = btnDelete
            };
            trackedRows.Add(newRow);

            foreach (Control uiElement in newRow.GetAllControls())
            {
                pnlElements.Controls.Add(uiElement);
            }

            btnPlus.Click += (s, args) => HandleEntryChange(newRow, 1);
            btnMinus.Click += (s, args) => HandleEntryChange(newRow, -1);
            btnDelete.Click += (s, args) => DeleteRow(newRow);

            // --- INPUT VALIDATION ---

            // 1. Force Entries box to ONLY accept whole numbers and the Backspace key
            txtEntries.KeyPress += (s, args) =>
            {
                // If the key is NOT a control key (like Backspace) AND NOT a digit (0-9)
                if (!char.IsControl(args.KeyChar) && !char.IsDigit(args.KeyChar))
                {
                    args.Handled = true; // This tells C# "I handled it, don't type this character on screen"
                }
            };

            // 2. Force Value box to accept numbers, Backspace, and exactly ONE decimal point
            txtValue.KeyPress += (s, args) =>
            {
                // Allow numbers, backspace, and the decimal point
                if (!char.IsControl(args.KeyChar) && !char.IsDigit(args.KeyChar) && (args.KeyChar != '.'))
                {
                    args.Handled = true;
                }

                // Prevent the user from typing a second decimal point (e.g., 5.9.2)
                if ((args.KeyChar == '.') && (txtValue.Text.IndexOf('.') > -1))
                {
                    args.Handled = true;
                }
            };
            // --- SMART LOGGING LOGIC ---

            // 1. When the user clicks INTO the box, memorize the starting text using the Tag property
            txtEntries.Enter += (s, args) => txtEntries.Tag = txtEntries.Text;
            txtValue.Enter += (s, args) => txtValue.Tag = txtValue.Text;
            txtAppTitle.Enter += (s, args) => txtAppTitle.Tag = txtAppTitle.Text;

            // 2. When the user clicks AWAY, check if the text changed
            txtAppTitle.Leave += (s, args) =>
            {
                // Only run the save logic if the memory isn't blank AND the text is different from the memory
                if (txtAppTitle.Tag != null && txtAppTitle.Text != txtAppTitle.Tag.ToString())
                {
                    SaveMasterData();
                    SaveLogEntry($"Time: {DateTime.Now.ToString("hh:mm:ss tt")}, Name: {"TITLE"}, Action: TITLE EDIT, Current Value: {lblTotalValue.Text}");
                    dgvLogs.Rows.Add("TITLE", "TITLE UPDATED", lblTotalValue.Text, DateTime.Now.ToString("hh:mm:ss tt"));

                    // Update the memory sticky note so it doesn't trigger again if they click in and out without typing
                    txtAppTitle.Tag = txtAppTitle.Text;
                }
            };

            // 2. When the user clicks AWAY, check if the text changed
            txtEntries.Leave += (s, args) =>
            {
                // Only run the save logic if the memory isn't blank AND the text is different from the memory
                if (txtEntries.Tag != null && txtEntries.Text != txtEntries.Tag.ToString())
                {
                    UpdateTotalValue();
                    SaveLogEntry($"Time: {DateTime.Now.ToString("hh:mm:ss tt")}, Name: {newRow.NameBox.Text}, Action: ENTRIES EDIT, Current Value: {lblTotalValue.Text}");
                    dgvLogs.Rows.Add(newRow.NameBox.Text, "ENTRIES UPDATED", lblTotalValue.Text, DateTime.Now.ToString("hh:mm:ss tt"));

                    // Update the memory sticky note so it doesn't trigger again if they click in and out without typing
                    txtEntries.Tag = txtEntries.Text;
                }
            };

            txtValue.Leave += (s, args) =>
            {
                if (txtValue.Tag != null && txtValue.Text != txtValue.Tag.ToString())
                {
                    UpdateTotalValue();
                    SaveLogEntry($"Time: {DateTime.Now.ToString("hh:mm:ss tt")}, Name: {newRow.NameBox.Text}, Action: VALUE EDIT, Current Value: {lblTotalValue.Text}");
                    dgvLogs.Rows.Add(newRow.NameBox.Text, "VALUE UPDATED", lblTotalValue.Text, DateTime.Now.ToString("hh:mm:ss tt"));

                    txtValue.Tag = txtValue.Text;
                }
            };
            // Wire up the new Clear logic
            btnClear.Click += (s, args) =>
            {
                newRow.EntriesBox.Text = "0";
                UpdateTotalValue();

                string logEntry = $"Time: {DateTime.Now.ToString("hh:mm:ss tt")}, Name: {newRow.NameBox.Text}, Action: CLEARED, Current Value: {lblTotalValue.Text}";
                SaveLogEntry(logEntry);
                dgvLogs.Rows.Add(newRow.NameBox.Text, "CLEARED", lblTotalValue.Text, DateTime.Now.ToString("hh:mm:ss tt"));
            };

            verticalPosition += verticalChange;
            ResizeResponsiveRows();
            SaveMasterData();
        }

        // 19. RESPONSIVE HEADER ALIGNMENT
        private void AlignHeaders()
        {
            int currentWidth = pnlElements.ClientSize.Width;
            if (currentWidth < 400) currentWidth = 400;

            // Recreate the exact same math used for the right-aligned textboxes
            int deleteLeft = currentWidth - 30 - 10; // Delete button is 30px wide
            int valueLeft = deleteLeft - 50 - 10;    // Value box is 50px wide
            int entriesLeft = valueLeft - 50 - 5;    // Entries box is 50px wide
            int nameLeft = 10 + 30 + 5 + 30 + 10;    // Left margin + Clear + gap + Minus + gap

            // Apply the coordinates directly (Removed pnlElements.Left!)
            lblNameHeader.Left = nameLeft;
            lblEntriesHeader.Left = entriesLeft;
            lblValueHeader.Left = valueLeft;
            lblDeleteHeader.Left = deleteLeft - 20;
                        
        }

        // 3.5 DELETING A ROW
        private void DeleteRow(ElementRow row)
        {

            // 1. SAFETY CHECK: Does this element have entries above 0?
            // Use TryParse so it safely defaults to 0 if the box is empty or invalid
            int currentEntries = 0;
            int.TryParse(row.EntriesBox.Text, out currentEntries);

            if (currentEntries > 0)
            {
                // Ask for confirmation using our custom pop-up
                bool isConfirmed = ConfirmDeletion(row.NameBox.Text);

                if (!isConfirmed)
                {
                    return;
                }
            }

            // 2. Find the index (position in the list) of the row we are about to delete
            int deletedIndex = trackedRows.IndexOf(row);

            // 3. CLEANUP: Erase the UI elements from the panel using our handy list!
            foreach (Control uiElement in row.GetAllControls())
            {
                pnlElements.Controls.Remove(uiElement);
            }

            // 4. Remove the row from our tracking list
            trackedRows.Remove(row);

            for (int i = deletedIndex; i < trackedRows.Count; i++)
            {
                ElementRow rowToMove = trackedRows[i];

                // Iterate through every single UI element inside this specific row
                foreach (Control uiElement in rowToMove.GetAllControls())
                {
                    uiElement.Top -= verticalChange;
                }
            }

            // 5. Adjust the master vertical position 
            verticalPosition -= verticalChange;

            // Recalculate the grand total immediately
            UpdateTotalValue();
            SaveMasterData();

            // (Optional) Log the deletion to your DataGridView and text file list
            string logEntry = $"Time: {DateTime.Now.ToString("hh:mm:ss tt")}, Name: {row.NameBox.Text}, Action: DELETED, Current Value: {lblTotalValue.Text}";
            SaveLogEntry(logEntry);
            dgvLogs.Rows.Add(row.NameBox.Text, "DELETED", lblTotalValue.Text, DateTime.Now.ToString("hh:mm:ss tt"));
        }

        // 10. DELETION CONFIRMATION POP-UP
        private bool ConfirmDeletion(string elementName)
        {
            // If the user already disabled the warning, instantly give the green light
            if (appSettings["SkipDeleteWarning"].Value) return true;

            // Create the custom warning window
            Form prompt = new Form()
            {
                Width = 380,
                Height = 200,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Confirm Deletion",
                StartPosition = FormStartPosition.CenterParent
            };

            // The warning message
            Label lblWarning = new Label()
            {
                Left = 20,
                Top = 20,
                Width = 320,
                Height = 40,
                Text = $"The element '{elementName}' has active entries. Deleting it will remove these entries from your total. Proceed?"
            };

            // The "Don't show again" checkbox
            CheckBox chkDontShow = new CheckBox()
            {
                Left = 20,
                Top = 70,
                Width = 250,
                Text = "Don't show this message again"
            };

            // The Yes and No buttons
            Button btnProceed = new Button() { Text = "Proceed", Left = 60, Top = 110, Width = 100, DialogResult = DialogResult.Yes };
            Button btnCancel = new Button() { Text = "Cancel", Left = 190, Top = 110, Width = 100, DialogResult = DialogResult.No };

            // Add everything to the window
            prompt.Controls.Add(lblWarning);
            prompt.Controls.Add(chkDontShow);
            prompt.Controls.Add(btnProceed);
            prompt.Controls.Add(btnCancel);

            prompt.AcceptButton = btnProceed;
            prompt.CancelButton = btnCancel;

            // Show the window and wait for the user's choice
            bool userWantsToProceed = (prompt.ShowDialog() == DialogResult.Yes);

            // If they clicked Proceed AND checked the box, update the setting and save it permanently!
            if (userWantsToProceed && chkDontShow.Checked)
            {
                appSettings["SkipDeleteWarning"].Value = true;
                SaveMasterData();
            }

            return userWantsToProceed;
        }

        // 15. STOPWATCH RESET CONFIRMATION
        private bool ConfirmStopwatchReset()
        {
            if (skipStopwatchWarning) return true;

            Form prompt = new Form()
            {
                Width = 380,
                Height = 180,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Confirm Reset",
                StartPosition = FormStartPosition.CenterParent
            };

            Label lblWarning = new Label() { Left = 20, Top = 20, Width = 320, Text = "The stopwatch is not at zero. Are you sure you want to reset your tracked time?" };
            CheckBox chkDontShow = new CheckBox() { Left = 20, Top = 60, Width = 250, Text = "Don't show this message again" };
            Button btnYes = new Button() { Text = "Reset", Left = 60, Top = 90, Width = 100, DialogResult = DialogResult.Yes };
            Button btnNo = new Button() { Text = "Cancel", Left = 190, Top = 90, Width = 100, DialogResult = DialogResult.No };

            prompt.Controls.AddRange(new Control[] { lblWarning, chkDontShow, btnYes, btnNo });
            prompt.AcceptButton = btnYes;
            prompt.CancelButton = btnNo;

            bool userWantsToReset = (prompt.ShowDialog() == DialogResult.Yes);

            if (userWantsToReset && chkDontShow.Checked)
            {
                skipStopwatchWarning = true;
                // Optionally save this to your master settings dictionary here!
            }

            return userWantsToReset;
        }

        // 7. SETTING THE GOAL
        // 7. SETTING THE GOALS
        //private void BtnSetGoal_Click(object sender, EventArgs e)
        //{
        //    Form prompt = new Form()
        //    {
        //        Width = 350,
        //        Height = 220,
        //        FormBorderStyle = FormBorderStyle.FixedDialog,
        //        Text = "Set Goals",
        //        StartPosition = FormStartPosition.CenterParent
        //    };

        //    // --- TOTAL GOAL CONTROLS ---
        //    // Read from the new boolean instead of checking if currentGoal > 0
        //    CheckBox chkTotalGoal = new CheckBox() { Left = 20, Top = 20, Width = 150, Text = "Set total Goal", Checked = isTotalGoalActive };
        //    TextBox txtTotalGoal = new TextBox() { Left = 180, Top = 20, Width = 100, Enabled = chkTotalGoal.Checked };
        //    txtTotalGoal.Text = currentGoal.ToString(); // Always show the number even if it's 0

        //    // --- PER HOUR GOAL CONTROLS ---
        //    CheckBox chkEPHGoal = new CheckBox() { Left = 20, Top = 60, Width = 150, Text = "Set per hour Goal", Checked = isEPHGoalActive };
        //    TextBox txtEPHGoal = new TextBox() { Left = 180, Top = 60, Width = 100, Enabled = chkEPHGoal.Checked };
        //    txtEPHGoal.Text = currentEPHGoal.ToString();

        //    // Link the textboxes to the checkboxes
        //    chkTotalGoal.CheckedChanged += (s, args) => txtTotalGoal.Enabled = chkTotalGoal.Checked;
        //    chkEPHGoal.CheckedChanged += (s, args) => txtEPHGoal.Enabled = chkEPHGoal.Checked;

        //    // --- BUTTONS ---
        //    Button btnOK = new Button() { Text = "Save", Left = 80, Top = 120, Width = 75, DialogResult = DialogResult.OK };
        //    Button btnCancel = new Button() { Text = "Cancel", Left = 175, Top = 120, Width = 75, DialogResult = DialogResult.Cancel };

        //    prompt.Controls.AddRange(new Control[] { chkTotalGoal, txtTotalGoal, chkEPHGoal, txtEPHGoal, btnOK, btnCancel });
        //    prompt.AcceptButton = btnOK;
        //    prompt.CancelButton = btnCancel;

        //    if (prompt.ShowDialog() == DialogResult.OK)
        //    {
        //        // 1. Save the Checkbox states
        //        isTotalGoalActive = chkTotalGoal.Checked;
        //        isEPHGoalActive = chkEPHGoal.Checked;

        //        // 2. Parse Total Goal
        //        if (isTotalGoalActive)
        //        {
        //            decimal.TryParse(txtTotalGoal.Text, out currentGoal);
        //        }

        //        // 3. Parse EPH Goal
        //        if (isEPHGoalActive)
        //        {
        //            decimal.TryParse(txtEPHGoal.Text, out currentEPHGoal);
        //        }

        //        // Apply changes
        //        UpdateGoalPositions();
        //        UpdateTotalValue();
        //        UpdateStopwatchDisplay();
        //        SaveMasterData();
        //    }
        //}

        private void HandleEntryChange(ElementRow row, int changeAmount)
        {
            // Read the current entries safely
            int currentEntries = 0;
            int.TryParse(row.EntriesBox.Text, out currentEntries);

            // Calculate new entries
            int newEntries = currentEntries + changeAmount;

            // Ensure entries don't drop below 0
            if (newEntries < 0) return;

            // Update the text box on screen
            row.EntriesBox.Text = newEntries.ToString();

            // Recalculate the grand total and captura the result in a new variable
            decimal currentGrandTotal = UpdateTotalValue();

            // Log the action to our "database" list
            decimal elementValue = 0;
            decimal.TryParse(row.ValueBox.Text, out elementValue);

            string logEntry = $"{row.NameBox.Text}, {(changeAmount > 0 ? "+" : "-")}, Current Value: {currentGrandTotal}, Time: {DateTime.Now.ToString("hh:mm:ss tt")}";
            SaveLogEntry(logEntry);
            // Add the data as a new row to the visual table on the screen
            dgvLogs.Rows.Add(row.NameBox.Text, (changeAmount > 0 ? "+" : "-"), currentGrandTotal, DateTime.Now.ToString("hh:mm:ss tt"));

            // Recalculate the grand total
            UpdateTotalValue();
            SaveMasterData();
        }

        private decimal UpdateTotalValue()
        {
            decimal grandTotal = 0;
            //hola crayola
            foreach (var row in trackedRows)
            {
                int entries = 0;
                decimal value = 0;

                // TryParse safely tries to read the numbers. If the box is empty, it uses 0.
                int.TryParse(row.EntriesBox.Text, out entries);
                decimal.TryParse(row.ValueBox.Text, out value);

                grandTotal += (entries * value);
            }

            // Update the giant 0 label on the screen
            lblTotalValue.Text = grandTotal.ToString();

            // DYNAMIC POSITIONING: Push the goal label safely to the right of the total value
            // 'Right' is the exact pixel coordinate where lblTotalValue ends. We add 5 pixels for breathing room.
            //lblTotalGoal.Left = lblGoalTitle.Right;
            

            // COLOR LOGIC: Check if a goal is actually set
            if (isTotalGoalActive)
            {
                // Calculate the percentage (e.g., 50 / 100 = 0.50)
                decimal percentage = grandTotal / currentGoal;

                if (percentage < 0.50m)
                    lblTotalValue.ForeColor = Color.Red;
                else if (percentage <= 0.75m)
                    lblTotalValue.ForeColor = Color.Orange;
                else if (percentage <= 0.90m)
                    lblTotalValue.ForeColor = Color.Gold;
                else if (percentage < 1.00m)
                    lblTotalValue.ForeColor = Color.Blue;
                else
                    lblTotalValue.ForeColor = Color.Green; // 100% or more!
            }
            else
            {
                // If no goal is set, ensure it stays black
                lblTotalValue.ForeColor = Color.Black;
            }

            UpdateStopwatchDisplay();
            pnlProgressBar.Invalidate();
            // Hand the final number back
            return grandTotal;
        }

        // 14. STOPWATCH MATH & DISPLAY
        private void UpdateStopwatchDisplay()
        {
            // 1. Format the total seconds into HH:MM:SS
            TimeSpan time = TimeSpan.FromSeconds(totalStopwatchSeconds);

            // Using "D2" forces it to always show two digits (e.g., 05 instead of 5)
            // We use Math.Floor for hours just in case they track past 24 hours!
            txtHours.Text = Math.Floor(time.TotalHours).ToString("00");
            txtMinutes.Text = time.Minutes.ToString("D2");
            txtSeconds.Text = time.Seconds.ToString("D2");

            // 2. Calculate Entries Per Hour
            decimal currentTotal = 0;
            decimal.TryParse(lblTotalValue.Text, out currentTotal);

            decimal hours = totalStopwatchSeconds / 3600.0m;

            if (hours > 0)
            {
                decimal eph = currentTotal / hours;
                lblEPH.Text = Math.Round(eph, 2).ToString();

                // COLOR LOGIC FOR EPH
                if (isEPHGoalActive)
                {
                    decimal percentage = eph / currentEPHGoal;

                    if (percentage < 0.50m) lblEPH.ForeColor = Color.Red;
                    else if (percentage <= 0.75m) lblEPH.ForeColor = Color.Orange;
                    else if (percentage <= 0.90m) lblEPH.ForeColor = Color.Gold;
                    else if (percentage < 1.00m) lblEPH.ForeColor = Color.Blue;
                    else lblEPH.ForeColor = Color.Green;
                }
                else
                {
                    lblEPH.ForeColor = Color.Black; // Default if no goal set
                }
            }
            else
            {
                lblEPH.Text = "0.00";
                lblEPH.ForeColor = Color.Black;
            }
            pnlSpeedBar.Invalidate();
        }

        // 9. THE AUTO-SAVE SYSTEM
        // 9. THE AUTO-SAVE SYSTEM
        private void SaveMasterData()
        {
            List<string> linesToSave = new List<string>();

            // SAVE SETTINGS DYNAMICALLY
            foreach (var kvp in appSettings)
            {
                linesToSave.Add($"SETTING|{kvp.Key}|{kvp.Value.Value}");
            }

            // SAVE THE CUSTOM TITLE
            linesToSave.Add($"TITLE|{txtAppTitle.Text}");

            // SAVE GOAL (If allowed)
            if (appSettings["optSaveGoal"].Value)
            {
                linesToSave.Add($"GOAL_ACTIVE|{isTotalGoalActive}");
                linesToSave.Add($"GOAL|{currentGoal}");
                linesToSave.Add($"EPHGOAL_ACTIVE|{isEPHGoalActive}");
                linesToSave.Add($"EPHGOAL|{currentEPHGoal}");
            }
                        
            // SAVE ELEMENTS (If allowed)
            if (appSettings["optSaveElements"].Value)
            {
                foreach (ElementRow row in trackedRows)
                {
                    // If they turned off Entries or Values, save "0" instead of reading the textbox
                    string savedEntries = appSettings["optSaveEntries"].Value ? row.EntriesBox.Text : "0";
                    string savedValue = appSettings["optSaveElements"].Value ? row.ValueBox.Text : "0";

                    string rowData = $"ELEMENT|{row.NameBox.Text}|{savedEntries}|{savedValue}";
                    linesToSave.Add(rowData);
                }
            }

            string masterSavePath = Path.Combine(saveDirectory, "master_save.txt");
            File.WriteAllLines(masterSavePath, linesToSave);
        }

        // 11. DAILY AUTO-LOGGER
        private void SaveLogEntry(string logMessage)
        {
            if (!appSettings["optDailyLogging"].Value) return;

            string dateFileName = DateTime.Now.ToString("dd_MMMM_yyyy") + ".txt";
            string fullPath = Path.Combine(saveDirectory, dateFileName);
            File.AppendAllText(fullPath, logMessage + Environment.NewLine);
        }

        // 12. THE SETTINGS MENU (DATA-DRIVEN)
        private void BtnSettings_Click(object sender, EventArgs e)
        {
            Form settingsForm = new Form()
            {
                Width = 350,
                Height = 400,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Preferences",
                StartPosition = FormStartPosition.CenterParent
            };

            int currentY = 20;

            // We need a temporary dictionary just for this menu to remember which checkbox belongs to which setting
            Dictionary<string, CheckBox> generatedBoxes = new Dictionary<string, CheckBox>();

            // 1. DYNAMICALLY DRAW THE CHECKBOXES
            foreach (var kvp in appSettings)
            {
                CheckBox chk = new CheckBox
                {
                    Text = kvp.Value.DisplayText,
                    Checked = kvp.Value.Value,
                    Left = 20,
                    Top = currentY,
                    Width = 300
                };
                settingsForm.Controls.Add(chk);
                generatedBoxes.Add(kvp.Key, chk); // Link the internal name (e.g., "optSaveGoal") to this specific checkbox

                currentY += 30; // Push the next checkbox down
            }

            // 2. DIRECTORY BUTTON
            Button btnChangeDir = new Button { Text = "Change Save Folder...", Left = 20, Top = currentY += 10, Width = 150 };
            btnChangeDir.Click += (s, args) =>
            {
                using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
                {
                    if (folderDialog.ShowDialog() == DialogResult.OK)
                    {
                        saveDirectory = folderDialog.SelectedPath;
                        File.WriteAllText("config.txt", saveDirectory);
                        MessageBox.Show("Save folder updated successfully!");
                    }
                }
            };
            settingsForm.Controls.Add(btnChangeDir);

            // 3. OK / CANCEL BUTTONS
            Button btnOK = new Button { Text = "Save", Left = 160, Top = currentY += 40, Width = 75, DialogResult = DialogResult.OK };
            Button btnCancel = new Button { Text = "Cancel", Left = 245, Top = currentY, Width = 75, DialogResult = DialogResult.Cancel };
            settingsForm.Controls.Add(btnOK);
            settingsForm.Controls.Add(btnCancel);

            settingsForm.AcceptButton = btnOK;
            settingsForm.CancelButton = btnCancel;

            // 4. APPLY SETTINGS ON OK
            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                // Loop through our generated boxes and update the master dictionary values
                foreach (var kvp in generatedBoxes)
                {
                    appSettings[kvp.Key].Value = kvp.Value.Checked;
                }

                SaveMasterData();
            }
        }

       
        // 20. RESPONSIVE ROW RESIZING
        private void ResizeResponsiveRows()
        {
            // Use ClientSize.Width to ignore the vertical scrollbar if it appears!
            int currentWidth = pnlElements.ClientSize.Width;

            // Safety check: Don't let the math break if the window is shrunk too far
            if (currentWidth < 400) currentWidth = 400;

            foreach (ElementRow row in trackedRows)
            {
                // 1. Left-aligned elements (Fixed width)
                row.ClearButton.Left = 10;
                row.MinusButton.Left = row.ClearButton.Right + 5;
                row.NameBox.Left = row.MinusButton.Right + 10;

                // 2. Right-aligned elements (Calculated backward from the right edge!)
                row.DeleteButton.Left = currentWidth - row.DeleteButton.Width - 10;
                row.ValueBox.Left = row.DeleteButton.Left - row.ValueBox.Width - 10;
                row.EntriesBox.Left = row.ValueBox.Left - row.EntriesBox.Width - 5;
                row.PlusButton.Left = row.EntriesBox.Left - row.PlusButton.Width - 10;

                // 3. The Stretchy Middle (Name Box)
                // Calculate remaining space between the Minus button and the Plus button
                row.NameBox.Width = row.PlusButton.Left - row.NameBox.Left - 10;

                // Ensure text is centered in the Entries box as requested earlier
                List<TextBox> txtBox = new List<TextBox> {row.NameBox, row.ValueBox, row.EntriesBox};
                // ... keep the rest of your loading loop exactly the same

                foreach (TextBox lmnt in txtBox)
                {
                    lmnt.TextAlign = HorizontalAlignment.Center;
                }
            }

            // Force the headers to align to these new coordinates
            AlignHeaders();
        }


        // --- COLOR HELPER ---
        private Color GetGoalColor(decimal percentage)
        {
            if (percentage < 0.50m) return ColorTranslator.FromHtml("#E07A5F"); // Red
            if (percentage <= 0.75m) return ColorTranslator.FromHtml("#F4A261"); // Orange
            if (percentage <= 0.90m) return ColorTranslator.FromHtml("#E9C46A"); // Gold
            if (percentage < 1.00m) return ColorTranslator.FromHtml("#3A7CA5"); // Blue
            return ColorTranslator.FromHtml("#2A9D8F"); // Green
        }

        // --- TOTAL PROGRESS BAR ---
        private void PnlProgressBar_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(ColorTranslator.FromHtml("#D8E2EA")); // Empty gray background

            if (chkGoal.Checked && decimal.TryParse(txtGoal.Text, out decimal goal) && goal > 0)
            {
                decimal.TryParse(lblTotalValue.Text, out decimal current);
                decimal percentage = current / goal;
                if (percentage > 1m) percentage = 1m; // Cap at 100% visually

                int fillWidth = (int)(pnlProgressBar.Width * percentage);

                using (SolidBrush brush = new SolidBrush(GetGoalColor(percentage)))
                {
                    g.FillRectangle(brush, 0, 0, fillWidth, pnlProgressBar.Height);
                }
            }
        }

        // --- SPEED BAR (PER HOUR) ---
        private void PnlSpeedBar_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(ColorTranslator.FromHtml("#D8E2EA"));

            if (chkEPHGoal.Checked && decimal.TryParse(txtEPHGoal.Text, out decimal goal) && goal > 0)
            {
                decimal maxScale = 1.15m;
                int w = pnlSpeedBar.Width;
                int h = pnlSpeedBar.Height;

                // 1. Draw the colored zones
                using (SolidBrush bRed = new SolidBrush(GetGoalColor(0.49m)))
                using (SolidBrush bOrg = new SolidBrush(GetGoalColor(0.74m)))
                using (SolidBrush bGld = new SolidBrush(GetGoalColor(0.89m)))
                using (SolidBrush bBlu = new SolidBrush(GetGoalColor(0.99m)))
                using (SolidBrush bGrn = new SolidBrush(GetGoalColor(1.01m)))
                {
                    g.FillRectangle(bRed, 0, 0, (int)(w * (0.50m / maxScale)), h);
                    g.FillRectangle(bOrg, (int)(w * (0.50m / maxScale)), 0, (int)(w * (0.25m / maxScale)), h);
                    g.FillRectangle(bGld, (int)(w * (0.75m / maxScale)), 0, (int)(w * (0.15m / maxScale)), h);
                    g.FillRectangle(bBlu, (int)(w * (0.90m / maxScale)), 0, (int)(w * (0.10m / maxScale)), h);
                    g.FillRectangle(bGrn, (int)(w * (1.00m / maxScale)), 0, (int)(w * (0.15m / maxScale)), h);
                }

                // 2. Calculate Arrow Position
                decimal.TryParse(lblEPH.Text, out decimal currentEph);
                decimal position = currentEph / (goal * maxScale);
                if (position > 1m) position = 1m; // Cap arrow at the far right edge

                int arrowX = (int)(w * position);

                // 3. Draw the Arrow (A black triangle pointing down)
                Point[] arrow = {
            new Point(arrowX - 6, 0),
            new Point(arrowX + 6, 0),
            new Point(arrowX, 10)
        };
                g.FillPolygon(Brushes.Black, arrow);

                // Draw a marker line spanning the rest of the height
                g.DrawLine(new Pen(Color.Black, 2), arrowX, 10, arrowX, h);
            }
        }
        public class ElementRow
        {
            public Button ClearButton { get; set; }
            public TextBox NameBox { get; set; }
            public TextBox EntriesBox { get; set; }
            public TextBox ValueBox { get; set; }

            public Button MinusButton { get; set; }
            public Button PlusButton { get; set; }
            public Button DeleteButton { get; set; }

            public List<Control> GetAllControls()
            {
                return new List<Control>
            {
                ClearButton, MinusButton, NameBox, PlusButton, EntriesBox, ValueBox, DeleteButton
            };
            }
        }

        // 13. SETTING BLUEPRINT
        public class AppSetting
        {
            public string DisplayText { get; set; }
            public bool Value { get; set; }
        }

        private void dgvLogs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtMinutes_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlRowHeaders_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
