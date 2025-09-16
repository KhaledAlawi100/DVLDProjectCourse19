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
    public partial class ucAppBasicInfo : UserControl
    {

        public int AppID { get; set; }

        public byte Status { get; set; }

        public float fees { get; set; }

        public string type {  get; set; }

        public string Applicant { get; set; }

        public DateTime Date { get; set; }

        public DateTime StatusDate { get; set; }    

        public string CreatedBy {  get; set; }


        public ucAppBasicInfo()
        {
            InitializeComponent();
        }

        public void RefreshUI()
        {
            lbID.Text = AppID.ToString();

            if (this.Status == 1)
            {

                lbStatus.Text = "New";

            }
            else if (this.Status == 2)
            {

                lbStatus.Text = "Cancelled";

            }
            else
                lbStatus.Text = "Compelted";

            lbFees.Text = this.fees.ToString();

            lbType.Text = this.type;

            lbApplicant.Text = this.Applicant;

            lbDate.Text = this.Date.ToString();

            lbStatusDate.Text = this.StatusDate.ToString();

            lbCreatedBy.Text = this.CreatedBy;


        }
        

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
