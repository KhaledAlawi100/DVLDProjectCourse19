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
            this.ucPersonInformation1 = new My_Project.Users.ucPersonInformation();
            this.kryptonButton1 = new Krypton.Toolkit.KryptonButton();
            this.SuspendLayout();
            // 
            // ucPersonInformation1
            // 
            this.ucPersonInformation1.Location = new System.Drawing.Point(0, 0);
            this.ucPersonInformation1.Name = "ucPersonInformation1";
            this.ucPersonInformation1.PersonID = 0;
            this.ucPersonInformation1.Size = new System.Drawing.Size(932, 353);
            this.ucPersonInformation1.TabIndex = 2;
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(713, 359);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(204, 50);
            this.kryptonButton1.TabIndex = 4;
            this.kryptonButton1.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.kryptonButton1.Values.Text = "Close";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // PersonDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 450);
            this.Controls.Add(this.kryptonButton1);
            this.Controls.Add(this.ucPersonInformation1);
            this.Name = "PersonDetails";
            this.Text = "PersonDetails";
            this.Load += new System.EventHandler(this.PersonDetails_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ucPersonInformation ucPersonInformation1;
        private Krypton.Toolkit.KryptonButton kryptonButton1;
    }
}