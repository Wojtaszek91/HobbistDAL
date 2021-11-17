using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Models.EntityFrameworkJoinEntities
{
    public class GroupProfileUserAccount
    {
        public int GroupProfileId { get; set; }
        public GroupProfile GroupProfile { get; set; }
        public int UserAccountId { get; set; }
        public UserAccount UserAccount { get; set; }
    }
}
