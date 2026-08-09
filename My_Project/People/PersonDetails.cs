using System;
using System.Drawing;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace My_Project.Users
{
    public partial class PersonDetails : KryptonForm
    {
        public int PersonID { get; private set; }

        public PersonDetails(int personID)
        {
            InitializeComponent();

            PersonID = personID;

            SetupUI();
        }

        private void PersonDetails_Load(object sender, EventArgs e)
        {
            UIButtons.ApplyPrimaryStyle(kryptonButton1);
            LoadPersonData();
        }

        // =====================================================
        // UI SETUP (Modern Look)
        // =====================================================
        private void SetupUI()
        {
            // Form Style
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(30, 30, 46);
            this.ForeColor = Color.White;

            // Title
            this.Text = "Person Details";

            // Optional: padding feel like card UI
            this.Padding = new Padding(10);
        }

        // =====================================================
        // LOAD DATA
        // =====================================================
        private void LoadPersonData()
        {
            if (PersonID <= 0)
            {
                MessageBox.Show(
                    "Invalid Person ID",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                Close();
                return;
            }

            // Loading effect (optional UI improvement)
            this.Cursor = Cursors.WaitCursor;

            ucPersonInformation1.PersonID = PersonID;
            ucPersonInformation1.RefreshUI();

            this.Cursor = Cursors.Default;
        }

        // =====================================================
        // CLOSE BUTTON (Modern UX)
        // =====================================================
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Smooth UI closing animation feel
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            this.Opacity = 0.9;
            base.OnFormClosing(e);
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}