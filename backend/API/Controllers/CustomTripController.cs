using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interface.Services;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomTripController : ControllerBase
    {
        private readonly ICustomTripService _customTripService;

        public CustomTripController(ICustomTripService customTripService)
        {
            _customTripService = customTripService;
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetCustomTrip(Guid id)
        {
            try
            {
                var result = await _customTripService.GetTripAsync(id);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}