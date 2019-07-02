using System;
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
            Boolean loginSuccessFlag = DatabaseAPI.DBAccessSingleton.Instance.LoginAPI.ValidateLogin(username_Login_textBox.Text, password_Login_textBox.Text);
            
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
            //TO DO: Use DatabaseAPI to return an user session object and set to singleton

            //Get domain object UserInfo and Set object to Business Singleton
            BusinessAPI.BusinessSingleton.Instance.userInfo = DatabaseAPI.DBAccessSingleton.Instance.UserInfoAPI.GetUserInfo(username);

            //Get domain object BudgetInfo and Set object to Business Singleton
            BusinessAPI.BusinessSingleton.Instance.budgetInfo = DatabaseAPI.DBAccessSingleton.Instance.BudgetInfoAPI.GetBudgetInfo_List(BusinessAPI.BusinessSingleton.Instance.userInfo.DeptId);

            //Initalize shared FormItems Data List to Business Singleton
            BusinessAPI.BusinessSingleton.Instance.formItemList = new List<Domain.FormItems>();
        }
    }
}
