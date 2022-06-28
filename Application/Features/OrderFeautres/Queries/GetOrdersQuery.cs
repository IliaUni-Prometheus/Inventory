using Shared.DTOs;
using static Shared.CQRSInfrastructure;

namespace Application.Features.OrderFeautres.Queries
{
    public class GetOrdersQuery : IQuery<BrowseResult<OrderDTO>>
    {
        public int Page { get; private set; }

        public GetOrdersQuery(int page) { this.Page = page; }
    }
}
