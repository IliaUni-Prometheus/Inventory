using Shared.DTOs;
using static Shared.CQRSInfrastructure;

namespace Application.Features.EmployeeFeatures.Queries
{
    public class GetEmployeesQuery : IQuery<BrowseResult<EmployeeDTO>>
    {
        public int Page { get; private set; }

        public GetEmployeesQuery(int page) { this.Page = page; }
    }
}
