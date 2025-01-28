using FluentValidation;

namespace TASK_session5.DTOs
{
    public class createProductDtoValidation:AbstractValidator<CreateProductDots>
    {
        public createProductDtoValidation()
        {
            RuleFor(x => x.Name).NotEmpty( ).MinimumLength(5).MaximumLength(30).WithMessage("name is empty");
            RuleFor(x => x.Price).NotEmpty().InclusiveBetween(20, 3000).WithMessage("price must be between 20,3000");
            RuleFor(x => x.Description).NotEmpty().MinimumLength(10).WithMessage( " description empty");
        }
    }
}
