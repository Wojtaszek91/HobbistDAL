﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Models.EntityFrameworkJoinEntities
{
    public class GroupProfileManagers
    {
        public int GroupProfileId { get; set; }
        public GroupProfile GroupProfileManager { get; set; }
        public int UserAccountManagerId { get; set; }
        public UserProfile UserAccountManager { get; set; }
    }
}
