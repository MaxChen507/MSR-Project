using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.DatabaseAPI
{
    class GroupsInfoAPI
    {
        public GroupsInfoAPI()
        {

        }

        //UserInfoAPI Methods
        public Domain.GroupsInfo GetGroupsInfo(String GroupsId)
        {
            Domain.GroupsInfo groupsInfo = null;

            SqlParameter groupsId_param = new SqlParameter("@GroupsId", GroupsId);

            List<SqlParameter> sqlParametersList = new List<SqlParameter>();
            sqlParametersList.Add(groupsId_param);

            String sql = "SELECT * FROM Groups WHERE GroupsId = @GroupsId";

            using (SqlDataReader dataReader = DBAccessSingleton.Instance.MyExecuteQuery(sql, sqlParametersList))
            {

                while (dataReader.Read())
                {
                    String _GroupsId = dataReader["GroupsId"].ToString();
                    String GroupsName = dataReader["GroupsName"].ToString();
                    String GroupsDesc = dataReader["GroupsDesc"].ToString();
                    String GroupsActiveFlag = dataReader["GroupsActiveFlag"].ToString();

                    groupsInfo = new Domain.GroupsInfo(_GroupsId, GroupsName, GroupsDesc, GroupsActiveFlag);
                }

                dataReader.Close();

            }
            return groupsInfo;
        }
    }
}
