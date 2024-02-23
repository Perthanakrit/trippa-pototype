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
    [Authorize]
    [Route("api/[controller]")]
    public class PhotosController : ControllerBase
    {
        private readonly IPhotoService _photoService;
        public PhotosController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        [HttpPost("user/addPhoto")]
        public async Task<IActionResult> UserAddPhoto(IFormFile file)
        {
            try
            {
                PhotoUploadOutput res = await _photoService.UserAddPhoto(file, "");
                return Ok(res);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}