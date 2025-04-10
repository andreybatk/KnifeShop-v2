using FluentValidation;
using KnifeShop.API.Contracts.Knife;

namespace KnifeShop.API.Validators
{
    public class CreateKnifeRequestValidator : AbstractValidator<CreateKnifeRequest>
    {
        public CreateKnifeRequestValidator()
        {
            RuleFor(x => x.Title)
                 .NotEmpty().WithMessage("Title cannot be empty.");

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Category cannot be empty.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(x => x.Images)
                .Must(images => images?.Count <= 5)
                .WithMessage("Maximum 5 images.")
                .When(x => x.Images != null && x.Images.Count > 0);
        }
    }
}
