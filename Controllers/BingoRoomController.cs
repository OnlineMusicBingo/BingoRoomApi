using BingoRoomApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace BingoRoomApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BingoRoomController : Controller
    {

        private readonly BingoRoomService _bingoRoomService;

        public BingoRoomController(BingoRoomService bingoRoomService) =>
            _bingoRoomService = bingoRoomService;

        [HttpGet]
        public async Task<List<BingoRoom>> Get() =>
            await _bingoRoomService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<BingoRoom>> Get(string id)
        {
            var bingoRoom = await _bingoRoomService.GetAsync(id);

            if (bingoRoom is null)
            {
                return NotFound();
            }

            return bingoRoom;
        }

        [HttpPost]
        public async Task<IActionResult> Post(BingoRoom newBingoRoom)
        {
            var userId = Request.Headers["caller_id"];

            newBingoRoom.OwnerId= userId.ToString();
            await _bingoRoomService.CreateAsync(newBingoRoom);

            return CreatedAtAction(nameof(Get), new { id = newBingoRoom.Id }, newBingoRoom);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, BingoRoom updatedBingoRoom)
        {
            var bingoRoom = await _bingoRoomService.GetAsync(id);

            if (bingoRoom is null)
            {
                return NotFound();
            }

            updatedBingoRoom.Id = bingoRoom.Id;
            updatedBingoRoom.OwnerId = bingoRoom.OwnerId;
            updatedBingoRoom.Participants = bingoRoom.Participants;

            await _bingoRoomService.UpdateAsync(id, updatedBingoRoom);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = Request.Headers["caller_id"];

            var bingoRoom = await _bingoRoomService.GetAsync(id);

            if (bingoRoom != null)
            {
               if (bingoRoom.OwnerId != userId.ToString())
                {
                    return Unauthorized();
                }
            }
            else if (bingoRoom is null)
            {
                return NotFound();
            }

            await _bingoRoomService.RemoveAsync(id);

            return NoContent();
        }
    }
}
