using DVLD_Business_Layer;
using My_Project.Licenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_Project.Applications
{
    public partial class LocalDrivingLicenseApps : Form
    {
        
        public LocalDrivingLicenseApps()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            NewLocalDrivingLicense frm = new NewLocalDrivingLicense(-1);
            frm.ShowDialog();
            _RefreshLDLList();
        }

        private void LocalDrivingLicenseApps_Load(object sender, EventArgs e)
        {
            cbFilter.SelectedIndex = 0;

            _RefreshLDLList();
        }

        DataTable List;
        private void _RefreshLDLList()
        {
            DataTable ListOfLDL= clsLDLApp.GetAllLDLApps();

            _SetTheData(ListOfLDL);

            dataGridView1.DataSource = ListOfLDL;
            List = ListOfLDL;   

        }
        DataTable Appoints1;


        private void _SetTheData(DataTable dt)
        {
            dt.Columns["LocalDrivingLicenseApplicationID"].ColumnName = "LDL APP ID";

            dt.Columns.Add("Driving Class",typeof(string));

            dt.Columns.Add("National NO",typeof(string));

            dt.Columns.Add("Full Name", typeof(string));

            dt.Columns.Add("Application Date",typeof(DateTime));

            dt.Columns.Add("Passed Tests",typeof(int));

            dt.Columns.Add("Status", typeof(string));

            foreach (DataRow row in dt.Rows)
            {
                int ClassID = (int)row["LicenseClassID"];
                row["Driving Class"]= clsLicensClass.Find(ClassID).ClassName;
                int AppID = (int)row["ApplicationID"];
                clsApplication app1 = clsApplication.Find(AppID);
                int PersonID = app1.ApplicationPersonID;
                clsPerson p1 = clsPerson.Find(PersonID);
                row["National NO"]= p1.NationalNumber;
                row["Full Name"] = p1.FirstName + " " + p1.SecondName + " " + p1.ThirdName + " " + p1.LastName;

                row["Application Date"]= app1.ApplicationDate;

                if (app1.ApplicationStatus == 1)
                {
                    row["Status"] = "New";
                }
                else if (app1.ApplicationStatus == 2)
                {

                    row["Status"] = "Cancelled";

                }
                else
                    row["Status"] = "Completed";

                int LDLID = (int)row["LDL APP ID"];

                 Appoints1 = clsTestAppointment.GetAlltTestAppointments(LDLID);

                int PassedTest = 0;

                foreach (DataRow dr in Appoints1.Rows) {

                    int AppointID = (int)dr["TestAppointmentID"];

                    clsTest test1 = clsTest.Find(AppointID);

                    if (test1 == null) {

                        row["Passed Tests"] = 0;
                    
                    }
                    else if (test1.TestResult)
                    {
                        PassedTest++;

                    }
                
                }

                row["Passed Tests"] = PassedTest;
            }

            


            dt.Columns.Remove("ApplicationID");
            dt.Columns.Remove("LicenseClassID");

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void sechduleTeseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            MessageBox.Show(ID.ToString());

            VisionTestAppointments frm = new VisionTestAppointments(ID);
            frm.ShowDialog();
            _RefreshLDLList();
            
        
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
           


        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int PassedTests = (int)dataGridView1.CurrentRow.Cells[5].Value;

            if (PassedTests == 0)
            {
                scheduleVisionTestToolStripMenuItem.Enabled = true;
                scheduleWrittenTestToolStripMenuItem.Enabled = false;
                schedultStreetTestToolStripMenuItem.Enabled = false;


            }
            else if (PassedTests == 1)
            {
                scheduleVisionTestToolStripMenuItem.Enabled = false;
                scheduleWrittenTestToolStripMenuItem.Enabled = true;
                schedultStreetTestToolStripMenuItem.Enabled = false;
            }
            else if (PassedTests == 2)
            {
                scheduleVisionTestToolStripMenuItem.Enabled = false;
                scheduleWrittenTestToolStripMenuItem.Enabled = false;
                schedultStreetTestToolStripMenuItem.Enabled = true;

            }
            else if (PassedTests == 3) {

                scheduleVisionTestToolStripMenuItem.Enabled = false;
                scheduleWrittenTestToolStripMenuItem.Enabled = false;
                schedultStreetTestToolStripMenuItem.Enabled = false;



            }



            string status = (string)dataGridView1.CurrentRow.Cells[6].Value;

            if ( (int)dataGridView1.CurrentRow.Cells[5].Value == 3 && status.Equals("New")  )
            {
                issueDrivingLicense1stToolStripMenuItem.Enabled=true;
            }
            else
                issueDrivingLicense1stToolStripMenuItem.Enabled=false;

            if (status.Equals("Completed"))
            {
                showLicenseToolStripMenuItem.Enabled = true;
                editApplicationToolStripMenuItem.Enabled = false;
                deleteApplicationToolStripMenuItem.Enabled = false;
                cancelApplicationToolStripMenuItem.Enabled = false;
            }
            else
            {
                showLicenseToolStripMenuItem.Enabled = false;
            }

            if((int)dataGridView1.CurrentRow.Cells[5].Value>0 || Appoints1.Rows.Count>0 )
                deleteApplicationToolStripMenuItem.Enabled = false;
            else
                deleteApplicationToolStripMenuItem.Enabled = true;







        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            MessageBox.Show(ID.ToString());

            WrittenTestAppointments frm = new WrittenTestAppointments(ID);
            frm.ShowDialog();
            _RefreshLDLList();

        }

        private void schedultStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            MessageBox.Show(ID.ToString());

            StreetTestAppointments frm = new StreetTestAppointments(ID);
            frm.ShowDialog();
            _RefreshLDLList();

        }

        private void issueDrivingLicense1stToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            IssueDrivingLicenseFirst frm = new IssueDrivingLicenseFirst(ID);
            frm.ShowDialog();
            _RefreshLDLList();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLAppID = (int)dataGridView1.CurrentRow.Cells[0].Value;

            clsLDLApp ldlApp = clsLDLApp.Find(LDLAppID);

            LicenseInfo frm = new LicenseInfo(ldlApp.ApplicationID);
            frm.ShowDialog();
            _RefreshLDLList();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            clsLDLApp LDLApp = clsLDLApp.Find(ID);

            LicensesHistory frm = new LicensesHistory(LDLApp.ApplicationID);
            frm.ShowDialog();

            _RefreshLDLList();
        }
        private enum enFilterMode { None=0 , LDLID=1,NationalNO=2,FullName=3,Sataus=4 }

        private enFilterMode Filter ; 


        private void SetFilter()
        {
            if(cbFilter.SelectedIndex == 0)
                Filter = enFilterMode.None;
            else if(cbFilter.SelectedIndex == 1)
                Filter = enFilterMode.LDLID;
            else if (cbFilter.SelectedIndex == 2)
                Filter = enFilterMode.NationalNO;
            else if (cbFilter.SelectedIndex == 3)
                Filter = enFilterMode.FullName;
            else if (cbFilter.SelectedIndex == 4)
                Filter = enFilterMode.Sataus;
        }

        private void FilterData()
        {
            DataView dv = this.List.DefaultView;

            switch (Filter)
            {

                case enFilterMode.None:
                    txFilter.Visible = false;
                    break;
                case enFilterMode.LDLID:

                    if (int.TryParse(txFilter.Text, out int ID))
                    {
                        dv.RowFilter = $"[LDL APP ID] = {ID}";
                    }
                    else
                        MessageBox.Show("ID should be a number");
                    break;
                case enFilterMode.NationalNO:
                    dv.RowFilter = $"[National NO] ='{txFilter.Text}'";
                    break;
                    case enFilterMode.FullName:
                    dv.RowFilter = $"[Full Name]='{txFilter.Text}'";
                    break;
                    case enFilterMode.Sataus:
                    dv.RowFilter = $"[Status]='{txFilter.Text}'";
                    break;


            }
        }
        private void CbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.SelectedIndex != 0)
                txFilter.Visible = true;
            else txFilter.Visible = false;
        }

        private void txFilter_TextChanged(object sender, EventArgs e)
        {
            if(txFilter.Text == string.Empty)
            {
                _RefreshLDLList();
                return;

            }

            SetFilter();
            FilterData();



        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dataGridView1.CurrentRow.Cells[0].Value;

            clsLDLApp LDLApp1= clsLDLApp.Find(ID);

            clsApplication app1 = clsApplication.Find(LDLApp1.ApplicationID);

            if (MessageBox.Show("Are you sure you want to cancel this application?", "Confirm", MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.Yes) {

                app1.ApplicationStatus = 2;
                if (app1.Save())
                {
                    MessageBox.Show("This Application is Cancelled");
                    _RefreshLDLList();
                }
                else
                    MessageBox.Show("Failed to cancel this application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dataGridView1.CurrentRow.Cells[0].Value;

            if (MessageBox.Show("Are you sure you want to delete this application ? ", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.Yes) {


                if (clsLDLApp.Delete(ID)) {

                    MessageBox.Show("The application was deleted successfully");
                    _RefreshLDLList();
                
                }
                else
                    MessageBox.Show("Failed to Delete this application"+clsLDLApp.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
    }
}
