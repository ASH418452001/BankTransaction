using AutoMapper;
using BankTransaction.DTO.UserDTO;
using BankTransaction.DTO;
using BankTransaction.Model.entities;
using BankTransaction.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankTransaction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private DataContext _context { get; set; }



        //public string Password { get; private set; }

        public readonly IMapper _mapper;

        public UserController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: api/<UsersController>
        [HttpGet("GETALL")]
        public async Task<ActionResult<List<GETUserDTO>>> Get(string SAS)
        {
            var entity = await _context.User.Where(x => !x.IsDeleted).ToListAsync();
            var user = _mapper.Map<List<GETUserDTO>>(entity);

            var count = entity.Count();
            return Ok(new ApiRespons<List<GETUserDTO>>()
            {
                Count = count,
                Result = user,
                StatusCode = 200,
            });

        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<object> Get(int id)
        {
            var entity = await _context.User.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return _mapper.Map<GETUserDTO>(entity); ;
        }





        // POST api/<UsersController>
        [HttpPost]
        public async Task<object> Post([FromBody] CreateUserDTO dto)
        {

            try
            {

                var _mappedUser = _mapper.Map<User>(dto);

                _context.User.Add(_mappedUser);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        // PUT api/<UsersController>/5
        [HttpPut]
        public async Task<object> Put([FromBody] UPDATEUserDTO dto)
        {
            try
            {
                var entity = _mapper.Map<User>(dto);
                var myEntity = await _context.User.FindAsync(dto.ID);
                if (myEntity == null)
                    return NotFound();
                myEntity.UserName = entity.UserName;
                myEntity.Email = entity.Email;
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var curretUser = await _context.User.FindAsync(id);
            if (curretUser == null)
            {
                return NotFound("User Not Exist");
            }

            curretUser.IsDeleted = true;
            await _context.SaveChangesAsync();
            return Ok();

        }
    }
}
