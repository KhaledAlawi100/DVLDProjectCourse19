using DVLD_Business_Layer;
using My_Project.Licenses;
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
    public partial class ReplacementforDamagedOrLostLicenses : Form
    {
        public ReplacementforDamagedOrLostLicenses()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void ReplacementforDamagedOrLostLicenses_Load(object sender, EventArgs e)
        {
            LoadData();
            SetAppFees();
        }

        private void LoadData()
        {
            rbDamage.Checked = true;
            lbApplicationDate.Text = DateTime.Now.ToString();

            lbCreatedBy.Text = clsUser.Find(clsGlobal.UserID).UserName;

            ucFindLicense1.IsForReplacement=true;

            btnIssue.Enabled = ucFindLicense1.IsFound;
        }

        private void SetAppFees()
        {
            if (rbDamage.Checked)
            {
                lbApplicationFees.Text = clsApplicationType.Find(4).Fees.ToString();

            }
            else
            {
                lbApplicationFees.Text = clsApplicationType.Find(3).Fees.ToString();

            }
        }

        private void rbDamage_CheckedChanged(object sender, EventArgs e)
        {
            SetAppFees();
        }

        private void rbLost_CheckedChanged(object sender, EventArgs e)
        {
            SetAppFees();
        }

        private void LoadDataAfterFound()
        {
            btnIssue.Enabled = ucFindLicense1.IsFound;

            if (ucFindLicense1.IsFound)
            {
                llbShowLicenseHistory.Enabled = true;
                OldLicense = clsLicense.Find(ucFindLicense1.LicenseID);


                lbOldLicenseID.Text = ucFindLicense1.LicenseID.ToString();
            }
        }

        private void ucFindLicense1_MouseLeave(object sender, EventArgs e)
        {
            LoadDataAfterFound();
        }

        private void ucFindLicense1_MouseMove(object sender, MouseEventArgs e)
        {
            LoadDataAfterFound();
        }
        clsApplication NewApp;
        clsLicense NewLicense1;
        clsLicense OldLicense;

        private void btnIssue_Click(object sender, EventArgs e)
        {
            NewApp = new clsApplication();

            clsApplication OldApp = clsApplication.Find( clsLicense.Find(ucFindLicense1.LicenseID).ApplicationID );

            NewApp.ApplicationPersonID = OldApp.ApplicationPersonID;

            NewApp.ApplicationDate= DateTime.Now;

            if (rbDamage.Checked)
                NewApp.ApplicationTypeID = 4;
            else if(rbLost.Checked)
                NewApp.ApplicationTypeID = 3;

            NewApp.ApplicationStatus = 3;
            NewApp.LastStatusDate = DateTime.Now;

            NewApp.PaidFees = int.Parse(lbApplicationFees.Text);

            NewApp.CreatedByUserID = clsGlobal.UserID;

            if (NewApp.Save())
            {
                NewLicense1 = new clsLicense();

                NewLicense1.ApplicationID = NewApp.ApplicationID;

                lbApplicationID.Text = NewApp.ApplicationID.ToString();

                NewLicense1.DriverID = OldLicense.DriverID;

                NewLicense1.LicenseClass = OldLicense.LicenseClass;

                NewLicense1.IssueDate = DateTime.Now;

                NewLicense1.ExpirationDate = OldLicense.ExpirationDate;
                
                NewLicense1.Notes = OldLicense.Notes;

                NewLicense1.PaidFees = int.Parse(lbApplicationFees.Text);

                NewLicense1.IsActive = true;

                if (rbDamage.Checked)
                    NewLicense1.IssueReason = 3;
                else if(rbLost.Checked)
                    NewLicense1.IssueReason = 4;

                NewLicense1.CreatedByUserID = clsGlobal.UserID;

                if (NewLicense1.Save())
                {
                    lbReplacedLicenseID.Text = NewLicense1.LicenseID.ToString();

                    OldLicense.IsActive = false;

                    MessageBox.Show("Replaced Successfully !  With ID =" + NewLicense1.LicenseID);


                    if (OldLicense.Save())
                    {
                        llbShowNewLicenseInfo.Enabled = true;


                    }
                    else
                    {
                        MessageBox.Show("Failed to update Old License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                    }


                }
                else
                {
                    MessageBox.Show("Failed to save the new license", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }




            }
            else
            {
                MessageBox.Show("Failed to add a new application ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }




        }

        private void llbShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LicenseInfo frm = new LicenseInfo(NewLicense1.ApplicationID);
            frm.ShowDialog();

        }

        private void llbShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LicensesHistory frm = new LicensesHistory(OldLicense.ApplicationID);
            frm.ShowDialog();

        }
    }
}
