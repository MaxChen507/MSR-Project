using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Domain
{
    class StockItems
    {
        public String BudgetPool { get; }
        public String ItemCode { get; }
        public String ItemDesc { get; }
        public String LookUp { get; }
        public String BarCode { get; }
        public String AC_No { get; }
        public String Unit { get; }
        public String ActiveFlag { get; }
        public StockItems(String BudgetPool, String ItemCode, String ItemDesc, String LookUp, String BarCode, String AC_No, String Unit, String ActiveFlag)
        {
            this.BudgetPool = BudgetPool;
            this.ItemCode = ItemCode;
            this.ItemDesc = ItemDesc;
            this.LookUp = LookUp;
            this.BarCode = BarCode;
            this.AC_No = AC_No;
            this.Unit = Unit;
            this.ActiveFlag = ActiveFlag;
        }

    }
}
