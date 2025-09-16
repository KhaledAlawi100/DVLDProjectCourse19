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

namespace My_Project.Drivers
{
    public partial class ListDrivers : Form
    {
        public ListDrivers()
        {
            InitializeComponent();
        }

        private void ListDrivers_Load(object sender, EventArgs e)
        {
            DataTable dt = clsDriver.GetAllDrivers();
            LoadData(dt);
            dataGridView1.DataSource = dt;
        }
        private void LoadData(DataTable dt)
        {

            dt.Columns.Add("National No",typeof(string));

            dt.Columns.Add("Full Name",typeof(string));

            dt.Columns["CreatedDate"].ColumnName = "Date";

            dt.Columns.Add("Active Licenses", typeof(int));

            foreach (DataRow row in dt.Rows) {

                int PersonID = (int)row["PersonID"];
                clsPerson p1 = clsPerson.Find(PersonID);

                row["National No"] = p1.NationalNumber;

                row["Full Name"]=p1.FirstName+" "+p1.SecondName+" "+p1.ThirdName+" "+p1.LastName;

                int DriverID = (int)row["DriverID"];

                DataTable LicensesList = clsLicense.GetAllLicensesByDriverID(DriverID);

                int ActiveLicenses = 0;
                foreach (DataRow dataRow in LicensesList.Rows) {

                    if ((bool)dataRow["IsActive"] == true)
                    {
                        ActiveLicenses++;   

                    }

                
                }
                row["Active Licenses"]= ActiveLicenses;


            
            }



        }
    }
}
