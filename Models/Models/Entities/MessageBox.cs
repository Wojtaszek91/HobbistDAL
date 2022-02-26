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
        public Guid ProfileTwoId { get; set; }
        public ICollection<UserMessage> MessageHistory { get; set; }

        public MessageBox(Guid profileOneId, Guid profileTwoId)
        {
            ProfileOneId = profileOneId;
            ProfileTwoId = profileTwoId;
            MessageHistory = new List<UserMessage>();
        }
        public MessageBox()
        {

        }
    }
}
