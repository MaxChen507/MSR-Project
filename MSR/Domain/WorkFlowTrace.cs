using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Domain
{
    static class WorkFlowTrace
    {
        public static String CreateMSR = "CreateMSR";
        public static String WaitForApprovalMSR = "WaitForApprovalMSR";
        public static String NeedReviewMSR = "NeedReviewMSR";
        public static String ApprovedMSR = "ApprovedMSR";

        public static String StandUser = "StandUser";
        public static String StandBH = "StandBH";
        public static String StandProcurement = "StandProcurement";

        public static String WAIT_FOR_APPROVAL = "WAIT_FOR_APPROVAL";
        public static String APPROVED = "APPROVED";
        public static String NEED_REVIEW = "NEED_REVIEW";
        public static String DECLINED = "DECLINED";
    }
}
