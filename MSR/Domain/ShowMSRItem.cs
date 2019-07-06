using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Domain
{
    class ShowMSRItem
    {
        public String MSRId { get; }
        public String Bp_No { get; }
        public String DeptName { get; }
        public String Originator { get; }
        public String Approver { get; }
        public DateTime Req_Date { get; }
        public String Comments { get; }

        public ShowMSRItem(string MSRId, string Bp_No, string DeptName, string Originator, string Approver, DateTime Req_Date, string Comments)
        {
            this.MSRId = MSRId;
            this.Bp_No = Bp_No;
            this.DeptName = DeptName;
            this.Originator = Originator;
            this.Approver = Approver;
            this.Req_Date = Req_Date;
            this.Comments = Comments;
        }
    }
}
