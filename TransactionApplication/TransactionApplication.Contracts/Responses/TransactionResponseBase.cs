namespace TransactionApplication.Contracts.Responses
{
    public class TransactionResponseBase
    {
        public string Message { get; set; }

        public decimal Balance { get; set; }
    }
}