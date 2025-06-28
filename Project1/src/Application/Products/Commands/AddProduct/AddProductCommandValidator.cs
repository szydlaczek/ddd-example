namespace Project1.Application.Products.Commands.AddProduct;

public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    public AddProductCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty()
            .WithMessage("Product name cannot be empty");

        RuleFor(c => c.Price).GreaterThan(0)
            .WithMessage("Price cannot be negative");

        RuleFor(c => c.Currency)
            .Length(3)
            .WithMessage("Currency must be a 3-letter ISO code (e.g., 'PLN')");
    }
}
