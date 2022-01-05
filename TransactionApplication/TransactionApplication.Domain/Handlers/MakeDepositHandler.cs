using Microsoft.EntityFrameworkCore;
using TransactionApplication.Domain.Models.Input;
using TransactionApplication.Domain.Models.Output;

namespace TransactionApplication.Domain.Handlers
{
    public class MakeDepositHandler : MakeTransactionHandlerBase<MakeDepositInput, MakeDepositOutput>
    {

        public MakeDepositHandler(DbContext context) : base(context)
        { }

        protected override void UpdatePlayerBalance(DataAccess.Entitties.Player player, decimal amount) =>
            player.Balance += amount;
    }
}