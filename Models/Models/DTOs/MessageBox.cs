using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models.DTOs
{
    public class MessageBox
    {
        public Guid TargetProfileId { get; set; }
        public string UserName { get; set; }
        public List<UserMessage> Messages { get; set; }

        public MessageBox(Guid targetUserId, string userName, List<UserMessage> messages)
        {
            TargetProfileId = targetUserId;
            UserName = userName;
            Messages = messages;
        }
    }
}
