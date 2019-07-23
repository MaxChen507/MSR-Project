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


    }
}
