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

namespace My_Project.Tests
{
    public partial class UpdateTestTypes : Form
    {
        int ID {  get; set; }
        string title { get; set; }
        string description { get; set; }

        float fees { get; set; }
        public UpdateTestTypes(int ID , string title , string Describtion,float fees)
        {
            InitializeComponent();
            this.ID = ID;
            this.title = title;
            this.description = Describtion;
            this.fees = fees;
        }

        private void UpdateTestTypes_Load(object sender, EventArgs e)
        {
            lbID.Text=this.ID.ToString();
            txTitle.Text= this.title.ToString();
            txDescribtion.Text = this.description.ToString();
            txFees.Text = this.fees.ToString();


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool EnableSaveBtn()
        {
            return !string.IsNullOrEmpty(txTitle.Text) && 
                !string.IsNullOrEmpty(txDescribtion.Text) &&
                ! string.IsNullOrEmpty(txDescribtion.Text) && 
                !string.IsNullOrEmpty(txFees.Text) 
                && decimal.TryParse(txFees.Text,out decimal _);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (clsTestType.UpdateTestType(this.ID, txTitle.Text, txDescribtion.Text, Convert.ToSingle(txFees.Text))){

                MessageBox.Show("Data updated succefully");

            }
            else
                MessageBox.Show("Failed to update data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

        private void txTitle_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = EnableSaveBtn();
        }

        private void txDescribtion_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = EnableSaveBtn();

        }

        private void txFees_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = EnableSaveBtn();

        }
    }
}
