using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSR.Domain;

namespace MSR.BusinessAPI
{
    class MSRInfoAPI_B
    {
        public ICollection<Domain.ShowMSRItem> GetShowMSR_List(String DeptId, String StateFlag)
        {
            ICollection<Domain.ShowMSRItem> showMSRData = null;

            using (var context = new MSR_Max_V2Entities())
            {
                //Log DB commands to console
                context.Database.Log = Console.WriteLine;

                showMSRData = new List<Domain.ShowMSRItem>();

                var showMSRData_db = (from d in context.V_ShowMSR
                            where (((d.DeptId_Og.ToString().Equals(DeptId)) || (d.DeptId_Ap.ToString().Equals(DeptId))) && d.StateFlag.Equals(StateFlag))
                            select new Domain.ShowMSRItem { MSRId = d.MSRId.ToString(), Bp_No = d.BP_No, DeptName = d.DeptName, Originator = d.Originator, Approver = d.Approver, Req_Date = d.Req_Date, Comments = d.Comments, Appr_Date = d.Appr_Date.ToString() } ).ToList();

                showMSRData = showMSRData_db;

            }
            return showMSRData;
        }


        public ICollection<Domain.ShowMSRItem> GetFiltershowMSR_List(ICollection<Domain.ShowMSRItem> showMSRList, string searchMSRID, string searchDept, string searchOg, string searchAp)
        {

            List<string> searchMSRIDList = searchMSRID.Split(' ').ToList();

            List<string> searchDeptList = searchDept.Split(' ').ToList();

            List<string> searchOgList = searchOg.Split(' ').ToList();

            List<string> searchApList = searchAp.Split(' ').ToList();

            ICollection<Domain.ShowMSRItem> results = (
                                           from item in showMSRList
                                           where
                                                searchMSRIDList.All(sTring => item.MSRId.ToUpperInvariant().Contains(sTring.ToUpperInvariant()))
                                                &&
                                                searchDeptList.All(sTring => item.DeptName.ToUpperInvariant().Contains(sTring.ToUpperInvariant()))
                                                &&
                                                searchOgList.All(sTring => item.Originator.ToUpperInvariant().Contains(sTring.ToUpperInvariant()))
                                                &&
                                                searchApList.All(sTring => item.Approver.ToUpperInvariant().Contains(sTring.ToUpperInvariant()))
                                           select item
                                           ).ToList();

            return results;
        }
        
        public ICollection<Domain.FormItems> GetDomain_FormItems(ICollection<FormItem> formItems, String BP_No)
        {
            ICollection<Domain.FormItems> formItemsData = new List<Domain.FormItems>();

            foreach (FormItem item in formItems)
            {
                Domain.FormItems temp = new Domain.FormItems(BP_No, item.ItemCode, item.ItemDesc, item.Quantity.ToString(), item.Unit, item.UnitPrice.ToString(), item.Currency, item.ROS_Date, item.Comments, item.AC_No);
                formItemsData.Add(temp);
            }
            
            return formItemsData;
        }

        public MSR GetMSRByMSRId(String MSRId)
        {
            MSR msr = null;

            using (var context = new MSR_Max_V2Entities())
            {
                //Log DB commands to console
                context.Database.Log = Console.WriteLine;
                
                var msr_db = context.MSRs
                    .Include("FormItems")
                    .Include("Usr2")
                    .Include("Usr")
                    .Include("Usr1")
                    .Where(x => x.MSRId.ToString().Equals(MSRId))
                    .First();

                msr = msr_db;
            }

            return msr;
        }

        public void DeleteFormItemsByMSRId(String MSRId)
        {
            using (var context = new MSR_Max_V2Entities())
            {
                //Log DB commands to console
                context.Database.Log = Console.WriteLine;

                var msr_db = context.MSRs
                    .Include("FormItems")
                    .Where(x => x.MSRId.ToString().Equals(MSRId))
                    .First();

                var formItemsToDelete = msr_db.FormItems;
                context.FormItems.RemoveRange(formItemsToDelete);
                context.SaveChanges();
            }
        }

        public String GetOriginatorID(String MSRId)
        {
            String OgId = null;

            using (var context = new MSR_Max_V2Entities())
            {
                //Log DB commands to console
                context.Database.Log = Console.WriteLine;

                var msr_db = context.MSRs.Find(Int32.Parse(MSRId));

                OgId = msr_db.Usr2.ToString();
            }

            return OgId;
        }

        public void InsertInitialFormItems(ICollection<FormItems> formItemList, int MSRId)
        {
            using (var context = new MSR_Max_V2Entities())
            {
                foreach (Domain.FormItems item in formItemList)
                {
                    var tempFormItem = new FormItem
                    {
                        ItemCode = item.ItemCode,
                        ItemDesc = item.ItemDesc,
                        Quantity = Double.Parse(item.Quantity),
                        Unit = item.Unit,
                        UnitPrice = String.IsNullOrEmpty(item.UnitPrice) ? (double?)null : Convert.ToDouble(item.UnitPrice),
                        Currency = item.Currency,
                        ROS_Date = item.ROS_Date,
                        Comments = item.Comments,
                        MSRId = MSRId,
                        AC_No = item.AC_No

                    };

                    context.FormItems.Add(tempFormItem);

                }
                context.SaveChanges();

            }

        }

        public void InsertIntialMSR(Domain.ApproverInfo approverInfo, String Project, String VWL, String Comments, String BudgetYear, String BP_No, String AFE, String SugVendor, String ContactVendor, DateTime Req_Date)
        {
            using (var context = new MSR_Max_V2Entities())
            {
                //Log DB commands to console
                context.Database.Log = Console.WriteLine;

                //First obtain referenced users
                var usr_RO = context.Usrs.Find(BusinessAPI.BusinessSingleton.Instance.userInfo_EF.UserId);
                var usr_CA = context.Usrs.Find(Int32.Parse(approverInfo.UserId));

                //Initialize new MSR
                var tempMSR = new MSR
                {
                    Project = Project,
                    WVL = VWL,
                    Comments = Comments,
                    BudgetYear = Int32.Parse(BudgetYear),
                    BP_No = BP_No,
                    AFE = AFE,
                    SugVendor = SugVendor,
                    ContactVendor = ContactVendor,
                    Usr2 = usr_RO,
                    Usr = usr_CA,
                    Req_Date = Req_Date,
                    PUR_Comment = "",
                    Decline_Comment = "",
                    Review_Comment = "",
                    StateFlag = Domain.WorkFlowTrace.CREATED
                };

                //Adds the newly created MSR
                context.MSRs.Add(tempMSR);
                context.SaveChanges();

                //Gets the just inserted MSR
                int MSRId = tempMSR.MSRId;

                //Attaches the FormItems to that MSR just created
                InsertInitialFormItems(BusinessAPI.BusinessSingleton.Instance.formItemList_CreateMSR, MSRId);
            }
        }

        public void UpdateMSR_ApproveButton(int MSRId, String ApproveButton, String CommentFromApproveButton, DateTime ApprovalDate, String StateFlag)
        {
            if (ApproveButton.Equals("Approve"))
            {
                using (var context = new MSR_Max_V2Entities())
                {
                    var msr_db = context.MSRs.Find(MSRId);
                    if (msr_db != null)
                    {
                        msr_db.Appr_Date = ApprovalDate;
                        msr_db.StateFlag = StateFlag;

                        context.SaveChanges();
                    }

                }

            }
            else if (ApproveButton.Equals("Send for Review"))
            {
                using (var context = new MSR_Max_V2Entities())
                {
                    var msr_db = context.MSRs.Find(MSRId);
                    if (msr_db != null)
                    {
                        msr_db.Review_Comment = CommentFromApproveButton;
                        msr_db.StateFlag = StateFlag;

                        context.SaveChanges();
                    }

                }

            }
            else if (ApproveButton.Equals("Decline"))
            {
                using (var context = new MSR_Max_V2Entities())
                {
                    var msr_db = context.MSRs.Find(MSRId);
                    if (msr_db != null)
                    {
                        msr_db.Decline_Comment = CommentFromApproveButton;
                        msr_db.StateFlag = StateFlag;

                        context.SaveChanges();
                    }

                }

            }
            else
            {

            }

        }



    }
}
