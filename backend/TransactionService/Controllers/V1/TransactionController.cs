using System;
using System.Collections.Generic;
using TransactionService.Domains;
using Microsoft.AspNetCore.Mvc;
using TransactionService.Contracts.V1;
using TransactionService.Domains.Model;
using TransactionService.Domains.Dtos;
using MediatR;
using Microsoft.Extensions.Logging;
using TransactionService.Contracts.V1.Requests;
using System.Threading.Tasks;

namespace TransactionService.Controllers.V1
{
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(IMediator mediator, ILogger<TransactionController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet(ApiRoutes.Transaction.GetAll)]
        public async Task<IActionResult> GetAllTransactions()
        {
            _logger.LogTrace("GetAllTransactions");
            var result = await _mediator.Send(new GetTransactionsRequest());

            result.ForEach(t => _logger.LogTrace($" Date:{t.Date}, [Id:{t.Id}, Account:{t.Account}, Amount:{t.Amount}, Desc:{t.Description}]"));

            return Ok(result);
        }

        [HttpPost(ApiRoutes.Transaction.Create)]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionDto transaction)
        {
            _logger.LogTrace("AddTransaction");

            var result =  await _mediator.Send(new CreateTransactionRequest {Transaction = transaction});
            return Ok(result);
        }

         [HttpPut(ApiRoutes.Transaction.Update)]
        public async Task<IActionResult> UpdateTransaction(int transactionId, [FromBody] TransactionDto transaction)
        {
            _logger.LogTrace("UpdateTransaction");

            var result =  await _mediator.Send(new UpdateTransactionRequest {Transaction = transaction});
            return Ok(result);
        }       
    }
}