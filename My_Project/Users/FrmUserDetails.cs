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
    public partial class FrmUserDetails : Form
    {
        int PersonID;
        int UserID;
        public FrmUserDetails(int personID, int userID)
        {
            InitializeComponent();
            PersonID = personID;
            UserID = userID;
        }

        private void FrmUserDetails_Load(object sender, EventArgs e)
        {
            

            ucLoginInfo1.ucPersonInformation1.RefreshUI();

            clsUser u1 = clsUser.Find(UserID);

            ucLoginInfo1.UserID = u1.UserID;
            ucLoginInfo1.UserName = u1.UserName;
            ucLoginInfo1.IsActive = u1.IsActive;
            ucLoginInfo1.RefreshUI();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucLoginInfo1_Load(object sender, EventArgs e)
        {

        }
    }
}
