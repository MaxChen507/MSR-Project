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
        public ICollection<Domain.ApproverInfo> GetBudgetHolder_List(String Bp_No)
        {
            ICollection<Domain.ApproverInfo> budgetHolderData = null;

            Domain.ApproverInfo approverInfo;

            SqlParameter bp_No_param = new SqlParameter("@bp_No", Convert.ToInt32(Bp_No));

            List<SqlParameter> sqlParametersList = new List<SqlParameter>();
            sqlParametersList.Add(bp_No_param);

            String sql = "SELECT Usr.UserId, Usr.Username, Usr.FullName, Usr.Email, Usr.DeptId FROM BudgetHolder INNER JOIN BudgetPool ON BudgetHolder.BP_No = BudgetPool.BP_No INNER JOIN Usr ON BudgetHolder.UserId = Usr.UserId WHERE BudgetPool.BP_No = @bp_No";

            using (SqlDataReader dataReader = DBAccessSingleton.Instance.MyExecuteQuery(sql, sqlParametersList))
            {
                budgetHolderData = new List<Domain.ApproverInfo>();

                while (dataReader.Read())
                {
                    String UserId = dataReader["UserId"].ToString();
                    String Username = dataReader["Username"].ToString();
                    String FullName = dataReader["FullName"].ToString();
                    String Email = dataReader["Email"].ToString();
                    String DeptId = dataReader["DeptId"].ToString();

                    approverInfo = new Domain.ApproverInfo(UserId, Username, FullName, Email, DeptId);

                    budgetHolderData.Add(approverInfo);
                }

                dataReader.Close();

            }
            return budgetHolderData;
        }

    }
}
