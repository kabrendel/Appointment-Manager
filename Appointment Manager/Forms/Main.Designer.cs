namespace Appointment_Manager
{
    partial class Main
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
            this.AppointmentsGridView = new System.Windows.Forms.DataGridView();
            this.buttonCust = new System.Windows.Forms.Button();
            this.buttonApt = new System.Windows.Forms.Button();
            this.buttonAll = new System.Windows.Forms.Button();
            this.buttonWeek = new System.Windows.Forms.Button();
            this.buttonMonth = new System.Windows.Forms.Button();
            this.lvlReport = new System.Windows.Forms.Label();
            this.buttonConsult = new System.Windows.Forms.Button();
            this.buttonMonthly = new System.Windows.Forms.Button();
            this.buttonCusts = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSearch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.AppointmentsGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.AppointmentsGridView.AllowUserToAddRows = false;
            this.AppointmentsGridView.AllowUserToDeleteRows = false;
            this.AppointmentsGridView.AllowUserToResizeRows = false;
            this.AppointmentsGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AppointmentsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AppointmentsGridView.Location = new System.Drawing.Point(158, 12);
            this.AppointmentsGridView.MultiSelect = false;
            this.AppointmentsGridView.Name = "dataGridView1";
            this.AppointmentsGridView.ReadOnly = true;
            this.AppointmentsGridView.RowHeadersVisible = false;
            this.AppointmentsGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.AppointmentsGridView.Size = new System.Drawing.Size(630, 375);
            this.AppointmentsGridView.TabIndex = 0;
            this.AppointmentsGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.AppointmentsGridView_DataBindingComplete);
            // 
            // buttonCust
            // 
            this.buttonCust.Location = new System.Drawing.Point(40, 28);
            this.buttonCust.Name = "buttonCust";
            this.buttonCust.Size = new System.Drawing.Size(85, 25);
            this.buttonCust.TabIndex = 1;
            this.buttonCust.Text = "Customer";
            this.buttonCust.UseVisualStyleBackColor = true;
            this.buttonCust.Click += new System.EventHandler(this.ButtonCust_Click);
            // 
            // buttonApt
            // 
            this.buttonApt.Location = new System.Drawing.Point(40, 59);
            this.buttonApt.Name = "buttonApt";
            this.buttonApt.Size = new System.Drawing.Size(85, 25);
            this.buttonApt.TabIndex = 2;
            this.buttonApt.Text = "Appointments";
            this.buttonApt.UseVisualStyleBackColor = true;
            this.buttonApt.Click += new System.EventHandler(this.ButtonApt_Click);
            // 
            // buttonAll
            // 
            this.buttonAll.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonAll.Location = new System.Drawing.Point(320, 404);
            this.buttonAll.Name = "buttonAll";
            this.buttonAll.Size = new System.Drawing.Size(100, 25);
            this.buttonAll.TabIndex = 7;
            this.buttonAll.Text = "All Appointments";
            this.buttonAll.UseVisualStyleBackColor = true;
            this.buttonAll.Click += new System.EventHandler(this.ButtonAll_Click);
            // 
            // buttonWeek
            // 
            this.buttonWeek.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonWeek.Location = new System.Drawing.Point(426, 404);
            this.buttonWeek.Name = "buttonWeek";
            this.buttonWeek.Size = new System.Drawing.Size(100, 25);
            this.buttonWeek.TabIndex = 8;
            this.buttonWeek.Text = "Current Week";
            this.buttonWeek.UseVisualStyleBackColor = true;
            this.buttonWeek.Click += new System.EventHandler(this.ButtonWeek_Click);
            // 
            // buttonMonth
            // 
            this.buttonMonth.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonMonth.Location = new System.Drawing.Point(532, 404);
            this.buttonMonth.Name = "buttonMonth";
            this.buttonMonth.Size = new System.Drawing.Size(100, 25);
            this.buttonMonth.TabIndex = 9;
            this.buttonMonth.Text = "Current Month";
            this.buttonMonth.UseVisualStyleBackColor = true;
            this.buttonMonth.Click += new System.EventHandler(this.ButtonMonth_Click);
            // 
            // lvlReport
            // 
            this.lvlReport.AutoSize = true;
            this.lvlReport.Location = new System.Drawing.Point(37, 136);
            this.lvlReport.Name = "lvlReport";
            this.lvlReport.Size = new System.Drawing.Size(47, 13);
            this.lvlReport.TabIndex = 6;
            this.lvlReport.Text = "Reports:";
            // 
            // buttonConsult
            // 
            this.buttonConsult.Location = new System.Drawing.Point(40, 183);
            this.buttonConsult.Name = "buttonConsult";
            this.buttonConsult.Size = new System.Drawing.Size(85, 25);
            this.buttonConsult.TabIndex = 5;
            this.buttonConsult.Text = "Consultants";
            this.buttonConsult.UseVisualStyleBackColor = true;
            this.buttonConsult.Click += new System.EventHandler(this.ButtonConsultants_Click);
            // 
            // buttonMonthly
            // 
            this.buttonMonthly.Location = new System.Drawing.Point(40, 152);
            this.buttonMonthly.Name = "buttonMonthly";
            this.buttonMonthly.Size = new System.Drawing.Size(85, 25);
            this.buttonMonthly.TabIndex = 4;
            this.buttonMonthly.Text = "Monthly";
            this.buttonMonthly.UseVisualStyleBackColor = true;
            this.buttonMonthly.Click += new System.EventHandler(this.ButtonMonthly_Click);
            // 
            // buttonCusts
            // 
            this.buttonCusts.Location = new System.Drawing.Point(40, 214);
            this.buttonCusts.Name = "buttonCusts";
            this.buttonCusts.Size = new System.Drawing.Size(85, 25);
            this.buttonCusts.TabIndex = 6;
            this.buttonCusts.Text = "Customers";
            this.buttonCusts.UseVisualStyleBackColor = true;
            this.buttonCusts.Click += new System.EventHandler(this.ButtonCustomers_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonExit.Location = new System.Drawing.Point(40, 404);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(85, 25);
            this.buttonExit.TabIndex = 9;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.ButtonExit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Records:";
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(40, 90);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(85, 23);
            this.buttonSearch.TabIndex = 3;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.ButtonSearch_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 441);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonCusts);
            this.Controls.Add(this.buttonMonthly);
            this.Controls.Add(this.buttonConsult);
            this.Controls.Add(this.lvlReport);
            this.Controls.Add(this.buttonMonth);
            this.Controls.Add(this.buttonWeek);
            this.Controls.Add(this.buttonAll);
            this.Controls.Add(this.buttonApt);
            this.Controls.Add(this.buttonCust);
            this.Controls.Add(this.AppointmentsGridView);
            this.MinimumSize = new System.Drawing.Size(815, 480);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Appointment Calendar";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AppointmentsGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView AppointmentsGridView;
        private System.Windows.Forms.Button buttonCust;
        private System.Windows.Forms.Button buttonApt;
        private System.Windows.Forms.Button buttonAll;
        private System.Windows.Forms.Button buttonWeek;
        private System.Windows.Forms.Button buttonMonth;
        private System.Windows.Forms.Label lvlReport;
        private System.Windows.Forms.Button buttonConsult;
        private System.Windows.Forms.Button buttonMonthly;
        private System.Windows.Forms.Button buttonCusts;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSearch;
    }
}

