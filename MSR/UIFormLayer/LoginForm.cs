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
            Boolean loginSuccessFlag = BusinessAPI.BusinessSingleton.Instance.LoginAPI_B.ValidateLogin(username_Login_textBox.Text, password_Login_textBox.Text);

            if (loginSuccessFlag)
            {
                SetUserSession(username_Login_textBox.Text);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Invalid username and password.");
            }
        }

        private void Cancel_Login_button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void SetUserSession(String username)
        {
            //Initalize shared FormItems Data List to Business Singleton
            BusinessAPI.BusinessSingleton.Instance.formItemList_CreateMSR = new List<Domain.FormItems>();

            BusinessAPI.BusinessSingleton.Instance.SetUsrLoginSessionVariables(username);

            //Usr testUsr = BusinessAPI.BusinessSingleton.Instance.userInfo_EF;
        }

        private void Password_Login_textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ok_Login_button.PerformClick();
            }
        }
    }
}
