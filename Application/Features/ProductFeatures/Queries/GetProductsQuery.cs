using Infrastructure.Models;
using static Shared.CQRSInfrastructure;

namespace Application.Features.ProductFeatures.Queries
{
    public class GetProductsQuery : IQuery<IEnumerable<Product>>
    {

    }
}
