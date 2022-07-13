using AutoMapper;
using Car.Core.CustomEntities;
using Car.Core.DTOs;
using Car.Core.Entities;
using Car.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Car.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        public TransactionController(ITransactionService transactionService, IMapper mapper)
        {
            _transactionService = transactionService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllTransaction()
        {
            var transactions = _transactionService.GetAllTransaction();
            var transactionDtos = _mapper.Map<IEnumerable<TransactionResponse>>(transactions);

            var response = new ApiResponse<IEnumerable<TransactionResponse>>(transactionDtos)
            {
                Message = new Message
                {
                    Description = "List Data Transaction"
                }
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction(CreateTransactionRequest request)
        {
            var result = await _transactionService.AddTransaction(request);

            var response = new ApiResponse<bool>(result)
            {
                Message = new Message
                {
                    Description = "Success Create Transaction"

                }
            };

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(Guid id, UpdateTransactionRequest request)
        {
            var transactionMap = _mapper.Map<Transaction>(request);
            var result = await _transactionService.UpdateTransaction(id, transactionMap);

            var response = new ApiResponse<bool>(result)
            {
                Message = new Message
                {
                    Description = "Success Update Transaction"

                }
            };

            return Ok(response);
        }
    }
}
