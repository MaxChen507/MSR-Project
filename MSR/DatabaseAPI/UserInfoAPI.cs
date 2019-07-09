using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.DatabaseAPI
{
    class UserInfoAPI
    {
        public UserInfoAPI()
        {

        }

        //UserInfoAPI Methods
        public Domain.UserInfo GetUserInfo(String username)
        {
            Domain.UserInfo userInfo = null;

            SqlParameter username_param = new SqlParameter("@username", username);

            List<SqlParameter> sqlParametersList = new List<SqlParameter>();
            sqlParametersList.Add(username_param);

            String sql = "SELECT * FROM Usr WHERE Username = @username";

            using (SqlDataReader dataReader = DBAccessSingleton.Instance.MyExecuteQuery(sql, sqlParametersList))
            {
                while (dataReader.Read())
                {
                    String UserId = dataReader["UserId"].ToString();
                    String Username = dataReader["Username"].ToString();
                    String Password = dataReader["Password"].ToString();
                    String FullName = dataReader["FullName"].ToString();
                    String Title = dataReader["Title"].ToString();
                    String Email = dataReader["Email"].ToString();
                    String ActiveFlag = dataReader["ActiveFlag"].ToString();
                    String DeptId = dataReader["DeptId"].ToString();
                    String GroupsId = dataReader["GroupsId"].ToString();

                    userInfo = new Domain.UserInfo(UserId, Username, Password, FullName, Title, Email, ActiveFlag, DeptId, GroupsId);
                }

                dataReader.Close();

            }
            return userInfo;
        }

    }
}
