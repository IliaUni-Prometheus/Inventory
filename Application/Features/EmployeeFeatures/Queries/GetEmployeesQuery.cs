using Shared.DTOs;
using static Shared.CQRSInfrastructure;

namespace Application.Features.EmployeeFeatures.Queries
{
    public class GetEmployeesQuery : IQuery<BrowseResult<EmployeeDTO>>
    {
        public int Page { get; private set; }
        public int PageSize { get; private set; }

        public GetEmployeesQuery(int page, int pageSize)
        {
            this.Page = page;
            this.PageSize = pageSize;
        }
    }
}
