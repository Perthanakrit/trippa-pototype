using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CommunityTripController : ControllerBase
    {
        private readonly ICommunityTripService _tripService;
        public CommunityTripController(ICommunityTripService tripService)
        {
            _tripService = tripService;
        }

        [HttpGet]
        public async Task<IActionResult> GetListOfAllTrips()
        {
            try
            {
                var result = await _tripService.GetListOfAllTripsAsync();
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet("{tripId}")]
        public async Task<IActionResult> GetTrip(Guid tripId)
        {
            try
            {
                var result = await _tripService.GetTripAsync(tripId);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewTrip([FromBody] CommuTripInput input)
        {
            try
            {
                await _tripService.CreateNewTripAsync(input);
                return Created("api/CommunityTrip", new { message = "success" });
            }
            catch (ArgumentException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpDelete("{tripId}")]
        public async Task<IActionResult> DeleteTrip(Guid tripId)
        {
            try
            {
                await _tripService.DeleteTripAsync(tripId);
                return Ok(new { message = "success" });
            }
            catch (ArgumentException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost("{tripId}/photo")]
        public async Task<IActionResult> AddPhotoToTrip(Guid tripId, IFormFile file)
        {
            try
            {
                await _tripService.UploadPhotoAsync(tripId, file);
                return Created("api/CommunityTrip", new { message = "success" });
            }
            catch (ArgumentException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut("{tripId}")]
        public async Task<IActionResult> UpdateTrip(Guid tripId, [FromBody] CommuTripInput input)
        {
            try
            {
                await _tripService.UpdateTripAsync(tripId, input);
                return Ok(new { message = "success" });
            }
            catch (ArgumentException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

    }
}