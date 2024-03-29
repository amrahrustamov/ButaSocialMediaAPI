﻿using ButaAPI.Database.Model.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ButaAPI.Database.Base;

namespace ButaAPI.Database.Model
{
    public class Message:BaseEntity<int>,IAuditable
    {
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public MessageStatus Status { get; set; } = MessageStatus.Sent;
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public User Receiver { get; set; }
    }
}
