using Models.Models.DTOs.Profile;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Models.DTOs
{
    public class UserProfileViewModel
    {
        public UpsertProfileDto UserProfle { get; set; }
        public IEnumerable<string> HashTagNames { get; set; }
    }
}
