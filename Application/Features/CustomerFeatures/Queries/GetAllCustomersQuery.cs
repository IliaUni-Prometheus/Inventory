using Domain.Models.Abstraction;
using static Shared.CQRSInfrastructure;

namespace Application.Features.CustomerFeatures.Queries
{
    /// <summary>
    /// Query
    /// </summary>
    public class GetAllCustomersQuery : IQuery<IEnumerable<AllCustomersQueryResult>>
    {
    }

    /// <summary>
    /// Query handler
    /// </summary>
    public class GetAllCustomersHandler : IQueryHandler<GetAllCustomersQuery, IEnumerable<AllCustomersQueryResult>>
    {
        private readonly ICustomerRepository _repo;

        // Constructor
        public GetAllCustomersHandler(ICustomerRepository repo)
        {
            this._repo = repo;
        }

        public async Task<IEnumerable<AllCustomersQueryResult>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            return (await _repo.RetrieveAllAsync())
                .Select(c => new AllCustomersQueryResult()
                {
                    CustomerId = c.CustomerId,
                    CompanyName = c.CompanyName,
                    ContactName = c.ContactName,
                    ContactTitle = c.ContactTitle,
                    Address = c.Address,
                    City = c.City,
                    Region = c.Region,
                    PostalCode = c.PostalCode,
                    Country = c.Country,
                    Phone = c.Phone
                });
        }
    }

    /// <summary>
    /// Query result
    /// </summary>
    public class AllCustomersQueryResult
    {
        public string CustomerId { get; set; }
        public string CompanyName { get; set; }
        public string? ContactName { get; set; }
        public string? ContactTitle { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
    }
}
