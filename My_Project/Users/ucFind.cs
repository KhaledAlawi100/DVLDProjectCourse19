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
    public partial class ucFind : UserControl
    {
        public ucFind()
        {
            InitializeComponent();
        }
        public bool IsFound {  get; set; }
        public int PersonID { get; set; }

        private void button2_Click(object sender, EventArgs e)
        {
            AddEditPerson addEditPerson = new AddEditPerson(-1);
          
            addEditPerson.DataBack += AddEditPersonDataBack;

            addEditPerson.ShowDialog();
        }

        private void AddEditPersonDataBack(object sender, int PersonID) {

           textBox1.Text = PersonID.ToString();
            comboBox1.SelectedIndex = 1;
            button1.PerformClick();
            IsFound = true;


        }

        private void ucFind_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void SetTheData(clsPerson p1)
        {
            if (p1 != null)
            {
                IsFound = true;
                PersonID = p1.PersonID;

                ucPersonInformation1.PersonID = p1.PersonID;

                

                ucPersonInformation1.RefreshUI();

            }
            else
                MessageBox.Show("This Person is not in the system !");

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {

                clsPerson p1 = clsPerson.Find(textBox1.Text);
                SetTheData(p1);
            }
            else if (comboBox1.SelectedIndex == 1) {

                if(!int.TryParse(textBox1.Text, out int n))
                {
                    MessageBox.Show("Person ID should be a number !","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;

                }
                clsPerson p2 = clsPerson.Find(int.Parse(textBox1.Text));
                SetTheData(p2);
            }
        }

        private void ucPersonInformation1_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ucPersonInformation1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
