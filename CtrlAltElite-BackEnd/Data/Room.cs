using System.ComponentModel.DataAnnotations;

namespace CtrlAltElite_BackEnd.Data
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CreatorId { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
