using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSR
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            UIFormLayer.LoginForm fLogin = new UIFormLayer.LoginForm();
            if (fLogin.ShowDialog() == DialogResult.OK)
            {
                //User is from a standard dept
                if (BusinessAPI.BusinessSingleton.Instance.userInfo_EF.Group.GroupsName.Equals(Domain.WorkFlowTrace.StandUser) || BusinessAPI.BusinessSingleton.Instance.userInfo_EF.Group.GroupsName.Equals(Domain.WorkFlowTrace.StandBH))
                {
                    Application.Run(new UIFormLayer.MSRMainForm());
                }
                //User is from procurement dept
                else if (BusinessAPI.BusinessSingleton.Instance.userInfo_EF.Group.GroupsName.Equals(Domain.WorkFlowTrace.StandProcurement))
                {
                    Application.Run(new UIFormLayer.MSRMain_ProcurementForm());
                }
                else
                {
                    MessageBox.Show("Error! You do not belong to a correct group.");
                }
                
            }
            else
            {
                Application.Exit();
            }
        }

    }
}
