using Domain.Models.Abstraction;
using static Shared.CQRSInfrastructure;

namespace Application.Features.OrderFeautres.Queries
{
    public class GetOrderByIdQuery : IQuery<OrderByIdQueryResult?>
    {
        // Query request params
        public int Id { get; private set; }

        // Constructor
        public GetOrderByIdQuery(int id)
        {
            this.Id = id;
        }
    }

    public class GetOrderByIdHandler : IQueryHandler<GetOrderByIdQuery, OrderByIdQueryResult?>
    {
        private readonly IOrderRepository _repo;

        // Constructor
        public GetOrderByIdHandler(IOrderRepository repo)
        {
            this._repo = repo;
        }

        public async Task<OrderByIdQueryResult?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _repo.RetrieveByIdAsync(request.Id);
            if (order is null) { return null; }

            return new OrderByIdQueryResult()
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                Freight = order.Freight,
                ShipPostalCode = order.ShipPostalCode
            };
        }
    }

    /// <summary>
    /// Query result
    /// </summary>
    public class OrderByIdQueryResult
    {
        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? Freight { get; set; }
        public string? ShipAddress { get; set; }
        public string? ShipPostalCode { get; set; }
    }
}
