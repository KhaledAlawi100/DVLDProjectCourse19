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
    public partial class ScheduleVisionTest : Form
    {
        int AppointmentID;
        int LDLAppID;
        bool isRetake;

        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        clsTestAppointment _TestAppointment;



        public ScheduleVisionTest(int AppointmentID,int LDLAppID,bool isRetake)
        {
            InitializeComponent();

            this.AppointmentID = AppointmentID;
            this.LDLAppID = LDLAppID;
            this.isRetake = isRetake;

            if(AppointmentID==-1) _Mode = enMode.AddNew;
            else _Mode = enMode.Update;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ScheduleVisionTest_Load(object sender, EventArgs e)
        {
            LoadData();
            
        }
        clsPerson p1;
        clsApplication app1;
        clsLDLApp ldlApp1;
        private void LoadData()
        {
            
          

            lbID.Text=this.LDLAppID.ToString();

             ldlApp1 = clsLDLApp.Find(this.LDLAppID);

            clsLicensClass LiClass1 = clsLicensClass.Find(ldlApp1.LicenseClassID);

            lbDClass.Text = LiClass1.ClassName;

             app1 = clsApplication.Find(ldlApp1.ApplicationID);

            

             p1 = clsPerson.Find(app1.ApplicationPersonID);

            lbName.Text = p1.FirstName+" "+p1.SecondName+" "+p1.ThirdName+" "+p1.LastName;

            LbFees.Text = clsTestType.Find(1).TestTypeFees.ToString();

            DataTable dt = clsTestAppointment.GetAlltTestAppointments(this.LDLAppID, 1);

            if (dt != null)
                lbTrail.Text = dt.Rows.Count.ToString();
            else
            {
                int x = 0;
                lbTrail.Text = x.ToString();
            }

            if (isRetake) { 

                clsApplicationType appType = clsApplicationType.Find(7);

                ucRetakeTestInfo1.RetakeFees = appType.Fees;
            
            }

            ucRetakeTestInfo1.Enabled = isRetake;

            ucRetakeTestInfo1.TotalFees = int.Parse(LbFees.Text) +ucRetakeTestInfo1.RetakeFees;

            ucRetakeTestInfo1.RefreshUI();

            if (_Mode == enMode.AddNew)
            {
                _TestAppointment = new clsTestAppointment();
                 dateTimePicker1.MinDate = DateTime.Now;
                
                return;

            }

            

            _TestAppointment = clsTestAppointment.Find(this.AppointmentID);

            dateTimePicker1.Value = _TestAppointment.AppointmentDate;

            dateTimePicker1.MinDate = _TestAppointment.AppointmentDate;

            if (_TestAppointment.IsLocked) { 

                lbLocked.Visible = true;

                dateTimePicker1.Enabled=false;

                btnSave.Enabled=false;

            
            }

            


        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Fill _TestAppointment data
            _TestAppointment.TestTypeID = 1;

            _TestAppointment.LocalDrivingLicenseApplicationID = this.LDLAppID;

            _TestAppointment.AppointmentDate = dateTimePicker1.Value;
            _TestAppointment.PaidFees = ucRetakeTestInfo1.TotalFees;
            _TestAppointment.CreatedByUserID = clsGlobal.UserID;
             this._TestAppointment.IsLocked = false;
            this._TestAppointment.RetakeTestApplicationID = -1;

            if (isRetake)
            {
                clsApplication newApp = new clsApplication();

                newApp.ApplicationPersonID = p1.PersonID;
                newApp.ApplicationDate = dateTimePicker1.Value;
                newApp.ApplicationTypeID = 7; // Consider replacing with constant or enum
                newApp.ApplicationStatus = this.app1?.ApplicationStatus ?? 1; // fallback if app1 is null
                newApp.LastStatusDate = DateTime.Now;
                newApp.PaidFees = ucRetakeTestInfo1.RetakeFees;
                newApp.CreatedByUserID = clsGlobal.UserID;
                


                if (!newApp.Save())
                {
                    MessageBox.Show("Failed to add retake application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Update UI with new Application ID
                ucRetakeTestInfo1.ID = newApp.ApplicationID;
                this._TestAppointment.RetakeTestApplicationID= newApp.ApplicationID; ;
                ucRetakeTestInfo1.RefreshUI();
            }

            // Save test appointment
            if (_TestAppointment.Save())
            {
                MessageBox.Show("Data saved successfully");
            }
            else
            {
                MessageBox.Show("Failed to add a test appointment"+clsTestAppointment.errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void lbID_Click(object sender, EventArgs e)
        {

        }

        private void lbDClass_Click(object sender, EventArgs e)
        {

        }

        private void lbName_Click(object sender, EventArgs e)
        {

        }

        private void lbTrail_Click(object sender, EventArgs e)
        {

        }

        private void LbFees_Click(object sender, EventArgs e)
        {

        }

        private void lbLocked_Click(object sender, EventArgs e)
        {

        }

        private void ucRetakeTestInfo1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
