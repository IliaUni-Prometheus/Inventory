using Infrastructure.Models;
using static Shared.CQRSInfrastructure;

namespace Application.Features.ProductFeatures.Queries
{
    public class GetProductByIdQuery : IQuery<Product>
    {
        public int Id { get; private set; }

        public GetProductByIdQuery(int id) { this.Id = id; }
    }
}
