using FluentValidation;

namespace BlazorApp1.ViewModels
{
    public class ProductFluentValidator : AbstractValidator<ProductVM>
    {
        public ProductFluentValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(1, 100);

            RuleFor(x => x.Price)
                .GreaterThan(0);

            RuleForEach(x => x.Courses)
                .SetValidator(new CourseFluentValidator());
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<ProductVM>.CreateWithOptions((ProductVM)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
