namespace TransactionApplication.Domain.Models.Output
{
    public class TransactionOutputBase
    {
        public string Message { get; set; }

        public decimal Balance { get; set; }
    }
}