using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using static Shared.CQRSInfrastructure;

namespace Application.Features.EmployeeFeatures.Queries
{
    public class AllEmployeesQueryHandler : IQueryHandler<AllEmployeesQuery, IEnumerable<AllEmployeesQueryResult>>
    {
        private readonly NorthwindContext _db;
        public AllEmployeesQueryHandler(NorthwindContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<AllEmployeesQueryResult>> Handle(AllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = _db.Employees.Select(x => new AllEmployeesQueryResult
            {
                EmployeeId = x.EmployeeId,
                EmployeeName = x.FirstName
            }).ToList();

            if (employees.Count > 5)
                throw new AppException("More than 10");

            return employees;
        }
    }

    public class AllEmployeesQuery : IQuery<IEnumerable<AllEmployeesQueryResult>>
    {

    }

    public class AllEmployeesQueryResult
    {
        public int? EmployeeId { get; set; }
        public string EmployeeName { get; set; }
    }
}
