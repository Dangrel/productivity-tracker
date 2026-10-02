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
        // Master Settings
        bool skipDeleteWarning = false;
        bool optSaveGoal = true;
        bool optSaveElements = true;
        bool optSaveValues = true;
        bool optSaveEntries = true;
        bool optDailyLogging = true;
        bool optDarkMode = false;
        // Remembers the path to the user's chosen save folder
        string saveDirectory = "";
        List<ElementRow> trackedRows = new List<ElementRow>();
        // Stopwatch variables
        int totalStopwatchSeconds = 0;
        bool skipStopwatchWarning = false;


        public Form1()
        {
            InitializeComponent();

            // The (s, args) catches the required button click data, and the => runs your custom method
            btnAddElement.Click += (s, args) => CreateRow("", "0", "0");
            btnSetGoal.Click += BtnSetGoal_Click;
            btnSettings.Click += BtnSettings_Click;
            // --- FOCUS MANAGEMENT ---
            // If the user clicks the blank background of the main window, drop the textbox focus
            this.Click += (s, args) => this.ActiveControl = null;

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
                    if (parts[0] == "GOAL" && parts.Length == 2)
                    {
                        if (decimal.TryParse(parts[1], out decimal savedGoal) && savedGoal > 0)
                        {
                            currentGoal = savedGoal;
                            lblGoal.Text = currentGoal.ToString();
                            lblGoal.Visible = true;
                        }
                    }
                    // Route 2: The line is an Element (Needs exactly 4 parts: ELEMENT, Name, Entries, Value)
                    else if (parts[0] == "ELEMENT" && parts.Length == 4)
                    {
                        // parts[1] is the Name, parts[2] is Entries, parts[3] is Value
                        CreateRow(parts[1], parts[2], parts[3]);
                    }
                    // Route 3: The line is a Setting
                    // Route 3: The line is a Setting
                    else if (parts[0] == "SETTING" && parts.Length == 3)
                    {
                        bool savedSettingValue = false;
                        bool.TryParse(parts[2], out savedSettingValue);

                        switch (parts[1])
                        {
                            case "optSaveGoal": optSaveGoal = savedSettingValue; break;
                            case "optSaveElements": optSaveElements = savedSettingValue; break;
                            case "optSaveValues": optSaveValues = savedSettingValue; break;
                            case "optSaveEntries": optSaveEntries = savedSettingValue; break;
                            case "optDailyLogging": optDailyLogging = savedSettingValue; break;
                            case "SkipDeleteWarning": skipDeleteWarning = savedSettingValue; break;
                            case "optDarkMode": optDarkMode = savedSettingValue; break;
                        }
                    }
                    // Route 4: The Custom Title
                    else if (parts[0] == "TITLE" && parts.Length == 2)
                    {
                        txtAppTitle.Text = parts[1];
                    }
                }

                // Update the math and colors once everything is loaded
                UpdateTotalValue();
                SaveMasterData();
            }
                        
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
                lblGoal.Visible = false;

                // 4. Force a label update
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
            chkEnableStopwatch.CheckedChanged += (s, args) =>
            {
                bool isEnabled = chkEnableStopwatch.Checked;
                btnStartPause.Enabled = isEnabled;
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

        // 8. REUSABLE ROW CREATOR
        private void CreateRow(string startingName, string startingEntries, string startingValue)
        {
            // Notice the X coordinates (the first number in the Point) are shifted right!
            Button btnClear = new Button { Text = "C", Location = new Point(20, verticalPosition), Width = 30 };
            Button btnMinus = new Button { Text = "-", Location = new Point(60, verticalPosition), Width = 30 };
            TextBox txtName = new TextBox { Text = startingName, Location = new Point(100, verticalPosition), Width = 150 };
            Button btnPlus = new Button { Text = "+", Location = new Point(260, verticalPosition), Width = 30 };
            TextBox txtEntries = new TextBox { Text = startingEntries, Location = new Point(300, verticalPosition), Width = 50};
            TextBox txtValue = new TextBox { Text = startingValue, Location = new Point(360, verticalPosition), Width = 50 };
            Button btnDelete = new Button { Text = "X", ForeColor = Color.Red, Location = new Point(420, verticalPosition), Width = 30 };

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
            SaveMasterData();
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
            if (skipDeleteWarning) return true;

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
                skipDeleteWarning = true;
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
        private void BtnSetGoal_Click(object sender, EventArgs e)
        {
            // Create a brand new mini-window (Form) on the fly
            Form prompt = new Form()
            {
                Width = 250,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Goal setup",
                StartPosition = FormStartPosition.CenterParent
            };

            // Create the text, input box, and OK button for the pop-up
            Label textLabel = new Label() { Left = 20, Top = 20, Text = "Set goal:" };
            TextBox inputBox = new TextBox() { Left = 20, Top = 50, Width = 190 };
            Button confirmation = new Button() { Text = "OK", Left = 135, Width = 75, Top = 80, DialogResult = DialogResult.OK };

            // Add them to the mini-window
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(inputBox);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation; // Allows the user to just press Enter to click OK

            // Show the window and wait for the user to click OK
            if (prompt.ShowDialog() == DialogResult.OK)
            {
                decimal.TryParse(inputBox.Text, out decimal parsedGoal);
                // TryParse checks if they actually typed a number, not letters
                if (parsedGoal > 0)
                {
                    currentGoal = parsedGoal;
                    lblGoal.Text = currentGoal.ToString();
                    lblGoal.Visible = true; // Reveal the label!
                    lblGoalDash.Visible = true;

                    UpdateTotalValue(); // Force an update so the colors and position adjust instantly
                }
                else if (parsedGoal == 0)
                {
                    currentGoal = parsedGoal;
                    lblGoal.Visible = false;
                    lblGoalDash.Visible = false;
                    UpdateTotalValue();
                }
                else
                {
                    MessageBox.Show("Please enter a valid number.");
                }
            }
        }

        // 12. THE SETTINGS MENU
        private void BtnSettings_Click(object sender, EventArgs e)
        {
            Form settingsForm = new Form()
            {
                Width = 350,
                Height = 380,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Preferences",
                StartPosition = FormStartPosition.CenterParent
            };

            // --- CHECKBOXES ---
            int currentY = 20;
            CheckBox chkGoal = new CheckBox { Text = "Save Goal between sessions", Checked = optSaveGoal, Left = 20, Top = currentY, Width = 300 };
            CheckBox chkElements = new CheckBox { Text = "Save Elements list", Checked = optSaveElements, Left = 20, Top = currentY += 30, Width = 300 };
            CheckBox chkValues = new CheckBox { Text = "Save Element Values", Checked = optSaveValues, Left = 20, Top = currentY += 30, Width = 300 };
            CheckBox chkEntries = new CheckBox { Text = "Save Element Entries", Checked = optSaveEntries, Left = 20, Top = currentY += 30, Width = 300 };
            CheckBox chkLogging = new CheckBox { Text = "Enable Daily Text File Logging", Checked = optDailyLogging, Left = 20, Top = currentY += 30, Width = 300 };
            CheckBox chkWarning = new CheckBox { Text = "Skip Deletion Warnings", Checked = skipDeleteWarning, Left = 20, Top = currentY += 30, Width = 300 };
            CheckBox chkTheme = new CheckBox { Text = "Enable Dark Mode", Checked = optDarkMode, Left = 20, Top = currentY += 30, Width = 300 };

            // --- DIRECTORY BUTTON ---
            Button btnChangeDir = new Button { Text = "Change Save Folder...", Left = 20, Top = currentY += 40, Width = 150 };
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

            // --- OK / CANCEL BUTTONS ---
            Button btnOK = new Button { Text = "Save", Left = 160, Top = currentY += 50, Width = 75, DialogResult = DialogResult.OK };
            Button btnCancel = new Button { Text = "Cancel", Left = 245, Top = currentY, Width = 75, DialogResult = DialogResult.Cancel };

            // Add everything to the window
            settingsForm.Controls.AddRange(new Control[] { chkGoal, chkElements, chkValues, chkEntries, chkLogging, chkWarning, chkTheme, btnChangeDir, btnOK, btnCancel });
            settingsForm.AcceptButton = btnOK;
            settingsForm.CancelButton = btnCancel;

            // Apply the settings if they clicked OK
            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                optSaveGoal = chkGoal.Checked;
                optSaveElements = chkElements.Checked;
                optSaveValues = chkValues.Checked;
                optSaveEntries = chkEntries.Checked;
                optDailyLogging = chkLogging.Checked;
                skipDeleteWarning = chkWarning.Checked;
                optDarkMode = chkTheme.Checked;

                SaveMasterData(); // Save the new preferences immediately

                // TODO: Apply Dark Mode visually here later!
            }
        }

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
            lblGoal.Left = lblGoalDash.Right;
            

            // COLOR LOGIC: Check if a goal is actually set
            if (currentGoal > 0)
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
                lblEPH.Text = $"{Math.Round(eph, 2)}";
            }
            else
            {
                lblEPH.Text = "0.00";
            }
        }

        // 9. THE AUTO-SAVE SYSTEM
        // 9. THE AUTO-SAVE SYSTEM
        private void SaveMasterData()
        {
            List<string> linesToSave = new List<string>();

            // SAVE SETTINGS
            linesToSave.Add($"SETTING|optSaveGoal|{optSaveGoal}");
            linesToSave.Add($"SETTING|optSaveElements|{optSaveElements}");
            linesToSave.Add($"SETTING|optSaveValues|{optSaveValues}");
            linesToSave.Add($"SETTING|optSaveEntries|{optSaveEntries}");
            linesToSave.Add($"SETTING|optDailyLogging|{optDailyLogging}");
            linesToSave.Add($"SETTING|SkipDeleteWarning|{skipDeleteWarning}");
            linesToSave.Add($"SETTING|optDarkMode|{optDarkMode}");
            linesToSave.Add($"TITLE|{txtAppTitle.Text}");

            // SAVE GOAL (If allowed)
            if (optSaveGoal)
            {
                linesToSave.Add($"GOAL|{currentGoal}");
            }

            // SAVE ELEMENTS (If allowed)
            if (optSaveElements)
            {
                foreach (ElementRow row in trackedRows)
                {
                    // If they turned off Entries or Values, save "0" instead of reading the textbox
                    string savedEntries = optSaveEntries ? row.EntriesBox.Text : "0";
                    string savedValue = optSaveValues ? row.ValueBox.Text : "0";

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
            if (!optDailyLogging) return; // Instantly kill the method if logging is disabled

            string dateFileName = DateTime.Now.ToString("dd_MMMM_yyyy") + ".txt";
            string fullPath = Path.Combine(saveDirectory, dateFileName);
            File.AppendAllText(fullPath, logMessage + Environment.NewLine);
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

        private void dgvLogs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtMinutes_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
