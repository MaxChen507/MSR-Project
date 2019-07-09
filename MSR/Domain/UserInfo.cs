using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Domain
{
    class UserInfo
    {
        public String UserId { get; }
        public String Username { get; }
        public String Password { get; }
        public String FullName { get; }
        public String Title { get; }
        public String Email { get; }
        public String ActiveFlag { get; }
        public String DeptId { get; }
        public String GroupsId { get; }
        public UserInfo(String UserId, String Username, String Password, String FullName, String Title, String Email, String ActiveFlag, String DeptId, String GroupsId)
        {
            this.UserId = UserId;
            this.Username = Username;
            this.Password = Password;
            this.FullName = FullName;
            this.Title = Title;
            this.Email = Email;
            this.ActiveFlag = ActiveFlag;
            this.DeptId = DeptId;
            this.GroupsId = GroupsId;
        }
    }
}
