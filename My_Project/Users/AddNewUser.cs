using DVLD_Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_Project.Users
{
    public partial class AddNewUser : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;
        int _UserId;
        clsUser _User;

        public AddNewUser(int UserID)
        {
            InitializeComponent();
            this._UserId = UserID;

            if (UserID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;

            
        }
        private bool IsPasswordConfirmed {  get; set; }
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage2);


        }

        private void AddNewUser_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
            _LoadData();
            if (_Mode == enMode.Update)
            {
                ucFind1.comboBox1.SelectedIndex = 1;
                ucFind1.textBox1.Text = _User.PersonID.ToString();
                ucFind1.button1.PerformClick();
                ucFind1.textBox1.Enabled = false;
                ucFind1.button1.Enabled = false;
                ucFind1.button2.Enabled = false;
                ucFind1.comboBox1.Enabled = false;
               
            }

        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txUserName.Text))
            {
                e.Cancel = true;
                txUserName.Focus();
                errorProvider1.SetError(txUserName,"Username must have a value!");
            }
            else
            {
                if (clsUser.isUserExists(txUserName.Text))
                {
                    e.Cancel = true;
                    txUserName.Focus();
                    errorProvider1.SetError(txUserName, "This username is used !");

                }
                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(txUserName, "");
                }
            }
        }

        private void txPassword_Validating(object sender, CancelEventArgs e)
        {
           
        }

        private void txConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
                if (!txConfirmPassword.Text.Equals(txPassword.Text))
                {
                    e.Cancel = true;
                    txConfirmPassword.Focus();
                   errorProvider1.SetError(txConfirmPassword, "Passwords do not match");

                }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txConfirmPassword, "");
                IsPasswordConfirmed = true;
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (  !ucFind1.IsFound||
                string.IsNullOrWhiteSpace(txUserName.Text) ||
                string.IsNullOrWhiteSpace(txPassword.Text) ||
                string.IsNullOrWhiteSpace(txConfirmPassword.Text) )
            {
                MessageBox.Show("There is missing information ","Missing Information",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            

            //MessageBox.Show("test");
            if (_User == null) MessageBox.Show("It is null");
           // _User.UserName = txUserName.Text;

            string OrigianlPassword = txPassword.Text.Trim();

            



            _User.Password = OrigianlPassword;
            _User.UserName = txUserName.Text;   
            _User.IsActive = checkBox1.Checked;
            _User.PersonID = ucFind1.PersonID;
            

            if (_User.Save())
            {
                MessageBox.Show("Data Saved successfully.");
                MessageBox.Show(clsUser.messageOfhashedPassword);
            }
            else
                MessageBox.Show("Error : Data was not saved !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            _Mode = enMode.Update;
            lbTitle.Text = "Edit User ID = " + _User.UserID.ToString();
            lbUserID.Text = _User.UserID.ToString();
        }

        public void _LoadData()
        {
            if (_Mode == enMode.AddNew)
            {
                lbTitle.Text = "Add New Person";
                _User = new clsUser();
                return;
            }
            // Here the user will be found and its mode will be change to update.
             _User  = clsUser.Find(this._UserId);
            if (_User == null) {
                MessageBox.Show($"This form will be closed because No user with ID: {_UserId}");
                this.Close();
                return;

            }
            lbTitle.Text = "Edit Person ID = " + _UserId;
            lbUserID.Text = this._UserId.ToString();
            txUserName.Text = _User.UserName;
            txPassword.Text = _User.Password;
            txConfirmPassword.Text = _User.Password;


        }

       



        private void txUserName_TextChanged(object sender, EventArgs e)
        {
        }

        private void txPassword_TextChanged(object sender, EventArgs e)
        {
        }

        private void txConfirmPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage1);

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}
