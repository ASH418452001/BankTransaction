using AutoMapper;
using BankTransaction.DTO.bankTranactionDTO;
using BankTransaction.Filter;
using BankTransaction.Model;
using BankTransaction.Model.entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankTransaction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankTransactionController : ControllerBase
    {
        private DataContext _context;
        public readonly IMapper _mapper;
        public BankTransactionController (DataContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: api/<BankTransactionController>
        [HttpGet]
        public async Task<IEnumerable<GETbankTransaction>> Get(bankTransactionFilter filter)
        {
            var BT = await _context.bankTransaction.Where(a => string.IsNullOrEmpty(filter.NameOfSender) || a.NameOfSender.Contains(filter.NameOfSender))
                .Where(a => string.IsNullOrEmpty(filter.NameOfReciever) || a.NameOfReciever.Contains(filter.NameOfReciever))
                .Where(a => string.IsNullOrEmpty(filter.governorate) || a.governorate.Contains(filter.governorate))
                .Where(a => filter.fromAmountdolar == null || a.AmountInDollar >= filter.fromAmountdolar)
                 .Where(a => filter.toAmountdolar == null || a.AmountInDollar <= filter.toAmountdolar)
                 .Where(a => filter.fromAmountEuro == null || a.AmountInEuro >= filter.fromAmountEuro)
                 .Where(a => filter.toAmountEuro == null || a.AmountInEuro <= filter.toAmountEuro)
                 .Where(a => filter.fromdailyprice == null || a.DailyPrice >= filter.fromdailyprice)
                 .Where(a => filter.todailyprice == null || a.DailyPrice <= filter.todailyprice)
                 //.Where(a => filter.fromdateOfTransaction == null || a.DateOfTransaction >= filter.fromdateOfTransaction)
                 //.Where(a => filter.todate == null || a.DateOfTransaction <= filter.todate)

                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
                  return _mapper.Map<IEnumerable<GETbankTransaction>>(BT);
        }

        // GET api/<BankTransactionController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GETbankTransaction>> Get(int id)
        {
            var entity = await _context.bankTransaction.FindAsync(id);
            if (entity == null)
                return NotFound();
            return _mapper.Map<GETbankTransaction>(entity);
            
        }

        [HttpGet("transactionfromEuroTO$/{id}")]
        public async Task<ActionResult<object>> GetTransactiontoEuro(int id)
        {
            // Fetch the transaction from the database or any other data source
            var transaction =  _context.bankTransaction.FirstOrDefault(t => t.ID == id);

            if (transaction == null)
            {
                return NotFound();
            }

            // Divide the amount in dollars by the daily price to convert to euros
            transaction.AmountInDollar = transaction.AmountInEuro / transaction.DailyPrice;

            return Ok(transaction.AmountInDollar);
        }




        [HttpGet("transactionfrom$TOEuro/{id}")]
        public async Task<ActionResult<object>> GetTransactionToDollar(int id)
        {
            // Fetch the transaction from the database or any other data source
            var transaction = _context.bankTransaction.FirstOrDefault(t => t.ID == id);

            if (transaction == null)
            {
                return NotFound();
            }

            // Divide the amount in dollars by the daily price to convert to euros
            transaction.AmountInEuro = transaction.AmountInDollar * transaction.DailyPrice;

            return Ok(transaction.AmountInEuro);
        }




        // POST api/<BankTransactionController>
        [HttpPost]
        public async Task<ActionResult<CreatebankTransactionDTO>> Post([FromBody] CreatebankTransactionDTO dto)
        {

            try
            {
                var _mappedUser = _mapper.Map<bankTransaction>(dto);

                _context.bankTransaction.Add(_mappedUser);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<BankTransactionController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UPDATEbankTransactionDTO>> Put([FromBody] UPDATEbankTransactionDTO dto)
        {
            try
            {
                var entity = _mapper.Map<bankTransaction>(dto);
                var myEntity = await _context.bankTransaction.FindAsync(dto.ID);
                if (myEntity == null)
                    return NotFound();
                myEntity.NameOfSender = entity.NameOfReciever;
                myEntity.NameOfReciever = entity.NameOfReciever;
                myEntity.PhoneNumber = entity.PhoneNumber;
                myEntity.governorate = entity.governorate;
                myEntity.AmountInDollar = entity.AmountInDollar;
                myEntity.AmountInEuro = entity.AmountInEuro;
                myEntity.DailyPrice = entity.DailyPrice;
                myEntity.DateOfReciever = entity.DateOfReciever;
                myEntity.DateOfTransaction = entity.DateOfTransaction;
                myEntity.Notes = entity.Notes;
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<BankTransactionController>/5
        [HttpDelete("{id}")]
        public async Task<object> Delete(int id)
        {
            try
            {
                var myEntity = await _context.bankTransaction.FindAsync(id);
                if (myEntity == null)
                    return NotFound();
                _context.bankTransaction.Remove(myEntity);
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
