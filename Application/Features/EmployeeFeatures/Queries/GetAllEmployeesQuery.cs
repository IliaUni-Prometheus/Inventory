using Domain.Models.Abstraction;
using static Shared.CQRSInfrastructure;

namespace Application.Features.EmployeeFeatures.Queries
{
    /// <summary>
    /// Query
    /// </summary>
    public class GetAllEmployeesQuery : IQuery<IEnumerable<AllEmployeesQueryResult>>
    {

    }

    /// <summary>
    /// Query handler
    /// </summary>
    public class GetAllEmployeesHandler : IQueryHandler<GetAllEmployeesQuery, IEnumerable<AllEmployeesQueryResult>>
    {
        private readonly IEmployeeRepository _repo;

        // Constructor
        public GetAllEmployeesHandler(IEmployeeRepository repo)
        {
            this._repo = repo;
        }

        public async Task<IEnumerable<AllEmployeesQueryResult>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {

            // fetch employees and map it to DTO
            return (await _repo.RetrieveAllAsync())
                .Select(e => new AllEmployeesQueryResult()
                {
                    EmployeeId = e.EmployeeId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Country = e.Country,
                    PostalCode = e.PostalCode,
                });
        }
    }

    /// <summary>
    /// Query result
    /// </summary>
    public class AllEmployeesQueryResult
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
    }
}
