using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models.DTOs.Post
{
    public class PostIdFollowerIdModel
    {
        public Guid PostId { get; set; }
        public Guid FollowerId { get; set; }
    }
}
