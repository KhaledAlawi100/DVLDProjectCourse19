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

namespace My_Project.Applications
{
    public partial class ManageApplicationTypes : Form
    {
        public ManageApplicationTypes()
        {
            InitializeComponent();
        }

        private void ManageApplicationTypes_Load(object sender, EventArgs e)
        {
            dgvAppTypesList.DataSource = clsApplicationType.GetAllAppTypes();

            dgvAppTypesList.Columns["ApplicationTypeID"].HeaderText = "ID";
            dgvAppTypesList.Columns["ApplicationTypeTitle"].HeaderText = "Title";
            dgvAppTypesList.Columns["ApplicationFees"].HeaderText = "Fees";

            dgvAppTypesList.Columns["ApplicationTypeTitle"].Width = 300;

            lbRecords.Text = dgvAppTypesList.Rows.Count.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateApplicationType frm = new UpdateApplicationType(
    Convert.ToInt32(dgvAppTypesList.CurrentRow.Cells[0].Value),
    dgvAppTypesList.CurrentRow.Cells[1].Value.ToString(),
    Convert.ToSingle(dgvAppTypesList.CurrentRow.Cells[2].Value));
            frm.ShowDialog();

        }
    }
}
