using DVLD_Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_Project.Licenses
{
    public partial class NewInterNationalLicense : Form
    {
        public NewInterNationalLicense()
        {
            InitializeComponent();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucFindLicense1_Load(object sender, EventArgs e)
        {
           
        }

        private void NewInterNationalLicense_Load(object sender, EventArgs e)
        {
            btnIssue.Enabled = ucFindLicense1.IsFound;
            LoadData();
        }
        private void LoadData()
        {

            lbApplicationDate.Text = DateTime.Now.ToString();
            lbIssueDate.Text = DateTime.Now.ToString(); 
            lbFees.Text = clsApplicationType.Find(6).Fees.ToString();
            lbExpiratioinDate.Text = DateTime.Now.AddYears(1).ToString();
            lbCreatedBy.Text= clsUser.Find(clsGlobal.UserID).UserName;


        }

        private void ucFindLicense1_Click(object sender, EventArgs e)
        {
            btnIssue.Enabled = ucFindLicense1.IsFound;
        }

        private void ucFindLicense1_MouseHover(object sender, EventArgs e)
        {
            btnIssue.Enabled = ucFindLicense1.IsFound;

        }

        private void ucFindLicense1_MouseLeave(object sender, EventArgs e)
        {
            btnIssue.Enabled = ucFindLicense1.IsFound;
            if(ucFindLicense1.IsFound)
                lbLocalLicenseID.Text = ucFindLicense1.LicenseID.ToString();

        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            

            clsLicense license = clsLicense.Find(ucFindLicense1.LicenseID);

            if (clsInternationalLicense.IsExist(license.DriverID))
            {
                MessageBox.Show("This Driver Already Have international License!","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            clsApplication app1 = new clsApplication();

            app1.ApplicationPersonID = ucFindLicense1.PersonID;
            app1.ApplicationDate = DateTime.Now;
            app1.ApplicationTypeID = 6;
            app1.ApplicationStatus = 1;
            app1.LastStatusDate = DateTime.Now;
            app1.PaidFees = clsApplicationType.Find(6).Fees;
            app1.CreatedByUserID = clsGlobal.UserID;

            if (app1.Save()) { 

                lbInternationalAppID.Text = app1.ApplicationID.ToString();

                clsInternationalLicense InterLicense = new clsInternationalLicense();

                InterLicense.ApplicationID = app1.ApplicationID;
                InterLicense.DriverID = license.DriverID;
                InterLicense.IssuedUsingLocalLicenseID = license.LicenseID;
                InterLicense.IssueDate = DateTime.Now;
                InterLicense.ExpirationDate = DateTime.Now.AddYears(1);
                InterLicense.IsActive = true;
                InterLicense.CreatedByUserID = clsGlobal.UserID;

                if (InterLicense.Save())
                {
                    llbLicenseInfo.Enabled = true;

                    lbInternationalID.Text = InterLicense.InternationalLicenseID.ToString();

                    MessageBox.Show("New International License was add with ID = " + InterLicense.InternationalLicenseID);

                }
                else
                {
                    MessageBox.Show("Failed to add a new international license","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }


            }
            else
            {
                MessageBox.Show("Failed to add a new Application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }


        }

        private void llbLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            InternationalDriverInfo frm = new InternationalDriverInfo(int.Parse(lbInternationalID.Text));
            frm.ShowDialog();
        }

    }
}
