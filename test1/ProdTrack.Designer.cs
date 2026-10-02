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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnAddElement = new System.Windows.Forms.Button();
            this.lblTotalValue = new System.Windows.Forms.Label();
            this.lblValueHeader = new System.Windows.Forms.Label();
            this.lblNameHeader = new System.Windows.Forms.Label();
            this.lblEntriesHeader = new System.Windows.Forms.Label();
            this.dgvLogs = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblDeleteHeader = new System.Windows.Forms.Label();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.btnNewSession = new System.Windows.Forms.Button();
            this.pnlElements = new System.Windows.Forms.Panel();
            this.btnSettings = new System.Windows.Forms.Button();
            this.txtAppTitle = new System.Windows.Forms.TextBox();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.tmrStopwatch = new System.Windows.Forms.Timer(this.components);
            this.pnlStopwatch = new System.Windows.Forms.Panel();
            this.lblEPHText = new System.Windows.Forms.Label();
            this.txtSeconds = new System.Windows.Forms.TextBox();
            this.txtMinutes = new System.Windows.Forms.TextBox();
            this.txtHours = new System.Windows.Forms.TextBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnStartPause = new System.Windows.Forms.Button();
            this.chkEnableStopwatch = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblEPH = new System.Windows.Forms.Label();
            this.lblClearHeader = new System.Windows.Forms.Label();
            this.pnlRowHeaders = new System.Windows.Forms.Panel();
            this.pnlStats = new System.Windows.Forms.Panel();
            this.txtGoal = new System.Windows.Forms.TextBox();
            this.chkGoal = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnlProgressBar = new System.Windows.Forms.Panel();
            this.lbl0 = new System.Windows.Forms.Label();
            this.lbl100 = new System.Windows.Forms.Label();
            this.chkEPHGoal = new System.Windows.Forms.CheckBox();
            this.txtEPHGoal = new System.Windows.Forms.TextBox();
            this.pnlSpeedBar = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).BeginInit();
            this.pnlStopwatch.SuspendLayout();
            this.pnlRowHeaders.SuspendLayout();
            this.pnlStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddElement
            // 
            this.btnAddElement.FlatAppearance.BorderSize = 0;
            this.btnAddElement.Location = new System.Drawing.Point(447, 10);
            this.btnAddElement.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAddElement.Name = "btnAddElement";
            this.btnAddElement.Size = new System.Drawing.Size(120, 37);
            this.btnAddElement.TabIndex = 2;
            this.btnAddElement.Text = "Add element";
            this.btnAddElement.UseVisualStyleBackColor = true;
            // 
            // lblTotalValue
            // 
            this.lblTotalValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F);
            this.lblTotalValue.Location = new System.Drawing.Point(0, 0);
            this.lblTotalValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalValue.Name = "lblTotalValue";
            this.lblTotalValue.Size = new System.Drawing.Size(367, 69);
            this.lblTotalValue.TabIndex = 7;
            this.lblTotalValue.Text = "0";
            this.lblTotalValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblValueHeader
            // 
            this.lblValueHeader.AutoSize = true;
            this.lblValueHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValueHeader.ForeColor = System.Drawing.Color.White;
            this.lblValueHeader.Location = new System.Drawing.Point(473, 7);
            this.lblValueHeader.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblValueHeader.Name = "lblValueHeader";
            this.lblValueHeader.Size = new System.Drawing.Size(43, 19);
            this.lblValueHeader.TabIndex = 8;
            this.lblValueHeader.Text = "Value";
            // 
            // lblNameHeader
            // 
            this.lblNameHeader.AutoSize = true;
            this.lblNameHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNameHeader.ForeColor = System.Drawing.Color.White;
            this.lblNameHeader.Location = new System.Drawing.Point(221, 7);
            this.lblNameHeader.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNameHeader.Name = "lblNameHeader";
            this.lblNameHeader.Size = new System.Drawing.Size(49, 17);
            this.lblNameHeader.TabIndex = 9;
            this.lblNameHeader.Text = "Name";
            // 
            // lblEntriesHeader
            // 
            this.lblEntriesHeader.AutoSize = true;
            this.lblEntriesHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntriesHeader.ForeColor = System.Drawing.Color.White;
            this.lblEntriesHeader.Location = new System.Drawing.Point(391, 7);
            this.lblEntriesHeader.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEntriesHeader.Name = "lblEntriesHeader";
            this.lblEntriesHeader.Size = new System.Drawing.Size(59, 17);
            this.lblEntriesHeader.TabIndex = 13;
            this.lblEntriesHeader.Text = "Entries";
            this.lblEntriesHeader.Click += new System.EventHandler(this.label6_Click);
            // 
            // dgvLogs
            // 
            this.dgvLogs.AllowUserToDeleteRows = false;
            this.dgvLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLogs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dgvLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLogs.Location = new System.Drawing.Point(0, 0);
            this.dgvLogs.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvLogs.Name = "dgvLogs";
            this.dgvLogs.ReadOnly = true;
            this.dgvLogs.Size = new System.Drawing.Size(451, 220);
            this.dgvLogs.TabIndex = 15;
            this.dgvLogs.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLogs_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column1.HeaderText = "Element";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column2.HeaderText = "Action";
            this.Column2.MinimumWidth = 40;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 72;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column3.HeaderText = "Value";
            this.Column3.MinimumWidth = 40;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 69;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column4.HeaderText = "Time";
            this.Column4.MinimumWidth = 40;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 64;
            // 
            // lblDeleteHeader
            // 
            this.lblDeleteHeader.AutoSize = true;
            this.lblDeleteHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeleteHeader.ForeColor = System.Drawing.Color.White;
            this.lblDeleteHeader.Location = new System.Drawing.Point(536, 7);
            this.lblDeleteHeader.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDeleteHeader.Name = "lblDeleteHeader";
            this.lblDeleteHeader.Size = new System.Drawing.Size(55, 17);
            this.lblDeleteHeader.TabIndex = 16;
            this.lblDeleteHeader.Text = "Delete";
            // 
            // btnClearAll
            // 
            this.btnClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearAll.FlatAppearance.BorderSize = 0;
            this.btnClearAll.Location = new System.Drawing.Point(586, 10);
            this.btnClearAll.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(120, 37);
            this.btnClearAll.TabIndex = 19;
            this.btnClearAll.Text = "Clear All";
            this.btnClearAll.UseVisualStyleBackColor = true;
            // 
            // btnNewSession
            // 
            this.btnNewSession.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewSession.FlatAppearance.BorderSize = 0;
            this.btnNewSession.Location = new System.Drawing.Point(714, 10);
            this.btnNewSession.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNewSession.Name = "btnNewSession";
            this.btnNewSession.Size = new System.Drawing.Size(120, 37);
            this.btnNewSession.TabIndex = 20;
            this.btnNewSession.Text = "New Session";
            this.btnNewSession.UseVisualStyleBackColor = true;
            // 
            // pnlElements
            // 
            this.pnlElements.AutoScroll = true;
            this.pnlElements.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlElements.Location = new System.Drawing.Point(0, 32);
            this.pnlElements.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlElements.Name = "pnlElements";
            this.pnlElements.Size = new System.Drawing.Size(451, 192);
            this.pnlElements.TabIndex = 21;
            // 
            // btnSettings
            // 
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.Location = new System.Drawing.Point(8, 10);
            this.btnSettings.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(120, 37);
            this.btnSettings.TabIndex = 22;
            this.btnSettings.Text = "⚙ Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            // 
            // txtAppTitle
            // 
            this.txtAppTitle.BackColor = System.Drawing.Color.White;
            this.txtAppTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAppTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAppTitle.Location = new System.Drawing.Point(0, 73);
            this.txtAppTitle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtAppTitle.Name = "txtAppTitle";
            this.txtAppTitle.Size = new System.Drawing.Size(367, 53);
            this.txtAppTitle.TabIndex = 23;
            this.txtAppTitle.Text = "Current value";
            this.txtAppTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.FlatAppearance.BorderSize = 0;
            this.btnOpenFolder.Location = new System.Drawing.Point(319, 10);
            this.btnOpenFolder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(120, 37);
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
            this.pnlStopwatch.Controls.Add(this.lblEPHText);
            this.pnlStopwatch.Controls.Add(this.txtSeconds);
            this.pnlStopwatch.Controls.Add(this.pnlSpeedBar);
            this.pnlStopwatch.Controls.Add(this.txtMinutes);
            this.pnlStopwatch.Controls.Add(this.txtEPHGoal);
            this.pnlStopwatch.Controls.Add(this.txtHours);
            this.pnlStopwatch.Controls.Add(this.chkEPHGoal);
            this.pnlStopwatch.Controls.Add(this.btnReset);
            this.pnlStopwatch.Controls.Add(this.btnStartPause);
            this.pnlStopwatch.Controls.Add(this.chkEnableStopwatch);
            this.pnlStopwatch.Controls.Add(this.label1);
            this.pnlStopwatch.Controls.Add(this.label2);
            this.pnlStopwatch.Controls.Add(this.lblEPH);
            this.pnlStopwatch.Location = new System.Drawing.Point(8, 287);
            this.pnlStopwatch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlStopwatch.Name = "pnlStopwatch";
            this.pnlStopwatch.Size = new System.Drawing.Size(367, 217);
            this.pnlStopwatch.TabIndex = 26;
            // 
            // lblEPHText
            // 
            this.lblEPHText.Location = new System.Drawing.Point(2, 75);
            this.lblEPHText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEPHText.Name = "lblEPHText";
            this.lblEPHText.Size = new System.Drawing.Size(133, 40);
            this.lblEPHText.TabIndex = 9;
            this.lblEPHText.Text = "Entries per hour";
            // 
            // txtSeconds
            // 
            this.txtSeconds.Enabled = false;
            this.txtSeconds.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSeconds.Location = new System.Drawing.Point(302, 4);
            this.txtSeconds.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSeconds.Name = "txtSeconds";
            this.txtSeconds.Size = new System.Drawing.Size(65, 53);
            this.txtSeconds.TabIndex = 5;
            this.txtSeconds.Text = "00";
            // 
            // txtMinutes
            // 
            this.txtMinutes.Enabled = false;
            this.txtMinutes.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMinutes.Location = new System.Drawing.Point(218, 4);
            this.txtMinutes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMinutes.Name = "txtMinutes";
            this.txtMinutes.Size = new System.Drawing.Size(65, 53);
            this.txtMinutes.TabIndex = 4;
            this.txtMinutes.Text = "00";
            this.txtMinutes.TextChanged += new System.EventHandler(this.txtMinutes_TextChanged);
            // 
            // txtHours
            // 
            this.txtHours.Enabled = false;
            this.txtHours.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHours.Location = new System.Drawing.Point(134, 4);
            this.txtHours.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtHours.Name = "txtHours";
            this.txtHours.Size = new System.Drawing.Size(65, 53);
            this.txtHours.TabIndex = 3;
            this.txtHours.Text = "00";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(255, 78);
            this.btnReset.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(113, 37);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            // 
            // btnStartPause
            // 
            this.btnStartPause.Location = new System.Drawing.Point(134, 78);
            this.btnStartPause.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnStartPause.Name = "btnStartPause";
            this.btnStartPause.Size = new System.Drawing.Size(113, 37);
            this.btnStartPause.TabIndex = 1;
            this.btnStartPause.Text = "Start";
            this.btnStartPause.UseVisualStyleBackColor = true;
            // 
            // chkEnableStopwatch
            // 
            this.chkEnableStopwatch.AutoSize = true;
            this.chkEnableStopwatch.Location = new System.Drawing.Point(5, 4);
            this.chkEnableStopwatch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkEnableStopwatch.Name = "chkEnableStopwatch";
            this.chkEnableStopwatch.Size = new System.Drawing.Size(121, 21);
            this.chkEnableStopwatch.TabIndex = 0;
            this.chkEnableStopwatch.Text = "Track with time";
            this.chkEnableStopwatch.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 35F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(189, -1);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 54);
            this.label1.TabIndex = 6;
            this.label1.Text = ":";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 35F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(273, 1);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 54);
            this.label2.TabIndex = 7;
            this.label2.Text = ":";
            // 
            // lblEPH
            // 
            this.lblEPH.AutoSize = true;
            this.lblEPH.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEPH.Location = new System.Drawing.Point(5, 29);
            this.lblEPH.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEPH.Name = "lblEPH";
            this.lblEPH.Size = new System.Drawing.Size(97, 46);
            this.lblEPH.TabIndex = 8;
            this.lblEPH.Text = "0.00";
            // 
            // lblClearHeader
            // 
            this.lblClearHeader.AutoSize = true;
            this.lblClearHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClearHeader.ForeColor = System.Drawing.Color.White;
            this.lblClearHeader.Location = new System.Drawing.Point(3, 7);
            this.lblClearHeader.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClearHeader.Name = "lblClearHeader";
            this.lblClearHeader.Size = new System.Drawing.Size(46, 17);
            this.lblClearHeader.TabIndex = 29;
            this.lblClearHeader.Text = "Clear";
            // 
            // pnlRowHeaders
            // 
            this.pnlRowHeaders.BackColor = System.Drawing.Color.White;
            this.pnlRowHeaders.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlRowHeaders.Controls.Add(this.lblClearHeader);
            this.pnlRowHeaders.Controls.Add(this.lblNameHeader);
            this.pnlRowHeaders.Controls.Add(this.lblEntriesHeader);
            this.pnlRowHeaders.Controls.Add(this.lblValueHeader);
            this.pnlRowHeaders.Controls.Add(this.lblDeleteHeader);
            this.pnlRowHeaders.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRowHeaders.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlRowHeaders.Location = new System.Drawing.Point(0, 0);
            this.pnlRowHeaders.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlRowHeaders.Name = "pnlRowHeaders";
            this.pnlRowHeaders.Size = new System.Drawing.Size(451, 32);
            this.pnlRowHeaders.TabIndex = 31;
            this.pnlRowHeaders.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlRowHeaders_Paint);
            // 
            // pnlStats
            // 
            this.pnlStats.BackColor = System.Drawing.Color.White;
            this.pnlStats.Controls.Add(this.lbl100);
            this.pnlStats.Controls.Add(this.lbl0);
            this.pnlStats.Controls.Add(this.pnlProgressBar);
            this.pnlStats.Controls.Add(this.txtGoal);
            this.pnlStats.Controls.Add(this.chkGoal);
            this.pnlStats.Controls.Add(this.lblTotalValue);
            this.pnlStats.Controls.Add(this.txtAppTitle);
            this.pnlStats.Location = new System.Drawing.Point(8, 54);
            this.pnlStats.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlStats.Name = "pnlStats";
            this.pnlStats.Size = new System.Drawing.Size(367, 225);
            this.pnlStats.TabIndex = 32;
            // 
            // txtGoal
            // 
            this.txtGoal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGoal.Location = new System.Drawing.Point(70, 132);
            this.txtGoal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtGoal.Name = "txtGoal";
            this.txtGoal.Size = new System.Drawing.Size(50, 23);
            this.txtGoal.TabIndex = 30;
            this.txtGoal.Text = "0";
            // 
            // chkGoal
            // 
            this.chkGoal.AutoSize = true;
            this.chkGoal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkGoal.Location = new System.Drawing.Point(5, 134);
            this.chkGoal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkGoal.Name = "chkGoal";
            this.chkGoal.Size = new System.Drawing.Size(57, 21);
            this.chkGoal.TabIndex = 29;
            this.chkGoal.Text = "Goal";
            this.chkGoal.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(383, 54);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pnlElements);
            this.splitContainer1.Panel1.Controls.Add(this.pnlRowHeaders);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvLogs);
            this.splitContainer1.Size = new System.Drawing.Size(451, 449);
            this.splitContainer1.SplitterDistance = 224;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 33;
            // 
            // pnlProgressBar
            // 
            this.pnlProgressBar.Location = new System.Drawing.Point(11, 178);
            this.pnlProgressBar.Name = "pnlProgressBar";
            this.pnlProgressBar.Size = new System.Drawing.Size(346, 20);
            this.pnlProgressBar.TabIndex = 31;
            // 
            // lbl0
            // 
            this.lbl0.AutoSize = true;
            this.lbl0.Location = new System.Drawing.Point(9, 159);
            this.lbl0.Name = "lbl0";
            this.lbl0.Size = new System.Drawing.Size(28, 17);
            this.lbl0.TabIndex = 32;
            this.lbl0.Text = "0%";
            // 
            // lbl100
            // 
            this.lbl100.AutoSize = true;
            this.lbl100.Location = new System.Drawing.Point(317, 159);
            this.lbl100.Name = "lbl100";
            this.lbl100.Size = new System.Drawing.Size(44, 17);
            this.lbl100.TabIndex = 32;
            this.lbl100.Text = "100%";
            // 
            // chkEPHGoal
            // 
            this.chkEPHGoal.AutoSize = true;
            this.chkEPHGoal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEPHGoal.Location = new System.Drawing.Point(4, 119);
            this.chkEPHGoal.Margin = new System.Windows.Forms.Padding(4);
            this.chkEPHGoal.Name = "chkEPHGoal";
            this.chkEPHGoal.Size = new System.Drawing.Size(57, 21);
            this.chkEPHGoal.TabIndex = 29;
            this.chkEPHGoal.Text = "Goal";
            this.chkEPHGoal.UseVisualStyleBackColor = true;
            // 
            // txtEPHGoal
            // 
            this.txtEPHGoal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEPHGoal.Location = new System.Drawing.Point(69, 117);
            this.txtEPHGoal.Margin = new System.Windows.Forms.Padding(4);
            this.txtEPHGoal.Name = "txtEPHGoal";
            this.txtEPHGoal.Size = new System.Drawing.Size(50, 23);
            this.txtEPHGoal.TabIndex = 30;
            this.txtEPHGoal.Text = "0";
            // 
            // pnlSpeedBar
            // 
            this.pnlSpeedBar.Location = new System.Drawing.Point(3, 147);
            this.pnlSpeedBar.Name = "pnlSpeedBar";
            this.pnlSpeedBar.Size = new System.Drawing.Size(346, 20);
            this.pnlSpeedBar.TabIndex = 31;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 516);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.pnlStats);
            this.Controls.Add(this.pnlStopwatch);
            this.Controls.Add(this.btnOpenFolder);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnNewSession);
            this.Controls.Add(this.btnClearAll);
            this.Controls.Add(this.btnAddElement);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).EndInit();
            this.pnlStopwatch.ResumeLayout(false);
            this.pnlStopwatch.PerformLayout();
            this.pnlRowHeaders.ResumeLayout(false);
            this.pnlRowHeaders.PerformLayout();
            this.pnlStats.ResumeLayout(false);
            this.pnlStats.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnAddElement;
        private System.Windows.Forms.Label lblTotalValue;
        private System.Windows.Forms.Label lblValueHeader;
        private System.Windows.Forms.Label lblNameHeader;
        private System.Windows.Forms.Label lblEntriesHeader;
        private System.Windows.Forms.DataGridView dgvLogs;
        private System.Windows.Forms.Label lblDeleteHeader;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.Button btnNewSession;
        private System.Windows.Forms.Panel pnlElements;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.TextBox txtAppTitle;
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
        private System.Windows.Forms.Label lblEPHText;
        private System.Windows.Forms.Label lblEPH;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblClearHeader;
        private System.Windows.Forms.Panel pnlRowHeaders;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.Panel pnlStats;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txtGoal;
        private System.Windows.Forms.CheckBox chkGoal;
        private System.Windows.Forms.Panel pnlProgressBar;
        private System.Windows.Forms.Label lbl100;
        private System.Windows.Forms.Label lbl0;
        private System.Windows.Forms.Panel pnlSpeedBar;
        private System.Windows.Forms.TextBox txtEPHGoal;
        private System.Windows.Forms.CheckBox chkEPHGoal;
    }
}

