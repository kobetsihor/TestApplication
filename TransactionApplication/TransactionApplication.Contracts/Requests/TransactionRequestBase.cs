using System;

namespace TransactionApplication.Contracts.Requests
{
    public class TransactionRequestBase
    {
        public Guid PlayerId { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }
    }
}