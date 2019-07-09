using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Domain
{
    class GroupsInfo
    {
        public String GroupsId { get; }
        public String GroupsName { get; }
        public String GroupsDesc { get; }
        public String GroupsActiveFlag { get; }
        public GroupsInfo(String GroupsId, String GroupsName, String GroupsDesc, String GroupsActiveFlag)
        {
            this.GroupsId = GroupsId;
            this.GroupsName = GroupsName;
            this.GroupsDesc = GroupsDesc;
            this.GroupsActiveFlag = GroupsActiveFlag;
        }
    }
}
