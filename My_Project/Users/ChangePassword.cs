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
    public partial class ChangePassword : Form
    {
        public int PersonID { get; set; }
        public int UserID
        { get; set;
        }
        clsPerson _Person;
        clsUser _User;
        bool _IsPasswordConfirmed = false;

        public ChangePassword(int PersonID,int UserID)
        {
            InitializeComponent();
            this.PersonID = PersonID;
            this.UserID = UserID;
        }
        private void ChangePassword_Load(object sender, EventArgs e)
        {
            ucLoginInfo1.ucPersonInformation1.ID= this.PersonID.ToString();
             _Person = clsPerson.Find(this.PersonID);

            ucLoginInfo1.ucPersonInformation1.NationalNumber = _Person.NationalNumber;
            ucLoginInfo1.ucPersonInformation1.FullName = _Person.FirstName+" "+_Person.SecondName+" "+_Person.ThirdName+" "+_Person.LastName;
            if (_Person.Gender == 0)
                ucLoginInfo1.ucPersonInformation1.Gender = "Male";
            else
                ucLoginInfo1.ucPersonInformation1.Gender = "Female";
            ucLoginInfo1.ucPersonInformation1.Phone = _Person.Phone;
            ucLoginInfo1.ucPersonInformation1.Email = _Person.Email;
            ucLoginInfo1.ucPersonInformation1.Address = _Person.Address;
            ucLoginInfo1.ucPersonInformation1.DateOfBirth = _Person.DateOfBirth.ToString("yyyy-MM-dd");
            clsCountry c1 = clsCountry.Find(_Person.NationalityCountryID);
            ucLoginInfo1.ucPersonInformation1.Country = c1.countryName;
            ucLoginInfo1.ucPersonInformation1.ImagePath = _Person.ImagePath;

            ucLoginInfo1.ucPersonInformation1.RefreshUI();

            ucLoginInfo1.UserID = this.UserID;
            _User = clsUser.Find(this.UserID);
            ucLoginInfo1.UserName = _User.UserName;
            ucLoginInfo1.IsActive = _User.IsActive;
            
            ucLoginInfo1.RefreshUI();

        }

        private void ucLoginInfo1_Load(object sender, EventArgs e)
        {

        }

        private void txCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (!_User.Password.Equals(txCurrentPassword.Text))
            {
                e.Cancel = true;
                txCurrentPassword.Focus();
                errorProvider1.SetError(txCurrentPassword,"The Current Password Is Wrong");
            }
            else
            {
                e.Cancel= false;
                errorProvider1.SetError(txCurrentPassword,""); 
            }   
        }

        private void txConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            if (txNewPassword.Text.Equals(txConfirmPassword.Text)) {

                _IsPasswordConfirmed = true; 
            }
            else
               _IsPasswordConfirmed= false;
        }

        private void txConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (!_IsPasswordConfirmed) { 
            
                e.Cancel = true;
                txConfirmPassword.Focus();
                errorProvider1.SetError(txConfirmPassword, "Paswords do not match");
            }
            else
            {
                e.Cancel=false;
                errorProvider1.SetError(txConfirmPassword, "");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _User.Password = txNewPassword.Text;

            if (_User.Save() && _IsPasswordConfirmed)
            {
                MessageBox.Show("Password Changed Successfully");
            }
            else
            {
                MessageBox.Show("Error , Password Failed to be changed ","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
