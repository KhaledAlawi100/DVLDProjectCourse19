using DVLD_Business_Layer;
using My_Project.Applications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace My_Project.Licenses
{
    public partial class LicensesHistory : Form
    {
        int ApplicationID;
        public LicensesHistory(int ApplicationID)
        {
            InitializeComponent();
            this.ApplicationID = ApplicationID;
        }

        private void LicensesHistory_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        int DriverID;
        private void LoadData()
        {
            clsApplication app1 = clsApplication.Find(ApplicationID);

            ucFind1.comboBox1.SelectedIndex = 1;

            ucFind1.textBox1.Text = app1.ApplicationPersonID.ToString();

            ucFind1.button1.PerformClick();

            ucFind1.textBox1.Enabled = false;
            ucFind1.button2.Enabled = false;
            ucFind1.button1.Enabled = false;
            ucFind1.comboBox1.Enabled = false;


            clsDriver driver = clsDriver.FindbyPersonID(app1.ApplicationPersonID);

            DataTable LocalLicenses = clsLicense.GetAllLicensesByDriverID(driver.DriverID);

            if (LocalLicenses.Rows.Count > 0)
            {
                this.DriverID = Convert.ToInt32(LocalLicenses.Rows[0]["DriverID"]);
            }

            LoadLocalLicenses(LocalLicenses);

            dgvLocalLicenses.DataSource = LocalLicenses;


            DataTable InternationalLicensses = clsInternationalLicense.GetAllInternationalLicenseByDriverID(this.DriverID);

            LoadInternationalLicenses(InternationalLicensses);

            dgvInternationalLicenses.DataSource= InternationalLicensses;
            
        }

        private void LoadLocalLicenses(DataTable Locals)
        {
            // Add a new column to hold the class name
            if (!Locals.Columns.Contains("ClassName"))
                Locals.Columns.Add("ClassName", typeof(string));

            foreach (DataRow Row in Locals.Rows)
            {
                int ClassID = Convert.ToInt32(Row["LicenseClass"]);

                clsLicensClass classLicens = clsLicensClass.Find(ClassID);

                Row["ClassName"] = classLicens.ClassName; // Assign string to new column
            }

            // Optional: remove the numeric column if not needed
            Locals.Columns.Remove("LicenseClass");

            // Rename new column to display name
            Locals.Columns["ClassName"].ColumnName = "Class Name";

            // Remove unwanted columns
            Locals.Columns.Remove("DriverID");
            Locals.Columns.Remove("Notes");
            Locals.Columns.Remove("PaidFees");
            Locals.Columns.Remove("IssueReason");
            Locals.Columns.Remove("CreatedByUserID");
        }

        private void LoadInternationalLicenses(DataTable dt)
        {
            dt.Columns.Remove("CreatedByUserID");


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvLocalLicenses.CurrentRow.Cells[1].Value;
            LicenseInfo frm = new LicenseInfo(ID);
            frm.ShowDialog();
        }

        private void showLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvInternationalLicenses.CurrentRow.Cells[0].Value;

            InternationalDriverInfo frm = new InternationalDriverInfo(ID);
            frm.ShowDialog();

        }
    }
}
