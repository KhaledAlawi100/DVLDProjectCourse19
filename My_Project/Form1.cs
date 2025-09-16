using My_Project.Applications;
using My_Project.Drivers;
using My_Project.Licenses;
using My_Project.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new Manage_People();
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form frm = new TestForm();
            frm.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void accountSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void signOutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginScreen loginScreen = new LoginScreen();
            loginScreen.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageUsers manageUsers = new ManageUsers();
            manageUsers.ShowDialog();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmUserDetails frm = new FrmUserDetails(clsGlobal.PersonID, clsGlobal.UserID);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void peopleToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Manage_People frm = new Manage_People();
            frm.ShowDialog();
        }

        private void usersToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ManageUsers frm = new ManageUsers();
            frm.ShowDialog();
        }

        private void cuurentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmUserDetails frm = new FrmUserDetails(clsGlobal.PersonID,clsGlobal.UserID);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            int UserID = clsGlobal.UserID;

            int PersonID = clsGlobal.PersonID;

            ChangePassword frm = new ChangePassword(PersonID, UserID);
            frm.ShowDialog();
            
        }

        private void signOutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            LoginScreen frm = new LoginScreen();
            frm.ShowDialog();
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageApplicationTypes frm = new ManageApplicationTypes();
            frm.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LIstTestTypes frm = new LIstTestTypes();
            frm.ShowDialog();
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewLocalDrivingLicense frm = new NewLocalDrivingLicense(-1);
            frm.ShowDialog();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LocalDrivingLicenseApps frm = new LocalDrivingLicenseApps();
            frm.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListDrivers frm = new ListDrivers();
            frm.ShowDialog();

        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewInterNationalLicense frm = new NewInterNationalLicense();
            frm.ShowDialog();

        }

        private void internationalLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InternationalDrivingLicenseApps frm = new InternationalDrivingLicenseApps();
            frm.ShowDialog();

        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RenewLocalDrivingLicense frm = new RenewLocalDrivingLicense();
            frm.ShowDialog();
        }

        private void replacementForLostOrDamagedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReplacementforDamagedOrLostLicenses frm = new ReplacementforDamagedOrLostLicenses();
            frm.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DetainLicense frm = new DetainLicense(-1);
            frm.ShowDialog();
        }

        private void releaseDetainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReleaseDeatainedLicense frm = new ReleaseDeatainedLicense(-1);
            frm.ShowDialog();
        }

        private void manageDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListDetainedLicenses frm = new ListDetainedLicenses();
            frm.ShowDialog();
        }
    }
}
