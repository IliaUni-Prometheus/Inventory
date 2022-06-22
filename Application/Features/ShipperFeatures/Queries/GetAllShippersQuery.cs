using Domain.Models.Abstraction;
using static Shared.CQRSInfrastructure;

namespace Application.Features.ShipperFeatures.Queries
{
    /// <summary>
    /// Query
    /// </summary>
    public class GetAllShippersQuery : IQuery<IEnumerable<AllShippersQueryResult>>
    {
    }

    /// <summary>
    /// Query handler
    /// </summary>
    public class GetAllShippersHandler : IQueryHandler<GetAllShippersQuery, IEnumerable<AllShippersQueryResult>>
    {
        private readonly IShipperRepository _repo;

        // Constructor
        public GetAllShippersHandler(IShipperRepository repo)
        {
            this._repo = repo;
        }

        public async Task<IEnumerable<AllShippersQueryResult>> Handle(GetAllShippersQuery request, CancellationToken cancellationToken)
        {
            return (await _repo.RetrieveAllAsync())
                .Select(s => new AllShippersQueryResult()
                {
                    ShipperId = s.ShipperId,
                    CompanyName = s.CompanyName,
                    Phone = s.Phone
                });
        }
    }

    /// <summary>
    /// Query result
    /// </summary>
    public class AllShippersQueryResult
    {
        public int ShipperId { get; set; }
        public string CompanyName { get; set; } = null!;
        public string? Phone { get; set; }
    }
}
