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

        //BudgetInfoAPI Methods
        public List<Domain.BudgetInfo> GetBudgetInfo_List(String DeptId)
        {
            ICollection<Domain.BudgetInfo> budgetInfoData = null;

            SqlParameter deptId_param = new SqlParameter("@deptId", Convert.ToInt32(DeptId));
            
            List<SqlParameter> sqlParametersList = new List<SqlParameter>();
            sqlParametersList.Add(deptId_param);
           
            String sql = "SELECT Bp_No, Ac_No FROM V_BP_AC_DEPT WHERE DeptId = @deptId";

            using (SqlDataReader dataReader = DBAccessSingleton.Instance.MyExecuteQuery(sql, sqlParametersList))
            {
                budgetInfoData = new List<Domain.BudgetInfo>();

                while (dataReader.Read())
                {
                    String bp_No = dataReader["Bp_No"].ToString();
                    String ac_No = dataReader["Ac_No"].ToString();

                    Domain.BudgetInfo temp = new Domain.BudgetInfo(bp_No, ac_No);
                    budgetInfoData.Add(temp);
                }

                dataReader.Close();

            }
            return budgetInfoData.ToList();
        }

        //Where should this method belong?
        public ICollection<Domain.BudgetInfo> GetFilterBudgetInfo_List(ICollection<Domain.BudgetInfo> budgetInfoList, string Bp_No)
        {
            ICollection<Domain.BudgetInfo> results = (
                                           from item in budgetInfoList
                                           where item._Bp_No == Bp_No
                                           select item
                                           ).ToList();

            return results;
        }

    }
}
