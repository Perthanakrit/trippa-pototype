using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Services;
using Core.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class TripController : ControllerBase
    {
        private readonly ITripService _tripService;

        public TripController(ITripService tripService)
        {
            _tripService = tripService;
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = SD.TourUser)]
        public async Task<IActionResult> CreateTrip(TripServiceInput input)
        {
            try
            {
                await _tripService.CreateNewTripAsync(input);
                return Created("CreateTrip", new { message = "Success" });
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
        [Route("[action]/{id}")]
        [Authorize]
        public async Task<IActionResult> GetTrip(Guid id)
        {
            try
            {
                var result = await _tripService.GetTripAsync(id);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> GetListOfAllTrips()
        {
            try
            {
                var result = await _tripService.GetListOfAllTripsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("[action]/{id}")]
        [Authorize(Policy = "IsHostPolicy", Roles = SD.TourUser)]
        public async Task<IActionResult> UpdateTrip(Guid id, TripServiceInput input)
        {
            try
            {
                var result = await _tripService.UpdateTripAsync(id, input);
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

        [HttpDelete("{id}")]
        [Authorize(Policy = "IsHostPolicy", Roles = SD.TourUser)]
        public async Task<IActionResult> DeleteTrip(Guid id)
        {
            try
            {
                var result = await _tripService.DeleteTripAsync(id);
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

        [HttpPost]
        [Route("[action]/{tripId}")]
        public async Task<IActionResult> UpdateAttendee(Guid tripId)
        {
            try
            {
                await _tripService.UpdateAttendeeAsync(tripId);
                return Ok();
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

        [HttpPost]
        [Authorize(Policy = "IsHostPolicy")]
        [Route("[action]/{tripId}")]
        public async Task<IActionResult> AcceptAttendee(Guid tripId, [FromQuery] string mail)
        {
            try
            {
                await _tripService.AcceptAttendeeAsync(tripId, mail);
                return Ok();
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

        [HttpPost]
        [Authorize(Policy = "IsHostPolicy")]
        [Route("[action]/{tripId}")]
        public async Task<IActionResult> RejectAttendee(Guid tripId, [FromQuery] string mail)
        {
            try
            {
                await _tripService.RejectAttendeeAsync(tripId, mail);
                return Ok();
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