using FluentValidation;

namespace BlazorApp1.ViewModels
{
    public class CourseFluentValidator : AbstractValidator<CourseVM>
    {
        public CourseFluentValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(1, 100);
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<CourseVM>.CreateWithOptions((CourseVM)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
