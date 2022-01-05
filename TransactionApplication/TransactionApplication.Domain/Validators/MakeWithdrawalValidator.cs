using FluentValidation;
using TransactionApplication.Domain.Models.Input;

namespace TransactionApplication.Domain.Validators
{
    /// <summary>
    /// Validator for applying businees rules related to making withdrawal
    /// </summary>
    public class MakeWithdrawalValidator : AbstractValidator<MakeWithdrawalInput>
    {
        public MakeWithdrawalValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .DependentRules(() =>
                {
                    RuleFor(x => x.Amount)
                        .Must(amount => amount <= 25 && amount > 0);
                });
        }
    }
}