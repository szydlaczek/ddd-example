namespace Project1.Application.Orders.Commands.PlaceOrder;

public class CreateOrderItemDtoValidator : AbstractValidator<PlaceOrderItemDto>
{
    public CreateOrderItemDtoValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("ProductId is required.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0.");
    }
}
