using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using My_Project.Users;

namespace My_Project.Users
{

    public partial class PersonDetails : Form
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
        public PersonDetails(string ID, string NationalNumber, string FirstName, string SecondName, string ThirdName, string LastName,
            string dobString, string Gender, string Address, string Phone, string Email, string Country, string ImagePath)
        {
            InitializeComponent();

            this.ID = ID;
            this.NationalNumber = NationalNumber;
            this.FullName = FirstName+" "+SecondName+" "+ThirdName+" "+LastName;
            this.Gender = Gender;
            this.Phone = Phone;
            this.Email = Email;
            this.Address = Address;
            this.DateOfBirth = dobString;
            this.Country = Country;
            this.ImagePath = ImagePath;


          

        }

        private void PersonDetails_Load(object sender, EventArgs e)
        {
            ucPersonInformation1.ID = ID;
            ucPersonInformation1.NationalNumber = NationalNumber;
            ucPersonInformation1.FullName = FullName;
            ucPersonInformation1.Gender = Gender;
            ucPersonInformation1.Phone = Phone;
            ucPersonInformation1.Email = Email;
            ucPersonInformation1.Address = Address;
            ucPersonInformation1.DateOfBirth = DateOfBirth;
            ucPersonInformation1.Country = Country;
            ucPersonInformation1.ImagePath = ImagePath;

            ucPersonInformation1.RefreshUI();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
