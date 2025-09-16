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
    public partial class ucPersonInformation : UserControl
    {
        public string ID { get; set; }
        public string NationalNumber { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string DateOfBirth { get; set; }  // or DateTime if you later prefer
        public string Country { get; set; }
        public string ImagePath { get; set; }
        public ucPersonInformation()
        {
            InitializeComponent();
            

            
        }
        //public ucPersonInformation(
        //string PersonID, string NationalNo, string FullName,
        //string Gender, string Phone, string Email,
        //string Address, string DateOfBirth, string country, string ImagePath)
        //{
        //    InitializeComponent();

        //    lbPersonID.Text = PersonID;
        //    lbName.Text = FullName;
        //    lbNationalNo.Text = NationalNo;
        //    lbGender.Text = Gender;
        //    lbPhone.Text = Phone;
        //    lbEmail.Text = Email;
        //    lbDateOfBirth.Text = DateOfBirth;
        //    lbAddress.Text = Address;
        //    lbCountry.Text = country;
        //    pbProfile.ImageLocation = ImagePath;
        //}



        //public  void LoadTheData(string PersonID , string FullName ,string NationalNo ,
        //    string gender , string phone , string email ,string address , string DateOfBirth , string country)
        //{



        //}
        public void RefreshUI()
        {
            lbPersonID.Text = ID;
            lbName.Text = FullName;
            lbNationalNumber.Text = NationalNumber;
            lbGender.Text = Gender;
            lbPhone.Text = Phone;
            lbEmail.Text = Email;
            lbDateOfBirth.Text = DateOfBirth;
            lbAddress.Text = Address;
            lbCountry.Text = Country;
            pbProfile.ImageLocation = ImagePath;
            string gender= this.Gender;
            if (gender.Equals("Male"))
            {
                pbGender.Image = Properties.Resources.Man_32;
                pbProfile.Image = Properties.Resources.Male_512;
            }
            else
            {
                pbProfile.Image = Properties.Resources.Female_512;

                pbGender.Image= Properties.Resources.Woman_32;
            }
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        public void RefreshUI(int PersonId)
        {
            clsPerson p1 = clsPerson.Find(PersonId);

            lbPersonID.Text = PersonId.ToString();
            lbName.Text = p1.FirstName+" "+p1.SecondName+" "+p1.ThirdName+" "+p1.LastName;
            lbNationalNumber.Text = p1.NationalNumber;
            if (p1.Gender == 0) lbGender.Text = "Male";
            else lbGender.Text = "Female";
            lbPhone.Text = p1.Phone;
            lbEmail.Text = p1.Email;
            lbDateOfBirth.Text = p1.DateOfBirth.ToString("yyyy-mm-dd");
            lbAddress.Text = p1.Address;

            lbCountry.Text = clsCountry.Find(p1.NationalityCountryID).countryName;

            pbProfile.ImageLocation = p1.ImagePath;
            string gender = lbGender.Text;
            if (gender.Equals("Male"))
            {
                pbGender.Image = Properties.Resources.Man_32;
                pbProfile.Image = Properties.Resources.Male_512;
            }
            else
            {
                pbProfile.Image = Properties.Resources.Female_512;

                pbGender.Image = Properties.Resources.Woman_32;
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (string.IsNullOrEmpty(ID))
            {
                MessageBox.Show("There is no Person to update it","Empty Person",MessageBoxButtons.OK,MessageBoxIcon.Warning);

                return;

            }
            AddEditPerson frm = new AddEditPerson(int.Parse(ID));
            frm.ShowDialog();
        }

        private void ucPersonInformation_Load(object sender, EventArgs e)
        {

        }
    }
}
