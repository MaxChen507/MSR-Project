using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSR.UserInterfaceAPI
{
    class UserInterfaceSIngleton
    {
        //Singleton instance
        private static UserInterfaceSIngleton instance;

        //User variables


        private UserInterfaceSIngleton()
        {

        }

        public static UserInterfaceSIngleton Instance
        {
            //not thread safe???
            get
            {
                if (instance == null)
                {
                    instance = new UserInterfaceSIngleton();
                }
                return instance;
            }
        }

        public void UpdateBusinessSingletonFormItemList(DataGridView formItemsDGV)
        {
            //Data List Initialization
            ICollection<Domain.FormItems> formItemData_List = new List<Domain.FormItems>();

            //Copy data from data grid view and populate addListData
            foreach (DataGridViewRow row in formItemsDGV.Rows)
            {
                String BudgetPool = row.Cells["BudgetPool"].FormattedValue.ToString();
                String ItemCode = row.Cells["ItemCode"].FormattedValue.ToString();
                String ItemDesc = row.Cells["ItemDesc"].FormattedValue.ToString();
                String Unit = row.Cells["Unit"].FormattedValue.ToString();
                String AC_No = row.Cells["AC_No"].FormattedValue.ToString();

                Domain.FormItems addItem = new Domain.FormItems(BudgetPool, ItemCode, ItemDesc, "1", Unit, "", "", "", "", AC_No);

                formItemData_List.Add(addItem);
            }

            BusinessAPI.BusinessSingleton.Instance.formItemList = formItemData_List;
        }


    }
}