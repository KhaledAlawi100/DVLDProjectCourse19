using DVLD_Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_Project.Users
{
    public partial class AddEditPerson : Form
    {

        public delegate void DataBackEventHandler(object sender,int PersonID);

        public event DataBackEventHandler DataBack;
        public bool IsDataSaved;

        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;
        int _PersonId;
        clsPerson _Person;

        bool IsPicSet=false;
        public AddEditPerson(int PersonID)
        {
            InitializeComponent();

            this._PersonId = PersonID;

            if (PersonID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;
        }

        private void _FillCountriesInComboBox()
        {
            DataTable dtCountries = clsCountry.GetAllCountries();

            foreach(DataRow row in dtCountries.Rows)
            {
                cbCountry.Items.Add(row["CountryName"]);
            }
            try
            {
                cbCountry.SelectedIndex = (clsCountry.Find("Saudi Arabia").id) - 2;
            }
            catch (Exception ex) { 
            
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void AddEditPerson_Load(object sender, EventArgs e)
        {
            dateTimePicker1.MaxDate = DateTime.Today.AddYears(-18);

            rbMale.Checked = true;

            if (pictureBox1.Image == null)
            {
                pictureBox1.Image = Properties.Resources.Male_512;
                

            }
            if(_Mode==enMode.Update)
                llbRemove.Visible=true;
           
            _LodaData();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void _LodaData()
        {
            _FillCountriesInComboBox();

            if (_Mode == enMode.AddNew)
            {
                lbTitle.Text = "Add New Person";
                _Person = new clsPerson();
                return;
                
            }
            // Here the person will be found and its mode will be change to update.
            _Person = clsPerson.Find(this._PersonId);
            if (_Person == null) {

                MessageBox.Show($"This form will be closed because No person with ID: {_PersonId}");
                this.Close();
                return;
            
            }

            lbTitle.Text = "Edit Person ID = "+_PersonId;
            lbPersonID.Text = _PersonId.ToString();
            
            txNationalNO.Text = _Person.NationalNumber;

            txFirstName.Text = _Person.FirstName;
            txSecondName.Text = _Person.SecondName;
            txThirdName.Text = _Person.ThirdName;
            txLastName.Text = _Person.LastName;

            dateTimePicker1.Value= _Person.DateOfBirth;

            if (_Person.Gender == 0) { 
                rbMale.Checked=true;
            }
            else
            {
                rbFemale.Checked=true;
            }
            txPhone.Text = _Person.Phone;
            txEmail.Text = _Person.Email;
            txAddress.Text = _Person.Address;

            cbCountry.SelectedIndex = cbCountry.FindString(clsCountry.Find(_Person.NationalityCountryID).countryName);

            if (_Person.ImagePath != "") { 
                pictureBox1.Load(_Person.ImagePath);
                IsPicSet = true;
            }

        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (IsPicSet) return;
            pictureBox1.Image = Properties.Resources.Male_512;
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if(IsPicSet) return;
            pictureBox1.Image= Properties.Resources.Female_512;



        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = EnableSaveButton();
 
        }

        private void txFirstName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txFirstName.Text))
            {
                e.Cancel = true;
                txFirstName.Focus();
                errorProvider1.SetError(txFirstName, "First name must have a value !");
            }
            else
            {
                e.Cancel= false;
                errorProvider1.SetError(txFirstName, "");
            }
        }

        private void txSecondName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txSecondName.Text))
            {
                e.Cancel = true;
                txSecondName.Focus();
                errorProvider1.SetError(txSecondName, "Second name must have a value !");
            }
            else
            {
                e.Cancel= false;
                errorProvider1.SetError(txSecondName, "");
            }
        }

        private void txLastName_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = EnableSaveButton();

        }

        private void txLastName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txLastName.Text)) {
                e.Cancel = true;
                txLastName.Focus();
                errorProvider1.SetError(txLastName, "Last name must have a value!");

            }
            else
            {
                e.Cancel= false;
                errorProvider1.SetError(txLastName, "");
            }
        }

        private void txNationalNO_Validating(object sender, CancelEventArgs e)
        {
            if (clsPerson.IsPersonExists(txNationalNO.Text))
            {
                e.Cancel=true;
                txNationalNO.Focus();
                errorProvider1.SetError(txNationalNO, "This national number is used ! ");

            }
            else
            {
                e.Cancel= false;
                errorProvider1.SetError(txNationalNO, "");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsDataSaved)
            {
                int PersonID = _Person.PersonID;
                
                DataBack?.Invoke(this,PersonID);

            }

            this.Close();
        }

        private void txPhone_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txPhone.Text)) { 

                e.Cancel = true;
                txPhone.Focus();
                errorProvider1.SetError(txPhone, "Phone must have a value!");

            }
            else
            {
                if (!int.TryParse(txPhone.Text, out int value))
                {
                    e.Cancel = true;
                    txPhone.Focus();
                    errorProvider1.SetError(txPhone, "Phone must only contains digits!");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(txPhone, "");
                }
            }
        }

        private void txEmail_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txEmail.Text))
            {
                if (!IsValidEmail(txEmail.Text)) {

                    e.Cancel = true;
                    txEmail.Focus();
                    errorProvider1.SetError(txEmail,"Email fromat is incorrect !");
                
                }
                else
                {
                    e.Cancel= false;
                    errorProvider1.SetError(txEmail, "");
                }

            }
        }

        private static bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }

        private void txAddress_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = EnableSaveButton();

        }

        private void txAddress_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txAddress.Text)) { 

                e.Cancel = true;
                txAddress.Focus();
                errorProvider1.SetError(txAddress, "Address must have a value !");
            }
            else
            {
                e.Cancel= false;
                errorProvider1.SetError(txAddress,"");
            }
        }

        private void llImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                string selectedFilePath = openFileDialog1.FileName;
                //MessageBox.Show("Selected Image is:" + selectedFilePath);

                pictureBox1.Load(selectedFilePath);
                IsPicSet = true;
                llbRemove.Visible= true;
                // ...
            }
        }

        private void llbRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            IsPicSet=false;
            if (rbFemale.Checked)
            {
                pictureBox1.Image = Properties.Resources.Female_512;
            }
            else if (rbMale.Checked) {
                pictureBox1.Image = Properties.Resources.Male_512;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int CountryID = clsCountry.Find(cbCountry.Text).id;

            _Person.NationalNumber = txNationalNO.Text;

            _Person.FirstName = txFirstName.Text;
            _Person.SecondName = txSecondName.Text;
            _Person.ThirdName= txThirdName.Text;
            _Person.LastName = txLastName.Text;

            _Person.DateOfBirth = dateTimePicker1.Value;

            if (rbMale.Checked)
                _Person.Gender = 0;
            else
                _Person.Gender = 1;

            _Person.Address = txAddress.Text;

            _Person.Phone = txPhone.Text;
            _Person.Email = txEmail.Text;
            _Person.NationalityCountryID = CountryID;

            if (IsPicSet)
                _Person.ImagePath = pictureBox1.ImageLocation;
            else
                _Person.ImagePath = "";
            if (_Person.Save())
            {
                MessageBox.Show("Data Saved successfully.");
                IsDataSaved = true;
            }
            else
                MessageBox.Show("Error : Data was not saved !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            _Mode = enMode.Update;
            lbTitle.Text = "Edit Person ID = "+_Person.PersonID.ToString();
            lbPersonID.Text = _Person.PersonID.ToString();

        }

        private bool EnableSaveButton()
        {
            return !string.IsNullOrEmpty(txFirstName.Text) &&
                !string.IsNullOrEmpty(txSecondName.Text) &&
                !string.IsNullOrEmpty(txLastName.Text) &&
                !string.IsNullOrEmpty(txNationalNO.Text) &&
                !string.IsNullOrEmpty(txPhone.Text) &&
                !string.IsNullOrEmpty(txAddress.Text);


        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txFirstName_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = EnableSaveButton();
        }

        private void txSecondName_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = EnableSaveButton();

        }

        private void txPhone_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = EnableSaveButton();

        }
    }
}
