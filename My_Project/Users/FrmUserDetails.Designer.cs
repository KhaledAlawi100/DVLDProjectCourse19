namespace My_Project.Users
{
    partial class FrmUserDetails
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
            this.ucLoginInfo1 = new My_Project.Users.ucLoginInfo();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ucLoginInfo1
            // 
            this.ucLoginInfo1.IsActive = false;
            this.ucLoginInfo1.Location = new System.Drawing.Point(12, 33);
            this.ucLoginInfo1.Name = "ucLoginInfo1";
            this.ucLoginInfo1.Size = new System.Drawing.Size(923, 508);
            this.ucLoginInfo1.TabIndex = 0;
            this.ucLoginInfo1.UserID = 0;
            this.ucLoginInfo1.UserName = null;
            this.ucLoginInfo1.Load += new System.EventHandler(this.ucLoginInfo1_Load);
            // 
            // button1
            // 
            this.button1.Image = global::My_Project.Properties.Resources.Close_32;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(772, 491);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(175, 50);
            this.button1.TabIndex = 1;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmUserDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1051, 553);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ucLoginInfo1);
            this.Name = "FrmUserDetails";
            this.Text = "FrmUserDetails";
            this.Load += new System.EventHandler(this.FrmUserDetails_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ucLoginInfo ucLoginInfo1;
        private System.Windows.Forms.Button button1;
    }
}