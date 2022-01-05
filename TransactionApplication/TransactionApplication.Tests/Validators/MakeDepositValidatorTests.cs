﻿using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation.TestHelper;
using TransactionApplication.Domain.Models.Input;
using TransactionApplication.Domain.Validators;
using Xunit;

namespace TransactionApplication.Tests.Validators
{
    public class MakeDepositValidatorTests
    {
        [Fact]
        public async Task Validate_ValidModel_SuccessfulValidation()
        {
            //Arrange
            var input = new MakeDepositInput { Amount = 10 };
            var validator = new MakeDepositValidator();

            //Act
            var result = await validator.TestValidateAsync(input);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public async void Validate_AmountIsIncorrect_ValidationFailedWithOneError()
        {
            //Arrange
            var input = new MakeDepositInput { Amount = 9 };
            var validator = new MakeDepositValidator();

            //Act
            var result = await validator.TestValidateAsync(input);

            //Assert
            result.Errors.Should().HaveCount(1);
            result.ShouldHaveValidationErrorFor(r => r.Amount)
                .WithErrorCode("PredicateValidator");
        }
    }
}