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
    public partial class NewLocalDrivingLicense : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        int _LDLID;

        clsLDLApp _LDLApp;


  
        public NewLocalDrivingLicense(int LDLID)
        {
            InitializeComponent();

            this._LDLID = LDLID;
            if(LDLID==-1)
                _Mode = enMode.AddNew;
            else _Mode = enMode.Update;


        }
        string ApplicationDate;

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void NewLocalDrivingLicense_Load(object sender, EventArgs e)
        {
            ApplicationDate = DateTime.Now.ToString("yyyy-MM-dd");

            lbDate.Text = ApplicationDate;

           lbFees.Text= clsApplicationType.Find(1).Fees.ToString();

            lbUsername.Text = clsUser.Find(clsGlobal.UserID).UserName;


            FillClassesInComboBox();
            cbLicenseClasses.SelectedIndex = 2;
            LoadData();

        }

        private void LoadData()
        {
            if(_Mode == enMode.AddNew)
            {
                _LDLApp = new clsLDLApp();

                return;

            }






        }

        private void FillClassesInComboBox()
        {
            DataTable dt = clsLicensClass.GetAllClassLicenses();

            foreach (DataRow dr in dt.Rows) {

                cbLicenseClasses.Items.Add(dr["ClassName"]);

            
            }
            
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void lbDate_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            int personID = ucFind1.PersonID;

            if (clsLDLApp.haveSameApp(personID, cbLicenseClasses.SelectedIndex + 1))
            {
                MessageBox.Show("This person have previous new app with same class", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LDLApp.ApplicationPersonID = personID;
            _LDLApp.ApplicationDate = DateTime.Now;
            _LDLApp.ApplicationTypeID = 1;
            _LDLApp.ApplicationStatus = 1;
            _LDLApp.LastStatusDate= DateTime.Now;
            _LDLApp.PaidFees = clsApplicationType.Find(1).Fees;
            _LDLApp.CreatedByUserID= clsGlobal.UserID;
            _LDLApp.LicenseClassID = cbLicenseClasses.SelectedIndex+1;

            if (_LDLApp.Save())
            {
                MessageBox.Show("Data save successfully!");

            }
            else
                MessageBox.Show("Error : Data was not saved !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            _Mode = enMode.Update;
            lbID.Text = _LDLApp.LocalDrivingLicenseApplicationID.ToString();

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            if(ucFind1.IsFound)btnSave.Enabled = true;
        }

        private void cbLicenseClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ucFind1.IsFound) btnSave.Enabled = true;


        }

        private void tabPage2_MouseHover(object sender, EventArgs e)
        {
            if (ucFind1.IsFound) btnSave.Enabled = true;


        }
    }
}
