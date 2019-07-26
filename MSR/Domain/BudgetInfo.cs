using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Domain
{
    public class BudgetInfo
    {
        public String BpNo { get; set; }
        public String ACNo { get; set; }
        public String ACDesc { get; set; }

        public BudgetInfo()
        {

        }

        public BudgetInfo(String BpNo, String ACNo, String ACDesc)
        {
            this.BpNo = BpNo;
            this.ACNo = ACNo;
            this.ACDesc = ACDesc;
        }

    }
}
