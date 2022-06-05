using Application.Features.EmployeeFeatures.Commands;
using Domain.Models.Abstraction;
using Infrastructure.Models;
using static Shared.CQRSInfrastructure;

namespace Application.Features.EmployeeFeatures.Handlers
{
    public class UpdateProductHandler : ICommandHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _repo;

        public UpdateProductHandler(IProductRepository repo) { this._repo = repo; }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            return await _repo.UpdateAsync(request.Product);
        }
    }
}
