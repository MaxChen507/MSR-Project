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
        public String Bp_No { get; set; }
        public String DeptName { get; set; }
        public String Originator { get; set; }
        public String Approver { get; set; }
        public DateTime Req_Date { get; set; }
        public String Comments { get; set; }
        public String Appr_Date { get; set; }

        public ShowMSRItem()
        {

        }
        public ShowMSRItem(string MSRId, string Bp_No, string DeptName, string Originator, string Approver, DateTime Req_Date, string Comments, String Appr_Date)
        {
            this.MSRId = MSRId;
            this.Bp_No = Bp_No;
            this.DeptName = DeptName;
            this.Originator = Originator;
            this.Approver = Approver;
            this.Req_Date = Req_Date;
            this.Comments = Comments;
            this.Appr_Date = Appr_Date;
        }
    }
}
