using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ButaAPI.Database.Model
{
    public class Notifications
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public User Owner { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public bool Read { get; set; } = false;
        public string Content { get; set; }
        public string? URl {  get; set; }
    }
}
