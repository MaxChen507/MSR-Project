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

                foreach (V_ShowMSR item in showMSRData_db)
                {
                    Domain.ShowMSRItem temp = new Domain.ShowMSRItem(item.MSRId.ToString(), item.BP_No, item.DeptName, item.Originator, item.Approver, item.Req_Date, item.Comments, item.Appr_Date.ToString());
                    showMSRData.Add(temp);
                }

            }
            return showMSRData;
        }
    }
}
