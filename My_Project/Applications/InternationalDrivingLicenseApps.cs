using DVLD_Business_Layer;
using My_Project.Licenses;
using My_Project.Users;
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
    public partial class InternationalDrivingLicenseApps : Form
    {
        public InternationalDrivingLicenseApps()
        {
            InitializeComponent();
        }

        private void InternationalDrivingLicenseApps_Load(object sender, EventArgs e)
        {

         

            LoadData();

            




        }

        private void LoadData()
        {
            DataTable dt = clsInternationalLicense.GetAllInternationalLicense();

            SetData(dt);
         
            dataGridView1.DataSource = dt;

        }
        private void SetData(DataTable dt) {

            dt.Columns.Remove("CreatedByUserID");


        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            NewInterNationalLicense frm = new NewInterNationalLicense();
            frm.ShowDialog();
            LoadData();
            
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dataGridView1.CurrentRow.Cells[2].Value;

            clsDriver driver = clsDriver.Find(DriverID);

            int PersonID = driver.PersonID;

            clsPerson p1 = clsPerson.Find(PersonID);


                
            PersonDetails frm = new PersonDetails( p1.PersonID.ToString() , p1.NationalNumber , p1.FirstName , p1.SecondName,p1.ThirdName,
                p1.LastName,p1.DateOfBirth.ToString(),p1.Gender==0?"Male":"Female",p1.Address,p1.Phone,p1.Email,clsCountry.Find(p1.NationalityCountryID).countryName,p1.ImagePath);

            frm.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID  = (int)dataGridView1.CurrentRow.Cells[0].Value;

            InternationalDriverInfo frm = new InternationalDriverInfo( ID );
            frm.ShowDialog();


        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dataGridView1.CurrentRow.Cells[1].Value;


            LicensesHistory frm = new LicensesHistory(ID);
            frm.ShowDialog();
        }
    }
}
