using System.ComponentModel.DataAnnotations;
using CtrlAltElite_BackEnd.Data.DTO;
using Microsoft.AspNetCore.Identity;

namespace CtrlAltElite_BackEnd.Data
{
    public class RoomUserAssociation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int RoomId { get; set; }
        public Room Room { get; set; }

        [Required]
        public string UserId { get; set; }  

    }
}
