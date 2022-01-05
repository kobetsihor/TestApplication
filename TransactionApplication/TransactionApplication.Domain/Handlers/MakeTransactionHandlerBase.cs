using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TransactionApplication.DataAccess.Entitties;
using TransactionApplication.Domain.Models.Input;
using TransactionApplication.Domain.Models.Output;
using TransactionApplication.Infrastructure.Constants;
using TransactionApplication.Infrastructure.Exceptions;

namespace TransactionApplication.Domain.Handlers
{
    /// <summary>
    /// Common logic was moved to this base class
    /// </summary>
    /// <typeparam name="TInput">Transaction input data</typeparam>
    /// <typeparam name="TOutput">Transaction output data</typeparam>
    public abstract class MakeTransactionHandlerBase<TInput, TOutput> : IRequestHandler<TInput, TOutput>
        where TInput : TransactionInputBase<TOutput>
        where TOutput : TransactionOutputBase, new()
    {
        protected readonly DbContext _context;

        public MakeTransactionHandlerBase(DbContext context)
        {
            _context = context;
        }

        public async Task<TOutput> Handle(TInput input, CancellationToken cancellationToken)
        {
            var player = _context.Set<Player>().FirstOrDefault(x => x.Id == input.PlayerId);
            if (player == null)
            {
                throw new EntityNotFoundException(string.Format(Constants.PlayerNotFoundMessage, input.PlayerId));
            }

            UpdatePlayerBalance(player, input.Amount);
            await _context.SaveChangesAsync(cancellationToken);

            return new TOutput
            {
                Balance = player.Balance,
                Message = Constants.UpdatedBalanceMessage
            };
        }

        protected abstract void UpdatePlayerBalance(Player player, decimal amount);
    }
}