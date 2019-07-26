using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.BusinessAPI
{
    class LoginAPI
    {
        public Boolean ValidateLogin(String username, String password)
        {
            Boolean loginFlag = false;

            using (var context = new MSR_MaxEntities())
            {
                //Log DB commands to console
                context.Database.Log = Console.WriteLine;

                //Retrieve usrs whose username and password match db - Linq-to-Entities Query Syntax
                var users = (from u in context.Usrs
                             where u.Username == username && u.Password == password
                             select u).ToList();

                if (users.Count == 1)
                {
                    loginFlag = true;
                }
                else
                {
                    loginFlag = false;
                }
            }

            return loginFlag;
        }

        public Usr GetUsrByUsername(String username)
        {
            Usr usr = null;

            using (var context = new MSR_MaxEntities())
            {
                //Log DB commands to console
                context.Database.Log = Console.WriteLine;

                //Retrieve usrs whose username and password match db - Linq-to-Entities Query Syntax
                var usr_db = (from u in context.Usrs
                                .Include("Department")
                                .Include("Group")
                                .Include("BudgetInfoes")
                            where u.Username == username
                            select u).FirstOrDefault<Usr>();

                usr = usr_db;

            }

            return usr;
        }

        public ICollection<V_BP_AC_DEPT> GetBudgetInfoAccessByDeptId(int deptId)
        {
            ICollection<V_BP_AC_DEPT> v_BP_DEPT_List = null;

            using (var context = new MSR_MaxEntities())
            {
                //Log DB commands to console
                context.Database.Log = Console.WriteLine;

                var v_BP_DEPT_db = (from v in context.V_BP_AC_DEPT
                                    where v.DeptId == deptId
                                    select v).ToList();

                v_BP_DEPT_List = v_BP_DEPT_db;
            }

            return v_BP_DEPT_List;
        }

        public ICollection<V_BH_BI> GetACAccessByBPList(ICollection<V_BP_AC_DEPT> v_BP_DEPT_List)
        {
            ICollection<V_BH_BI> v_BH_BI_Access_List = null;

            using (var context = new MSR_MaxEntities())
            {
                //Log DB commands to console
                context.Database.Log = Console.WriteLine;

                //Grab the list from context first
                ICollection<V_BH_BI> v_BH_BI_db = context.V_BH_BI.ToList();

                //Perform the match between two lists
                ICollection<V_BH_BI> v_BH_BI_Access_temp = v_BH_BI_db.Where(x =>
                                            v_BP_DEPT_List.Any(y => y.BP_No.Equals(x.BP_No))).ToList();

                v_BH_BI_Access_List = v_BH_BI_Access_temp;
            }

            return v_BH_BI_Access_List;
        }
    }
}
