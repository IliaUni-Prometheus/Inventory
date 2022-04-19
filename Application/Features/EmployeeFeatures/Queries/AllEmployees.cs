using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var employees = await _db.Orders.Select(x => new AllEmployeesQueryResult
            {
                EmployeeId = x.EmployeeId,
                EmployeeName = x.ShipName
            }).ToListAsync();

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
