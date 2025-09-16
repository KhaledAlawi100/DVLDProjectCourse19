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

namespace My_Project
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void TestForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(txPersonID.Text);

            clsPerson p1 = clsPerson.Find(ID);
            if(p1 != null)
            {
                txNationalNo.Text = p1.NationalNumber;

                txFirstName.Text = p1.FirstName;

                txSecondName.Text = p1.SecondName;
                txThirdName.Text = p1.ThirdName;
                txLastName.Text = p1.LastName;

                txGender.Text = p1.Gender.ToString();

                txAddress.Text = p1.Address;

                txPhone.Text = p1.Phone;

                txEmail.Text = p1.Email;

                txCountryID.Text = p1.NationalityCountryID.ToString();

                txImagePath.Text = p1.ImagePath;





                
            }

            else
            {
                MessageBox.Show("There is no Person with this ID ");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(txGetCountryID.Text);

            clsCountry c1 = clsCountry.Find(ID);

            string name="";
            if (c1 != null) {
                
                 name = c1.countryName;
            
            }

            lbCountryname.Text = name;

            
        }
    }
}
