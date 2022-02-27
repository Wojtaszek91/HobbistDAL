using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.Models.Entities
{
    public class MessageBox
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ProfileOneId { get; set; }
        public string ProfileOneUsername { get; set; }
        public Guid ProfileTwoId { get; set; }
        public string ProfileTwoUsername { get; set; }
        public ICollection<UserMessage> MessageHistory { get; set; }

        public MessageBox(Guid profileOneId, string profileOneUsername, Guid profileTwoId, string profileTwoUsername)
        {
            ProfileOneId = profileOneId;
            ProfileOneUsername = profileOneUsername;
            ProfileTwoId = profileTwoId;
            ProfileTwoUsername = profileTwoUsername;
            MessageHistory = new List<UserMessage>();
        }
        public MessageBox()
        {

        }
    }
}
