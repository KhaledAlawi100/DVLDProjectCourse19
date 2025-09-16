namespace My_Project.Applications
{
    partial class IssueDrivingLicenseFirst
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
            this.ucAppBasicInfo1 = new My_Project.Applications.ucAppBasicInfo();
            this.ucDLAppInfo1 = new My_Project.Applications.ucDLAppInfo();
            this.txNotes = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ucAppBasicInfo1
            // 
            this.ucAppBasicInfo1.AppID = 0;
            this.ucAppBasicInfo1.Applicant = null;
            this.ucAppBasicInfo1.CreatedBy = null;
            this.ucAppBasicInfo1.Date = new System.DateTime(((long)(0)));
            this.ucAppBasicInfo1.fees = 0F;
            this.ucAppBasicInfo1.Location = new System.Drawing.Point(81, 191);
            this.ucAppBasicInfo1.Name = "ucAppBasicInfo1";
            this.ucAppBasicInfo1.Size = new System.Drawing.Size(770, 244);
            this.ucAppBasicInfo1.Status = ((byte)(0));
            this.ucAppBasicInfo1.StatusDate = new System.DateTime(((long)(0)));
            this.ucAppBasicInfo1.TabIndex = 14;
            this.ucAppBasicInfo1.type = null;
            // 
            // ucDLAppInfo1
            // 
            this.ucDLAppInfo1.ClassName = null;
            this.ucDLAppInfo1.LDLAppID = 0;
            this.ucDLAppInfo1.Location = new System.Drawing.Point(81, 28);
            this.ucDLAppInfo1.Name = "ucDLAppInfo1";
            this.ucDLAppInfo1.PassedTests = 0;
            this.ucDLAppInfo1.Size = new System.Drawing.Size(898, 157);
            this.ucDLAppInfo1.TabIndex = 13;
            // 
            // txNotes
            // 
            this.txNotes.Location = new System.Drawing.Point(222, 441);
            this.txNotes.Multiline = true;
            this.txNotes.Name = "txNotes";
            this.txNotes.Size = new System.Drawing.Size(629, 147);
            this.txNotes.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(103, 453);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 21);
            this.label1.TabIndex = 16;
            this.label1.Text = "Notes";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::My_Project.Properties.Resources.Notes_32;
            this.pictureBox1.Location = new System.Drawing.Point(162, 453);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(30, 35);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Image = global::My_Project.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(531, 602);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(138, 44);
            this.btnClose.TabIndex = 18;
            this.btnClose.Text = "close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = global::My_Project.Properties.Resources.Save_32;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(713, 602);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(138, 44);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // IssueDrivingLicenseFirst
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1062, 742);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txNotes);
            this.Controls.Add(this.ucAppBasicInfo1);
            this.Controls.Add(this.ucDLAppInfo1);
            this.Name = "IssueDrivingLicenseFirst";
            this.Text = "IssueDrivingLicenseFirst";
            this.Load += new System.EventHandler(this.IssueDrivingLicenseFirst_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ucAppBasicInfo ucAppBasicInfo1;
        private ucDLAppInfo ucDLAppInfo1;
        private System.Windows.Forms.TextBox txNotes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
    }
}