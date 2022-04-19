using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Infrastructure.Shared.Infastrucure;

namespace Application.Features.OrderFeatures.Commands
{
    public class ChangeOrderShipNameCommandHandler : DefaultCommandHandler<ChangeOrderShipNameCommand>
    {
        private readonly NorthWindContext _db;

        public ChangeOrderShipNameCommandHandler(NorthWindContext db)
        {
            _db = db;
        }

        public override async Task<CommandExecutionResult> Handle(ChangeOrderShipNameCommand request, CancellationToken cancellationToken)
        {
            var order = _db.Orders.FirstOrDefault(o => o.OrderId == request.OrderId);
            if (order == null) throw new ArgumentException("Not Found");

            order.ShipName = request.ShipName;

            await _db.SaveChangesAsync();

            return new CommandExecutionResult { Id = order.OrderId };
        }
    }

    public class ChangeOrderShipNameCommand : Command
    {
        public int OrderId { get; set; }
        public string ShipName { get; set; }
    }
}
