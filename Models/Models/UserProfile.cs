using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Models
{
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public string VideoLink { get; set; }
        public string ProfilePhoto { get; set; }
        public int ProfileViews { get; set; }    
        public int UserAccountId { get; set; }
        [ForeignKey("UserAccountId")]
        public virtual UserAccount UserAccount { get; set; }
        public ICollection<UserAccount> FollowersId { get; set; }
    }
}
