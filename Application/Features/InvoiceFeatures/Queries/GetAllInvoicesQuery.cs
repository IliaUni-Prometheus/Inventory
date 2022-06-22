using Domain.Models.Abstraction;
using static Shared.CQRSInfrastructure;

namespace Application.Features.InvoiceFeatures.Queries
{
    /// <summary>
    /// Query
    /// </summary>
    public class GetAllInvoicesQuery : IQuery<IEnumerable<AllInvoicesQueryResult>>
    {
    }

    /// <summary>
    /// Query handler
    /// </summary>
    public class GetAllInvoicesHandler : IQueryHandler<GetAllInvoicesQuery, IEnumerable<AllInvoicesQueryResult>>
    {
        private readonly IInvoiceRepository _repo;

        // Constructor
        public GetAllInvoicesHandler(IInvoiceRepository repo)
        {
            this._repo = repo;
        }

        public async Task<IEnumerable<AllInvoicesQueryResult>> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
        {
            return (await _repo.RetrieveAllAsync())
                .Select(i => new AllInvoicesQueryResult()
                {
                    ShipName = i.ShipName,
                    ShipAddress = i.ShipAddress,
                    CustomerName = i.CustomerName,
                    Salesperson = i.Salesperson,
                    OrderDate = i.OrderDate
                });
        }
    }

    /// <summary>
    /// Query result
    /// </summary>
    public class AllInvoicesQueryResult
    {
        public string? ShipName { get; set; }
        public string? ShipAddress { get; set; }
        public string CustomerName { get; set; }
        public string Salesperson { get; set; }
        public DateTime? OrderDate { get; set; }
    }
}
