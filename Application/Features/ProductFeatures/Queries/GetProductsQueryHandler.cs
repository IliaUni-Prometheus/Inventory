using Domain.Models.Abstraction;
using static Shared.CQRSInfrastructure;

namespace Application.Features.EmployeeFeatures.Queries;

public class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, IEnumerable<GetProductsResponse>>
{
    private readonly IProductRepository _repository;

    public GetProductsQueryHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<GetProductsResponse>> Handle(GetProductsQuery request,
        CancellationToken cancellationToken)
    {
        var products = await _repository.GetAllAsync();
        var productsResponse =
            products.Select(x => new GetProductsResponse
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName
            });
        return productsResponse;
    }
}

public class GetProductsQuery : IQuery<IEnumerable<GetProductsResponse>>
{

}

public class GetProductsResponse
{
    public int? ProductId { get; set; }
    public string? ProductName { get; set; }
}