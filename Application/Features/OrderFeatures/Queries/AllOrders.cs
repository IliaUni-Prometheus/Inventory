using Infrastructure;
using Microsoft.EntityFrameworkCore;
using static Infrastructure.Shared.Infastrucure;

namespace Application.Features.OrderFeatures.Queries
{
    public class AllOrdersQueryHandler : IQueryHandler<AllOrdersQuery, IEnumerable<OrderResultItem>>
    {
        private readonly NorthWindContext _db;
        public AllOrdersQueryHandler(NorthWindContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<OrderResultItem>> Handle(AllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _db.Orders.Select(x => new OrderResultItem { OrderId = x.OrderId })
                                         .ToListAsync();
            return orders;
        }
    }

    public class OrderResultItem
    {
        public int OrderId { get; set; }
    }

    public class AllOrdersQuery : IQuery<IEnumerable<OrderResultItem>>
    {

    }
}
