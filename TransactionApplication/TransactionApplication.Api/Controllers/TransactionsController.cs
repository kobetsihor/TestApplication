using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TransactionApplication.Api.Models;
using TransactionApplication.Contracts.Requests;
using TransactionApplication.Contracts.Responses;
using TransactionApplication.Domain.Models.Input;
using TransactionApplication.Domain.Models.Output;

namespace TransactionApplication.Api.Controllers
{
    /// <summary>
    /// Controller for running transactions, it was decided to devide into 2 separate methods
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : ControllerBase
    {
        protected readonly IMediator _mediator;
        protected readonly IMapper _mapper;

        public TransactionsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// method for making withdrawal from player account
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Balance and message what was done</returns>
        [HttpPost("make-withdrawal")]
        [ProducesResponseType(typeof(MakeWithdrawalResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestBody), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadRequestBody), StatusCodes.Status400BadRequest)]
        public Task<IActionResult> MakeWithdrawal(MakeWithdrawalRequest request) =>
            MakeTransaction<MakeWithdrawalRequest, MakeWithdrawalInput, MakeWithdrawalOutput, MakeWithdrawalResponse>(request);

        /// <summary>
        /// method for making deposit for player account
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Balance and message what was done</returns>
        [HttpPost("make-deposit")]
        [ProducesResponseType(typeof(MakeDepositResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestBody), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadRequestBody), StatusCodes.Status400BadRequest)]
        public Task<IActionResult> MakeDeposit(MakeDepositRequest request) =>
            MakeTransaction<MakeDepositRequest, MakeDepositInput, MakeDepositOutput, MakeDepositResponse>(request);

        private async Task<IActionResult> MakeTransaction<TRequest, TInput, TOutput, TResponse>(TRequest request)
            where TRequest : TransactionRequestBase
            where TInput : TransactionInputBase<TOutput>
            where TOutput : TransactionOutputBase
            where TResponse : TransactionResponseBase
        {
            var input = _mapper.Map<TInput>(request);
            var output = await _mediator.Send(input);
            var response = _mapper.Map<TResponse>(output);
            return Ok(response);
        }
    }
}