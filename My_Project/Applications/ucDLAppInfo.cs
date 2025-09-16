using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_Project.Applications
{
    public partial class ucDLAppInfo : UserControl
    {
        public ucDLAppInfo()
        {
            InitializeComponent();
        }

        public int LDLAppID {  get; set; }
        public string ClassName {  get; set; }

        public int PassedTests {  get; set; }   


        public void RefreshUI()
        {
            lbID.Text = LDLAppID.ToString();
            lbClass.Text = ClassName;
            lbPassedTests.Text = PassedTests.ToString();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void lbID_Click(object sender, EventArgs e)
        {

        }

        private void lbClass_Click(object sender, EventArgs e)
        {

        }

        private void lbPassedTests_Click(object sender, EventArgs e)
        {

        }
    }
}
