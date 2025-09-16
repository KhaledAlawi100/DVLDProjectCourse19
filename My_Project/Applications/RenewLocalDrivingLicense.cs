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
    public partial class RenewLocalDrivingLicense : Form
    {
        public RenewLocalDrivingLicense()
        {
            InitializeComponent();
        }

        private void ucFindLicense1_Load(object sender, EventArgs e)
        {


        }

        private void LoadData()
        {
            ucFindLicense1.IsForRenew=true;

            lbApplicationDate.Text = DateTime.Now.ToString();
            lbIssueDate.Text = DateTime.Now.ToString();
            lbCreatedBy.Text = clsUser.Find(clsGlobal.UserID).UserName;

            lbApplicationFees.Text = clsApplicationType.Find(2).Fees.ToString();

            





        }

        private void RenewLocalDrivingLicense_Load(object sender, EventArgs e)
        {
            LoadData();
            btnRenew.Enabled = ucFindLicense1.IsFound;

        }

        private void ucFindLicense1_Leave(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucFindLicense1_MouseLeave(object sender, EventArgs e)
        {
            btnRenew.Enabled = ucFindLicense1.IsFound;
            LoadAfterFound();
        }

        private void ucFindLicense1_MouseMove(object sender, MouseEventArgs e)
        {
            btnRenew.Enabled = ucFindLicense1.IsFound;

            
        }
        clsLicense OldLicense1;
        private  void LoadAfterFound()
        {
            if (ucFindLicense1.IsFound)
            {
                 OldLicense1 = clsLicense.Find(ucFindLicense1.LicenseID);

               lbShowLicenseHistory.Enabled = ucFindLicense1.IsFound;

                clsLicensClass cLi = clsLicensClass.Find(OldLicense1.LicenseClass);

                

                lbLicenseFees.Text = cLi.Fees.ToString();

                lbExpirationDate.Text = DateTime.Now.AddYears(cLi.ValidityLength).ToString();



                lbTotalFees.Text = (int.Parse(lbApplicationFees.Text) + int.Parse(lbLicenseFees.Text)).ToString();
                lbOldLicenseID.Text = OldLicense1.LicenseID.ToString();

            }

        }
        clsLicense NewLicense;
        private void btnRenew_Click(object sender, EventArgs e)
        {
            clsApplication oldApp = clsApplication.Find(OldLicense1.ApplicationID);

            


            clsApplication newApp = new clsApplication();

            newApp.ApplicationPersonID = oldApp.ApplicationPersonID;
            newApp.ApplicationDate = DateTime.Now;
            newApp.ApplicationTypeID = 2;
            newApp.ApplicationStatus = 3;
            newApp.LastStatusDate = DateTime.Now;
            newApp.PaidFees = int.Parse(lbApplicationFees.Text);
            newApp.CreatedByUserID = clsGlobal.UserID;

            if(newApp.Save())
            {
                 NewLicense = new clsLicense();

                NewLicense.ApplicationID = newApp.ApplicationID;

                NewLicense.DriverID = OldLicense1.DriverID;

                NewLicense.LicenseClass = OldLicense1.LicenseClass;

                NewLicense.IssueDate = DateTime.Now;

                NewLicense.ExpirationDate = DateTime.Now.AddYears( clsLicensClass.Find(NewLicense.LicenseClass).ValidityLength );

                NewLicense.Notes = string.IsNullOrEmpty(txNotes.Text) ? "" : txNotes.Text;

                NewLicense.PaidFees = int.Parse(lbTotalFees.Text);

                NewLicense.IsActive = true;

                NewLicense.IssueReason = 2;

                NewLicense.CreatedByUserID = clsGlobal.UserID;

                if (NewLicense.Save())
                {
                    OldLicense1.IsActive = false;

                    MessageBox.Show("Renewed Successfully !  With ID ="+NewLicense.LicenseID);
                    lbRenewAppID.Text = newApp.ApplicationID.ToString();
                    lbRenewedLicenseID.Text = NewLicense.LicenseID.ToString();

                    llbLicenseInfo.Enabled = true;



                    if (OldLicense1.Save())
                    {

                    }
                    else
                    {
                        MessageBox.Show("Failed to update the old licenses status"+clsLicense.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                    }


                }
                else
                {
                    MessageBox.Show("Failed to renew the  licenses ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                }




            }
            else
            {
                MessageBox.Show("Failed to add a new application ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }



        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LicensesHistory frm = new LicensesHistory(OldLicense1.ApplicationID);
            frm.ShowDialog();
        }

        private void llbLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LicenseInfo frm = new LicenseInfo(NewLicense.LicenseID);
            frm.ShowDialog();

        }
    }
}
