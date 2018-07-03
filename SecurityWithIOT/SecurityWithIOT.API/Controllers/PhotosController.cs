using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SecurityWithIOT.API.Helpers;
using SecurityWithIOT.API.Services;
using SecurityWithIOT.API.Data;
using CloudinaryDotNet;
using SecurityWithIOT.API.Dtos;
using System.Threading.Tasks;
using System.Security.Claims;
using CloudinaryDotNet.Actions;
using SecurityWithIOT.API.Model;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace SecurityWithIOT.API.Controllers
{
    [Authorize]
    [Route("api/users/{userId}/photos")]
    public class PhotosController : Controller
    {
        private readonly UserService _userService;
        private readonly PhotoService _photoService;
        private readonly IMapper _mapper;
        private readonly IOptions<ClaudinarySettings> _cloudinaryConfig;

        private Cloudinary _cloudinary;
        public PhotosController(UserService userService, PhotoService photoService, IMapper mapper, IOptions<ClaudinarySettings> cloudinarySettings)
        {
            _cloudinaryConfig = cloudinarySettings;
            _mapper = mapper;
            _userService = userService;
            _photoService = photoService;

            Account acc = new Account(
            _cloudinaryConfig.Value.CloudinaryName,
            _cloudinaryConfig.Value.ApiKey,
            _cloudinaryConfig.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(acc);
        }
        [HttpGet("{id}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id) {
            var photoFromRepo = await _photoService.GetAsync(id);

            var photo = _mapper.Map<PhotoForReturnDto>(photoFromRepo);

            return Ok(photo);

        }
        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser(int userId, PhotosForCreationDto photoDto)
        {
            try
            {
                var user = await _userService.GetAsync(userId);

                if (user == null) return BadRequest("Could not find user");

                var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                if (currentUserId != user.Id) return Unauthorized();

                var file = photoDto.File;
                var uploadResult = new ImageUploadResult();

                if (file.Length > 0)
                {
                    using (var stream = file.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(file.Name, stream),
                            Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                        };
                        uploadResult = _cloudinary.Upload(uploadParams);
                    }
                }
                photoDto.Url = uploadResult.Uri.ToString();
                photoDto.PublicId = uploadResult.PublicId;

                var photo = _mapper.Map<Photo>(photoDto);
                photo.User = user;

                if (!user.Photos.Any(m => m.IsMain)) photo.IsMain = true;

                user.Photos.Add(photo);

                await _userService.SaveAsync();
                var photoToReturn = _mapper.Map<PhotoForReturnDto>(photo); // bu maplemeyi servisi kayıt ettikten sonra yapıyoruz.
                // ki yüklenmiş fotonun id si cloud a o id ile gitsin. Eğer öncesinde yaparsak cloud otomatik uniq id verir.
                // Ve fotoya geri bir istekte bulunursak o id delki fotoyu cloud da bulamaz.
                //return Ok();
                return CreatedAtRoute("GetPhoto", new { id = photo.Id}, photoToReturn);

            }
            catch (System.Exception)
            {

                return BadRequest("Unexpected error has occured while uploading photos");
            }

        }

        public async Task<IActionResult> SetMainPhoto(int userId, int id) {

            try
            {
                 if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))  return Unauthorized();
                var photoFromRepo = await _photoService.GetAsync(id);

                if (photoFromRepo == null) return NotFound();

                if (photoFromRepo.IsMain) return BadRequest("This photo is already main photo");      
                
                var userMainPhoto = _photoService.GetAll().Where(x => x.UserId == userId).FirstOrDefault(x=> x.IsMain == true && !x.IsDelete);
                
                if (userMainPhoto == null) userMainPhoto.IsMain = false;
                
                photoFromRepo.IsMain = true;

                await _photoService.SaveAsync();

                return NoContent();
            }
            catch (System.Exception ex)
            {                
                return BadRequest("Could not set the main photo, error is :" + ex);
            }
           
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto (int userId, int id) {
            
            try
            {
                if ( userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) return Unauthorized();
                var photoFromRepo = await _photoService.GetAsync(id);

                if (photoFromRepo == null) return NotFound();

                if (photoFromRepo.IsMain) return BadRequest("You can not delete the main photo");

                if (photoFromRepo.PublicId != null) {

                    var deletionParams = new DeletionParams(photoFromRepo.PublicId);

                    var result = _cloudinary.Destroy(deletionParams);

                    if (result.Result == "ok") 
                    await  _photoService.DeleteAsync(photoFromRepo);
                    
                    return Ok();

                }
                else {
                    await _photoService.SaveAsync();

                    return Ok();
                }
            }
            catch (System.Exception)
            {
                return BadRequest("Could not delete this photo");
            }
            
        }
    }

}