using Infrastructure.Models;
using static Shared.CQRSInfrastructure;

namespace Application.Features.EmployeeFeatures.Queries
{
    public class GetProductsQuery : IQuery<IEnumerable<Product>>
    {

    }
}
