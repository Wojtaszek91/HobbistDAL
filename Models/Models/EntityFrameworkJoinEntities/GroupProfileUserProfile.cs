using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Models.EntityFrameworkJoinEntities
{
    public class GroupProfileUserProfile
    {
        public int GroupProfileId { get; set; }
        public GroupProfile GroupProfile { get; set; }
        public int ProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
