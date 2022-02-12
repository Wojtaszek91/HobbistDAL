using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models.DTOs.Post
{
    public class UpsertPostMarkModel
    {
        public Guid PostId { get; set; }
        public Guid UserProfileId { get; set; }
        public int Mark { get; set; }
    }
}
