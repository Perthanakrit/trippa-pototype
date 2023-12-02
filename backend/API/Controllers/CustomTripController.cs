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

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateCustomTrip(CustomTripServiceInput input)
        {
            try
            {
                var result = await _customTripService.CreateNewTripAsync(input);
                return Ok(new { result, message = "Create successfully" });
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
    }
}