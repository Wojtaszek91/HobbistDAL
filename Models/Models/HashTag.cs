using Models.Models.EntityFrameworkJoinEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Models
{
    public class HashTag
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string HashTagName { get; set; }
        public int Popularity { get; set; }
        public ICollection<UserAccountHashTag> UserAccountHashTags { get; set; }
    }
}
