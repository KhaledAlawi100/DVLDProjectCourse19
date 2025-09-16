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
    public partial class WrittenTestAppointments : Form
    {
        private int LDLID;
        private int AppID;
        public WrittenTestAppointments(int LDLID)
        {
            InitializeComponent();
            this.LDLID = LDLID;
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dataGridView1.CurrentRow.Cells[0].Value;

            clsTestAppointment appoint1 = clsTestAppointment.Find(ID);

            ScheduleWrittenTest frm2 = new ScheduleWrittenTest(ID, this.LDLID, appoint1.RetakeTestApplicationID != -1);

            frm2.ShowDialog();

            _RefreshTestAppointment();


        }

        private void WrittenTestAppointments_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            clsLDLApp LDLapp1 = clsLDLApp.Find(LDLID);
            this.AppID = LDLapp1.ApplicationID;

            if (LDLapp1 == null)
            {

                MessageBox.Show("Not Found");

            }

            ucDLAppInfo1.LDLAppID = LDLID;

            clsLicensClass clasLic = clsLicensClass.Find(LDLapp1.LicenseClassID);

            ucDLAppInfo1.ClassName = clasLic.ClassName;

            ucDLAppInfo1.RefreshUI();

            ucAppBasicInfo1.AppID = this.AppID;

            clsApplication app1 = clsApplication.Find(this.AppID);

            //MessageBox.Show(app1.PaidFees.ToString());

            ucAppBasicInfo1.Status = app1.ApplicationStatus;



            ucAppBasicInfo1.fees = app1.PaidFees;


            clsApplicationType appType = clsApplicationType.Find(app1.ApplicationTypeID);

            ucAppBasicInfo1.type = appType.Title;

            clsPerson p1 = clsPerson.Find(app1.ApplicationPersonID);

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




            _RefreshTestAppointment();



        }
        DataTable dt;

        private void _RefreshTestAppointment()
        {
            dt = clsTestAppointment.GetAlltTestAppointments(this.LDLID, 2);


            if (dt.Rows.Count == 0) return;



            dt.Columns.Remove("TestTypeID");
            dt.Columns.Remove("LocalDrivingLicenseApplicationID");
            dt.Columns.Remove("CreatedByUserID");
            dt.Columns.Remove("RetakeTestApplicationID");

            dataGridView1.DataSource = dt;

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("New aapoint");
                ScheduleWrittenTest frm = new ScheduleWrittenTest(-1, this.LDLID, false);
                frm.ShowDialog();
                _RefreshTestAppointment();
                return;
            }


            int AppointmentID = (int)dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells[0].Value;
            clsTest test1 = clsTest.Find(AppointmentID);

            clsTestAppointment apoint1 = clsTestAppointment.Find(AppointmentID);

            if (!apoint1.IsLocked)
            {

                MessageBox.Show("This Person Already Have an appointment! ");
                return;

            }



            if (test1.TestResult)
            {
                MessageBox.Show("This Person Already Passed The test ");
                return;
            }
            else
            {
                ScheduleWrittenTest frm = new ScheduleWrittenTest(-1, this.LDLID, true);
                 frm.ShowDialog();
                _RefreshTestAppointment();
                return;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dataGridView1.CurrentRow.Cells[0].Value;

            clsTestAppointment appoint1 = clsTestAppointment.Find(ID);

            if (appoint1.IsLocked)
            {
                MessageBox.Show("This Appointment was finished !");
                return;
            }

            TakeWrittenTest frm3 = new TakeWrittenTest(this.LDLID, ID);

            frm3.ShowDialog();

            _RefreshTestAppointment();

        }
    }
}
