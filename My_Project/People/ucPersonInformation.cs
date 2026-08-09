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
        public int PersonID { get; set; }
       
        public ucPersonInformation()
        {
            InitializeComponent();
            

            
        }
        
        

        
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        public void RefreshUI()
        {
            clsPerson p1 = clsPerson.Find(PersonID);

            if (p1 != null)
            {

                lbPersonID.Text = PersonID.ToString();
                lbName.Text = p1.FirstName + " " + p1.SecondName + " " + p1.ThirdName + " " + p1.LastName;
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

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (PersonID==0)
            {
                MessageBox.Show("There is no Person to update it","Empty Person",MessageBoxButtons.OK,MessageBoxIcon.Warning);

                return;

            }
            AddEditPerson frm = new AddEditPerson(PersonID);
            frm.ShowDialog();
        }

        private void ucPersonInformation_Load(object sender, EventArgs e)
        {

        }
    }
}
