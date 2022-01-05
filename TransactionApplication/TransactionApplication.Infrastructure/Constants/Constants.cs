namespace TransactionApplication.Infrastructure.Constants
{
    public static class Constants
    {
        public const string PlayerNotFoundMessage = "player with id {0} not found";
        public const string NegativePlayerBalanceMessage = "balance of selected player cannot be negative on withdrawals";
        public const string UpdatedBalanceMessage = "balance of selected player was updated";
        public const string DatabaseName = "TestDb";
        public const string InternalErrorDefaultMessage = "Something went srong";
    }
}
