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
    public partial class ucLoginInfo : UserControl
    {
        public ucLoginInfo()
        {
            InitializeComponent();
        }
        public int UserID {  get; set; }
        public string UserName { get; set; }
        public bool IsActive {  get; set; }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void ucLoginInfo_Load(object sender, EventArgs e)
        {

        }

        public void RefreshUI()
        {
            lbUserID.Text = UserID.ToString();
            lbUserName.Text = UserName.ToString();
            if (IsActive)
                lbIsActive.Text = "Yes";
            else
                lbIsActive.Text = "NO";
        }

        private void ucPersonInformation1_Load(object sender, EventArgs e)
        {

        }
    }
}
