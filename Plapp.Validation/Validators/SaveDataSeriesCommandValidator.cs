using FluentValidation;
using Plapp.BusinessLogic.Commands;

namespace Plapp.Validation.Validators
{
    public class SaveDataSeriesCommandValidator : AbstractValidator<SaveDataSeriesCommand>
    {
        public SaveDataSeriesCommandValidator()
        {
            RuleFor(x => x.DataSeries.DataPoints).NotEmpty();
            RuleFor(x => x.DataSeries.Tag).NotEmpty();
            RuleFor(x => x.DataSeries.Title).NotEmpty();
            RuleFor(x => x.DataSeries.Topic).NotNull();
        }
    }
}