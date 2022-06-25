using Domain.Models.Abstraction;
using static Shared.CQRSInfrastructure;

namespace Application.Features.RegionFeatures.Queries
{
    /// <summary>
    /// Query
    /// </summary>
    public class GetAllRegionsQuery : IQuery<IEnumerable<AllRegionsQueryResult>>
    {
    }

    /// <summary>
    /// Query handler
    /// </summary>
    public class GetAllRegionsHandler : IQueryHandler<GetAllRegionsQuery, IEnumerable<AllRegionsQueryResult>>
    {
        private readonly IRegionRepository _repo;

        // Constructor
        public GetAllRegionsHandler(IRegionRepository repo)
        {
            this._repo = repo;
        }

        public async Task<IEnumerable<AllRegionsQueryResult>> Handle(GetAllRegionsQuery request, CancellationToken cancellationToken)
        {
            return (await _repo.RetrieveAllAsync())
                .Select(r => new AllRegionsQueryResult()
                {
                    RegionId = r.RegionId,
                    RegionDescription = r.RegionDescription
                });
        }
    }

    /// <summary>
    /// Query result
    /// </summary>
    public class AllRegionsQueryResult
    {
        public int RegionId { get; set; }
        public string RegionDescription { get; set; }
    }
}
