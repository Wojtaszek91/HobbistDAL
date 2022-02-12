using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models.Entities
{
    public class PostMark
    {
        [Key, Column(Order = 0)]
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        [Key, Column(Order = 1)]
        public Guid UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        [Range(0, 3)]
        public int Mark { get; set; }
    }
}
