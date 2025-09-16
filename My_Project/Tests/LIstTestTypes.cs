using DVLD_Business_Layer;
using My_Project.Tests;
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
    public partial class LIstTestTypes : Form
    {
        public LIstTestTypes()
        {
            InitializeComponent();
        }

        private void LIstTestTypes_Load(object sender, EventArgs e)
        {
            dgvListTests.DataSource = clsTestType.GetAllTest();

            dgvListTests.Columns["TestTypeID"].HeaderText = "ID";
            dgvListTests.Columns["TestTypeTitle"].HeaderText = "Title";
            dgvListTests.Columns["TestTypeDescription"].HeaderText = "Description";
            dgvListTests.Columns["TestTypeFees"].HeaderText = "Fees";


            dgvListTests.Columns["TestTypeDescription"].Width = 400;
            dgvListTests.Columns["TestTypeTitle"].Width = 150;

            lbRecords.Text = dgvListTests.Rows.Count.ToString();

            


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID =(int) dgvListTests.CurrentRow.Cells[0].Value;
            string title = dgvListTests.CurrentRow.Cells[1].Value.ToString();
            string describtion = dgvListTests.CurrentRow.Cells[2].Value.ToString();
            float fees = Convert.ToSingle(dgvListTests.CurrentRow.Cells[3].Value);

            UpdateTestTypes frm = new UpdateTestTypes(ID, title, describtion, fees);
            frm.ShowDialog();

            dgvListTests.DataSource = clsTestType.GetAllTest();

        }
    }
}
