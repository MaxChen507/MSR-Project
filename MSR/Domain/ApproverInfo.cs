using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Domain
{
    class ApproverInfo
    {
        public String UserId { get; set; }
        public String Username { get; set; }
        public String FullName { get; set; }
        public String Email { get; set; }
        public String DeptId { get; set; }

        public ApproverInfo()
        {

        }
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
