namespace DasBoot
{
    partial class Form1
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ColumnComputer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLastReboot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnErrors = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonRebootAll = new System.Windows.Forms.Button();
            this.buttonRebootSelected = new System.Windows.Forms.Button();
            this.buttonQueryAll = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.buttonQuerySelected = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnComputer,
            this.ColumnLastReboot,
            this.ColumnStatus,
            this.ColumnErrors});
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(660, 292);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            // 
            // ColumnComputer
            // 
            this.ColumnComputer.HeaderText = "Computer";
            this.ColumnComputer.Name = "ColumnComputer";
            // 
            // ColumnLastReboot
            // 
            this.ColumnLastReboot.HeaderText = "Last Reboot";
            this.ColumnLastReboot.Name = "ColumnLastReboot";
            this.ColumnLastReboot.ReadOnly = true;
            this.ColumnLastReboot.Width = 120;
            // 
            // ColumnStatus
            // 
            this.ColumnStatus.HeaderText = "Status";
            this.ColumnStatus.Name = "ColumnStatus";
            this.ColumnStatus.ReadOnly = true;
            // 
            // ColumnErrors
            // 
            this.ColumnErrors.HeaderText = "Errors";
            this.ColumnErrors.Name = "ColumnErrors";
            this.ColumnErrors.Width = 300;
            // 
            // buttonRebootAll
            // 
            this.buttonRebootAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRebootAll.Location = new System.Drawing.Point(12, 346);
            this.buttonRebootAll.Name = "buttonRebootAll";
            this.buttonRebootAll.Size = new System.Drawing.Size(100, 23);
            this.buttonRebootAll.TabIndex = 3;
            this.buttonRebootAll.Text = "&Reboot All";
            this.buttonRebootAll.UseVisualStyleBackColor = true;
            this.buttonRebootAll.Click += new System.EventHandler(this.buttonRebootAll_Click);
            // 
            // buttonRebootSelected
            // 
            this.buttonRebootSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRebootSelected.Location = new System.Drawing.Point(118, 346);
            this.buttonRebootSelected.Name = "buttonRebootSelected";
            this.buttonRebootSelected.Size = new System.Drawing.Size(100, 23);
            this.buttonRebootSelected.TabIndex = 4;
            this.buttonRebootSelected.Text = "Reboot &Selected";
            this.buttonRebootSelected.UseVisualStyleBackColor = true;
            this.buttonRebootSelected.Click += new System.EventHandler(this.buttonRebootSelected_Click);
            // 
            // buttonQueryAll
            // 
            this.buttonQueryAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonQueryAll.Location = new System.Drawing.Point(267, 346);
            this.buttonQueryAll.Name = "buttonQueryAll";
            this.buttonQueryAll.Size = new System.Drawing.Size(100, 23);
            this.buttonQueryAll.TabIndex = 5;
            this.buttonQueryAll.Text = "&Query All";
            this.buttonQueryAll.UseVisualStyleBackColor = true;
            this.buttonQueryAll.Click += new System.EventHandler(this.buttonQueryAll_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // buttonQuerySelected
            // 
            this.buttonQuerySelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonQuerySelected.Location = new System.Drawing.Point(373, 346);
            this.buttonQuerySelected.Name = "buttonQuerySelected";
            this.buttonQuerySelected.Size = new System.Drawing.Size(100, 23);
            this.buttonQuerySelected.TabIndex = 6;
            this.buttonQuerySelected.Text = "Query S&elected";
            this.buttonQuerySelected.UseVisualStyleBackColor = true;
            this.buttonQuerySelected.Click += new System.EventHandler(this.buttonQuerySelected_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 307);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "(Use Ctrl+V to paste a list of computers)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 381);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonQuerySelected);
            this.Controls.Add(this.buttonQueryAll);
            this.Controls.Add(this.buttonRebootSelected);
            this.Controls.Add(this.buttonRebootAll);
            this.Controls.Add(this.dataGridView1);
            this.MinimumSize = new System.Drawing.Size(700, 420);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonRebootAll;
        private System.Windows.Forms.Button buttonRebootSelected;
        private System.Windows.Forms.Button buttonQueryAll;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnComputer;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLastReboot;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnErrors;
        private System.Windows.Forms.Button buttonQuerySelected;
        private System.Windows.Forms.Label label1;
    }
}

