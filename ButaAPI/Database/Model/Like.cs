﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using ButaAPI.Database.Base;

namespace ButaAPI.Database.Model
{
    public class Like : BaseEntity<int>
    {
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public int OwnerId { get; set; }
        public User Owner { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
