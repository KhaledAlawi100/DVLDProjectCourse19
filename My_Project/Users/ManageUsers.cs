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

namespace My_Project.Users
{
    public partial class ManageUsers : Form
    {
        public ManageUsers()
        {
            InitializeComponent();
        }
        private DataTable table { get; set; }
        private void ManageUsers_Load(object sender, EventArgs e)
        {
            _RefreshUsersList();
            cbFilter.SelectedIndex = 0;
            cbDataFilter.SelectedIndex = 0;
            dgvListOfUsers.Columns["Full Name"].Width = 300; // by column name


        }
        private enum enFilterMode
        {
            None = 0, UserID = 1, UserName = 2, PersonID = 3, FullName = 4, IsActive = 5
        }

        private enFilterMode filter;

        private void _SetTheFilter()
        {
            if (cbFilter.SelectedIndex == 0) { 

                filter = enFilterMode.None;
            }
            else if(cbFilter.SelectedIndex == 1) 
                filter = enFilterMode.UserID;
            else if(cbFilter.SelectedIndex == 2)
                filter = enFilterMode.UserName;
            else if(cbFilter.SelectedIndex == 3)
                filter = enFilterMode.PersonID;
            else if(cbFilter.SelectedIndex == 4)
                filter = enFilterMode.FullName;
            else if(cbFilter.SelectedIndex == 5)
                filter = enFilterMode.IsActive; 

        }
        private void _FilterData()
        {
            DataView dv = table.DefaultView;

            switch (filter) { 

                case enFilterMode.None:
                    txFilter.Visible = false; 
                    break;
                 case enFilterMode.UserID:
                    if (int.TryParse(txFilter.Text, out int id))
                    {

                        dv.RowFilter = $"[User ID]={id}";
                        lbRecords.Text = dv.Count.ToString();
                        dgvListOfUsers.DataSource = dv;
                    }
                    else
                        MessageBox.Show("Shuld be a number !");
                    break;
                case enFilterMode.UserName:
                    dv.RowFilter = $"[UserName:]='{txFilter.Text}'";
                    lbRecords.Text= dv.Count.ToString();
                    dgvListOfUsers.DataSource = dv;
                    break;
                case enFilterMode.PersonID:
                    if (int.TryParse(txFilter.Text, out int PersonID))
                    {

                        dv.RowFilter = $"[Person ID]={PersonID}";
                        lbRecords.Text = dv.Count.ToString();
                        dgvListOfUsers.DataSource = dv;
                    }
                    else
                        MessageBox.Show("Shuld be a number !");
                    break;
                   case enFilterMode.FullName:
                    dv.RowFilter = $"[Full Name]='{txFilter.Text}'";
                    lbRecords.Text = dv.Count.ToString();
                    dgvListOfUsers.DataSource = dv;
                    break;


            }
        }

        
        private void _RefreshUsersList()
        {
            table =clsUser.GetAllUsers();
            _SetData(table);
            dgvListOfUsers.DataSource = table;
            lbRecords.Text= table.Rows.Count.ToString();

        }

        private void _SetData(DataTable dt)
        {
            dt.Columns.Add("User ID",typeof(int));
            dt.Columns.Add("Person ID",typeof(int));
            dt.Columns.Add("Full Name", typeof(string));
            dt.Columns.Add("UserName:",typeof(string));
            dt.Columns.Add("Is Active",typeof(bool));

            //int count = 0;
            foreach (DataRow row in dt.Rows)
            {
                row["User ID"] = row["UserID"];
                row["Person ID"] = row["PersonID"];

                row["UserName:"] = row["UserName"];

                row["Is Active"] = row["IsActive"];

                
                int ID = (int)row["Person ID"];

                clsPerson p1 = clsPerson.Find(ID);

                //if (p1 == null)
                //{
                //    MessageBox.Show($"Null Object in the row number{count+1} with user ID {ID}");
                //    return;
                //}
                    string fullName = p1.FirstName + " " + p1.SecondName + " " + p1.ThirdName + " " + p1.LastName + " ";
                    row["Full Name"] = fullName;
               // count++;


            }

            dt.Columns.Remove("UserID");
            dt.Columns.Remove("PersonID");
            dt.Columns.Remove("UserName");
            dt.Columns.Remove("Password");
            dt.Columns.Remove("IsActive");




        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            AddNewUser addNewUser = new AddNewUser(-1);
            addNewUser.ShowDialog();
            _RefreshUsersList();


        }

        private void txFilter_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txFilter.Text))
            {
                _RefreshUsersList();
                return;  
            }
            _SetTheFilter();
            _FilterData();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.SelectedIndex != 0 && cbFilter.SelectedIndex != 5)
            {
                txFilter.Visible = true;
                cbDataFilter.Visible = false;
            }
            else if (cbFilter.SelectedIndex == 5)
            {
                cbDataFilter.Visible = true;
                txFilter.Visible = false;
            }
            else
            {

            cbDataFilter.Visible = false;
                txFilter.Visible = false;
            }
        }

        private void cbDataFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDataFilter.SelectedIndex == 0)
                _RefreshUsersList();
            else if(cbDataFilter.SelectedIndex == 1)
            {
                DataView dv = table.DefaultView;
                dv.RowFilter = $"[Is Active]={true}";
                lbRecords.Text=dv.Count.ToString();
                dgvListOfUsers.DataSource = dv;
            }
            else
            {
                DataView dv = table.DefaultView;
                dv.RowFilter = $"[Is Active]={false}";
                lbRecords.Text = dv.Count.ToString();
                dgvListOfUsers.DataSource = dv;

            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewUser frm =  new AddNewUser((int)dgvListOfUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshUsersList();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewUser frm = new AddNewUser(-1);
            frm.ShowDialog();
            _RefreshUsersList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Are you sure you want to delete the user with User ID [{ (int)dgvListOfUsers.CurrentRow.Cells[0].Value }]","Confirm Delete",MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.OK)
            {
                int ExceptionNumber = -1;

                if (clsUser.DeleteUser((int)dgvListOfUsers.CurrentRow.Cells[0].Value,ref ExceptionNumber))
                {
                    MessageBox.Show("User Deleted Successfully.");
                    _RefreshUsersList();
                }
                else if (ExceptionNumber == 547)
                {
                    MessageBox.Show("User Was Not Delete Because It Has Data Linked To It ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                    MessageBox.Show("Person Was Not Delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
        }

        private void chngePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvListOfUsers.CurrentRow.Cells[0].Value;

            int PersonID = (int)dgvListOfUsers.CurrentRow.Cells[1].Value;
            
            ChangePassword frm = new ChangePassword(PersonID,UserID);
            frm.ShowDialog();
            _RefreshUsersList();

        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmUserDetails frm = new FrmUserDetails((int)dgvListOfUsers.CurrentRow.Cells[1].Value, (int)dgvListOfUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
    }
}
