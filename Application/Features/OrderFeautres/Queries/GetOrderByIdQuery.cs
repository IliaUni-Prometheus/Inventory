using Shared.DTOs;
using static Shared.CQRSInfrastructure;

namespace Application.Features.OrderFeautres.Queries
{
    public class GetOrderByIdQuery : IQuery<OrderDTO>
    {
        public int Id { get; private set; }

        public GetOrderByIdQuery(int id) { this.Id = id; }
    }
}
