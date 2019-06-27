using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.BusinessAPI
{
    class UserSessionSingleton
    {
        //Singleton instance
        private static UserSessionSingleton instance;

        //User variables
        public string _username { get; set; }

        //Test
        public string _deptId = "4";

        public ICollection<BudgetInfo> _budgetInfo = null;

        private UserSessionSingleton()
        {
            _budgetInfo = DatabaseAPI.DBAccessSingleton.Instance.BudgetInfoAPI.GetBudgetInfo_List(_deptId);
        }

        public static UserSessionSingleton Instance
        {
            //not thread safe???
            get
            {
                if (instance == null)
                {
                    instance = new UserSessionSingleton();
                }
                return instance;
            }
        }


    }
}
