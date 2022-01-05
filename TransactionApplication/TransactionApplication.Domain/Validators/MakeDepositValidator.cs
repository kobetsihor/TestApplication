using FluentValidation;
using TransactionApplication.Domain.Models.Input;

namespace TransactionApplication.Domain.Validators
{
    /// <summary>
    /// Validator for applying businees rules related to making deposit
    /// </summary>
    public class MakeDepositValidator : AbstractValidator<MakeDepositInput>
    {
        public MakeDepositValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .DependentRules(() =>
                {
                    RuleFor(x => x.Amount)
                        .Must(amount => amount >= 10);
                });
        }
    }
}
