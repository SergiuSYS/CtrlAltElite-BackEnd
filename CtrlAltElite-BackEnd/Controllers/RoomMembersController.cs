using CtrlAltElite_BackEnd.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CtrlAltElite_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomMembersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public RoomMembersController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost("AddUserToRoom")]
        public async Task<IActionResult> AddUserToRoom(RoomMembers newMember)
        {
            if(newMember is null)
            {
                return BadRequest();
            }
            await _context.roomsMembers.AddAsync(newMember);
            await _context.SaveChangesAsync();
            
            return Ok();
        }
    }
}
