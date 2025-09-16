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
    public partial class InternationalDriverInfo : Form
    {
        int InternationalLicenseID;
        public InternationalDriverInfo(int InternationalLicenseID)
        {
            InitializeComponent();
            this.InternationalLicenseID = InternationalLicenseID;
        }

        private void InternationalDriverInfo_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {

            clsInternationalLicense internationalLicense1 = clsInternationalLicense.Find(this.InternationalLicenseID);

            clsDriver driver1 = clsDriver.Find(internationalLicense1.DriverID);

            clsPerson p1 = clsPerson.Find(driver1.PersonID);

            lbName.Text = p1.FirstName + " " + p1.SecondName + " " + p1.ThirdName + " " + p1.LastName;

            lbInternationalLicense.Text = internationalLicense1.InternationalLicenseID.ToString();

            lbLicenseID.Text = internationalLicense1.IssuedUsingLocalLicenseID.ToString();

            lbNationalNo.Text = p1.NationalNumber;

            lbGender.Text = p1.Gender == 0 ? "Male" : "Female";

            lbIssueDate.Text = internationalLicense1.IssueDate.ToString();

            lbAppilicationID.Text = internationalLicense1.ApplicationID.ToString();

            lbIsActive.Text = internationalLicense1.IsActive ? "Yes" : "No";

            lbDateOfBirth.Text = p1.DateOfBirth.ToString();

            lbDriverID.Text = internationalLicense1.DriverID.ToString();

            lbExpirationDate.Text = internationalLicense1.ExpirationDate.ToString();


            try
            {
                pbProfile.Load(p1.ImagePath);

            }
            catch (Exception e)
            {

            }




        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
