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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrip(Guid id)
        {
            try
            {
                var result = await _tripService.GetTripAsync(id);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrip(Guid id)
        {
            try
            {
                await _tripService.DeleteTripAsync(id);
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

        [HttpPost("{id}/photo")]
        public async Task<IActionResult> AddPhotoToTrip(Guid id, IFormFile file)
        {
            try
            {
                await _tripService.UploadPhotoAsync(id, file);
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

        [HttpPut("{id}")]
        [Authorize(Policy = "IsTripHost")]
        public async Task<IActionResult> UpdateTrip(Guid id, [FromBody] CommuTripInput input)
        {
            try
            {
                await _tripService.UpdateTripAsync(id, input);
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


        [HttpPost("{id}/attend")]
        public async Task<IActionResult> UpdateAttendee(Guid id)
        {
            try
            {
                await _tripService.UpdateAttendeeAsync(id);
                return Ok(new { message = "success" });
            }
            catch (ArgumentException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("{id}/attendee")]
        public async Task<IActionResult> AddAttendee(Guid id, [FromBody] MailInput input)
        {
            try
            {
                await _tripService.AcceptAttendeeAsync(id, input);
                return Ok(new { message = "success" });
            }
            catch (ArgumentException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}/attendee")]
        public async Task<IActionResult> RejectAttendee(Guid id, [FromBody] MailInput input)
        {
            try
            {
                await _tripService.RejectAttendeeAsync(id, input);
                return Ok(new { message = "success" });
            }
            catch (ArgumentException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}