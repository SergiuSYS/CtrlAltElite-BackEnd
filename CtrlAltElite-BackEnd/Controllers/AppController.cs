using System.Security.Claims;
using CtrlAltElite_BackEnd.Data;
using CtrlAltElite_BackEnd.Data.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CtrlAltElite_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AppController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize]
        [HttpGet("GetCreatedRoomsbyUserId")]
        public async Task<ActionResult<List<Room>>> GetCreatedRoomsByUserId()
        {
            var MyId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (MyId is null)
            {
                return BadRequest("User not found.");
            }
            var currentRooms = await _context.rooms.Where(u => u.CreatorId == MyId.ToString()).ToListAsync();
            return Ok(currentRooms);
        }

        [Authorize]
        [HttpGet("GetJoinedRoomsByUserId")]
        public async Task<ActionResult<List<Room>>> GetJoinedRoomsByUserId()
        {
            var MyId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (MyId is null)
            {
                return BadRequest("User not found.");
            }
            var roomsId = await _context.roomUserAssociation
                .Where(u => u.UserId == MyId.ToString())
                .Select(u => u.RoomId)
                .Distinct()
                .ToListAsync();

            var currentRooms = await _context.rooms
                .Where(u => roomsId.Contains(u.Id))
                .ToListAsync();

            return Ok(currentRooms);
        }

        [Authorize]
        [HttpPost("CreateRoom")]
        public async Task<IActionResult> AddRoom(Room newRoom)
        {
            if (newRoom is null)
            {
                return BadRequest("Invalid room data.");
            }
            var creatorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var creator = await _context.Users.FirstOrDefaultAsync(u => u.Id == creatorId);
            if (creator is null)
            {
                return BadRequest("User not found.");
            }
            var room = new Room
            {
                Name = newRoom.Name,
                Description = newRoom.Description,
                CreatorId = creator.Id,

            };
            var roomUserAssociation = new RoomUserAssociation
            {
                Room = room,
                UserId = creator.Id  
            };
            await _context.rooms.AddAsync(room);
            await _context.roomUserAssociation.AddAsync(roomUserAssociation);
            await _context.SaveChangesAsync();
            var data = new { message = "new" };
            return Ok(data);
        }

        [Authorize]
        [HttpDelete("DeleteRoom/{roomId}")]
        public async Task<IActionResult> DeleteRoom(int roomId)
        {
            var MyId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (MyId is null) 
            {
                return BadRequest("You are not logged in");
            }

            var room = await _context.rooms.FirstOrDefaultAsync(u => u.Id == roomId);
            if (room is null) 
            {
                return BadRequest("The room dosen t exist");
            }
            if(room.CreatorId != MyId)
            {
                return BadRequest("you don t have permision to delete");
            }
            _context.rooms.Remove(room);
            await _context.SaveChangesAsync();
            return Ok("Room deleted");
        }

        [Authorize]
        [HttpPost]
        [Route("JoinRoom/{roomId}")]
        public async Task<IActionResult> JoinRoom(int roomId)
        {
            var MyId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (MyId is null)
            {
                return BadRequest("You are not logged in");
            }
            var room = await _context.rooms.FirstOrDefaultAsync(u => u.Id == roomId);
            if (room is null)
            {
                return BadRequest("The room does not exist");
            }
            var alreadyJoined = await _context.roomUserAssociation.AnyAsync(r => r.RoomId == roomId && r.UserId == MyId);

            if (alreadyJoined)
            {
                return BadRequest("You are already in this room");
            }

            var roomUserAssociation = new RoomUserAssociation
            {
                Room = room,
                RoomId = room.Id,
                UserId = MyId
            };
            _context.roomUserAssociation.Add(roomUserAssociation);
            await _context.SaveChangesAsync();
            return Ok("You have joined: " + room.Name );
        }

        [HttpGet("GetUerDetails/{userId}")]
        public async Task<ActionResult<userDto>> GetUserDetails(string userId)
        {
            var currentUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (currentUser is null)
            {
                return BadRequest("User not found.");
            }
            userDto userDto = new userDto
            {
                Id = currentUser.Id,
                Email = currentUser.Email
            };
            return Ok(userDto);
        }
        [HttpGet("getRoomUsersById/{roomId}")]
        public async Task<ActionResult<List<userDto>>> getRoomUsersById(int roomId)
        {
            var room = await _context.rooms.FirstOrDefaultAsync(u => u.Id == roomId);
            if(room is null)
            {
                return BadRequest("room dose not exist");
            }
            var associations = await _context.roomUserAssociation.Where(u => u.RoomId == roomId).ToArrayAsync();
            List<userDto> users = new List<userDto>();
            foreach(var obj in associations)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == obj.UserId);
                if (user != null)
                {
                    users.Add(new userDto
                    {
                        Id = user.Id,
                        Email = user.Email
                    });
                }
            }
            return Ok(users);
        }
        [HttpGet("GetCurrentUserId")]
        [Authorize]
        public IActionResult GetCurrentUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }
            return Ok(new { userId });
        }

    }
}
