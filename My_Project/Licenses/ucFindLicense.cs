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
    public partial class ucFindLicense : UserControl
    {
        public int LicenseID {  get; set; }

        public bool IsFound =false;

        public int PersonID { get; set; }
        public bool IsForRenew {  get; set; }

        public bool IsForReplacement;
        
        public bool IsForDetain { get; set; }

        public bool IsForRelease {  get; set; }




        public ucFindLicense()
        {
            InitializeComponent();
        }

        private void ucFindLicense_Load(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(int.TryParse(txtID.Text,out int LiceseID))
                this.LicenseID = LiceseID;
            else
            {
                MessageBox.Show("ID shuld be a number","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            clsLicense license1 = clsLicense.Find(this.LicenseID);

            if (license1 == null) {

                MessageBox.Show("This License does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;

            }

            if (license1.LicenseClass != 3 && !this.IsForRenew && !this.IsForReplacement)
            {
                MessageBox.Show("This License Class is not accepted !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(license1.ExpirationDate < DateTime.Now && !this.IsForRenew)
            {
                MessageBox.Show("This License Is Expired !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            if (!license1.IsActive)
            {

                MessageBox.Show("This License Is Not Active !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            if( IsForRenew && license1.ExpirationDate > DateTime.Now)
            {
                MessageBox.Show("This License Is Still New ! , It will be expired in "+license1.ExpirationDate.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            clsDetainedLicense DL1 = clsDetainedLicense.FindByLicenseID(LicenseID);

            if ( IsForDetain && ( DL1 != null && DL1.IsReleased==false) )
            {
                
                MessageBox.Show("This License Is Already Detained  ","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(IsForRelease && (DL1==null || DL1.IsReleased==true) )
            {
                MessageBox.Show("This License Is Not Detained  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }


            this.IsFound = true;
            int DriverID = license1.DriverID;

            int ApplicationID = license1.ApplicationID;

            clsApplication app1 = clsApplication.Find(ApplicationID);

            clsPerson p1 = clsPerson.Find(app1.ApplicationPersonID);

            this.PersonID = p1.PersonID;


            clsLicensClass cls1 = clsLicensClass.Find(license1.LicenseClass);

            lbClass.Text = cls1.ClassName;

            lbName.Text = p1.FirstName + " " + p1.SecondName + " " + p1.ThirdName + " " + p1.LastName;

            lbLicenseID.Text = license1.LicenseID.ToString();

            lbNationalNo.Text = p1.NationalNumber;

            lbIsDetained.Text =  DL1==null  ? "NO" : DL1.IsReleased?"No":"Yes";

            lbGender.Text = p1.Gender == 0 ? "Male" : "Female";

            lbIssueDate.Text = license1.IssueDate.ToString();

            lbIssueReason.Text = license1.IssueReason == 1 ? "First Time" : license1.IssueReason == 2 ? "Renew" : license1.IssueReason == 3 ? "Replacement For Damage"
              : "Replacement For Lost";

            if (!string.IsNullOrEmpty(license1.Notes))
            {
                lbNotes.Text = license1.Notes;
            }

            if (license1.IsActive)
                lbIsActive.Text = "Yes";
            else
                lbIsActive.Text = "No";

            lbDateOfBirth.Text = p1.DateOfBirth.ToString();

            lbExpirationDate.Text = license1.ExpirationDate.ToString();

            lbDriverID.Text = license1.DriverID.ToString();

            try
            {
                pbProfile.Load(p1.ImagePath);

            }
            catch (Exception ex)
            {

            }





        }
    }
}
