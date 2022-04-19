using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Shared.CQRSInfrastructure;

namespace Application.Features.EmployeeFeatures.Commands
{
    public class ChangeEmployeeNameCommandHandler : DefaultCommandHandler<ChangeEmployeeNameCommand>
    {
        private readonly NorthwindContext _db;
        public ChangeEmployeeNameCommandHandler(NorthwindContext db)
        {
            _db = db;
        }

        public override async Task<CommandExecutionResult> Handle(ChangeEmployeeNameCommand request, CancellationToken cancellationToken)
        {
            var employee = _db.Employees.FirstOrDefault(x => x.EmployeeId == request.EmployeeId);
            if (employee == null) throw new ArgumentException("Not Found");

            employee.ChangeName(request.NewName);

            _db.SaveChanges();

            return new CommandExecutionResult { Id = employee.EmployeeId };
        }
    }

    public class ChangeEmployeeNameCommand : Command
    {
        public int EmployeeId { get; set; }
        public string NewName { get; set; }
    }
}
