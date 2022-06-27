using Microsoft.AspNetCore.Authorization;
using ClothesShop.SharedVMs.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClotheShop.API.Data;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ClotheShop.SharedVMs.Users;
using AutoMapper;
using ClothesShop.SharedVMs.Enum;

namespace ClothesShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ClothesDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public LoginController(ClothesDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        //[AllowAnonymous]
        //[HttpPost]
        //public IActionResult Login([FromBody] UserLogin userLogin)
        //{
        //    var user = Authenticate(userLogin);

        //    if (user != null)
        //    {
        //        var token = Generate(user);
        //        return Ok(token);
        //    }

        //    return NotFound("User not found");
        //}

        //private string Generate(UserModel user)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SymmetricKey"]));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var claims = new[]
        //    {
        //        new Claim(ClaimTypes.NameIdentifier, user.Username),
        //        new Claim(ClaimTypes.Email, user.EmailAddress),
        //        new Claim(ClaimTypes.GivenName, user.GivenName),
        //        new Claim(ClaimTypes.Surname, user.Surname),
        //        new Claim(ClaimTypes.Role, user.Role)
        //    };

        //    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
        //      _configuration["Jwt:Audience"],
        //      claims,
        //      expires: DateTime.Now.AddMinutes(15),
        //      signingCredentials: credentials);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        //private UserModel Authenticate(UserLogin userLogin)
        //{
        //    var currentUser = UserConstants.Users.FirstOrDefault(o => o.Username.ToLower() == userLogin.Username.ToLower() && o.Password == userLogin.Password);

        //    if (currentUser != null)
        //    {
        //        return currentUser;
        //    }

        //    return null;
        //}

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginReadDto _oLoginModel)
        {
            if (_oLoginModel != null)
            {
                var _oUser = Authenticate(_oLoginModel);

                var _oAuthClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, _oUser.Username),
                    new Claim(ClaimTypes.Role, _oUser.Role), 
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToString())
                };

                var token = CreateToken(_oAuthClaims);

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            else
            {
                return BadRequest();
            }
        }

        private UserReadDto Authenticate(LoginReadDto oLoginModel)
        {
            var _oCurrentUser = _context.Users.FirstOrDefault(o => o.Username.ToLower() == oLoginModel.Username.ToLower() && o.Password == oLoginModel.Password);

            if (_oCurrentUser != null)
            {
                return _mapper.Map<UserReadDto>(_oCurrentUser);
            }

            return new UserReadDto();
        }

        private JwtSecurityToken CreateToken(List<Claim> oAuthClaims)
        {
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SymmetricKey"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddMinutes(15),
                claims: oAuthClaims,
                signingCredentials: new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256));

            return token;
        }
    }
}
