using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFirstApi.API.Models.Domain;
using MyFirstApi.API.Models.DTO;
using MyFirstApi.API.Repositories;

namespace MyFirstApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        // POST
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);
            if (ModelState.IsValid) 
            {

                //conver dto to domain model
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription,
                };
                //user reppository to upload image
                await imageRepository.Upload(imageDomainModel);

                return Ok(imageDomainModel);
            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDto request) 
        {
            var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };
            if (allowedExtensions.Contains(Path.GetExtension(request.File.FileName))==false)
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }
            if(request.File.Length > 10485760) // if > 10MB dont accept
            {
                ModelState.AddModelError("file", "File size  more than 10MB");
            }
        }
    }
}
