namespace test1
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnAddElement = new System.Windows.Forms.Button();
            this.lblTotalValue = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvLogs = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSetGoal = new System.Windows.Forms.Button();
            this.lblGoal = new System.Windows.Forms.Label();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.btnNewSession = new System.Windows.Forms.Button();
            this.pnlElements = new System.Windows.Forms.Panel();
            this.btnSettings = new System.Windows.Forms.Button();
            this.txtAppTitle = new System.Windows.Forms.TextBox();
            this.lblGoalDash = new System.Windows.Forms.Label();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.tmrStopwatch = new System.Windows.Forms.Timer(this.components);
            this.pnlStopwatch = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSeconds = new System.Windows.Forms.TextBox();
            this.txtMinutes = new System.Windows.Forms.TextBox();
            this.txtHours = new System.Windows.Forms.TextBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnStartPause = new System.Windows.Forms.Button();
            this.chkEnableStopwatch = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblEPH = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).BeginInit();
            this.pnlStopwatch.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddElement
            // 
            this.btnAddElement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddElement.Location = new System.Drawing.Point(710, 82);
            this.btnAddElement.Name = "btnAddElement";
            this.btnAddElement.Size = new System.Drawing.Size(75, 23);
            this.btnAddElement.TabIndex = 2;
            this.btnAddElement.Text = "Add element";
            this.btnAddElement.UseVisualStyleBackColor = true;
            // 
            // lblTotalValue
            // 
            this.lblTotalValue.AutoSize = true;
            this.lblTotalValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F);
            this.lblTotalValue.Location = new System.Drawing.Point(-5, 42);
            this.lblTotalValue.Name = "lblTotalValue";
            this.lblTotalValue.Size = new System.Drawing.Size(57, 63);
            this.lblTotalValue.TabIndex = 7;
            this.lblTotalValue.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(772, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Value";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(562, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(706, 134);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Entries";
            // 
            // dgvLogs
            // 
            this.dgvLogs.AllowUserToDeleteRows = false;
            this.dgvLogs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLogs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dgvLogs.Location = new System.Drawing.Point(6, 134);
            this.dgvLogs.Name = "dgvLogs";
            this.dgvLogs.Size = new System.Drawing.Size(389, 306);
            this.dgvLogs.TabIndex = 15;
            this.dgvLogs.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLogs_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "Element";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
            this.Column2.HeaderText = "Action";
            this.Column2.MinimumWidth = 40;
            this.Column2.Name = "Column2";
            this.Column2.Width = 40;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.Column3.HeaderText = "Value";
            this.Column3.MinimumWidth = 40;
            this.Column3.Name = "Column3";
            this.Column3.Width = 40;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.Column4.HeaderText = "Time";
            this.Column4.MinimumWidth = 40;
            this.Column4.Name = "Column4";
            this.Column4.Width = 40;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(822, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Delete";
            // 
            // btnSetGoal
            // 
            this.btnSetGoal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetGoal.Location = new System.Drawing.Point(791, 82);
            this.btnSetGoal.Name = "btnSetGoal";
            this.btnSetGoal.Size = new System.Drawing.Size(75, 23);
            this.btnSetGoal.TabIndex = 17;
            this.btnSetGoal.Text = "Set Goal";
            this.btnSetGoal.UseVisualStyleBackColor = true;
            // 
            // lblGoal
            // 
            this.lblGoal.AutoSize = true;
            this.lblGoal.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.lblGoal.Location = new System.Drawing.Point(58, 98);
            this.lblGoal.Name = "lblGoal";
            this.lblGoal.Size = new System.Drawing.Size(24, 25);
            this.lblGoal.TabIndex = 18;
            this.lblGoal.Text = "0";
            this.lblGoal.Visible = false;
            // 
            // btnClearAll
            // 
            this.btnClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearAll.Location = new System.Drawing.Point(791, 53);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(75, 23);
            this.btnClearAll.TabIndex = 19;
            this.btnClearAll.Text = "Clear All";
            this.btnClearAll.UseVisualStyleBackColor = true;
            // 
            // btnNewSession
            // 
            this.btnNewSession.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewSession.Location = new System.Drawing.Point(791, 24);
            this.btnNewSession.Name = "btnNewSession";
            this.btnNewSession.Size = new System.Drawing.Size(79, 23);
            this.btnNewSession.TabIndex = 20;
            this.btnNewSession.Text = "New Session";
            this.btnNewSession.UseVisualStyleBackColor = true;
            // 
            // pnlElements
            // 
            this.pnlElements.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlElements.AutoScroll = true;
            this.pnlElements.Location = new System.Drawing.Point(405, 157);
            this.pnlElements.Name = "pnlElements";
            this.pnlElements.Size = new System.Drawing.Size(461, 283);
            this.pnlElements.TabIndex = 21;
            // 
            // btnSettings
            // 
            this.btnSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSettings.Location = new System.Drawing.Point(710, 24);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(79, 23);
            this.btnSettings.TabIndex = 22;
            this.btnSettings.Text = "⚙ Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            // 
            // txtAppTitle
            // 
            this.txtAppTitle.BackColor = System.Drawing.SystemColors.Control;
            this.txtAppTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAppTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAppTitle.Location = new System.Drawing.Point(5, 1);
            this.txtAppTitle.Name = "txtAppTitle";
            this.txtAppTitle.Size = new System.Drawing.Size(261, 46);
            this.txtAppTitle.TabIndex = 23;
            this.txtAppTitle.Text = "Current value";
            // 
            // lblGoalDash
            // 
            this.lblGoalDash.AutoSize = true;
            this.lblGoalDash.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGoalDash.Location = new System.Drawing.Point(12, 105);
            this.lblGoalDash.Name = "lblGoalDash";
            this.lblGoalDash.Size = new System.Drawing.Size(47, 17);
            this.lblGoalDash.TabIndex = 24;
            this.lblGoalDash.Text = "GOAL";
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenFolder.Location = new System.Drawing.Point(710, 57);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(80, 19);
            this.btnOpenFolder.TabIndex = 25;
            this.btnOpenFolder.Text = "Open Folder";
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            // 
            // tmrStopwatch
            // 
            this.tmrStopwatch.Interval = 1000;
            // 
            // pnlStopwatch
            // 
            this.pnlStopwatch.Controls.Add(this.label8);
            this.pnlStopwatch.Controls.Add(this.txtSeconds);
            this.pnlStopwatch.Controls.Add(this.txtMinutes);
            this.pnlStopwatch.Controls.Add(this.txtHours);
            this.pnlStopwatch.Controls.Add(this.btnReset);
            this.pnlStopwatch.Controls.Add(this.btnStartPause);
            this.pnlStopwatch.Controls.Add(this.chkEnableStopwatch);
            this.pnlStopwatch.Controls.Add(this.label1);
            this.pnlStopwatch.Controls.Add(this.label2);
            this.pnlStopwatch.Controls.Add(this.lblEPH);
            this.pnlStopwatch.Location = new System.Drawing.Point(310, 14);
            this.pnlStopwatch.Name = "pnlStopwatch";
            this.pnlStopwatch.Size = new System.Drawing.Size(321, 108);
            this.pnlStopwatch.TabIndex = 26;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 60);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Somethings per hour";
            // 
            // txtSeconds
            // 
            this.txtSeconds.Enabled = false;
            this.txtSeconds.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSeconds.Location = new System.Drawing.Point(264, 10);
            this.txtSeconds.Name = "txtSeconds";
            this.txtSeconds.Size = new System.Drawing.Size(50, 53);
            this.txtSeconds.TabIndex = 5;
            this.txtSeconds.Text = "00";
            // 
            // txtMinutes
            // 
            this.txtMinutes.Enabled = false;
            this.txtMinutes.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMinutes.Location = new System.Drawing.Point(201, 10);
            this.txtMinutes.Name = "txtMinutes";
            this.txtMinutes.Size = new System.Drawing.Size(50, 53);
            this.txtMinutes.TabIndex = 4;
            this.txtMinutes.Text = "00";
            this.txtMinutes.TextChanged += new System.EventHandler(this.txtMinutes_TextChanged);
            // 
            // txtHours
            // 
            this.txtHours.Enabled = false;
            this.txtHours.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHours.Location = new System.Drawing.Point(138, 10);
            this.txtHours.Name = "txtHours";
            this.txtHours.Size = new System.Drawing.Size(50, 53);
            this.txtHours.TabIndex = 3;
            this.txtHours.Text = "00";
            // 
            // btnReset
            // 
            this.btnReset.Enabled = false;
            this.btnReset.Location = new System.Drawing.Point(229, 78);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(85, 22);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            // 
            // btnStartPause
            // 
            this.btnStartPause.Enabled = false;
            this.btnStartPause.Location = new System.Drawing.Point(138, 78);
            this.btnStartPause.Name = "btnStartPause";
            this.btnStartPause.Size = new System.Drawing.Size(85, 22);
            this.btnStartPause.TabIndex = 1;
            this.btnStartPause.Text = "Start";
            this.btnStartPause.UseVisualStyleBackColor = true;
            // 
            // chkEnableStopwatch
            // 
            this.chkEnableStopwatch.AutoSize = true;
            this.chkEnableStopwatch.Location = new System.Drawing.Point(15, 84);
            this.chkEnableStopwatch.Name = "chkEnableStopwatch";
            this.chkEnableStopwatch.Size = new System.Drawing.Size(98, 17);
            this.chkEnableStopwatch.TabIndex = 0;
            this.chkEnableStopwatch.Text = "Track with time";
            this.chkEnableStopwatch.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 35F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(179, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 54);
            this.label1.TabIndex = 6;
            this.label1.Text = ":";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 35F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(242, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 54);
            this.label2.TabIndex = 7;
            this.label2.Text = ":";
            // 
            // lblEPH
            // 
            this.lblEPH.AutoSize = true;
            this.lblEPH.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEPH.Location = new System.Drawing.Point(7, 10);
            this.lblEPH.Name = "lblEPH";
            this.lblEPH.Size = new System.Drawing.Size(97, 46);
            this.lblEPH.TabIndex = 8;
            this.lblEPH.Text = "0.00";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 452);
            this.Controls.Add(this.pnlStopwatch);
            this.Controls.Add(this.lblGoal);
            this.Controls.Add(this.lblTotalValue);
            this.Controls.Add(this.btnOpenFolder);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.pnlElements);
            this.Controls.Add(this.btnNewSession);
            this.Controls.Add(this.btnClearAll);
            this.Controls.Add(this.btnSetGoal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgvLogs);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnAddElement);
            this.Controls.Add(this.lblGoalDash);
            this.Controls.Add(this.txtAppTitle);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).EndInit();
            this.pnlStopwatch.ResumeLayout(false);
            this.pnlStopwatch.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnAddElement;
        private System.Windows.Forms.Label lblTotalValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgvLogs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSetGoal;
        private System.Windows.Forms.Label lblGoal;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.Button btnNewSession;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.Panel pnlElements;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.TextBox txtAppTitle;
        private System.Windows.Forms.Label lblGoalDash;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.Timer tmrStopwatch;
        private System.Windows.Forms.Panel pnlStopwatch;
        private System.Windows.Forms.CheckBox chkEnableStopwatch;
        private System.Windows.Forms.TextBox txtSeconds;
        private System.Windows.Forms.TextBox txtMinutes;
        private System.Windows.Forms.TextBox txtHours;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnStartPause;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblEPH;
        private System.Windows.Forms.Label label2;
    }
}

