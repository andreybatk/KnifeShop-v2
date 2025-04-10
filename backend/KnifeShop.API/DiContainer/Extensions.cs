using FluentValidation;
using FluentValidation.Results;
using KnifeShop.API.Validators;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace KnifeShop.API.DiContainer
{
    public static class Extensions
    {
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreateKnifeRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<EditKnifeRequestValidator>();

            services.AddSingleton<RefreshTokenValidator>();
        }

        public static void AddToModelState(this ValidationResult result, ModelStateDictionary modelState)
        {
            foreach (var error in result.Errors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
        }
    }
}