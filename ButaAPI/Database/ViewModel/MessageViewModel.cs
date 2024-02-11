using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ButaAPI.Database.ViewModel
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
    }
}
