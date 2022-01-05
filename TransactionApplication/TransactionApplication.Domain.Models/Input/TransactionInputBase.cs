using MediatR;
using System;
using TransactionApplication.Domain.Models.Output;

namespace TransactionApplication.Domain.Models.Input
{
    public class TransactionInputBase<TOutput> : IRequest<TOutput>
        where TOutput : TransactionOutputBase
    {
        public Guid PlayerId { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }
    }
}