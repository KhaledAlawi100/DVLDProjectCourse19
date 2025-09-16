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
    public partial class TakeWrittenTest : Form
    {
        int LDLID;
        int TestAppointment;
        public TakeWrittenTest(int LDLID, int TestAppointment)
        {
            InitializeComponent();

            this.LDLID = LDLID;
            this.TestAppointment = TestAppointment;
        }

        private void TakeWrittenTest_Load(object sender, EventArgs e)
        {
            _LoadData();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        clsTestAppointment appoint1;
        private void _LoadData()
        {
            lbID.Text = this.LDLID.ToString();

            clsLDLApp LDLapp1 = clsLDLApp.Find(LDLID);

            clsLicensClass classLi1 = clsLicensClass.Find(LDLapp1.LicenseClassID);

            lbDClass.Text = classLi1.ClassName;

            clsApplication app1 = clsApplication.Find(LDLapp1.ApplicationID);

            clsPerson p1 = clsPerson.Find(app1.ApplicationPersonID);

            lbName.Text = p1.FirstName + " " + p1.SecondName + " " + p1.ThirdName + " " + p1.LastName;

            DataTable dt = clsTestAppointment.GetAlltTestAppointments(this.LDLID, 2);

            appoint1 = clsTestAppointment.Find(this.TestAppointment);

            if (dt != null)
                lbTrail.Text = dt.Rows.Count.ToString();
            else
            {
                int x = 0;
                lbTrail.Text = x.ToString();
            }

            lbDate.Text = appoint1.AppointmentDate.ToString();

            LbFees.Text = clsTestType.Find(2).TestTypeFees.ToString();



        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            clsTest newTest = new clsTest();

            newTest.Notes = txNotes.Text;

            if (rbPass.Checked)
                newTest.TestResult = true;
            else
                newTest.TestResult = false;

            newTest.TestAppointmentID = this.TestAppointment;

            newTest.CreatedByUserID = clsGlobal.UserID;

            if (newTest.AddNewTest())
            {
                this.appoint1.IsLocked = true;

                if (!this.appoint1.Save())
                {
                    MessageBox.Show("Failed to update the test Appointment", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {

                    MessageBox.Show("Data Saved Successfully !");

                }
            }
            else
            {
                MessageBox.Show("Failed to Add the new test ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            this.Close();
        }
    }

}
