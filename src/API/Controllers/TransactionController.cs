using API.Services;
using Microsoft.AspNetCore.Mvc;
using TrainsClasses;

namespace API.Controllers
{
    [Route("/transaction")]
    public class TransactionController: Controller
    {
        private readonly TransactionService _transactionService;

        public TransactionController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        //[HttpGet("{id:int}")]
        //public IActionResult Details(int id)
        //{
        //    if (id < 0)
        //    {
        //        return BadRequest();
        //    }

        //    var city = _cityService.Get(id);

        //    if (city is null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(city);
        //}

        [HttpPost("")]
        public ActionResult Create([FromBody] Transaction data, string token)
        {
            if (!_transactionService.Validate(data))
            {
                return BadRequest("Данные транзакции некорректны");
            }
            data = _transactionService.Add(data);
            return Ok(data);
        }
    }
}
