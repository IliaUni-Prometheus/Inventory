using Domain.Models.Abstraction;
using static Shared.CQRSInfrastructure;

namespace Application.Features.TerritoryFeatures.Queries
{
    /// <summary>
    /// Query
    /// </summary>
    public class GetAllTerritoriesQuery : IQuery<IEnumerable<AllTerritoriesQueryResult>>
    {
    }

    /// <summary>
    /// Query handler
    /// </summary>
    public class GetAllTerritoriesHandler : IQueryHandler<GetAllTerritoriesQuery, IEnumerable<AllTerritoriesQueryResult>>
    {
        private readonly ITerritoryRepository _repo;

        // Constructor
        public GetAllTerritoriesHandler(ITerritoryRepository repo)
        {
            this._repo = repo;
        }

        public async Task<IEnumerable<AllTerritoriesQueryResult>> Handle(GetAllTerritoriesQuery request, CancellationToken cancellationToken)
        {
            return (await _repo.RetrieveAllAsync())
                .Select(t => new AllTerritoriesQueryResult()
                {
                    TerritoryId = t.TerritoryId,
                    TerritoryDescription = t.TerritoryDescription,
                    RegionId = t.RegionId
                });
        }
    }

    /// <summary>
    /// Query result
    /// </summary>
    public class AllTerritoriesQueryResult
    {
        public string TerritoryId { get; set; } = null!;
        public string TerritoryDescription { get; set; } = null!;
        public int RegionId { get; set; }
    }
}
