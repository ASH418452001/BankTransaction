using AutoMapper;
using BankTransaction.DTO.UserDTO;
using BankTransaction.Model.entities;
using BankTransaction.Model;
using BankTransaction.shared;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using BankTransaction.DTO.AcoountDTO;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankTransaction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private DataContext _context { get; set; }
        public readonly IMapper _mapper;
        public AccountsController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("Get")]
        
        public async Task<object> Get()
        {
            var entity = await _context.User.FirstOrDefaultAsync() ;
            
            return entity;
        }

       
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(Login dto)
        {

            var entity = await _context.User.Where(a => a.UserName == dto.UserName).FirstOrDefaultAsync();

            if (entity == null)
                return BadRequest("username or password error");
            if (entity.Password != HashPassword(dto.Password))
                return BadRequest("username or password error");
            entity.Password = HashPassword(dto.Password);

            return Content(JwtExtension.CreateToken(entity.ID.ToString(), entity.UserName));
        }


        private string HashPassword(string password)
        {
            var sha = SHA256.Create();
            var asByteArray = Encoding.Default.GetBytes(password);
            var hashedPassword = sha.ComputeHash(asByteArray);
            return Convert.ToBase64String(hashedPassword);
        }
        [Route("Register")]
        [HttpPost]
        public async Task<object> Register([FromBody] CreateUserDTO dto)
        {
            try
            {
                var entity = _mapper.Map<User>(dto);
                entity.Password = HashPassword(dto.Password);
                _context.User.Add(entity);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
