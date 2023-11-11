using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
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
        public async Task<IActionResult> CreateTrip(TripServiceInput input)
        {
            try
            {
                var result = await _tripService.CreateNewTripAsync(input);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetTrip(Guid id)
        {
            try
            {
                var result = await _tripService.GetTripAsync(id);
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

        [HttpGet]
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

        [HttpDelete]
        [Route("[action]/{id}")]
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
    }
}