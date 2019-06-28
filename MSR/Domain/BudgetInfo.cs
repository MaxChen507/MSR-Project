using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Domain
{
    public class BudgetInfo
    {
        public String Bp_No { get; set; }
        public String AC_No { get; set; }

        public BudgetInfo(String Bp_No, String AC_No)
        {
            this.Bp_No = Bp_No;
            this.AC_No = AC_No;
        }

    }
}
