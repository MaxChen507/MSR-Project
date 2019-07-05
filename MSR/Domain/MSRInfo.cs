using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Domain
{
    class MSRInfo
    {
        public String Project { get; }
        public String WVL { get; }
        public String Comments { get; }
        public String BudgetYear { get; }
        public String BP_No { get; }
        public String AFE { get; }
        public String SugVendor { get; }
        public String ContactVendor { get; }
        public String Request_Originator { get; }
        public String Company_Approval { get; }
        public DateTime Req_Date { get; }
        public DateTime? Appr_Date { get; }
        public String Recieve_By { get; }
        public DateTime? Recieve_Date { get; }
        public String PUR_Comment { get; }
        public String Decline_Comment { get; }
        public String Review_Comment { get; }
        public String StateFlag { get; }

        public MSRInfo(String Project, String WVL, String Comments, String BudgetYear, String BP_No, String AFE, String SugVendor, String ContactVendor, String Request_Originator, String Company_Approval, DateTime Req_Date, DateTime Appr_Date, String Recieve_By, DateTime Recieve_Date, String PUR_Comment, String Decline_Comment, String Review_Comment, String StateFlag)
        {
            this.Project = Project;
            this.WVL = WVL;
            this.Comments = Comments;
            this.BudgetYear = BudgetYear;
            this.BP_No = BP_No;
            this.AFE = AFE;
            this.SugVendor = SugVendor;
            this.ContactVendor = ContactVendor;
            this.Request_Originator = Request_Originator;
            this.Company_Approval = Company_Approval;
            this.Req_Date = Req_Date;
            this.Appr_Date = Appr_Date;
            this.Recieve_Date = Recieve_Date;
            this.Recieve_By = Recieve_By;
            this.PUR_Comment = PUR_Comment;
            this.Decline_Comment = Decline_Comment;
            this.Review_Comment = Review_Comment;
            this.StateFlag = StateFlag;
        }

        public MSRInfo(String Project, String WVL, String Comments, String BudgetYear, String BP_No, String AFE, String SugVendor, String ContactVendor, String Request_Originator, String Company_Approval, DateTime Req_Date, String StateFlag)
        {
            this.Project = Project;
            this.WVL = WVL;
            this.Comments = Comments;
            this.BudgetYear = BudgetYear;
            this.BP_No = BP_No;
            this.AFE = AFE;
            this.SugVendor = SugVendor;
            this.ContactVendor = ContactVendor;
            this.Request_Originator = Request_Originator;
            this.Company_Approval = Company_Approval;
            this.Req_Date = Req_Date;
            this.Appr_Date = null;
            this.Recieve_Date = null;
            this.Recieve_By = null;
            this.PUR_Comment = "";
            this.Decline_Comment = "";
            this.Review_Comment = "";
            this.StateFlag = StateFlag;
        }

    }
}
