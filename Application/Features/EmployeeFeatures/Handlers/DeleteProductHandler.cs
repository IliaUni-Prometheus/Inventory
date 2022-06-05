using Application.Features.EmployeeFeatures.Commands;
using Domain.Models.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Shared.CQRSInfrastructure;

namespace Application.Features.EmployeeFeatures.Handlers
{
    public class DeleteProductHandler : ICommandHandler<DeleteProductCommand, bool?>
    {
        private readonly IProductRepository _repo;

        public DeleteProductHandler(IProductRepository repo) { this._repo = repo; }

        public async Task<bool?> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            return await _repo.DeleteByIdAsync(request.Id);
        }
    }
}
