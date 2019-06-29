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
        public ICollection<Domain.Budget_ActivityInfo> GetBudgetInfo_List(String DeptId)
        {
            ICollection<Domain.Budget_ActivityInfo> budgetInfoData = null;

            SqlParameter deptId_param = new SqlParameter("@deptId", Convert.ToInt32(DeptId));
            
            List<SqlParameter> sqlParametersList = new List<SqlParameter>();
            sqlParametersList.Add(deptId_param);
           
            String sql = "SELECT Bp_No, Ac_No FROM V_BP_AC_DEPT WHERE DeptId = @deptId";

            using (SqlDataReader dataReader = DBAccessSingleton.Instance.MyExecuteQuery(sql, sqlParametersList))
            {
                budgetInfoData = new List<Domain.Budget_ActivityInfo>();

                while (dataReader.Read())
                {
                    String bp_No = dataReader["Bp_No"].ToString();
                    String ac_No = dataReader["Ac_No"].ToString();

                    Domain.Budget_ActivityInfo temp = new Domain.Budget_ActivityInfo(bp_No, ac_No);
                    budgetInfoData.Add(temp);
                }

                dataReader.Close();

            }
            return budgetInfoData;
        }

        public ICollection<String> GetBudgetHolder_List(String Bp_No)
        {
            ICollection<String> budgetHolderData = null;

            SqlParameter bp_No_param = new SqlParameter("@bp_No", Convert.ToInt32(Bp_No));

            List<SqlParameter> sqlParametersList = new List<SqlParameter>();
            sqlParametersList.Add(bp_No_param);

            String sql = "SELECT Usr.Username FROM BudgetHolder INNER JOIN BudgetPool ON BudgetHolder.BP_No = BudgetPool.BP_No INNER JOIN Usr ON BudgetHolder.UserId = Usr.UserId WHERE BudgetPool.BP_No = @bp_No";

            using (SqlDataReader dataReader = DBAccessSingleton.Instance.MyExecuteQuery(sql, sqlParametersList))
            {
                budgetHolderData = new List<String>();

                while (dataReader.Read())
                {
                    String username = dataReader["Username"].ToString();

                    budgetHolderData.Add(username);
                }

                dataReader.Close();

            }
            return budgetHolderData;
        }

    }
}
