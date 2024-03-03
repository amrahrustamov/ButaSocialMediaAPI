using ButaAPI.Database.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ButaAPI.Database.Model
{
    public class Friendships: BaseEntity<int>
    {
        public int User1Id { get; set; }
        public int User2Id { get; set; }
    }
}
