using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Domain
{
    public class BudgetInfo
    {
        public String _Bp_No { get; set; }
        public String _AC_No { get; set; }

        public BudgetInfo(String Bp_No, String AC_No)
        {
            _Bp_No = Bp_No;
            _AC_No = AC_No;
        }

    }
}
