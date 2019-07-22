using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.BusinessAPI
{
    class StockItemsAPI_B
    {
        public ICollection<Domain.StockItems> GetFilterStockItem_List(ICollection<Domain.StockItems> stockItemList, string searchString, string lookUpString)
        {

            List<string> searchStringList = searchString.Split(' ').ToList();

            List<string> lookUpStringList = lookUpString.Split(' ').ToList();

            ICollection<Domain.StockItems> results = (
                                           from item in stockItemList
                                           where
                                                searchStringList.All(sTring => item.ItemDesc.ToUpperInvariant().Contains(sTring.ToUpperInvariant()))
                                                &&
                                                lookUpStringList.All(sTring => item.LookUp.ToUpperInvariant().Contains(sTring.ToUpperInvariant()))
                                           //where item.ItemDesc.ToLower().Contains(searchString.ToUpperInvariant()) && item.LookUp.Contains(lookUpString)
                                           select item
                                           ).ToList();

            return results;
        }

        public ICollection<String> GetLookUpCode_List(ICollection<Domain.StockItems> stockItemList)
        {
            ICollection<String> results = (
                                from item in stockItemList
                                select item.LookUp
                                ).Distinct().ToList();

            return results;
        }
    }
}
