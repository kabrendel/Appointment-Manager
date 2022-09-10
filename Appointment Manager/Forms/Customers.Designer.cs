
namespace Appointment_Scheduler
{
    partial class Customers
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
            this.CustomerGridView = new System.Windows.Forms.DataGridView();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.textName = new System.Windows.Forms.TextBox();
            this.textAdd1 = new System.Windows.Forms.TextBox();
            this.textCity = new System.Windows.Forms.TextBox();
            this.textCountry = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblAdr = new System.Windows.Forms.Label();
            this.lblCity = new System.Windows.Forms.Label();
            this.lblCountry = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblPost = new System.Windows.Forms.Label();
            this.textPostal = new System.Windows.Forms.TextBox();
            this.textAdd2 = new System.Windows.Forms.TextBox();
            this.textPhone = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.CustomerGridView.AllowUserToAddRows = false;
            this.CustomerGridView.AllowUserToDeleteRows = false;
            this.CustomerGridView.AllowUserToResizeColumns = false;
            this.CustomerGridView.AllowUserToResizeRows = false;
            this.CustomerGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CustomerGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CustomerGridView.Location = new System.Drawing.Point(12, 12);
            this.CustomerGridView.MultiSelect = false;
            this.CustomerGridView.Name = "dataGridView1";
            this.CustomerGridView.ReadOnly = true;
            this.CustomerGridView.RowHeadersVisible = false;
            this.CustomerGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.CustomerGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.CustomerGridView.ShowEditingIcon = false;
            this.CustomerGridView.Size = new System.Drawing.Size(629, 227);
            this.CustomerGridView.TabIndex = 0;
            this.CustomerGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.CustomerGridView_DataBindingComplete);
            this.CustomerGridView.SelectionChanged += new System.EventHandler(this.CustomerGridView_SelectionChanged);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.Location = new System.Drawing.Point(566, 266);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 8;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUpdate.Location = new System.Drawing.Point(566, 295);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(75, 23);
            this.buttonUpdate.TabIndex = 9;
            this.buttonUpdate.Text = "Update";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.ButtonUpdate_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelete.Location = new System.Drawing.Point(566, 321);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonDelete.TabIndex = 10;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonExit.Location = new System.Drawing.Point(566, 350);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(75, 23);
            this.buttonExit.TabIndex = 11;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.ButtonExit_Click);
            // 
            // textName
            // 
            this.textName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textName.Location = new System.Drawing.Point(60, 295);
            this.textName.MaxLength = 45;
            this.textName.Name = "textName";
            this.textName.Size = new System.Drawing.Size(100, 20);
            this.textName.TabIndex = 1;
            // 
            // textAdd1
            // 
            this.textAdd1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textAdd1.Location = new System.Drawing.Point(60, 321);
            this.textAdd1.MaxLength = 50;
            this.textAdd1.Name = "textAdd1";
            this.textAdd1.Size = new System.Drawing.Size(100, 20);
            this.textAdd1.TabIndex = 2;
            // 
            // textCity
            // 
            this.textCity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textCity.Location = new System.Drawing.Point(239, 295);
            this.textCity.MaxLength = 50;
            this.textCity.Name = "textCity";
            this.textCity.Size = new System.Drawing.Size(100, 20);
            this.textCity.TabIndex = 4;
            // 
            // textCountry
            // 
            this.textCountry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textCountry.Location = new System.Drawing.Point(239, 347);
            this.textCountry.MaxLength = 50;
            this.textCountry.Name = "textCountry";
            this.textCountry.Size = new System.Drawing.Size(100, 20);
            this.textCountry.TabIndex = 6;
            // 
            // lblName
            // 
            this.lblName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(9, 298);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 12;
            this.lblName.Text = "Name:";
            // 
            // lblAdr
            // 
            this.lblAdr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAdr.AutoSize = true;
            this.lblAdr.Location = new System.Drawing.Point(9, 324);
            this.lblAdr.Name = "lblAdr";
            this.lblAdr.Size = new System.Drawing.Size(48, 13);
            this.lblAdr.TabIndex = 13;
            this.lblAdr.Text = "Address:";
            // 
            // lblCity
            // 
            this.lblCity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCity.AutoSize = true;
            this.lblCity.Location = new System.Drawing.Point(206, 302);
            this.lblCity.Name = "lblCity";
            this.lblCity.Size = new System.Drawing.Size(27, 13);
            this.lblCity.TabIndex = 14;
            this.lblCity.Text = "City:";
            // 
            // lblCountry
            // 
            this.lblCountry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCountry.AutoSize = true;
            this.lblCountry.Location = new System.Drawing.Point(187, 350);
            this.lblCountry.Name = "lblCountry";
            this.lblCountry.Size = new System.Drawing.Size(46, 13);
            this.lblCountry.TabIndex = 16;
            this.lblCountry.Text = "Country:";
            // 
            // lblPhone
            // 
            this.lblPhone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(345, 300);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(81, 13);
            this.lblPhone.TabIndex = 17;
            this.lblPhone.Text = "Phone Number:";
            // 
            // lblPost
            // 
            this.lblPost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPost.AutoSize = true;
            this.lblPost.Location = new System.Drawing.Point(166, 324);
            this.lblPost.Name = "lblPost";
            this.lblPost.Size = new System.Drawing.Size(67, 13);
            this.lblPost.TabIndex = 15;
            this.lblPost.Text = "Postal Code:";
            // 
            // textPostal
            // 
            this.textPostal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textPostal.Location = new System.Drawing.Point(239, 321);
            this.textPostal.MaxLength = 10;
            this.textPostal.Name = "textPostal";
            this.textPostal.Size = new System.Drawing.Size(100, 20);
            this.textPostal.TabIndex = 5;
            // 
            // textAdd2
            // 
            this.textAdd2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textAdd2.Location = new System.Drawing.Point(60, 347);
            this.textAdd2.MaxLength = 50;
            this.textAdd2.Name = "textAdd2";
            this.textAdd2.Size = new System.Drawing.Size(100, 20);
            this.textAdd2.TabIndex = 3;
            // 
            // textPhone
            // 
            this.textPhone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textPhone.Location = new System.Drawing.Point(432, 297);
            this.textPhone.MaxLength = 20;
            this.textPhone.Name = "textPhone";
            this.textPhone.Size = new System.Drawing.Size(100, 20);
            this.textPhone.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 266);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Customer Information:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 242);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(237, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Click on \"New Customer\" to create a new record";
            // 
            // Customers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonExit;
            this.ClientSize = new System.Drawing.Size(654, 391);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textPhone);
            this.Controls.Add(this.textAdd2);
            this.Controls.Add(this.textPostal);
            this.Controls.Add(this.lblPost);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.lblCountry);
            this.Controls.Add(this.lblCity);
            this.Controls.Add(this.lblAdr);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.textCountry);
            this.Controls.Add(this.textCity);
            this.Controls.Add(this.textAdd1);
            this.Controls.Add(this.textName);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.CustomerGridView);
            this.MaximumSize = new System.Drawing.Size(670, 430);
            this.MinimumSize = new System.Drawing.Size(670, 430);
            this.Name = "Customers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Customer";
            this.Load += new System.EventHandler(this.Customers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CustomerGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView CustomerGridView;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.TextBox textName;
        private System.Windows.Forms.TextBox textAdd1;
        private System.Windows.Forms.TextBox textCity;
        private System.Windows.Forms.TextBox textCountry;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblAdr;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.Label lblCountry;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblPost;
        private System.Windows.Forms.TextBox textPostal;
        private System.Windows.Forms.TextBox textAdd2;
        private System.Windows.Forms.TextBox textPhone;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}