﻿using System;
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
    public partial class HelpFunction : Form
    {
        public HelpFunction()
        {
            InitializeComponent();
        }

        private void HelpFunction_Load(object sender, EventArgs e)
        {

        }

        private void lblHelpFunction_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
