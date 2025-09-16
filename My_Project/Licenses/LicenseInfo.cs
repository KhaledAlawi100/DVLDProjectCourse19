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

namespace My_Project.Applications
{
    public partial class LicenseInfo : Form
    {
        int AppID;
        public LicenseInfo(int AppID)
        {
            InitializeComponent();
            this.AppID = AppID;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void LicenseInfo_Load(object sender, EventArgs e)
        {
            LoadData();

        }

        private void LoadData()
        {
          

            clsLicense License1 = clsLicense.FindByAppID(AppID);

            clsApplication app1 = clsApplication.Find(AppID);

            clsPerson p1 = clsPerson.Find(app1.ApplicationPersonID);

            lbName.Text = p1.FirstName+ " " +p1.SecondName+" "+p1.ThirdName+" "+ p1.LastName;

            lbClass.Text = clsLicensClass.Find(License1.LicenseClass).ClassName;

            lbLicenseID.Text = License1.LicenseID.ToString();

            lbNationalNo.Text = p1.NationalNumber;

            lbGender.Text = p1.Gender == 0 ? "Male" : "Female";

            lbIssueDate.Text = License1.IssueDate.ToString();

            lbIssueReason.Text = License1.IssueReason == 1 ? "First Time" : License1.IssueReason == 2 ? "Renew" : License1.IssueReason == 3 ? "Replacement For Damage"
                : "Replacement For Lost";

            if (!string.IsNullOrEmpty(License1.Notes))
            {
                lbNotes.Text = License1.Notes;
            }

            if (License1.IsActive)
                lbIsActive.Text = "Yes";
            else
                lbIsActive.Text = "No";

            lbDateOfBirth.Text= p1.DateOfBirth.ToString();

            lbExpirationDate.Text = License1.ExpirationDate.ToString();

            lbDriverID.Text = License1.DriverID.ToString();

            try
            {
                pbProfile.Load(p1.ImagePath);

            }
            catch (Exception ex) { 
            
            }

            clsDetainedLicense DL1 = clsDetainedLicense.FindByLicenseID(License1.LicenseID);

            lbIsDetained.Text = DL1 == null ? "No" : DL1.IsReleased ? "No" : "Yes";
           

        }
    }
}
