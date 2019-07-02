using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.BusinessAPI
{
    class BusinessSingleton
    {
        //Singleton instance
        private static BusinessSingleton instance;

        //User variables
        public ICollection<Domain.Budget_ActivityInfo> budgetInfo { get; set; }
        public Domain.UserInfo userInfo { get; set; }

        public ICollection<Domain.FormItems> formItemList { get; set; }

        private BusinessSingleton()
        {
            
        }

        public static BusinessSingleton Instance
        {
            //not thread safe???
            get
            {
                if (instance == null)
                {
                    instance = new BusinessSingleton();
                }
                return instance;
            }
        }

        public ICollection<String> GetUniqueBP_List()
        {
            ICollection<String> results = budgetInfo.Select(x => x.Bp_No).Distinct().ToList();

            return results;
        }
       
        public ICollection<String> GetAC_List(string Bp_No)
        {
            ICollection<String> results = (
                                           from item in budgetInfo
                                           where item.Bp_No.Equals(Bp_No)
                                           select item.AC_No
                                           ).ToList();

            return results;
        }

    }
}
