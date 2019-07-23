using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.BusinessAPI
{
    class NonStockItemsAPI_B
    {
        public ICollection<Domain.BudgetInfo> GetFilterBudgetInfo_List(ICollection<Domain.BudgetInfo> budgetInfoList, string AcSearchString, string AcDescSearchString)
        {
            List<string> acSearchStringList = AcSearchString.Split(' ').ToList();

            List<string> acDescSearchStringList = AcDescSearchString.Split(' ').ToList();

            ICollection<Domain.BudgetInfo> results = (
                                           from item in budgetInfoList
                                           where
                                                acSearchStringList.All(sTring => item.AC_No.ToUpperInvariant().Contains(sTring.ToUpperInvariant()))
                                                &&
                                                acDescSearchStringList.All(sTring => item.AC_Desc.ToUpperInvariant().Contains(sTring.ToUpperInvariant()))
                                           //item.AC_No.ToUpperInvariant().Contains(AcSearchString.ToUpperInvariant()) && item.AC_Desc.ToUpperInvariant().Contains(AcDescSearchString.ToUpperInvariant())
                                           select item
                                           ).ToList();

            return results;
        }
    }
}
