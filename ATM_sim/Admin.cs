using ATM_sim.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATM_sim
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
            UC_Students students = new UC_Students();
            addUserControl(students);
        }

        private void Admin_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void lblHelpFunction_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(userControl);
            userControl.BringToFront();
        }
        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            UC_Students students = new UC_Students();
            addUserControl(students);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            UC_Transactions uC_Transactions = new UC_Transactions();
            addUserControl(uC_Transactions);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            UC_Feedback uC_Feedback = new UC_Feedback();    
            addUserControl(uC_Feedback);
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            UC_Admin uC_Admin = new UC_Admin();
            addUserControl(uC_Admin);
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            LoginPage log = new LoginPage();
            log.Show();   
            this.Hide();    
        }
    }
}
