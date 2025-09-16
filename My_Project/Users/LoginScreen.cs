using DVLD_Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_Project.Users
{
    public partial class LoginScreen : Form
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) { 

                WriteUserToFile(txUsername.Text, txPassword.Text,1);
                           
            }
            else
                WriteUserToFile(txUsername.Text, txPassword.Text, 0);


        }

        public void WriteUserToFile(string username, string password, int binaryFlag)
        {
            string filePath = "D:\\2-prog Adv\\1- course 19 Full Project ProgAdv\\docsusers.txt";
            string line = $"{username},{password},{binaryFlag}";

            // ✅ Overwrites the file with a new single entry
            File.WriteAllText(filePath, line + Environment.NewLine);
        }

        private void LoadRememberedUser()
        {
            string filePath = "D:\\2-prog Adv\\1- course 19 Full Project ProgAdv\\docsusers.txt";

            if (!File.Exists(filePath))
                return;

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');

                if (parts.Length == 3)
                {
                    string username = parts[0];
                    string password = parts[1];
                    int flag;

                    if (int.TryParse(parts[2], out flag) && flag == 1)
                    {
                        // ✅ Fill the textboxes
                        txUsername.Text = username;
                        txPassword.Text = password;
                        break; // Only fill the first matched user
                    }
                }
            }
        }

        private void LoginScreen_Load(object sender, EventArgs e)
        {
            LoadRememberedUser();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clsUser user1 = clsUser.Find(txUsername.Text);

            //MessageBox.Show("The is the hash of the password :" + ComputeHash(txPassword.Text.Trim()));

            if (user1 == null) {

                MessageBox.Show("The Username was not found !","Not Found !",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            //MessageBox.Show("The Password of user  is : " + user1.Password);
            //MessageBox.Show("The password in the password text box is : " + txPassword.Text);


            if ( !clsUser.isUserExists(user1.UserName,txPassword.Text.Trim())) {
                MessageBox.Show("The Password does not match with the username !", "Wrong password !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }
            if (!user1.IsActive)
            {
                MessageBox.Show("This user is not active !", "Not active user !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }
            this.Hide();
            clsGlobal.PersonID= user1.PersonID;
            clsGlobal.UserID= user1.UserID;
            Form1 form = new Form1();
            form.ShowDialog();
            



        }

        

        private void LoginScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!checkBox1.Checked)
            {
                WriteUserToFile(txUsername.Text,txPassword.Text,0);
            } 
        }

        private void txPassword_TextChanged(object sender, EventArgs e)
        {

        }

        static string ComputeHash(string input)
        {
            //SHA is Secutred Hash Algorithm.
            // Create an instance of the SHA-256 algorithm
            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute the hash value from the UTF-8 encoded input string
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convert the byte array to a lowercase hexadecimal string
                return BitConverter.ToString(hashBytes).Replace("-", "");
            }
        }
    }
}
