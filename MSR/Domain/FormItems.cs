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
        public DateTime ROSDate { get; }
        public String Comments { get; }
        public String ACNo { get; }
        
        public FormItems(String BudgetPool, String ItemCode, String ItemDesc, String Quantity, String Unit, String UnitPrice, String Currency, DateTime ROSDate, String Comments, String ACNo)
        {
            this.BudgetPool = BudgetPool;
            this.ItemCode = ItemCode;
            this.ItemDesc = ItemDesc;
            this.Quantity = Quantity; 
            this.Unit = Unit;
            this.UnitPrice = UnitPrice;
            this.Currency = Currency;
            this.ROSDate = ROSDate;
            this.Comments = Comments;
            this.ACNo = ACNo;
        }

    }
}
