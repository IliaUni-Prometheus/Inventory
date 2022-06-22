using Domain.Models.Abstraction;
using static Shared.CQRSInfrastructure;

namespace Application.Features.CategoryFeatures.Queries
{
    /// <summary>
    /// Query
    /// </summary>
    public class GetAllCategoriesQuery : IQuery<IEnumerable<AllCategoriesQueryResult>>
    {
    }

    /// <summary>
    /// Query handler
    /// </summary>
    public class GetAllCategoriesHandler : IQueryHandler<GetAllCategoriesQuery, IEnumerable<AllCategoriesQueryResult>>
    {
        private readonly ICategoryRepository _repo;

        // Constructor
        public GetAllCategoriesHandler(ICategoryRepository repo)
        {
            this._repo = repo;
        }

        public async Task<IEnumerable<AllCategoriesQueryResult>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            return (await _repo.RetrieveAllAsync())
                .Select(c => new AllCategoriesQueryResult()
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    Description = c.Description
                });
        }
    }

    /// <summary>
    /// Query result
    /// </summary>
    public class AllCategoriesQueryResult
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string? Description { get; set; }
    }
}
