using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.BusinessAPI
{
    class NonStockItemsAPI
    {
        public ICollection<Domain.BudgetInfo> GetFilterBudgetInfoList(ICollection<Domain.BudgetInfo> budgetInfoList, string acSearchString, string acDescSearchString)
        {
            List<string> acSearchStringList = acSearchString.Split(' ').ToList();

            List<string> acDescSearchStringList = acDescSearchString.Split(' ').ToList();

            ICollection<Domain.BudgetInfo> results = (
                                           from item in budgetInfoList
                                           where
                                                acSearchStringList.All(sTring => item.ACNo.ToUpperInvariant().Contains(sTring.ToUpperInvariant()))
                                                &&
                                                acDescSearchStringList.All(sTring => item.ACDesc.ToUpperInvariant().Contains(sTring.ToUpperInvariant()))
                                           //item.AC_No.ToUpperInvariant().Contains(AcSearchString.ToUpperInvariant()) && item.AC_Desc.ToUpperInvariant().Contains(AcDescSearchString.ToUpperInvariant())
                                           select item
                                           ).ToList();

            return results;
        }
    }
}
