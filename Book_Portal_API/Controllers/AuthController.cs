using Book_Portal_API.Helpers;
using Book_Portal_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Book_Portal_API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly PubsContext _context;
        public AuthController(PubsContext context)
        {
            this._context = context;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            ApplicationUser? user = null;
            
            if(request.Role == "author")
            {
                user = await _context.Authors.FirstOrDefaultAsync(a => a.Username == request.Username);
            }
            else if (request.Role == "publisher")
            {
                user = await _context.Publishers.FirstOrDefaultAsync(p => p.Username == request.Username);
            }

            if (user == null)
            {
                return NotFound(new { Message = "User Not Found" });
            }

            if (!PasswordHelper.Decode(request.Password, user.Password))
            {
                await Console.Out.WriteLineAsync(request.Password + " ---------------- " + user.Password);
                return BadRequest("Password Is Incorrect");
            }

            LoginResponse response = new LoginResponse();

            response.Token = CreateJWT(user);
            response.Message = "Logged In Successfully";

            return Ok(response);
        }

        [HttpPost("author/register")]
        public async Task<ActionResult<string>> RegisterAuthor([FromBody] Author author)
        {
            if (author == null)
            {
                return BadRequest();
            }
            if (await CheckUserNameExistsAsync(author.Username))
            {
                return BadRequest("Email Already Exists");
            }
            if (await CheckEmailExistsAsync(author.Email))
            {
                return BadRequest("Email Already Exists");
            }

            author.Password = PasswordHelper.Encode(author.Password);

            await _context.AddAsync(author);
            await _context.SaveChangesAsync();

            return Ok("User Registered Successfully");
        }


        [HttpPost("publisher/register")]
        public async Task<ActionResult<string>> Register([FromBody] Publisher publisher)
        {
            if (publisher == null)
            {
                return BadRequest();
            }
            if (await CheckUserNameExistsAsync(publisher.Username))
            {
                return BadRequest("Email Already Exists");
            }
            if (await CheckEmailExistsAsync(publisher.Email))
            {
                return BadRequest("Email Already Exists");
            }

            publisher.Password = PasswordHelper.Encode(publisher.Password);

            await _context.Publishers.AddAsync(publisher);
            await _context.SaveChangesAsync();

            return Ok("User Registered Successfully");
        }

        private async Task<bool> CheckUserNameExistsAsync(string username)
        {
            bool res1 = await _context.Authors.AnyAsync(u => u.Username == username);

            bool res2 = await _context.Publishers.AllAsync(u => u.Username == username);

            return res1 || res2;
        }

        private async Task<bool> CheckEmailExistsAsync(string email)
        {
            bool res1 = await _context.Authors.AnyAsync(u => u.Email == email);

            bool res2 = await _context.Publishers.AllAsync(u => u.Username == email);

            return res1 || res2;
        }

        private string CreateJWT(ApplicationUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("this is my secret key new");

            var idenity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role,user.Role),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(JsonClaimValueTypes.Json,JsonConvert.SerializeObject(user))
            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDiscriptor = new SecurityTokenDescriptor()
            {
                Subject = idenity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var token = jwtTokenHandler.CreateToken(tokenDiscriptor);

            return jwtTokenHandler.WriteToken(token);
        }
    }
}
