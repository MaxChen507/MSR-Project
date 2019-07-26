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
        public ICollection<Domain.FormItems> formItemListCreateMSR { get; set; }
        public ICollection<Domain.FormItems> formItemListWaitForApproval { get; set; }
        public ICollection<Domain.FormItems> formItemListNeedReview { get; set; }
        public ICollection<Domain.FormItems> formItemListApproved { get; set; }

        private BusinessSingleton()
        {
            LoginAPI = new LoginAPI();
            MSRInfoAPI = new MSRInfoAPI();
            StockItemsAPI = new StockItemsAPI();
            NonStockItemsAPI = new NonStockItemsAPI();
            BudgetInfoAPI = new BudgetInfoAPI();
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

        public LoginAPI LoginAPI { get; private set; }
        public MSRInfoAPI MSRInfoAPI { get; private set; }
        public StockItemsAPI StockItemsAPI { get; private set; }
        public NonStockItemsAPI NonStockItemsAPI { get; private set; }
        public BudgetInfoAPI BudgetInfoAPI { get; private set; }


        public Boolean IsNumeric(object expression)
        {
            double retNum;

            Boolean isNum = Double.TryParse(Convert.ToString(expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

        //NEW Functions:

        public void SetUsrLoginSessionVariables(string username)
        {
            //Sets the UserInfo
            userInfo_EF = LoginAPI.GetUsrByUsername(username);

            //User is from a standard dept
            if (userInfo_EF.Group.GroupsName.Equals(Domain.WorkFlowTrace.StandUser) || userInfo_EF.Group.GroupsName.Equals(Domain.WorkFlowTrace.StandBH))
            {
                //Initalize shared FormItems Data List to Business Singleton
                BusinessAPI.BusinessSingleton.Instance.formItemListCreateMSR = new List<Domain.FormItems>();

                //Sets the BPInfo User can access
                v_bp_dept_access_EF = LoginAPI.GetBudgetInfoAccessByDeptId(userInfo_EF.DeptId);

                //Sets the AC Holders of BPInfo User can access
                v_bp_bi_access_EF = LoginAPI.GetACAccessByBPList(v_bp_dept_access_EF);
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

        public ICollection<String> GetUniqueBPAccessList()
        {
            ICollection<String> results = v_bp_dept_access_EF.Select(x => x.BP_No).Distinct().ToList();

            return results;
        }

        public ICollection<Domain.BudgetInfo> GetFilterBudgetInfo(string bpNo)
        {
            ICollection<Domain.BudgetInfo> results = (
                                           from item in v_bp_dept_access_EF
                                           where item.BP_No.Equals(bpNo)
                                           select new Domain.BudgetInfo { BpNo = item.BP_No, ACNo = item.AC_No, ACDesc = item.AC_Desc}
                                           ).ToList();

            return results;
        }

        public Domain.GroupsInfo GetGroupsInfo()
        {
            Domain.GroupsInfo groupsInfo = new Domain.GroupsInfo(userInfo_EF.Group.GroupsId.ToString(), userInfo_EF.Group.GroupsName, userInfo_EF.Group.GroupsDesc, userInfo_EF.Group.GroupsActiveFlag.ToString());

            return groupsInfo;
        }

        public bool CheckACNoCAIdMatch(String acNo, String approverId)
        {
            bool matchFlag = false;

            try
            {
                V_BH_BI result = v_bp_bi_access_EF.Single(x => x.AC_No.Equals(acNo) && x.UserId == Int32.Parse(approverId));
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
