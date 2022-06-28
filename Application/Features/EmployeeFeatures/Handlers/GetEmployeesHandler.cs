using Application.Features.EmployeeFeatures.Queries;
using Domain.Models.Abstraction;
using Shared.DTOs;
using static Shared.CQRSInfrastructure;

namespace Application.Features.EmployeeFeatures.Handlers
{
    public class GetEmployeesHandler : IQueryHandler<GetEmployeesQuery, BrowseResult<EmployeeDTO>>
    {
        private readonly IEmployeeRepository _repo;

        public GetEmployeesHandler(IEmployeeRepository repo) { this._repo = repo; }

        public async Task<BrowseResult<EmployeeDTO>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            // how many items should be displayed on each page (temporarily hardcoded to 10)
            var itemsPerPage = 10f;

            // fetch employees and map it to DTO
            var employees = (await _repo.RetrieveAllAsync(request.Page, (int)itemsPerPage))
                .Select(e => new EmployeeDTO()
                {
                    EmployeeId = e.EmployeeId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Country = e.Country,
                    PostalCode = e.PostalCode,
                });

            // calculate how many pages there will be considering our items per page
            int employeesCount = await _repo.Count();
            var pageCount = Math.Ceiling(employeesCount / itemsPerPage);

            return new BrowseResult<EmployeeDTO>()
            {
                Data = employees.ToList(),
                Pages = (int)pageCount,
                CurrentPage = request.Page
            };
        }
    }
}
