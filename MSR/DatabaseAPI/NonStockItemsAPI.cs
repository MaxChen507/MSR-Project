using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.DatabaseAPI
{
    class NonStockItemsAPI
    {
        public ICollection<Domain.BudgetInfo> GetFilterBudgetInfo_List(ICollection<Domain.BudgetInfo> budgetInfoList, string AcSearchString, string AcDescSearchString)
        {
            ICollection<Domain.BudgetInfo> results = (
                                           from item in budgetInfoList
                                           where item.AC_No.ToLower().Contains(AcSearchString.ToLower()) && item.AC_Desc.ToLower().Contains(AcDescSearchString.ToLower())
                                           select item
                                           ).ToList();

            return results;
        }
    }
}
