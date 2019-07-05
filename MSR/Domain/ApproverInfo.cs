using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Domain
{
    class ApproverInfo
    {
        public String UserId { get; }
        public String Username { get; }
        public String FullName { get; }
        public String Email { get; }
        public String DeptId { get; }
        public ApproverInfo(String UserId, String Username, String FullName, String Email, String DeptId)
        {
            this.UserId = UserId;
            this.Username = Username;
            this.FullName = FullName;
            this.Email = Email;
            this.DeptId = DeptId;
        }
    }
}
