using Infrastructure.Models;
using static Shared.CQRSInfrastructure;

namespace Application.Features.ProductFeatures.Commands
{
    public class UpdateProductCommand : ICommand<bool>
    {
        public Product Product { get; private set; }
        public UpdateProductCommand(Product product) { this.Product = product; }
    }
}
