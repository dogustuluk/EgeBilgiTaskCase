using EgeBilgiTaskCase.Application.Abstractions.Services.Character;
using EgeBilgiTaskCase.Persistence.Services.Characters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EgeBilgiTaskCase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpPost("location")]
        public async Task<IActionResult> GetLocation()
        {
            try
            {
                var characters = _locationService.SaveAllLocationToDatabase();
                return Ok(characters);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}
