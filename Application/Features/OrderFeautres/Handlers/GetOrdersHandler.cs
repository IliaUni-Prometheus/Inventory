using Application.Features.OrderFeautres.Queries;
using Domain;
using Domain.Models.Abstraction;
using Shared.DTOs;
using static Shared.CQRSInfrastructure;

namespace Application.Features.OrderFeautres.Handlers
{
    public class GetOrdersHandler : IQueryHandler<GetOrdersQuery, BrowseResult<OrderDTO>>
    {
        private readonly IOrderRepository _repo;

        public GetOrdersHandler(IOrderRepository repo) { this._repo = repo; }

        public async Task<BrowseResult<OrderDTO>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            // how many items should be displayed on each page (temporarily hardcoded to 10)
            var itemsPerPage = 10f;

            // fetch orders and map it to DTO
            var orders = (await _repo.RetrieveAllAsync(request.Page, request.PageSize))
                .Select(o => new OrderDTO()
                {
                    OrderId = o.OrderId,
                    OrderDate = o.OrderDate,
                    Freight = o.Freight,
                    ShipAddress = o.ShipAddress,
                    ShipPostalCode = o.ShipPostalCode
                });

            // calculate how many pages there will be considering our items per page
            int ordersCount = await _repo.Count();
            var pageCount = Math.Ceiling(ordersCount / itemsPerPage);

            return new BrowseResult<OrderDTO>()
            {
                Data = orders.ToList(),
                Pages = (int)pageCount,
                CurrentPage = request.Page
            };
        }
    }
}
