using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TransactionApplication.DataAccess.Entitties;
using TransactionApplication.Domain.Handlers;
using TransactionApplication.Domain.Models.Input;
using TransactionApplication.Domain.Models.Output;
using TransactionApplication.Infrastructure.Exceptions;
using TransactionApplication.Tests.Extensions;
using Xunit;

namespace TransactionApplication.Tests.Handlers
{
    public class MakeWithdrawalHandlerTests : MakeTransactionHandlerTests<MakeWithdrawalInput, MakeWithdrawalOutput>
    {
        [Theory]
        [InlineData(15)]
        public Task HandleWithdrawal_ShouldUpdateBalance(decimal expectedBalance) =>
            Handle_ShouldUpdateBalance(expectedBalance);

        [Fact]
        public async Task HandleWithdrawal_BalanceIsNegative_ShouldThrowException()
        {
            //Arrange
            var cancellationToken = new CancellationToken(false);
            var playerId = Guid.NewGuid();
            var expectedErrorMessage = "balance of selected player cannot be negative on withdrawals";
            var dbContextMock = new Mock<DbContext>();
            var players = new List<Player> { new Player { Id = playerId, Balance = 20 } };
            dbContextMock.Setup(x => x.Set<Player>()).Returns(DbContextMock.GetQueryableMockDbSet(players));
            var handler = GetHandler(dbContextMock);

            var input = new MakeWithdrawalInput
            {
                Date = DateTime.Today,
                Amount = 21,
                PlayerId = playerId
            };

            //Act
            var exception = await Assert.ThrowsAsync<ValidatonException>(async () =>
                await handler.Handle(input, cancellationToken));

            //Assert
            Assert.Equal(expectedErrorMessage, exception.Message);
            dbContextMock.Verify(x => x.SaveChangesAsync(cancellationToken), Times.Never);
            dbContextMock.Verify(x => x.Set<Player>(), Times.Exactly(1));
        }

        protected override MakeTransactionHandlerBase<MakeWithdrawalInput, MakeWithdrawalOutput> GetHandler(Mock<DbContext> dbContextMock) =>
            new MakeWithdrawalHandler(dbContextMock.Object);
    }
}