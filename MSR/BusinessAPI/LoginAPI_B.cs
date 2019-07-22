using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.BusinessAPI
{
    class LoginAPI_B
    {
        public Boolean ValidateLogin_B(String username, String password)
        {
            Boolean loginFlag = false;

            using (var context = new MSR_Max_V2Entities())
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

            using (var context = new MSR_Max_V2Entities())
            {
                //Log DB commands to console
                context.Database.Log = Console.WriteLine;

                //Retrieve usrs whose username and password match db - Linq-to-Entities Query Syntax
                var user = (from u in context.Usrs
                                .Include("Department")
                                .Include("Group")
                                .Include("BudgetInfoes")
                            where u.Username == username
                            select u).FirstOrDefault<Usr>();

                usr = user;

            }

            return usr;
        }

    }
}
