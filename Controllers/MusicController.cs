using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Music.World.Service;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Music.World.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private readonly IMusicService _service;

        public MusicController(IMusicService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            try
            {
                Log.Information("Retrieve music details");

                var result = await _service.GetMusicDetails();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);

                return StatusCode(500, ex.Message);
            }
        }
    }
}