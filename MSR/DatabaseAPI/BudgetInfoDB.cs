using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.DatabaseAPI
{
    public class BudgetInfoDB
    {
        public String _Bp_No { get; set; }
        public String _AC_No { get; set; }

        public BudgetInfoDB(String Bp_No, String AC_No)
        {
            _Bp_No = Bp_No;
            _AC_No = AC_No;
        }
    }
}
