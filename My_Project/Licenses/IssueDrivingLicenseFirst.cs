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
    public partial class IssueDrivingLicenseFirst : Form
    {
        private int LDLID;
        private int AppID;
        public IssueDrivingLicenseFirst(int LDLID)
        {
            InitializeComponent();
            this.LDLID = LDLID;
        }
        clsPerson p1;
        clsLicensClass clasLic;
        clsApplication app1;

        private void LoadData()
        {
            clsLDLApp LDLapp1 = clsLDLApp.Find(LDLID);
            this.AppID = LDLapp1.ApplicationID;

            if (LDLapp1 == null)
            {

                MessageBox.Show("Not Found");

            }

            ucDLAppInfo1.LDLAppID = LDLID;

             clasLic = clsLicensClass.Find(LDLapp1.LicenseClassID);

            ucDLAppInfo1.ClassName = clasLic.ClassName;

            ucDLAppInfo1.RefreshUI();

            ucAppBasicInfo1.AppID = this.AppID;

             app1 = clsApplication.Find(this.AppID);

            //MessageBox.Show(app1.PaidFees.ToString());

            ucAppBasicInfo1.Status = app1.ApplicationStatus;



            ucAppBasicInfo1.fees = app1.PaidFees;


            clsApplicationType appType = clsApplicationType.Find(app1.ApplicationTypeID);

            ucAppBasicInfo1.type = appType.Title;

             p1 = clsPerson.Find(app1.ApplicationPersonID);

            ucAppBasicInfo1.Applicant = p1.FirstName + " " + p1.SecondName + " " + p1.ThirdName + " " + p1.LastName;

            ucAppBasicInfo1.Date = app1.ApplicationDate;

            ucAppBasicInfo1.StatusDate = app1.LastStatusDate;

            ucAppBasicInfo1.CreatedBy = clsUser.Find(clsGlobal.UserID).UserName;


            DataTable Appoints1 = clsTestAppointment.GetAlltTestAppointments(LDLID);

            int PassedTest = 0;

            foreach (DataRow dr in Appoints1.Rows)
            {

                int AppointID = (int)dr["TestAppointmentID"];

                clsTest test1 = clsTest.Find(AppointID);

                if (test1 == null)
                {

                    ucDLAppInfo1.PassedTests = 0;


                }
                else if (test1.TestResult)
                {
                    PassedTest++;

                }

            }

            ucDLAppInfo1.PassedTests = PassedTest;

            ucDLAppInfo1.RefreshUI();

            ucAppBasicInfo1.RefreshUI();







        }

        private void IssueDrivingLicenseFirst_Load(object sender, EventArgs e)
        {
            LoadData();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            clsLicense license1 = new clsLicense(); 

            license1.ApplicationID = ucAppBasicInfo1.AppID;
            
            clsDriver driver1 = clsDriver.FindbyPersonID(p1.PersonID);

            MessageBox.Show(p1.PersonID.ToString());
            MessageBox.Show(driver1.DriverID.ToString());

            license1.DriverID = driver1.DriverID;

            license1.LicenseClass = clasLic.ClassID;

            license1.IssueDate = DateTime.Now;

            license1.ExpirationDate = DateTime.Now.AddYears(clasLic.ValidityLength);

            license1.Notes = txNotes.Text;

            license1.PaidFees = app1.PaidFees;

            license1.IsActive = true;

            license1.IssueReason = 1;

            license1.CreatedByUserID = clsGlobal.UserID;

            if (license1.Save() )
            {
                app1.ApplicationStatus = 3;

                if(!app1.Save())
                {
                    MessageBox.Show("Error in updating the status of the application","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    MessageBox.Show("License Saved Successfully With ID:"+license1.LicenseID);
                }

            }
            else
            {
                MessageBox.Show("Error in Adding the new License "+clsLicense.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }






        }
    }
}
