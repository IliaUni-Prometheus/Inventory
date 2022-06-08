using Application.Features.OrderFeautres.Queries;
using Domain.Models.Abstraction;
using Shared.DTOs;
using static Shared.CQRSInfrastructure;

namespace Application.Features.OrderFeautres.Handlers
{
    public class GetOrderByIdHandler : IQueryHandler<GetOrderByIdQuery, OrderDTO?>
    {
        private readonly IOrderRepository _repo;

        public GetOrderByIdHandler(IOrderRepository repo) { this._repo = repo; }

        public async Task<OrderDTO?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _repo.RetrieveByIdAsync(request.Id);
            if (order is null) { return null; }

            return new OrderDTO()
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                Freight = order.Freight,
                ShipPostalCode = order.ShipPostalCode
            };
        }
    }
}
