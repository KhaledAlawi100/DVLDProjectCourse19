namespace My_Project.Users
{
    partial class PersonDetails
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
            this.button1 = new System.Windows.Forms.Button();
            this.ucPersonInformation1 = new My_Project.Users.ucPersonInformation();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::My_Project.Properties.Resources.Close_32;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(750, 397);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(179, 41);
            this.button1.TabIndex = 1;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ucPersonInformation1
            // 
            this.ucPersonInformation1.Address = null;
            this.ucPersonInformation1.Country = null;
            this.ucPersonInformation1.DateOfBirth = null;
            this.ucPersonInformation1.Email = null;
            this.ucPersonInformation1.FullName = null;
            this.ucPersonInformation1.Gender = null;
            this.ucPersonInformation1.ID = null;
            this.ucPersonInformation1.ImagePath = null;
            this.ucPersonInformation1.Location = new System.Drawing.Point(12, 68);
            this.ucPersonInformation1.Name = "ucPersonInformation1";
            this.ucPersonInformation1.NationalNumber = null;
            this.ucPersonInformation1.Phone = null;
            this.ucPersonInformation1.Size = new System.Drawing.Size(960, 353);
            this.ucPersonInformation1.TabIndex = 0;
            // 
            // PersonDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ucPersonInformation1);
            this.Name = "PersonDetails";
            this.Text = "PersonDetails";
            this.Load += new System.EventHandler(this.PersonDetails_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ucPersonInformation ucPersonInformation1;
        private System.Windows.Forms.Button button1;
    }
}