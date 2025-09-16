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
    public partial class DetainLicense : Form
    {
        enum enMode { New = 0, update = 1 }

        enMode mode;

        int DetainID;
        public DetainLicense(int DetainID)
        {
            InitializeComponent();

            this.DetainID = DetainID;

            if (DetainID == -1) 
                mode = enMode.New;
            else 
                mode = enMode.update;
        }

        private void DetainLicense_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        float fees = 150;
        private void LoadData()
        {
            if(mode == enMode.update)
            {
                clsDetainedLicense dl = clsDetainedLicense.Find(this.DetainID);


                ucFindLicense1.txtID.Text = dl.LicenseID.ToString();

                ucFindLicense1.btnSearch.PerformClick();

                LoadDataAfterFound();
            }

            lbDetainDate.Text = DateTime.Now.ToString();
            lbCreatedBy.Text = clsUser.Find(clsGlobal.UserID).UserName;
            lbFineFees.Text = fees.ToString() ;

            ucFindLicense1.IsForDetain = true;



        }

        private void LoadDataAfterFound()
        {

            if (ucFindLicense1.IsFound)
            {
                lbLicenseID.Text = ucFindLicense1.LicenseID.ToString();

                btnDetain.Enabled = true;


            }
        }

        private void ucFindLicense1_Load(object sender, EventArgs e)
        {

        }

        private void ucFindLicense1_Leave(object sender, EventArgs e)
        {
            LoadDataAfterFound();
        }

        private void ucFindLicense1_Enter(object sender, EventArgs e)
        {
            LoadDataAfterFound();

        }

        private void ucFindLicense1_MouseMove(object sender, MouseEventArgs e)
        {
            LoadDataAfterFound();

        }

        private void ucFindLicense1_MouseLeave(object sender, EventArgs e)
        {
            LoadDataAfterFound();

        }

        private void ucFindLicense1_MouseHover(object sender, EventArgs e)
        {
            LoadDataAfterFound();

        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            clsDetainedLicense DL1 = new clsDetainedLicense();

            DL1.LicenseID = ucFindLicense1.LicenseID;

            DL1.DetainDate = DateTime.Now;

            DL1.FineFees = fees;

            DL1.CreatedByUserID = clsGlobal.UserID;

            DL1.IsReleased = false;

            DL1.ReleaseDate= null;
            DL1.ReleasedByUserID= null;

            DL1.ReleaseApplicationID= null;

            if (DL1.Save())
            {
                MessageBox.Show("The License Is Detained Successfully");

                lbDetainID.Text = DL1.DetainID.ToString();

                ucFindLicense1.Enabled = false;

                btnDetain.Enabled = false;




            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
