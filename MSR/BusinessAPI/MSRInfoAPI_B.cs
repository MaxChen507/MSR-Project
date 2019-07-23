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

                var showMSRData_db = context.V_ShowMSR.Where(d => ( (d.DeptId_Og.ToString().Equals(DeptId)) || (d.DeptId_Ap.ToString().Equals(DeptId)) ) && d.StateFlag.Equals(StateFlag)).ToList();

                var temp_db = (from d in context.V_ShowMSR
                            where (((d.DeptId_Og.ToString().Equals(DeptId)) || (d.DeptId_Ap.ToString().Equals(DeptId))) && d.StateFlag.Equals(StateFlag))
                            select new Domain.ShowMSRItem { MSRId = d.MSRId.ToString(), Bp_No = d.BP_No, DeptName = d.DeptName, Originator = d.Originator, Approver = d.Approver, Req_Date = d.Req_Date, Comments = d.Comments, Appr_Date = d.Appr_Date.ToString() } ).ToList();

                showMSRData = temp_db;

            }
            return showMSRData;
        }
    }
}
