using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSR.Domain;

namespace MSR.BusinessAPI
{
    class MSRInfoAPI
    {
        public ICollection<Domain.ShowMSRItem> GetShowMSRList(String deptId, String stateFlag)
        {
            ICollection<Domain.ShowMSRItem> showMSRData = null;

            using (var context = new MSR_MaxEntities())
            {
                //Log DB commands to console
                context.Database.Log = Console.WriteLine;

                showMSRData = new List<Domain.ShowMSRItem>();

                var showMSRData_db = (from d in context.V_ShowMSR
                            where (((d.DeptId_Og.ToString().Equals(deptId)) || (d.DeptId_Ap.ToString().Equals(deptId))) && d.StateFlag.Equals(stateFlag))
                            select new Domain.ShowMSRItem { MSRId = d.MSRId.ToString(), BpNo = d.BP_No, DeptName = d.DeptName, Originator = d.Originator, Approver = d.Approver, ReqDate = d.Req_Date, Comments = d.Comments, ApprDate = d.Appr_Date.ToString() } ).ToList();

                showMSRData = showMSRData_db;

            }
            return showMSRData;
        }

        public ICollection<Domain.ShowMSRItem> GetShowMSRListProcure(String stateFlag)
        {
            ICollection<Domain.ShowMSRItem> showMSRData = null;

            using (var context = new MSR_MaxEntities())
            {
                //Log DB commands to console
                context.Database.Log = Console.WriteLine;

                showMSRData = new List<Domain.ShowMSRItem>();

                var showMSRData_db = (from d in context.V_ShowMSR
                                      where (d.StateFlag.Equals(stateFlag))
                                      select new Domain.ShowMSRItem { MSRId = d.MSRId.ToString(), BpNo = d.BP_No, DeptName = d.DeptName, Originator = d.Originator, Approver = d.Approver, ReqDate = d.Req_Date, Comments = d.Comments, ApprDate = d.Appr_Date.ToString() }).ToList();

                showMSRData = showMSRData_db;

            }
            return showMSRData;
        }

        public ICollection<Domain.ShowMSRItem> GetFilterShowMSRList(ICollection<Domain.ShowMSRItem> showMSRList, string searchMSRID, string searchDept, string searchOg, string searchAp)
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
        
        public ICollection<Domain.FormItems> GetDomainFormItems(ICollection<FormItem> formItems, String bpNo)
        {
            ICollection<Domain.FormItems> formItemsData = new List<Domain.FormItems>();

            foreach (FormItem item in formItems)
            {
                Domain.FormItems temp = new Domain.FormItems(bpNo, item.ItemCode, item.ItemDesc, item.Quantity.ToString(), item.Unit, item.UnitPrice.ToString(), item.Currency, item.ROS_Date, item.Comments, item.AC_No);
                formItemsData.Add(temp);
            }
            
            return formItemsData;
        }

        public MSR GetMSRByMSRId(String msrId)
        {
            MSR msr = null;

            using (var context = new MSR_MaxEntities())
            {
                //Log DB commands to console
                context.Database.Log = Console.WriteLine;
                
                var msr_db = context.MSRs
                    .Include("FormItems")
                    .Include("Usr_RO")
                    .Include("Usr_CA")
                    .Include("Usr_RecieveBy")
                    .Where(x => x.MSRId.ToString().Equals(msrId))
                    .First();

                msr = msr_db;
            }

            return msr;
        }

        public void DeleteFormItemsByMSRId(String msrId)
        {
            using (var context = new MSR_MaxEntities())
            {
                //Log DB commands to console
                context.Database.Log = Console.WriteLine;

                var msr_db = context.MSRs
                    .Include("FormItems")
                    .Where(x => x.MSRId.ToString().Equals(msrId))
                    .First();

                var formItemsToDelete = msr_db.FormItems;
                context.FormItems.RemoveRange(formItemsToDelete);
                context.SaveChanges();
            }
        }

        public String GetOriginatorID(String msrId)
        {
            String OgId = null;

            using (var context = new MSR_MaxEntities())
            {
                //Log DB commands to console
                context.Database.Log = Console.WriteLine;

                var msr_db = context.MSRs.Find(Int32.Parse(msrId));

                OgId = msr_db.Usr_RO.ToString();
            }

            return OgId;
        }

        public void InsertInitialFormItems(ICollection<FormItems> formItemList, int msrId)
        {
            using (var context = new MSR_MaxEntities())
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
                        ROS_Date = item.ROSDate,
                        Comments = item.Comments,
                        MSRId = msrId,
                        AC_No = item.ACNo

                    };

                    context.FormItems.Add(tempFormItem);

                }
                context.SaveChanges();

            }

        }

        public void InsertIntialMSR(Domain.ApproverInfo approverInfo, String project, String vwl, String comments, String budgetYear, String bpNo, String afe, String sugVendor, String contactVendor, DateTime reqDate)
        {
            using (var context = new MSR_MaxEntities())
            {
                //Log DB commands to console
                context.Database.Log = Console.WriteLine;

                //First obtain referenced users
                var usr_RO = context.Usrs.Find(BusinessAPI.BusinessSingleton.Instance.userInfo_EF.UserId);
                var usr_CA = context.Usrs.Find(Int32.Parse(approverInfo.UserId));

                //Initialize new MSR
                var tempMSR = new MSR
                {
                    Project = project,
                    WVL = vwl,
                    Comments = comments,
                    BudgetYear = Int32.Parse(budgetYear),
                    BP_No = bpNo,
                    AFE = afe,
                    SugVendor = sugVendor,
                    ContactVendor = contactVendor,
                    Usr_RO = usr_RO,
                    Usr_CA = usr_CA,
                    Req_Date = reqDate,
                    PUR_Comment = "",
                    Decline_Comment = "",
                    Review_Comment = "",
                    StateFlag = Domain.WorkFlowTrace.WAIT_FOR_APPROVAL
                };

                //Adds the newly created MSR
                context.MSRs.Add(tempMSR);
                context.SaveChanges();

                //Gets the just inserted MSR
                int MSRId = tempMSR.MSRId;

                //Attaches the FormItems to that MSR just created
                InsertInitialFormItems(BusinessAPI.BusinessSingleton.Instance.formItemListCreateMSR, MSRId);
            }
        }

        internal void UpdateMSRNeedReview(int msrId, String stateFlag)
        {
            using (var context = new MSR_MaxEntities())
            {
                var msr_db = context.MSRs.Find(msrId);
                if (msr_db != null)
                {
                    msr_db.StateFlag = stateFlag;

                    context.SaveChanges();
                }

            }
        }

        public void UpdateMSRWaitForApproval(int msrId, String path, String commentFromPath, DateTime approvalDate, String stateFlag)
        {
            if (path.Equals("Approve"))
            {
                using (var context = new MSR_MaxEntities())
                {
                    var msr_db = context.MSRs.Find(msrId);
                    if (msr_db != null)
                    {
                        msr_db.Appr_Date = approvalDate;
                        msr_db.StateFlag = stateFlag;

                        context.SaveChanges();
                    }

                }

            }
            else if (path.Equals("Send for Review"))
            {
                using (var context = new MSR_MaxEntities())
                {
                    var msr_db = context.MSRs.Find(msrId);
                    if (msr_db != null)
                    {
                        msr_db.Review_Comment = commentFromPath;
                        msr_db.StateFlag = stateFlag;

                        context.SaveChanges();
                    }

                }

            }
            else if (path.Equals("Decline"))
            {
                using (var context = new MSR_MaxEntities())
                {
                    var msr_db = context.MSRs.Find(msrId);
                    if (msr_db != null)
                    {
                        msr_db.Decline_Comment = commentFromPath;
                        msr_db.StateFlag = stateFlag;

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
