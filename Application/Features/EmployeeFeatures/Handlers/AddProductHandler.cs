using Application.Features.EmployeeFeatures.Commands;
using Domain.Models.Abstraction;
using Infrastructure.Models;
using static Shared.CQRSInfrastructure;

namespace Application.Features.EmployeeFeatures.Handlers
{
    public class AddProductHandler : ICommandHandler<AddProductCommand, Product?>
    {
        private readonly IProductRepository _repo;

        public AddProductHandler(IProductRepository repo) { this._repo = repo; }

        public async Task<Product?> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            await _repo.CreateAsync(request.Product);
            return request.Product;
        }
    }
}
