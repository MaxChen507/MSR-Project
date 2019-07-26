using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Domain
{
    class ShowMSRItem
    {
        public String MSRId { get; set; }
        public String BpNo { get; set; }
        public String DeptName { get; set; }
        public String Originator { get; set; }
        public String Approver { get; set; }
        public DateTime ReqDate { get; set; }
        public String Comments { get; set; }
        public String ApprDate { get; set; }

        public ShowMSRItem()
        {

        }
        public ShowMSRItem(string MSRId, string BpNo, string DeptName, string Originator, string Approver, DateTime ReqDate, string Comments, String ApprDate)
        {
            this.MSRId = MSRId;
            this.BpNo = BpNo;
            this.DeptName = DeptName;
            this.Originator = Originator;
            this.Approver = Approver;
            this.ReqDate = ReqDate;
            this.Comments = Comments;
            this.ApprDate = ApprDate;
        }
    }
}
