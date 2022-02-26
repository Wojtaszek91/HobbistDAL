using Models.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class UserMessage
    {
        [Key]
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string SenderName { get; set; }
        public DateTime SendTime { get; set; }
        public bool HasBeenOpen { get; set; }
        [Required]
        public Guid MessageBoxId { get; set; }
        [ForeignKey("MessageBoxId")]
        public virtual MessageBox MessageBox { get; set; }

        public UserMessage(string content, string senderName)
        {
            Content = content;
            SenderName = senderName;
            SendTime = DateTime.Now;
            HasBeenOpen = false;
        }
    }
}
