using ClothesShop.SharedVMs.Authenticate;
using ClothesShop.SharedVMs;
using ClothesShop.API.Data;
using ClothesShop.API.Authorization;
using BCrypt.Net;
using ClothesShop.API.Helpers;
using AutoMapper;
using ClothesShop.Helpers;
using Microsoft.Extensions.Options;
using ClothesShop.SharedVMs.Enum;
using ClothesShop.API.Models;

namespace ClothesShop.API.Services
{
    public interface IUserService
    {
        AuthenticateResponseDto Authenticate(AuthenticateRequestDto model);
        UserDto Register(RegisterRequestDto model);
        IEnumerable<UserDto> GetAll();
        UserDto GetById(int id);
    }

    public class UserService : IUserService
    {
        private ClothesDbContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UserService(ClothesDbContext context, IJwtUtils jwtUtils, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        public AuthenticateResponseDto Authenticate(AuthenticateRequestDto model)
        {
            try
            {
                var user = _context.Users.SingleOrDefault(x => x.Username == model.Username);

                // Validate
                if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                    return null;

                var userDto = _mapper.Map<UserDto>(user);

                // Authentication successful so generate jwt token
                var jwtToken = _jwtUtils.GenerateJwtToken(userDto);

                return new AuthenticateResponseDto(userDto, jwtToken);
            }
            catch
            {
                return null;
            }
        }

        public UserDto Register (RegisterRequestDto model)
        {
            try
            {
                var userDto = _mapper.Map<UserDto>(model);
                userDto.IsDeleted = false;
                userDto.Role = Role.Customer;
                userDto.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

                _context.Users.Add(_mapper.Map<User>(userDto));
                _context.SaveChanges();

                // Authentication successful so generate jwt token
                var jwtToken = _jwtUtils.GenerateJwtToken(userDto);

                return userDto;
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<UserDto> GetAll()
        {
            return _mapper.Map<IEnumerable<UserDto>>(_context.Users);
        }

        public UserDto GetById(int id)
        {
            var user = _context.Users.Find(id);
            var userDto = _mapper.Map<UserDto>(user);
            if (userDto == null)
                throw new KeyNotFoundException("User not found");
            return userDto;
        }
    }
}
