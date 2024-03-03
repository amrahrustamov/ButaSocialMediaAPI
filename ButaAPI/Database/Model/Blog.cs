﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ButaAPI.Database.Base;

namespace ButaAPI.Database.Model
{
    public class Blog: BaseEntity<int>
    {
        public string? Content { get; set; }
        public Location? Location { get; set; }
        public List<string>? Tags { get; set; }
        public ICollection<Comment>? Commets { get; set; }
        public ICollection<Like>? Likes { get; set; }
        public int OwnerId { get; set; }
        public User Owner { get; set; }
        public bool IsPublic { get; set; } = true;
        public DateTime DateTime { get; set; } = DateTime.Now;
        public List<string>? Image { get; set; }
    }
}
