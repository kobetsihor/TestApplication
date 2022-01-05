using Microsoft.EntityFrameworkCore;
using TransactionApplication.DataAccess.Entitties;
using TransactionApplication.Domain.Models.Input;
using TransactionApplication.Domain.Models.Output;
using TransactionApplication.Infrastructure.Constants;
using TransactionApplication.Infrastructure.Exceptions;

namespace TransactionApplication.Domain.Handlers
{
    public class MakeWithdrawalHandler : MakeTransactionHandlerBase<MakeWithdrawalInput, MakeWithdrawalOutput>
    {

        public MakeWithdrawalHandler(DbContext context) : base(context)
        { }

        protected override void UpdatePlayerBalance(Player player, decimal amount)
        {
            player.Balance -= amount;
            if (player.Balance < 0)
            {
                throw new ValidatonException(Constants.NegativePlayerBalanceMessage);
            }
        }
    }
}