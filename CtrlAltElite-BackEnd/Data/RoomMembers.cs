using System.ComponentModel.DataAnnotations;

namespace CtrlAltElite_BackEnd.Data
{
    public class RoomMembers
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int RoomId { get; set; } 
        [Required]
        public int UserId { get; set; }

    }
}
