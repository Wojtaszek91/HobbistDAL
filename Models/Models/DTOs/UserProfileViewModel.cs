using Models.Models.EntityFrameworkJoinEntities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Models.DTOs
{
    public class UserProfileViewModel
    {
        public ProfileDto UserProfle { get; set; }
        public IEnumerable<string> HashTags { get; set; }
    }
}
