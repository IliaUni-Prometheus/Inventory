using Shared.DTOs;
using static Shared.CQRSInfrastructure;

namespace Application.Features.OrderFeautres.Queries
{
    public class GetOrdersQuery : IQuery<BrowseResult<OrderDTO>>
    {
        public int Page { get; private set; }
        public int PageSize { get; private set; }

        public GetOrdersQuery(int page, int pageSize) { this.Page = page; this.PageSize = pageSize; }
    }
}
