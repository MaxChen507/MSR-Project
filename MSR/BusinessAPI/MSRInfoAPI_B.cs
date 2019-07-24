using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            ICollection<Domain.FormItems> formItemsData = null;

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

    }
}
