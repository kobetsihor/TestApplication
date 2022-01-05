using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using TransactionApplication.Domain.Handlers;
using TransactionApplication.Domain.Models.Input;
using TransactionApplication.Domain.Models.Output;
using Xunit;

namespace TransactionApplication.Tests.Handlers
{
    public class MakeDepositHandlerTests : MakeTransactionHandlerTests<MakeDepositInput, MakeDepositOutput>
    {
        [Theory]
        [InlineData(25)]
        public Task HandleDeposit_ShouldUpdateBalance(decimal expectedBalance) =>
            Handle_ShouldUpdateBalance(expectedBalance);

        protected override MakeTransactionHandlerBase<MakeDepositInput, MakeDepositOutput> GetHandler(Mock<DbContext> dbContextMock) =>
            new MakeDepositHandler(dbContextMock.Object);
    }
}