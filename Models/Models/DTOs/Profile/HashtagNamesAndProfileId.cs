using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models.DTOs.Profile
{
    public class HashtagNamesAndProfileId
    {
        public List<string> HashtagNamesList { get; set; }
        public Guid ProfileId { get; set; }
    }
}
