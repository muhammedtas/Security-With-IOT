using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SecurityWithIOT.API.Data;
using SecurityWithIOT.API.Dtos;
using SecurityWithIOT.API.Model;

namespace SecurityWithIOT.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _config;

        private readonly IMapper _mapper;

        public AuthController(IAuthRepository authRepository, IConfiguration config, IMapper mapper)
        {
            this._authRepository = authRepository;
            this._config = config;
            this._mapper = mapper;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserForRegisterDto userForRegisterDto)
        {

            if (!string.IsNullOrEmpty(userForRegisterDto.Username))
                userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            if (await _authRepository.Exist(userForRegisterDto.Username))
                ModelState.AddModelError("Username", "Username already exists...");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            // var userToCreate = new User
            // {
            //     Username = userForRegisterDto.Username,
            // };

            var userToCreate = _mapper.Map<User>(userForRegisterDto);  // 128

            var createdUser = await _authRepository.Register(userToCreate, userForRegisterDto.Password);


            //return StatusCode(201); // şimdilik bunu dönüyoruz. Daha sonra bir callbackroute sayfası yapıp, giren kullanıcıyı oraya yönlendireceğiz.

            var userToReturn = _mapper.Map<UserForDetailDto>(createdUser); // 128

            return CreatedAtRoute("GetUser", new { Controller = "Users", id = createdUser.Id }, userToReturn);  // 128
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserForLoginDto UserForLoginDto)
        {

            //throw new Exception("Error Handling");

            var userFromRepo = await _authRepository.Login(UserForLoginDto.Username.ToLower(), UserForLoginDto.Password);

            if (userFromRepo == null) return Unauthorized();

            var tokenHandler = new JwtSecurityTokenHandler();

            //var key = Encoding.ASCII.GetBytes("super secret key");

            var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {

                    new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                    new Claim(ClaimTypes.Name, userFromRepo.Username)

                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // Bu return tipi değiştirildi 111. dersin başında.
            
            var user = _mapper.Map<UserForListDto>(userFromRepo);

            return Ok(new { tokenString, user });

        }

    }
}