using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Models.EntityFrameworkJoinEntities
{
    public class UserAccountHashTag
    {
        public int HashTagId { get; set; }
        public HashTag HashTag { get; set; }
        public int UserAccountId { get; set; }
        public UserAccount UserAccount { get; set; }

    }
}
