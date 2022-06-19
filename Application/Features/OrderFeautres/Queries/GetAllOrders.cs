using Domain.Models.Abstraction;
using static Shared.CQRSInfrastructure;

namespace Application.Features.OrderFeautres.Queries
{
    /// <summary>
    /// Query
    /// </summary>
    public class GetAllOrdersQuery : IQuery<AllOrdersQueryResult>
    {
        // Query request params
        public int Page { get; private set; }
        public int ItemsPerPage { get; private set; }

        // Constructor
        public GetAllOrdersQuery(int page, int itemsPerPage)
        {
            this.Page = page;
            this.ItemsPerPage = itemsPerPage;
        }
    }

    /// <summary>
    /// Query handler
    /// </summary>
    public class GetAllOrdersQueryHandler : IQueryHandler<GetAllOrdersQuery, AllOrdersQueryResult>
    {
        private readonly IOrderRepository _repo;

        // Constructor
        public GetAllOrdersQueryHandler(IOrderRepository repo)
        {
            this._repo = repo;
        }

        public async Task<AllOrdersQueryResult> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            // Fetch orders and map it to DTO
            var orders = (await _repo.RetrieveAllAsync(request.Page, request.ItemsPerPage))
                .Select(o => new AllOrderDTO()
                {
                    OrderId = o.OrderId,
                    OrderDate = o.OrderDate,
                    Freight = o.Freight,
                    ShipAddress = o.ShipAddress,
                    ShipPostalCode = o.ShipPostalCode
                });

            // Calculate how many pages there will be considering our items per page
            int ordersCount = await _repo.Count();
            var pageCount = Math.Ceiling(ordersCount / (float)request.ItemsPerPage);

            return new AllOrdersQueryResult()
            {
                Items = orders.ToList(),
                Pages = (int)pageCount,
                CurrentPage = request.Page
            };
        }
    }

    /// <summary>
    /// Query result
    /// </summary>
    public class AllOrdersQueryResult
    {
        public List<AllOrderDTO> Items { get; set; } = new();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
    public class AllOrderDTO
    {
        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? Freight { get; set; }
        public string? ShipAddress { get; set; }
        public string? ShipPostalCode { get; set; }
    }
}
