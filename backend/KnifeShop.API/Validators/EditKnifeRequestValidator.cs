using FluentValidation;
using KnifeShop.Contracts.Knife;

namespace KnifeShop.API.Validators
{
    public class EditKnifeRequestValidator : AbstractValidator<EditKnifeRequest>
    {
        public EditKnifeRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title cannot be empty.");

            RuleFor(x => x.CategoryIds)
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