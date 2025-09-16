using DVLD_Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace My_Project.Users
{
    public partial class Manage_People : Form
    {
        private DataTable listOfPeople;

        public Manage_People()
        {
            InitializeComponent();

            listOfPeople = clsPerson.GetAllPeople();
        }
        private void Manage_People_Load(object sender, EventArgs e)
        {
            cbFilter.SelectedIndex = 0;
            txFilter.Visible = false;
            _RefreshPeopleList();
            lbRecords.Text = listOfPeople.Rows.Count.ToString();
        }

        private void _RefreshPeopleList()
        {
            listOfPeople = clsPerson.GetAllPeople();
            _SetTheData(listOfPeople);
            dataGridView1.DataSource = listOfPeople;
            lbRecords.Text = listOfPeople.Rows.Count.ToString();
        }

        private void _SetTheData(DataTable dt)
        {
            listOfPeople.Columns.Add("Person ID", typeof(int));
            listOfPeople.Columns.Add("National NO.", typeof(string));
            listOfPeople.Columns.Add("First Name",typeof(string));
            listOfPeople.Columns.Add("Second Name",typeof(string));
            listOfPeople.Columns.Add("Third Name",typeof(string));
            listOfPeople.Columns.Add("Last Name",typeof(string));
            listOfPeople.Columns.Add("Gender", typeof(string));
            listOfPeople.Columns.Add("Date Of Birth",typeof(DateTime));
            listOfPeople.Columns.Add("Nationality",typeof(string));
            listOfPeople.Columns.Add("Phone:",typeof(string));
            listOfPeople.Columns.Add("Email:", typeof(string));

            // 2. Populate the new column based on old values
            foreach (DataRow row in listOfPeople.Rows)
            {
                if (row["Gendor"] != DBNull.Value)
                {
                    byte gender = Convert.ToByte(row["Gendor"]);
                    row["Gender"] = (gender == 0) ? "Male" : "Female";  
                }

                int CountryID = (int)row["NationalityCountryID"];
                row["Nationality"] = clsCountry.Find(CountryID).countryName;
                row["Person ID"] = row["PersonID"];
                row["National NO."] = row["NationalNo"];
                row["First Name"] = row["FirstName"];
                row["Second Name"] = row["SecondName"];
                row["Third Name"] = row["ThirdName"];
                row["Last Name"] = row["LastName"];
                row["Date Of Birth"] = row["DateOfBirth"];
                row["Email:"] = row["Email"];
                row["Phone:"] = row["Phone"];    
            }

            // 3. Remove the original column
            listOfPeople.Columns.Remove("Gendor");
            listOfPeople.Columns.Remove("PersonID");
            listOfPeople.Columns.Remove("NationalNo");
            listOfPeople.Columns.Remove("ImagePath");
            listOfPeople.Columns.Remove("FirstName");
            listOfPeople.Columns.Remove("SecondName");
            listOfPeople.Columns.Remove("ThirdName");
            listOfPeople.Columns.Remove("LastName");
            listOfPeople.Columns.Remove("Address");
            listOfPeople.Columns.Remove("DateOfBirth");
            listOfPeople.Columns.Remove("NationalityCountryID");
            listOfPeople.Columns.Remove("Email");
            listOfPeople.Columns.Remove("PHone");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form frm = new AddEditPerson(-1);
            frm.ShowDialog();
            _RefreshPeopleList();

        }

        private enum enFilterMode { None =0 ,PersonID = 1, NationalNo = 2, FirstName = 3 , SecondName =4 , ThirdName=5 , LastName=6 , 
        Nationality=7 , Gender = 8 , Phone = 9 , Email = 10}

        private enFilterMode filter;
        private void _FilterData()
        {
            DataView dv = listOfPeople.DefaultView;

            switch (filter)
            {
                case enFilterMode.None:
                    txFilter.Visible = false ;
                    break;
                    case enFilterMode.PersonID:
                    if (!decimal.TryParse(txFilter.Text, out decimal _) && txFilter.Text != string.Empty)
                    {
                        MessageBox.Show("should be a number");
                        return;
                    }
                    int ID = int.Parse(txFilter.Text);
                    dv.RowFilter = $"[Person ID] = {ID}";

                    lbRecords.Text = dv.Count.ToString();
                    dataGridView1.DataSource = dv;
                    break;
                    case enFilterMode.NationalNo:
                    dv.RowFilter = $"[National NO.]='{txFilter.Text}'";
                    lbRecords.Text = dv.Count.ToString();
                    break;
                    case enFilterMode.FirstName:
                    dv.RowFilter = $"[First Name]='{txFilter.Text}'";
                    lbRecords.Text= dv.Count.ToString();
                    break ;
                    case enFilterMode.SecondName:
                    dv.RowFilter = $"[Second Name]='{txFilter.Text}'";
                    lbRecords.Text = dv.Count.ToString();
                    break;
                    case enFilterMode.ThirdName:
                    dv.RowFilter = $"[Third Name]='{txFilter.Text}'";
                    lbRecords.Text = dv.Count.ToString();
                    break ; 
                    case enFilterMode.LastName:
                    dv.RowFilter = $"[Last Name]='{txFilter.Text}'";
                    lbRecords.Text = dv.Count.ToString();
                    break ;
                    // #################################################
                    case enFilterMode.Nationality:
                    dv.RowFilter = $"[Nationality]='{txFilter.Text}'";
                    lbRecords.Text = dv.Count.ToString();
                    break ;
                    case enFilterMode.Gender:
                    dv.RowFilter = $"[Gender]='{txFilter.Text}'";
                    lbRecords .Text = dv.Count.ToString();
                    break ;
                    case enFilterMode.Phone:
                    dv.RowFilter = $"[Phone:]='{txFilter.Text}'";
                    lbRecords.Text = dv.Count.ToString();
                    break ;
                    case enFilterMode.Email:
                    dv.RowFilter = $"[Email:]='{txFilter.Text}'";
                    lbRecords.Text = dv.Count.ToString();
                    break ;     

            }
   
        }

        private void txFilter_TextChanged(object sender, EventArgs e)
        {
            if (txFilter.Text == string.Empty) {
                _RefreshPeopleList();
                return;
            }
            _SetFilter();
            _FilterData();

        }
        private void _SetFilter()
        {
            if(cbFilter.SelectedIndex == 0)
                filter = enFilterMode.None;
            else if(cbFilter.SelectedIndex == 1)
                filter = enFilterMode.PersonID;
            else if(cbFilter.SelectedIndex == 2)
                filter = enFilterMode.NationalNo;
            else if(cbFilter.SelectedIndex == 3)
                filter = enFilterMode.FirstName;
            else if(cbFilter.SelectedIndex == 4)
                filter = enFilterMode.SecondName;
            else if(cbFilter.SelectedIndex == 5)
                filter = enFilterMode.ThirdName;
            else if(cbFilter.SelectedIndex == 6)
                filter = enFilterMode.LastName;
            else if(cbFilter.SelectedIndex == 7)
                filter =enFilterMode.Nationality;
            else if(cbFilter.SelectedIndex == 8)
                filter = enFilterMode.Gender;
            else if (cbFilter.SelectedIndex == 9)
                filter = enFilterMode.Phone;
            else if(cbFilter.SelectedIndex == 10)
                filter = enFilterMode.Email;


        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbFilter.SelectedIndex != 0)
                txFilter.Visible = true;
            else txFilter.Visible = false;
        }

        private void tsmAddNewPerson_Click(object sender, EventArgs e)
        {
            Form frm = new AddEditPerson(-1);
            frm.ShowDialog();
            _RefreshPeopleList();

        }

        private void tsmEdit_Click(object sender, EventArgs e)
        {
            AddEditPerson frm = new AddEditPerson((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshPeopleList();   
        }

        private void tsmDelete_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to delete the person with ID :  [ " + dataGridView1.CurrentRow.Cells[0].Value+"]","Confirm Delete",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning) == DialogResult.OK)
            {
                int exceptionNumber = -1;
                if (clsPerson.DeletePerson((int)dataGridView1.CurrentRow.Cells[0].Value,ref exceptionNumber))
                {
                    MessageBox.Show("Person Deleted Successfully.");
                    _RefreshPeopleList();
                }
                else if(exceptionNumber==547)
                    MessageBox.Show("Person Was Not Delete Because It Has Data Linked To It ","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                else
                    MessageBox.Show("Person Was Not Delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
        }

        private void tsmShowDetails_Click(object sender, EventArgs e)
        {
            clsPerson p1 = clsPerson.Find((int)dataGridView1.CurrentRow.Cells[0].Value);

            if (p1 == null) {

                MessageBox.Show("Error!");
                return;
            }
            string gender = "";
            if (p1.Gender == 0)
            {
                gender = "Male";

            }
            else
                gender = "Female";
            string dobString = p1.DateOfBirth.ToString("yyyy-MM-dd");

            PersonDetails frm = new PersonDetails(p1.PersonID.ToString(), p1.NationalNumber,
                p1.FirstName, p1.SecondName, p1.ThirdName, p1.LastName, dobString,
                gender, p1.Address, p1.Phone,p1.Email, (string)dataGridView1.CurrentRow.Cells[8].Value, p1.ImagePath);
            frm.ShowDialog();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmSendEmail_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Functionality Is Not Implementd Yet ! ","Warning",MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void tsmPhoneCall_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Functionality Is Not Implementd Yet ! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
