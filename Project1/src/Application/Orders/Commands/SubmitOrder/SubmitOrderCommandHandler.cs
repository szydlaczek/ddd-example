using Project1.Application.Common.Exceptions;
using Project1.Application.Common.Interfaces;

namespace Project1.Application.Orders.Commands.SubmitOrder;

internal class SubmitOrderCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<SubmitOrderCommand>
{
    public async Task Handle(SubmitOrderCommand command, CancellationToken ct)
    {
        var order = await dbContext
            .Orders
            .FirstOrDefaultAsync(c => c.Id == command.Id, ct);

        AggregateNotFoundException.For(order, command.Id);

        order?.Submit();

        await dbContext.SaveChangesAsync(ct);
    }
}
