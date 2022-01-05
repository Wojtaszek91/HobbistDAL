﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class UserMessage
    {
        public Guid Id { get; set; }
        public Guid SenderProfileId { get; set; }
        public Guid TargetProfileId { get; set; }
        public string Content { get; set; }
        public DateTime SendTime { get; set; }
        public DateTime? RecivedTime { get; set; }
    }
}
