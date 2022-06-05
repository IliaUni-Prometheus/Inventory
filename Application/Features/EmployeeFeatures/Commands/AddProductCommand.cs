using Infrastructure.Models;
using static Shared.CQRSInfrastructure;

namespace Application.Features.EmployeeFeatures.Commands
{
    public class AddProductCommand : ICommand<Product>
    {
        public Product Product { get; private set; }
        public AddProductCommand(Product product) { this.Product = product; }
    }
}
