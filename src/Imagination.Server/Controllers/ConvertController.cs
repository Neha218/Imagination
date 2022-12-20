using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using System;
using System.IO;

namespace Imagination.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ConvertController : ControllerBase
    {
        [HttpPost]
        public ActionResult ConvertImage()
        {
            try
            {
                // Accessing file from request
                var inputStream = Request.Body;

                // Converting image file format to JPEG
                using (var outStream = new MemoryStream())
                {
                    var image = Image.Load(inputStream);
                    image.SaveAsJpeg(outStream);
                    Byte[] imageByte = outStream.ToArray();
                    return File(imageByte, "application/octet-stream");
                }
            }
            catch (InvalidImageContentException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid file.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
