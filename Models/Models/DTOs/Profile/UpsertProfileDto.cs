using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models.DTOs.Profile
{
    public class UpsertProfileDto
    {
        public string Username { get; set; }
        public string Description { get; set; }
        public string VideoLink { get; set; }
        public string ProfilePhoto { get; set; }
        public Guid ProfileId { get; set; }
    }
}
