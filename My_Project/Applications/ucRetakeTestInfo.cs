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
    public partial class ucRetakeTestInfo : UserControl
    {
        public ucRetakeTestInfo()
        {
            InitializeComponent();
        }
        public float RetakeFees {  get; set; }
        public float TotalFees {  get; set; }

        public int ID = -1;

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        public void RefreshUI()
        {
            if(this.ID!=-1)
                lbID.Text=ID.ToString();
            lbRFees.Text=this.RetakeFees.ToString();
            lbTotalFees.Text=this.TotalFees.ToString();
        }
    }
}
