using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Domain
{
    class StockItems
    {
        public String BudgetPool { get; set; }
        public String ItemCode { get; set; }
        public String ItemDesc { get; set; }
        public String LookUp { get; set; }
        public String BarCode { get; set; }
        public String AC_No { get; set; }
        public String Unit { get; set; }
        public String ActiveFlag { get; set; }

        public StockItems()
        {

        }
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
