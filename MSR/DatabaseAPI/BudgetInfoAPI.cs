using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.DatabaseAPI
{
    class BudgetInfoAPI
    {
        public BudgetInfoAPI()
        {

        }

        //Bp_Ac_API Methods
        public List<BudgetInfoDB> GetBudgetInfo_List(String DeptId)
        {
            ICollection<BudgetInfoDB> budgetInfoData = null;

            SqlParameter deptId_param = new SqlParameter("@deptId", Convert.ToInt32(DeptId));
            
            List<SqlParameter> sqlParametersList = new List<SqlParameter>();
            sqlParametersList.Add(deptId_param);
           
            String sql = "SELECT Bp_No, Ac_No FROM V_BP_AC_DEPT WHERE DeptId = @deptId";

            using (SqlDataReader dataReader = DBAccessSingleton.Instance.MyExecuteQuery(sql, sqlParametersList))
            {
                budgetInfoData = new List<BudgetInfoDB>();

                while (dataReader.Read())
                {
                    String bp_No = dataReader["Bp_No"].ToString();
                    String ac_No = dataReader["Ac_No"].ToString();

                    BudgetInfoDB temp = new BudgetInfoDB(bp_No, ac_No);
                    budgetInfoData.Add(temp);
                }

                dataReader.Close();

            }
            return budgetInfoData.ToList();
        }

        public ICollection<BudgetInfoDB> GetFilterBudgetInfo_List(ICollection<BudgetInfoDB> budgetInfoList, string filterString)
        {
            ICollection<BudgetInfoDB> results = (
                                           from item in budgetInfoList
                                           where item._Bp_No == filterString
                                           select item
                                           ).ToList();

            return results;
        }
    }
}
