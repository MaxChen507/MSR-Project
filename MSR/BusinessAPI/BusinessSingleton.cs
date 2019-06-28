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
        public ICollection<Domain.BudgetInfo> budgetInfo { get; set; }
        public Domain.UserInfo userInfo { get; set; }

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

    }
}
