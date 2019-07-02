using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Domain
{
    class FormItems
    {
        public String ItemCode { get; }
        public String ItemDesc { get; }
        public String Quantity { get; }
        public String Unit { get; }
        public String UnitPrice { get; }
        public String Currency { get; }
        public String AC_No { get; }
        
        
        public FormItems(String ItemCode, String ItemDesc, String Quantity, String Unit, String UnitPrice, String Currency, String AC_No)
        {
            this.ItemCode = ItemCode;
            this.ItemDesc = ItemDesc;
            this.Quantity = Quantity;
            this.Unit = Unit;
            this.UnitPrice = UnitPrice;
            this.Currency = Currency;
            this.AC_No = AC_No;
        }
    }
}
