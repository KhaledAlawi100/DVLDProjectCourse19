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

namespace My_Project.Licenses
{
    public partial class ListDetainedLicenses : Form
    {
        public ListDetainedLicenses()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ListDetainedLicenses_Load(object sender, EventArgs e)
        {
            LoadData();

        }

        private void LoadData()
        {

            DataTable ListOfDetainedLicense = clsDetainedLicense.GetAllDetainedLicenses();

            dataGridView1.DataSource = ListOfDetainedLicense;



        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if ((bool)dataGridView1.CurrentRow.Cells[5].Value)
                releaseDetaiedLicenseToolStripMenuItem.Enabled = false;
            else
                releaseDetaiedLicenseToolStripMenuItem.Enabled=true;
        }

        private void releaseDetaiedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReleaseDeatainedLicense frm = new ReleaseDeatainedLicense((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            LoadData();
        }
    }
}
