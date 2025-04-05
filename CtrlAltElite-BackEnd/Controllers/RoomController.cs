using CtrlAltElite_BackEnd.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CtrlAltElite_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public RoomController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllJoinedRooms")]
        public async Task<ActionResult<List<Room>>> GetAllJoinedRooms(int id_user)
        {
            var roomIds = await _context.roomsMembers
                .Where(u => u.UserId == id_user)
                .Select(u => u.RoomId)
                .Distinct()
                .ToListAsync();

            var currentRooms = await _context.rooms
                .Where(r => roomIds.Contains(r.Id))
                .ToListAsync();

            return Ok(currentRooms);
        }
        [HttpGet("GetRoom/{id}")]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
            var currentRoom = await _context.rooms.FirstOrDefaultAsync(u => u.Id == id);
            if(currentRoom is null)
            {
                return BadRequest();
            }
            return currentRoom;
        }

        [HttpPost("CreateRoom")]
        public async Task<IActionResult> CreateRoom(Room newRoom)
        {
            if(newRoom is null)
            {
                return BadRequest();
            }
            await _context.rooms.AddAsync(newRoom);
            _context.SaveChanges();
            return Ok();
        }
        [HttpGet("GetCreatedRooms/{id}")]
        public async Task<ActionResult<List<Room>>> GetCreatedRooms(int id)
        {
            var createdRooms = await _context.rooms
                .Where(u => u.CreatorId == id)
                .ToListAsync();
            return Ok(createdRooms);
        }
    }
}
