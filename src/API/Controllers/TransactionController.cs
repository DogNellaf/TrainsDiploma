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

        [HttpGet]
        public List<Transaction> Tickets()
        {
            return _transactionService.Objects;
        }

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
