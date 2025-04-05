using System.ComponentModel.DataAnnotations;
using CtrlAltElite_BackEnd.Data.DTO;
using Microsoft.AspNetCore.Identity;

namespace CtrlAltElite_BackEnd.Data
{
    public class Room
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; } 

        public string? CreatorId { get; set; } 
    }
}
