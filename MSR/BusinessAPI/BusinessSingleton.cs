using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.BusinessAPI
{
    class BusinessSingleton
    {
        //Singleton instance
        private static BusinessSingleton instance;

        //OLD User variables
        public ICollection<Domain.BudgetInfo> budgetInfo { get; set; }
        public Domain.UserInfo userInfo { get; set; }
        public Domain.GroupsInfo groupsInfo { get; set; }
        


        //New Variables
        public Usr userInfo_EF { get; set; }
        public ICollection<V_BP_DEPT> v_bp_dept_access_EF { get; set; }

        // Shared Data of FormItemList
        public ICollection<Domain.FormItems> formItemList_CreateMSR { get; set; }
        public ICollection<Domain.FormItems> formItemList_WaitForApproval { get; set; }
        public ICollection<Domain.FormItems> formItemList_NeedReview { get; set; }
        public ICollection<Domain.FormItems> formItemList_Approved { get; set; }

        private BusinessSingleton()
        {
            LoginAPI_B = new LoginAPI_B();
            MSRInfoAPI_B = new MSRInfoAPI_B();
            StockItemsAPI_B = new StockItemsAPI_B();
        }

        public static BusinessSingleton Instance
        {
            //not thread safe???
            get
            {
                if (instance == null)
                {
                    instance = new BusinessSingleton();
                }
                return instance;
            }
        }

        public LoginAPI_B LoginAPI_B { get; private set; }
        public MSRInfoAPI_B MSRInfoAPI_B { get; private set; }
        public StockItemsAPI_B StockItemsAPI_B { get; private set; }

        public Boolean IsNumeric(object Expression)
        {
            double retNum;

            Boolean isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

        //OLD Functions:

        public ICollection<String> GetAC_List(string Bp_No)
        {
            ICollection<String> results = (
                                           from item in budgetInfo
                                           where item.Bp_No.Equals(Bp_No)
                                           select item.AC_No
                                           ).ToList();

            return results;
        }

        public ICollection<Domain.BudgetInfo> GetFilterBudgetInfo(string Bp_No)
        {
            ICollection<Domain.BudgetInfo> results = (
                                           from item in budgetInfo
                                           where item.Bp_No.Equals(Bp_No)
                                           select item
                                           ).ToList();

            return results;
        }


        //NEW Functions:

        public void SetUsrLoginSessionVariables(string username)
        {
            //Sets the UserInfo
            userInfo_EF = LoginAPI_B.GetUsrByUsername(username);

            //Sets the BPInfo User can access
            v_bp_dept_access_EF = LoginAPI_B.GetBudgetInfo_AccessByDeptId(userInfo_EF.DeptId);

        }

        public DateTime GetDateTime()
        {
            DateTime dateTime = DateTime.MinValue;

            using (var context = new MSR_Max_V2Entities())
            {
                //Log DB commands to console
                context.Database.Log = Console.WriteLine;

                var dateTimeVar = (from dt in context.Database.SqlQuery<DateTime>("Select Getdate() AS DateTime")
                         select dt).FirstOrDefault();

                dateTime = dateTimeVar;
            }

            return dateTime;
        }

        public ICollection<String> GetUniqueBP_Access_List()
        {
            ICollection<String> results = v_bp_dept_access_EF.Select(x => x.BP_No).Distinct().ToList();

            return results;
        }

    }
}
