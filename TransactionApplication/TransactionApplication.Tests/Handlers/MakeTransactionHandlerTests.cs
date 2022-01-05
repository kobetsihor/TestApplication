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
    public abstract class MakeTransactionHandlerTests<TInput, TOutput>
        where TInput : TransactionInputBase<TOutput>, new()
        where TOutput : TransactionOutputBase, new()
    {
        [Fact]
        public async Task Handle_PlayerDoesNotExist_ShouldThrowExceptionAsync()
        {
            //Arrange
            var cancellationToken = new CancellationToken(false);
            var playerId = Guid.NewGuid();
            var expectedErrorMessage = $"player with id {playerId} not found";
            var dbContextMock = new Mock<DbContext>();
            dbContextMock.Setup(x => x.Set<Player>()).Returns(DbContextMock.GetQueryableMockDbSet(new List<Player>()));
            var handler = GetHandler(dbContextMock);

            var input = new TInput
            {
                Date = DateTime.Today,
                Amount = 5,
                PlayerId = playerId
            };

            //Act
            var exception = await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
                await handler.Handle(input, cancellationToken));

            //Assert
            Assert.Equal(expectedErrorMessage, exception.Message);
            dbContextMock.Verify(x => x.SaveChangesAsync(cancellationToken), Times.Never);
            dbContextMock.Verify(x => x.Set<Player>(), Times.Exactly(1));
        }

        protected async Task Handle_ShouldUpdateBalance(decimal expectedBalance)
        {
            //Arrange
            var cancellationToken = new CancellationToken(false);
            var playerId = Guid.NewGuid();
            var dbContextMock = new Mock<DbContext>();
            var players = new List<Player> { new Player { Id = playerId, Balance = 20 } };
            dbContextMock.Setup(x => x.Set<Player>()).Returns(DbContextMock.GetQueryableMockDbSet(players));
            dbContextMock.Setup(x => x.SaveChangesAsync(cancellationToken)).ReturnsAsync(1);
            var handler = GetHandler(dbContextMock);

            var input = new TInput
            {
                Date = DateTime.Today,
                Amount = 5,
                PlayerId = playerId
            };

            //Act
            var result = await handler.Handle(input, cancellationToken);

            //Assert
            Assert.Equal(expectedBalance, result.Balance);
            dbContextMock.Verify(x => x.SaveChangesAsync(cancellationToken), Times.Exactly(1));
            dbContextMock.Verify(x => x.Set<Player>(), Times.Exactly(1));
        }

        protected abstract MakeTransactionHandlerBase<TInput, TOutput> GetHandler(Mock<DbContext> dbContextMock);
    }
}