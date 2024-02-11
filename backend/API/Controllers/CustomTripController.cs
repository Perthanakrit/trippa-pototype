using Core.Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CustomTripController : ControllerBase
    {
        private readonly ICustomTripService _customTripService;

        public CustomTripController(ICustomTripService customTripService)
        {
            _customTripService = customTripService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateCustomTrip(CustomTripServiceInput input)
        {
            try
            {
                await _customTripService.CreateNewTripAsync(input);
                return Created("api/CustomTrip", new { message = "Success" });
            }
            catch (ArgumentException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomTrips()
        {
            try
            {
                var result = await _customTripService.GetListOfAllTripsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
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

        [HttpPut]
        [Route("[action]/{id}")]
        public async Task<IActionResult> UpdateCustomTrip(Guid id, CustomTripServiceInput input)
        {
            try
            {
                var result = await _customTripService.UpdateTripAsync(id, input);
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

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCustomTrip(Guid id)
        {
            try
            {
                await _customTripService.DeleteTripAsync(id);
                return Ok(new { message = "Success" });
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