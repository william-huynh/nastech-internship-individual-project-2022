using Microsoft.AspNetCore.Mvc;
using ClothesShop.SharedVMs;
using ClothesShop.API.Services;
using ClothesShop.SharedVMs.Authenticate;
using AutoMapper;
using ClothesShop.API.Interfaces;
using ClothesShop.API.Models;

namespace ClothesShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _user;
        private IUserService _userService;

        public UsersController(IUserService userService, IMapper mapper, IUserRepository user)
        {
            _userService = userService;
            _mapper = mapper;
            _user = user;
        }

        // Get (all): api/Users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _user.GetAsync();
                if (!users.Any())
                    return NotFound("Users empty!");
                var usersDto = _mapper.Map<List<UserDto>>(users);
                return Ok(usersDto);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong! Error: " + ex.Message);
            }
        }

        // Get (single): api/Users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                var user = await _user.GetByIdAsync(id);
                if (user == null)
                    return NotFound("User not found!");
                var userDto = _mapper.Map<UserDto>(user);
                return Ok(userDto);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong! Error: " + ex.Message);
            }
        }

        // Post: api/Users
        /* [HttpPost]
        public async Task<IActionResult> PostUser(UserDto userCreate)
        {
            try
            {
                var user = _mapper.Map<User>(userCreate);
                var userCreated = await _user.PostAsync(user);
                return Ok(_mapper.Map<UserDto>(userCreated));
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong! Error: " + ex.Message);
            }
        } */

        // Authenticate: api/Users/authenticate
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequestDto model)
        {
            try
            {
                var response = _userService.Authenticate(model);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong! Error: " + ex.Message);
            }
        }

        // Put: api/Users/{id}
        /* [HttpPut]
        public async Task<IActionResult> PutUser(UserDto userUpdate)
        {
            try
            {
                var userChecked = await _user.GetByIdAsync(userUpdate.Id);
                if (userChecked == null)
                    return NotFound("User not found!");
                var user = _mapper.Map<User>(userUpdate);
                var userUpdated = await _user.PutAsync(user);
                return Ok(_mapper.Map<UserDto>(userUpdated));
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong! Error: " + ex.Message);
            }
        }

        // Delete: api/Users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var userChecked = await _user.GetByIdAsync(id);
                if (userChecked == null)
                    return NotFound("User not found!");
                await _user.DeleteAsync(id);
                return Ok("User deleted!");
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong! Error: " + ex.Message);
            }
        } */
    }
}
