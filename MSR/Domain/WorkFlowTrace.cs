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
        public static String waitForApproval = "waitForApproval";
        public static String needReview = "needReview";
        public static String approvedMSR = "approvedMSR";

        public static String StandUser = "StandUser";
        public static String StandBH = "StandBH";
    }
}
