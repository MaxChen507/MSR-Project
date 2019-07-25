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

        //New Variables
        public Usr userInfo_EF { get; set; }
        public ICollection<V_BP_AC_DEPT> v_bp_dept_access_EF { get; set; }
        public ICollection<V_BH_BI> v_bp_bi_access_EF { get; set; }

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
            NonStockItemsAPI_B = new NonStockItemsAPI_B();
            BudgetInfoAPI_B = new BudgetInfoAPI_B();
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
        public NonStockItemsAPI_B NonStockItemsAPI_B { get; private set; }
        public BudgetInfoAPI_B BudgetInfoAPI_B { get; private set; }


        public Boolean IsNumeric(object Expression)
        {
            double retNum;

            Boolean isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

        //NEW Functions:

        public void SetUsrLoginSessionVariables(string username)
        {
            //Sets the UserInfo
            userInfo_EF = LoginAPI_B.GetUsrByUsername(username);

            //User is from a standard dept
            if (userInfo_EF.Group.GroupsName.Equals(Domain.WorkFlowTrace.StandUser) || userInfo_EF.Group.GroupsName.Equals(Domain.WorkFlowTrace.StandBH))
            {
                //Initalize shared FormItems Data List to Business Singleton
                BusinessAPI.BusinessSingleton.Instance.formItemList_CreateMSR = new List<Domain.FormItems>();

                //Sets the BPInfo User can access
                v_bp_dept_access_EF = LoginAPI_B.GetBudgetInfo_AccessByDeptId(userInfo_EF.DeptId);

                //Sets the AC Holders of BPInfo User can access
                v_bp_bi_access_EF = LoginAPI_B.GetAC_AccessByBPList(v_bp_dept_access_EF);
            }
            //User is from procurement dept
            else if (userInfo_EF.Group.GroupsName.Equals(Domain.WorkFlowTrace.StandProcurement))
            {

            }
            else
            {
                return;
            }


        }

        public DateTime GetDateTime()
        {
            DateTime dateTime = DateTime.MinValue;

            using (var context = new MSR_MaxEntities())
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

        public ICollection<Domain.BudgetInfo> GetFilterBudgetInfo(string Bp_No)
        {
            ICollection<Domain.BudgetInfo> results = (
                                           from item in v_bp_dept_access_EF
                                           where item.BP_No.Equals(Bp_No)
                                           select new Domain.BudgetInfo { Bp_No = item.BP_No, AC_No = item.AC_No, AC_Desc = item.AC_Desc}
                                           ).ToList();

            return results;
        }

        public Domain.GroupsInfo GetGroupsInfo()
        {
            Domain.GroupsInfo groupsInfo = new Domain.GroupsInfo(userInfo_EF.Group.GroupsId.ToString(), userInfo_EF.Group.GroupsName, userInfo_EF.Group.GroupsDesc, userInfo_EF.Group.GroupsActiveFlag.ToString());

            return groupsInfo;
        }

        public bool CheckACNo_CAId_Match(String AC_No, String ApproverId)
        {
            bool matchFlag = false;

            try
            {
                V_BH_BI result = v_bp_bi_access_EF.Single(x => x.AC_No.Equals(AC_No) && x.UserId == Int32.Parse(ApproverId));
                matchFlag = true;
            }
            catch
            {
                matchFlag = false;
            }

            return matchFlag;
        }
        
        public Domain.ApproverInfo GetApproverInfoByUsername(String username)
        {
            Domain.ApproverInfo approverInfo = null;

            using (var context = new MSR_MaxEntities())
            {
                Usr usr_db = context.Usrs.SingleOrDefault(u => u.Username.Equals(username));
                
                Domain.ApproverInfo approverInfoTemp = new Domain.ApproverInfo
                {
                    UserId = usr_db.UserId.ToString(),
                    Username = usr_db.Username,
                    FullName = usr_db.FullName,
                    Email = usr_db.Email,
                    DeptId = usr_db.DeptId.ToString()
                };

                approverInfo = approverInfoTemp;
            }

            return approverInfo;
        }
    }
}
