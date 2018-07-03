using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecurityWithIOT.API.Data;
using SecurityWithIOT.API.Dtos;
using SecurityWithIOT.API.Helpers;
using SecurityWithIOT.API.Model;
using SecurityWithIOT.API.Model.Interfaces;

namespace SecurityWithIOT.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        //public DataContext _context { get; }

        private readonly IUser _userService;

        public readonly IMapper _mapper;

        public UsersController(IUser userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
            // _context = context;
        }
        // GET api/users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            // var value = await _context.Users.ToListAsync();
            try
            {
            var users = await _userService.GetAll()
            .Include(x=>x.Photos)
            .Include(x=>x.Department)
            .Include(x=>x.Company)
            .Include(x=>x.Addresses)
            .ToListAsync();
            var userToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);
            return Ok(userToReturn);
            }
            catch (System.Exception ex)
            {
             return BadRequest(ex);
            }     
        }

        // GET api/users/5
        [HttpGet("{id}" , Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
            var user = await _userService.GetAll()
            .Include(x=>x.Photos)
            .Include(x=>x.Department)
            .Include(x=>x.Company)
            .Include(x=>x.Addresses)
            .FirstOrDefaultAsync(x=>x.Id == id);
            var userToReturn = _mapper.Map<UserForDetailDto>(user);
            //return Ok(await _context.Users.FirstOrDefaultAsync(x => x.Id == id));
            return Ok(userToReturn);
            }
            catch (System.Exception ex)
            {
                return BadRequest("Error in getUser method" + ex);
            }
            
        }

        // PUT api/users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserForUpdateDto userForUpdateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userFromRepo = await _userService.GetAsync(currentUserId);

            if (userFromRepo == null) return NotFound($"Could not find user with an ID of {id}");
           
           if (currentUserId != userFromRepo.Id) return Unauthorized();
           
           _mapper.Map(userForUpdateDto, userFromRepo);

           if (await _userService.SaveAsync() > 0)
           {
               return NoContent();
           }
           throw new Exception($"Updating user {id} failed on save"); // Controller içerisinde exception fırlattık. Olacak şey değil monakke.
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = _userService.Get(id);
            await _userService.DeleteAsync(user);
            return Ok(await _userService.SaveAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            //_context.Add(user);
            _userService.Add(user);
            return Ok(await _userService.SaveAsync());
        }
    }
}