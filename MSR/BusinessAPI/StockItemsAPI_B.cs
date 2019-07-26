using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.BusinessAPI
{
    class StockItemsAPI
    {
        public ICollection<Domain.StockItems> GetStockItemList(String bpNo)
        {
            ICollection<Domain.StockItems> stockItemData = null;

            using (var context = new MSR_MaxEntities())
            {
                //Log DB commands to console
                context.Database.Log = Console.WriteLine;

                stockItemData = new List<Domain.StockItems>();

                var stockItemData_db = (from s in context.V_StockItem_BudgetInfo
                                        where s.BP_No.ToString().Equals(bpNo)
                                        select new Domain.StockItems { BudgetPool = s.BP_No, ItemCode = s.ItemCode, ItemDesc = s.ItemDesc, LookUp = s.LookUp, BarCode = s.BarCode, ACNo = s.AC_No, Unit = s.Unit, ActiveFlag = s.ActiveFlag.ToString() } ).ToList();

                stockItemData = stockItemData_db;
            }

            return stockItemData;
        }

        public ICollection<Domain.StockItems> GetFilterStockItemList(ICollection<Domain.StockItems> stockItemList, string searchString, string lookUpString)
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

        public ICollection<String> GetLookUpCodeList(ICollection<Domain.StockItems> stockItemList)
        {
            ICollection<String> results = (
                                from item in stockItemList
                                select item.LookUp
                                ).Distinct().ToList();

            return results;
        }
    }
}
