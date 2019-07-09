using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Domain
{
    public class FormItems
    {
        public String BudgetPool { get; }
        public String ItemCode { get; }
        public String ItemDesc { get; }
        public String Quantity { get; }
        public String Unit { get; }
        public String UnitPrice { get; }
        public String Currency { get; }
        public DateTime ROS_Date { get; }
        public String Comments { get; }
        public String AC_No { get; }
        
        public FormItems(String BudgetPool, String ItemCode, String ItemDesc, String Quantity, String Unit, String UnitPrice, String Currency, DateTime ROS_Date, String Comments, String AC_No)
        {
            this.BudgetPool = BudgetPool;
            this.ItemCode = ItemCode;
            this.ItemDesc = ItemDesc;
            this.Quantity = Quantity; 
            this.Unit = Unit;
            this.UnitPrice = UnitPrice;
            this.Currency = Currency;
            this.ROS_Date = ROS_Date;
            this.Comments = Comments;
            this.AC_No = AC_No;
        }

    }
}
