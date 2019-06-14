﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSR
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void Ok_Login_button_Click(object sender, EventArgs e)
        {            
            Boolean loginSuccessFlag = true;
            if (loginSuccessFlag)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void Cancel_Login_button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
