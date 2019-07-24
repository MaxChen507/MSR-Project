using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Domain
{
    static class WorkFlowTrace
    {
        public static String createMSR = "createMSR";
        public static String waitForApprovalMSR = "waitForApprovalMSR";
        public static String needReviewMSR = "needReviewMSR";
        public static String approvedMSR = "approvedMSR";

        public static String StandUser = "StandUser";
        public static String StandBH = "StandBH";

        public static String WAIT_FOR_APPROVAL = "WAIT_FOR_APPROVAL";
        public static String APPROVED = "APPROVED";
        public static String NEED_REVIEW = "NEED_REVIEW";
        public static String DECLINED = "DECLINED";
    }
}
