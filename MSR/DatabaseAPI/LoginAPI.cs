using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.DatabaseAPI
{
    class LoginAPI
    {
        public LoginAPI()
        {

        }

        public Boolean ValidateLogin(String username, String password)
        {
            Boolean loginFlag = false;

            SqlParameter _username = new SqlParameter("@username", username);
            SqlParameter _password = new SqlParameter("@password", password);

            List<SqlParameter> sqlParametersList = new List<SqlParameter>();
            sqlParametersList.Add(_username);
            sqlParametersList.Add(_password);

            String sql = "SELECT COUNT(*) As Count FROM Usr WHERE Username = @username AND Password = @password";

            using (SqlDataReader dataReader = DBAccessSingleton.Instance.MyExecuteQuery(sql, sqlParametersList))
            {
                while (dataReader.Read())
                {
                    if(Convert.ToInt32(dataReader["Count"].ToString()) == 1)
                    {
                        loginFlag = true;
                    }
                    else
                    {
                        loginFlag = false;
                    }
                    
                }

            }

            return loginFlag;
        }

    }
}
