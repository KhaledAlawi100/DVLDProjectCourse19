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
    public partial class ReleaseDeatainedLicense : Form
    {
        enum enMode { New = 0, update = 1 }

        enMode mode;

        int DetainID;
        public ReleaseDeatainedLicense(int DetainID)
        {
            InitializeComponent();

            this.DetainID = DetainID;

            if(DetainID == -1)
            {
                mode = enMode.New;
            }else
                mode = enMode.update;
        }

        private void ReleaseDeatainedLicense_Load(object sender, EventArgs e)
        {
            LoadData();

        }
        clsDetainedLicense DL1;
        private void LoadData()
        {

            if (mode == enMode.update)
            {
                clsDetainedLicense dl = clsDetainedLicense.Find(this.DetainID);


                ucFindLicense1.txtID.Text = dl.LicenseID.ToString();

                ucFindLicense1.btnSearch.PerformClick();

                LoadAfterFound();
            }
            ucFindLicense1.IsForRelease=true;

            lbDetainDate.Text = DateTime.Now.ToString();

            lbAppFees.Text =  clsApplicationType.Find(5).Fees.ToString();

            lbCreatedBy.Text = clsUser.Find(clsGlobal.UserID).UserName;

            



        }

        private void LoadAfterFound()
        {

            if (ucFindLicense1.IsFound)
            {
                btnRelease.Enabled = true;
                
                DL1 = clsDetainedLicense.FindByLicenseID(ucFindLicense1.LicenseID);

                lbDetainID.Text = DL1.DetainID.ToString();

                lbFineFees.Text= DL1.FineFees.ToString();

                lbLicenseID.Text = DL1.LicenseID.ToString();

                float AppFees = clsApplicationType.Find(5).Fees;

                float FineFees = DL1.FineFees;

                float TotalFees = AppFees+FineFees;

                lbTotalFees.Text = TotalFees.ToString();



            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucFindLicense1_Load(object sender, EventArgs e)
        {

        }

        private void ucFindLicense1_MouseLeave(object sender, EventArgs e)
        {
            LoadAfterFound();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            clsApplication app1 = new clsApplication();

            clsLicense license1 = clsLicense.Find(DL1.LicenseID);

            clsApplication app2 = clsApplication.Find(license1.ApplicationID);

            app1.ApplicationPersonID = app2.ApplicationPersonID;

            app1.ApplicationDate = DateTime.Now;

            app1.ApplicationTypeID = 5;

            app1.ApplicationStatus = 3;

            app1.LastStatusDate = DateTime.Now;

            app1.PaidFees = int.Parse(lbTotalFees.Text);

            app1.CreatedByUserID = clsGlobal.UserID;

            if (app1.Save()) { 

                DL1.IsReleased = true;

                DL1.ReleaseDate = DateTime.Now;

                DL1.ReleasedByUserID = clsGlobal.UserID;

                DL1.ReleaseApplicationID = app1.ApplicationID;
                

                lbAppID.Text=app1.ApplicationID.ToString();

                if (DL1.Save()) {

                    MessageBox.Show("The license released succeesfully !");

                    ucFindLicense1.Enabled = false;
                    btnRelease.Enabled = false;

                
                }else
                    MessageBox.Show("Faileed to release the license  :"+clsDetainedLicense.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);




            }
            else
               MessageBox.Show("Faileed to create the application ","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);


            
        }
    }
}
